namespace System
open System.Reflection

[<assembly: AssemblyTitleAttribute("Exira.EventStore")>]
[<assembly: AssemblyProductAttribute("Exira.EventStore")>]
[<assembly: AssemblyDescriptionAttribute("Exira.EventStore is an F# implementation for EventStore (https://geteventstore.com/)")>]
[<assembly: AssemblyVersionAttribute("0.4.10")>]
[<assembly: AssemblyFileVersionAttribute("0.4.10")>]
[<assembly: AssemblyMetadataAttribute("githash","c981bedc21930e0ace564abed51ab69b3a3c26c4")>]
do ()

module internal AssemblyVersionInformation =
    let [<Literal>] Version = "0.4.10"
