namespace System
open System.Reflection

[<assembly: AssemblyTitleAttribute("Exira.EventStore")>]
[<assembly: AssemblyProductAttribute("Exira.EventStore")>]
[<assembly: AssemblyDescriptionAttribute("Exira.EventStore is an F# implementation for EventStore (https://geteventstore.com/)")>]
[<assembly: AssemblyVersionAttribute("0.8.0")>]
[<assembly: AssemblyFileVersionAttribute("0.8.0")>]
[<assembly: AssemblyMetadataAttribute("githash","e58e6b5c3c34c74cb0e7ce7a6fd6ee9b386bb646")>]
do ()

module internal AssemblyVersionInformation =
    let [<Literal>] Version = "0.8.0"
