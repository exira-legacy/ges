namespace System
open System.Reflection

[<assembly: AssemblyTitleAttribute("Exira.EventStore")>]
[<assembly: AssemblyProductAttribute("Exira.EventStore")>]
[<assembly: AssemblyDescriptionAttribute("Exira.EventStore is an F# implementation for EventStore (https://geteventstore.com/)")>]
[<assembly: AssemblyVersionAttribute("0.3.8")>]
[<assembly: AssemblyFileVersionAttribute("0.3.8")>]
[<assembly: AssemblyMetadataAttribute("githash","22c7a7166eff6f8ac98ba43757f3b9f728f227a5")>]
do ()

module internal AssemblyVersionInformation =
    let [<Literal>] Version = "0.3.8"
