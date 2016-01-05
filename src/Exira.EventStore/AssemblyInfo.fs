namespace System
open System.Reflection

[<assembly: AssemblyTitleAttribute("Exira.EventStore")>]
[<assembly: AssemblyProductAttribute("Exira.EventStore")>]
[<assembly: AssemblyDescriptionAttribute("Exira.EventStore is an F# implementation for EventStore (https://geteventstore.com/)")>]
[<assembly: AssemblyVersionAttribute("0.8.26")>]
[<assembly: AssemblyFileVersionAttribute("0.8.26")>]
[<assembly: AssemblyMetadataAttribute("githash","337b000000bbe9f14f7c8287f84e127d07a0d83f")>]
do ()

module internal AssemblyVersionInformation =
    let [<Literal>] Version = "0.8.26"
