namespace System
open System.Reflection

[<assembly: AssemblyTitleAttribute("Exira.EventStore")>]
[<assembly: AssemblyProductAttribute("Exira.EventStore")>]
[<assembly: AssemblyDescriptionAttribute("Exira.EventStore is an F# implementation for EventStore (https://geteventstore.com/)")>]
[<assembly: AssemblyVersionAttribute("0.8.0")>]
[<assembly: AssemblyFileVersionAttribute("0.8.0")>]
[<assembly: AssemblyMetadataAttribute("githash","fc39826028118cf5b2041257c2b79b32675524a3")>]
do ()

module internal AssemblyVersionInformation =
    let [<Literal>] Version = "0.8.0"
