namespace System
open System.Reflection

[<assembly: AssemblyTitleAttribute("Exira.EventStore")>]
[<assembly: AssemblyProductAttribute("Exira.EventStore")>]
[<assembly: AssemblyDescriptionAttribute("Exira.EventStore is an F# implementation for EventStore (https://geteventstore.com/)")>]
[<assembly: AssemblyVersionAttribute("0.5.13")>]
[<assembly: AssemblyFileVersionAttribute("0.5.13")>]
[<assembly: AssemblyMetadataAttribute("githash","5eb896b09f83b2f9748862ed26e7f09fa8591d0f")>]
do ()

module internal AssemblyVersionInformation =
    let [<Literal>] Version = "0.5.13"
