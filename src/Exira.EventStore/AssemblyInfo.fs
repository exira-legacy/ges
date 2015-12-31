namespace System
open System.Reflection

[<assembly: AssemblyTitleAttribute("Exira.EventStore")>]
[<assembly: AssemblyProductAttribute("Exira.EventStore")>]
[<assembly: AssemblyDescriptionAttribute("Exira.EventStore is an F# implementation for EventStore (https://geteventstore.com/)")>]
[<assembly: AssemblyVersionAttribute("0.8.25")>]
[<assembly: AssemblyFileVersionAttribute("0.8.25")>]
[<assembly: AssemblyMetadataAttribute("githash","edb7c198344de84b02b0eb1f940fc7b8cc9b846e")>]
do ()

module internal AssemblyVersionInformation =
    let [<Literal>] Version = "0.8.25"
