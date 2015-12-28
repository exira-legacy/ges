namespace System
open System.Reflection

[<assembly: AssemblyTitleAttribute("Exira.EventStore")>]
[<assembly: AssemblyProductAttribute("Exira.EventStore")>]
[<assembly: AssemblyDescriptionAttribute("Exira.EventStore is an F# implementation for EventStore (https://geteventstore.com/)")>]
[<assembly: AssemblyVersionAttribute("0.7.17")>]
[<assembly: AssemblyFileVersionAttribute("0.7.17")>]
[<assembly: AssemblyMetadataAttribute("githash","07dc05c08ef2cc351f7c415bad27d1b66a7a7be8")>]
do ()

module internal AssemblyVersionInformation =
    let [<Literal>] Version = "0.7.17"
