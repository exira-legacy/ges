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

    let toStreamId prefix (id: Guid) = sprintf "%s-%O" prefix id |> StreamId

    let connect configuration =
        async {
            let port = configuration.Port |> ServerPort.value
            let endpoint = IPEndPoint(configuration.Address, port)
            let standardEsSettings =
                ConnectionSettings
                    .Create()
                    .UseConsoleLogger()
                    .SetDefaultUserCredentials(UserCredentials(configuration.Username, configuration.Password))
                    .KeepReconnecting()
                    .KeepRetrying()

            let esSettings =
                match configuration.UseSsl with
                | true -> standardEsSettings.UseSslConnection(configuration.TargetHost, true).Build()
                | false -> standardEsSettings.Build()

            let connection = EventStoreConnection.Create(esSettings, endpoint, null)
            do! connection.AsyncConnect()
            return connection
        }

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

    let appendToStream (store: IEventStoreConnection) stream expectedVersion newEvents =
        async {
            let serializedEvents =
                newEvents
                |> List.map serialize
                |> List.toArray

            do! store.AsyncAppendToStream stream expectedVersion serializedEvents |> Async.Ignore
        }

    let initalizeCheckpoint (store: IEventStoreConnection) stream =
        async {
            let! lastMetaData = store.AsyncGetStreamMetadata stream

            if (lastMetaData.MetastreamVersion <> -1) then
                return ()

            let metadata = StreamMetadata.Build().SetMaxCount(1).Build()

            do! store.AsyncSetStreamMetadata stream ExpectedVersion.Any metadata |> Async.Ignore
        }

    let storeCheckpoint (store: IEventStoreConnection) stream (position: Position) =
        async {
            let checkpoint = LastCheckPoint ({ LastCommitPosition = position.CommitPosition; LastPreparePosition = position.PreparePosition })

            do! appendToStream store stream ExpectedVersion.Any [checkpoint]
        }

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

    let subscribeToAllFrom (store: IEventStoreConnection) lastPosition resolveLinkTos eventAppeared liveProcessingStarted subscriptionDropped =
        store.SubscribeToAllFrom(
            lastCheckpoint = lastPosition,
            resolveLinkTos = resolveLinkTos,
            eventAppeared = Action<EventStoreCatchUpSubscription, ResolvedEvent>(eventAppeared),
            liveProcessingStarted = Action<EventStoreCatchUpSubscription>(liveProcessingStarted),
            subscriptionDropped = Action<EventStoreCatchUpSubscription, SubscriptionDropReason, Exception>(subscriptionDropped))
