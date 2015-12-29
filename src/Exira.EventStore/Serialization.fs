namespace Exira.EventStore

module Serialization =
    open System
    open System.Text
    open EventStore.ClientAPI
    open Microsoft.FSharp.Reflection
    open Chiron

    // Credits to @pezi_pink
    let generateEventType (event: 'a) =
        // TODO: Cache the event types to prevent reflection hit
        let rec buildLabel s current =
            let case, o = FSharpValue.GetUnionFields(current, current.GetType())
            if o.Length > 0 && not(isNull o.[0]) && FSharpType.IsUnion(o.[0].GetType()) then
                buildLabel (case.Name :: s) o.[0]
            else case.Name :: s

        buildLabel [] event
        |> List.rev
        |> String.concat "."

    let inline internal serialize (event: 'a) =
        let data =
            event
            |> Json.serialize
            |> Json.format
            |> Encoding.UTF8.GetBytes

        let eventType = generateEventType event
        EventData(Guid.NewGuid(), eventType, isJson = true, data = data, metadata = null)

    let inline deserialize (event: ResolvedEvent) =
        Encoding.UTF8.GetString event.Event.Data
        |> Json.parse
        |> Json.deserialize
