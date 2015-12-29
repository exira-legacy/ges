namespace System
open System.Reflection

[<assembly: AssemblyTitleAttribute("Exira.EventStore")>]
[<assembly: AssemblyProductAttribute("Exira.EventStore")>]
[<assembly: AssemblyDescriptionAttribute("Exira.EventStore is an F# implementation for EventStore (https://geteventstore.com/)")>]
[<assembly: AssemblyVersionAttribute("0.8.23")>]
[<assembly: AssemblyFileVersionAttribute("0.8.23")>]
[<assembly: AssemblyMetadataAttribute("githash","557bd5cf0ce7d0cd2afd11d6eb52a14031ac9f10")>]
do ()

module internal AssemblyVersionInformation =
    let [<Literal>] Version = "0.8.23"
