namespace Exira.EventStore

module EventStore =
    open System
    open System.Net
    open EventStore.ClientAPI
    open EventStore.ClientAPI.SystemData

    open Serialization

    type InternalEvent =
        | LastCheckPoint of LastCheckPointEvent

    and LastCheckPointEvent = {
        LastCommitPosition: int64
        LastPreparePosition: int64
    }

    /// Construct a `StreamId` based on a `prefix` and a value.
    let toStreamId prefix id = sprintf "%s-%O" prefix id |> StreamId

    /// Connects asynchronously to a destination.
    let connect (connectionString: string) =
        async {
            let connection = EventStoreConnection.Create(connectionString)
            do! connection.AsyncConnect()
            return connection
        }

    /// Reads `count` events from a `stream` forwards (e.g. oldest to newest) starting from position `version`.
    let readFromStream (store: IEventStoreConnection) stream version count =
        async {
            let! slice = store.AsyncReadStreamEventsForward stream version count true

            let events: list<'a> =
                slice.Events
                |> Seq.map deserialize<'a>
                |> Seq.cast
                |> Seq.toList

            let nextEventNumber =
                if slice.IsEndOfStream
                then None
                else Some slice.NextEventNumber

            return events, slice.LastEventNumber, nextEventNumber
        }

    /// Appends `events` asynchronously to a `stream`.
    let appendToStream (store: IEventStoreConnection) stream expectedVersion events =
        async {
            let serializedEvents =
                events
                |> List.map serialize
                |> List.toArray

            do! store.AsyncAppendToStream stream expectedVersion serializedEvents |> Async.Ignore
        }

    /// Initialize a new checkpoint `stream` which holds exactly 1 event.
    let initalizeCheckpoint (store: IEventStoreConnection) stream =
        async {
            let! lastMetaData = store.AsyncGetStreamMetadata stream

            if (lastMetaData.MetastreamVersion <> -1) then
                return ()

            let metadata = StreamMetadata.Build().SetMaxCount(1).Build()

            do! store.AsyncSetStreamMetadata stream ExpectedVersion.Any metadata |> Async.Ignore
        }

    /// Store a checkpoint `position` to the checkpoint `stream`.
    let storeCheckpoint (store: IEventStoreConnection) stream (position: Position) =
        async {
            let checkpoint = LastCheckPoint ({ LastCommitPosition = position.CommitPosition; LastPreparePosition = position.PreparePosition })

            do! appendToStream store stream ExpectedVersion.Any [checkpoint]
        }

    /// Retrieve a checkpoint position from a checkpoint `stream`.
    let getCheckpoint (store: IEventStoreConnection) stream =
        async {
            let buildCheckpoint event =
                event
                |> deserialize<InternalEvent>
                |> function
                    | InternalEvent.LastCheckPoint e -> Nullable(Position(e.LastCommitPosition, e.LastPreparePosition))

            let! lastEvent = store.AsyncReadEvent stream -1 false

            return
                match lastEvent.Status with
                    | EventReadStatus.Success ->
                        match lastEvent.Event.HasValue with
                        | true -> buildCheckpoint lastEvent.Event.Value
                        | false -> AllCheckpoint.AllStart
                    | _ -> AllCheckpoint.AllStart
        }

    /// Subscribes to a all events. Existing events from `lastPosition` onwards are read from the Event Store and presented to the user as if they had been pushed.
    /// Once the end of the stream is read the subscription is transparently (to the user) switched to push new events as they are written.
    /// The action `liveProcessingStarted` is called when the subscription switches from the reading phase to the live subscription phase.
    /// To receive all events in the database, use AllCheckpoint.AllStart. If events have already been received and resubscription from the same point is desired, use the position representing the last event processed which appeared on the subscription.
    /// NOTE: Using Position.Start here will result in missing the first event in the stream.
    let subscribeToAllFrom (store: IEventStoreConnection) lastPosition resolveLinkTos eventAppeared liveProcessingStarted subscriptionDropped =
        store.SubscribeToAllFrom(
            lastCheckpoint = lastPosition,
            resolveLinkTos = resolveLinkTos,
            eventAppeared = Action<EventStoreCatchUpSubscription, ResolvedEvent>(eventAppeared),
            liveProcessingStarted = Action<EventStoreCatchUpSubscription>(liveProcessingStarted),
            subscriptionDropped = Action<EventStoreCatchUpSubscription, SubscriptionDropReason, Exception>(subscriptionDropped))
