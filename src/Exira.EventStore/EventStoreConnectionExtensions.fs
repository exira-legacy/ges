namespace Exira.EventStore

[<AutoOpen>]
module internal EventStoreConnectionExtensions =
    open EventStore.ClientAPI

    type private IEventStoreConnection with
        member this.AsyncConnect() =
            Async.AwaitTask(this.ConnectAsync())

        member this.AsyncReadEvent stream eventNumber resolveLinkTos =
            let (StreamId streamId) = stream
            Async.AwaitTask(this.ReadEventAsync(streamId, eventNumber, resolveLinkTos))

        member this.AsyncReadStreamEventsForward stream start count resolveLinkTos =
            let (StreamId streamId) = stream
            Async.AwaitTask(this.ReadStreamEventsForwardAsync(streamId, start, count, resolveLinkTos))

        member this.AsyncAppendToStream stream expectedVersion events =
            let (StreamId streamId) = stream
            Async.AwaitTask(this.AppendToStreamAsync(streamId, expectedVersion, events))

        member this.AsyncSubscribeToAll resolveLinkTos eventAppeared =
            Async.AwaitTask(this.SubscribeToAllAsync(resolveLinkTos, eventAppeared))

        member this.AsyncGetStreamMetadata stream =
            let (StreamId streamId) = stream
            Async.AwaitTask(this.GetStreamMetadataAsync(streamId))

        member this.AsyncSetStreamMetadata stream expectedMetastreamVersion (metadata: StreamMetadata) =
            let (StreamId streamId) = stream
            Async.AwaitTask(this.SetStreamMetadataAsync(streamId, expectedMetastreamVersion, metadata))