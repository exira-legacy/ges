namespace System
open System.Reflection

[<assembly: AssemblyTitleAttribute("Exira.EventStore")>]
[<assembly: AssemblyProductAttribute("Exira.EventStore")>]
[<assembly: AssemblyDescriptionAttribute("Exira.EventStore is an F# implementation for EventStore (https://geteventstore.com/)")>]
[<assembly: AssemblyVersionAttribute("0.5.11")>]
[<assembly: AssemblyFileVersionAttribute("0.5.11")>]
[<assembly: AssemblyMetadataAttribute("githash","f95507d5aa82b10581d62e0d0f07ff73b96dc38c")>]
do ()

module internal AssemblyVersionInformation =
    let [<Literal>] Version = "0.5.11"
