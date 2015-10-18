namespace System
open System.Reflection

[<assembly: AssemblyTitleAttribute("Exira.EventStore")>]
[<assembly: AssemblyProductAttribute("Exira.EventStore")>]
[<assembly: AssemblyDescriptionAttribute("Exira.EventStore is an F# implementation for EventStore (https://geteventstore.com/)")>]
[<assembly: AssemblyVersionAttribute("0.2.6")>]
[<assembly: AssemblyFileVersionAttribute("0.2.6")>]
[<assembly: AssemblyMetadataAttribute("githash","8300c51b0e4392225403bc9393cd7ba49acb2493")>]
do ()

module internal AssemblyVersionInformation =
    let [<Literal>] Version = "0.2.6"
