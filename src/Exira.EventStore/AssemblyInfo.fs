namespace System
open System.Reflection

[<assembly: AssemblyTitleAttribute("Exira.EventStore")>]
[<assembly: AssemblyProductAttribute("Exira.EventStore")>]
[<assembly: AssemblyDescriptionAttribute("Exira.EventStore is an F# implementation for EventStore (https://geteventstore.com/)")>]
[<assembly: AssemblyVersionAttribute("0.7.18")>]
[<assembly: AssemblyFileVersionAttribute("0.7.18")>]
[<assembly: AssemblyMetadataAttribute("githash","0d7b3c31b9174c8a121ab1f10971fa8f31aae00d")>]
do ()

module internal AssemblyVersionInformation =
    let [<Literal>] Version = "0.7.18"
