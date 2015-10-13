namespace System
open System.Reflection

[<assembly: AssemblyTitleAttribute("Exira.EventStore")>]
[<assembly: AssemblyProductAttribute("Exira.EventStore")>]
[<assembly: AssemblyDescriptionAttribute("Exira.EventStore is an F# implementation for EventStore (https://geteventstore.com/)")>]
[<assembly: AssemblyVersionAttribute("0.2.5")>]
[<assembly: AssemblyFileVersionAttribute("0.2.5")>]
[<assembly: AssemblyMetadataAttribute("githash","40e4a23919f6a4d446021821f9ec0870227b2a8f")>]
do ()

module internal AssemblyVersionInformation =
    let [<Literal>] Version = "0.2.5"
