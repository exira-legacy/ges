namespace Exira.EventStore

module EventStore =
    open System
    open EventStore.ClientAPI
    open ExtCore.Collections.AsyncSeqExtensions
    open Chiron
    open Chiron.Operators

    open Serialization

    type InternalEvent =
        | LastCheckPoint of LastCheckPointEvent
    with
        static member ToJson (e: InternalEvent) =
            match e with
            | LastCheckPoint lastCheckPoint -> Json.writeWith Json.serialize "lastCheckPoint" lastCheckPoint

        static member FromJson (_: InternalEvent) = function
            | Property "lastCheckPoint" x as json -> Json.init (LastCheckPoint x) json
            | json -> Json.error (sprintf "couldn't deserialise %A to InternalEvent" json) json

    and LastCheckPointEvent = {
        LastCommitPosition: int64
        LastPreparePosition: int64
    } with
        static member ToJson (e: LastCheckPointEvent) =
            Json.write "lastCommitPosition" e.LastCommitPosition
            *> Json.write "lastPreparePosition" e.LastPreparePosition

        static member FromJson (_: LastCheckPointEvent) =
            (fun lastCommitPosition lastPreparePosition -> { LastCommitPosition = lastCommitPosition; LastPreparePosition = lastPreparePosition })
            <!> Json.read "lastCommitPosition"
            <*> Json.read "lastPreparePosition"

    let private systemPrefix = "$"

    let private toSystemStreamId stream =
        let (StreamId streamId) = stream
        if streamId.StartsWith systemPrefix then stream
        else sprintf "%s%s" systemPrefix streamId |> StreamId

    /// Construct a `StreamId` based on a `prefix` and a value.
    let toStreamId prefix id = sprintf "%s-%O" prefix id |> StreamId

    /// Connects asynchronously to a destination.
    let connect (connectionString: string) =
        async {
            let connection = EventStoreConnection.Create(connectionString)
            do! connection.AsyncConnect()
            return connection
        }

    // Reads all events from a `stream` forwards (e.g. oldest to newest) starting from position `version`.
    let inline readAllFromStream (store: IEventStoreConnection) stream version =
        let rec readAllSlices (store: IEventStoreConnection) stream version =
            asyncSeq {
                let size = 4096
                let! slice = store.AsyncReadStreamEventsForward stream version size true

                if (slice.IsEndOfStream) then
                    yield slice
                else
                    yield slice
                    yield! readAllSlices store stream (version + size)
            }

        async {
            return
                readAllSlices store stream version
                |> AsyncSeq.toSeq
                |> Seq.collect (fun slice -> slice.Events)
                |> Seq.map deserialize
        }

    /// Reads `count` events from a `stream` forwards (e.g. oldest to newest) starting from position `version`.
    let inline readFromStream (store: IEventStoreConnection) stream version count =
        async {
            let! slice = store.AsyncReadStreamEventsForward stream version count true

            let events =
                slice.Events
                |> Seq.map deserialize

            let nextEventNumber =
                if slice.IsEndOfStream
                then None
                else Some slice.NextEventNumber

            return events, slice.LastEventNumber, nextEventNumber
        }

    /// Appends `events` asynchronously to a `stream`.
    let inline appendToStream (store: IEventStoreConnection) stream expectedVersion events =
        async {
            let serializedEvents =
                events
                |> Seq.map serialize
                |> Seq.toArray

            do! store.AsyncAppendToStream stream expectedVersion serializedEvents |> Async.Ignore
        }

    /// Initialize a new checkpoint `stream` which holds exactly 1 event.
    let initalizeCheckpoint (store: IEventStoreConnection) stream =
        async {
            let stream = toSystemStreamId stream

            let! lastMetaData = store.AsyncGetStreamMetadata stream

            if (lastMetaData.MetastreamVersion <> -1) then
                return ()

            let metadata = StreamMetadata.Build().SetMaxCount(1).Build()

            do! store.AsyncSetStreamMetadata stream ExpectedVersion.Any metadata |> Async.Ignore
        }

    /// Store a checkpoint `position` to the checkpoint `stream`.
    let storeCheckpoint (store: IEventStoreConnection) stream (position: Position) =
        async {
            let stream = toSystemStreamId stream

            let checkpoint = LastCheckPoint ({ LastCommitPosition = position.CommitPosition; LastPreparePosition = position.PreparePosition })

            do! appendToStream store stream ExpectedVersion.Any [checkpoint]
        }

    /// Retrieve a checkpoint position from a checkpoint `stream`.
    let getCheckpoint (store: IEventStoreConnection) stream =
        async {
            let stream = toSystemStreamId stream

            let buildCheckpoint event =
                event
                |> deserialize
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
