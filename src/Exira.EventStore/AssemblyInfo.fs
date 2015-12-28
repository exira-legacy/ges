namespace System
open System.Reflection

[<assembly: AssemblyTitleAttribute("Exira.EventStore")>]
[<assembly: AssemblyProductAttribute("Exira.EventStore")>]
[<assembly: AssemblyDescriptionAttribute("Exira.EventStore is an F# implementation for EventStore (https://geteventstore.com/)")>]
[<assembly: AssemblyVersionAttribute("0.7.15")>]
[<assembly: AssemblyFileVersionAttribute("0.7.15")>]
[<assembly: AssemblyMetadataAttribute("githash","1aafaa480dbcab9ea943a036c39d921abae35da0")>]
do ()

module internal AssemblyVersionInformation =
    let [<Literal>] Version = "0.7.15"
