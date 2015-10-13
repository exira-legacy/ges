namespace System
open System.Reflection

[<assembly: AssemblyTitleAttribute("Exira.EventStore")>]
[<assembly: AssemblyProductAttribute("Exira.EventStore")>]
[<assembly: AssemblyDescriptionAttribute("Exira.EventStore is an F# implementation for EventStore (https://geteventstore.com/)")>]
[<assembly: AssemblyVersionAttribute("0.2.0")>]
[<assembly: AssemblyFileVersionAttribute("0.2.0")>]
[<assembly: AssemblyMetadataAttribute("githash","db29786b65d2c9b8092bfaa919110ec8c64b8623")>]
do ()

module internal AssemblyVersionInformation =
    let [<Literal>] Version = "0.2.0"
