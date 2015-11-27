﻿namespace Exira.EventStore

module Serialization =
    open System
    open System.Text
    open EventStore.ClientAPI
    open Microsoft.FSharp.Reflection
    open Nessos.FsPickler.Json

    let private json = FsPickler.CreateJsonSerializer(indent = false)

    let internal serialize (event: 'a) =
        let serializedEvent = json.PickleToString event
        let data = Encoding.UTF8.GetBytes serializedEvent
        let case, _ = FSharpValue.GetUnionFields(event, typeof<'a>)
        EventData(Guid.NewGuid(), case.Name, isJson = true, data = data, metadata = null)

    let deserialize<'a> (event: ResolvedEvent) =
        let serializedString = Encoding.UTF8.GetString event.Event.Data
        let event : 'a = json.UnPickleOfString<'a>(pickle = serializedString)
        event
