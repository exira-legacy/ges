namespace System
open System.Reflection

[<assembly: AssemblyTitleAttribute("Exira.EventStore")>]
[<assembly: AssemblyProductAttribute("Exira.EventStore")>]
[<assembly: AssemblyDescriptionAttribute("Exira.EventStore is an F# implementation for EventStore (https://geteventstore.com/)")>]
[<assembly: AssemblyVersionAttribute("0.2.3")>]
[<assembly: AssemblyFileVersionAttribute("0.2.3")>]
[<assembly: AssemblyMetadataAttribute("githash","8dfa4f1130f8df166c7a3d897e5ff651745a96a7")>]
do ()

module internal AssemblyVersionInformation =
    let [<Literal>] Version = "0.2.3"
