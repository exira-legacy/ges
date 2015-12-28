namespace Exira.EventStore

module Serialization =
    open System
    open System.Text
    open EventStore.ClientAPI
    open Microsoft.FSharp.Reflection
    open Nessos.FsPickler.Json

    let private json = FsPickler.CreateJsonSerializer(indent = false)

    // Credits to @pezi_pink
    let generateEventType (event: 'a) =
        let rec buildLabel s current =
            let case, o = FSharpValue.GetUnionFields(current, current.GetType())
            if o.Length > 0 && FSharpType.IsUnion(o.[0].GetType()) then
                buildLabel (case.Name :: s) o.[0]
            else case.Name :: s

        buildLabel [] event
        |> List.rev
        |> String.concat "."

    let internal serialize (event: 'a) =
        let serializedEvent = json.PickleToString event
        let data = Encoding.UTF8.GetBytes serializedEvent
        let eventType = generateEventType event
        EventData(Guid.NewGuid(), eventType, isJson = true, data = data, metadata = null)

    let deserialize<'a> (event: ResolvedEvent) =
        let serializedString = Encoding.UTF8.GetString event.Event.Data
        let event : 'a = json.UnPickleOfString<'a>(pickle = serializedString)
        event
