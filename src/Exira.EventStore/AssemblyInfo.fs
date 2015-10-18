namespace System
open System.Reflection

[<assembly: AssemblyTitleAttribute("Exira.EventStore")>]
[<assembly: AssemblyProductAttribute("Exira.EventStore")>]
[<assembly: AssemblyDescriptionAttribute("Exira.EventStore is an F# implementation for EventStore (https://geteventstore.com/)")>]
[<assembly: AssemblyVersionAttribute("0.3.7")>]
[<assembly: AssemblyFileVersionAttribute("0.3.7")>]
[<assembly: AssemblyMetadataAttribute("githash","2f160b09f30abb65b7d673c681b3e16c9adfcbb8")>]
do ()

module internal AssemblyVersionInformation =
    let [<Literal>] Version = "0.3.7"
