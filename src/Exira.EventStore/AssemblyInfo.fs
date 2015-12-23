namespace System
open System.Reflection

[<assembly: AssemblyTitleAttribute("Exira.EventStore")>]
[<assembly: AssemblyProductAttribute("Exira.EventStore")>]
[<assembly: AssemblyDescriptionAttribute("Exira.EventStore is an F# implementation for EventStore (https://geteventstore.com/)")>]
[<assembly: AssemblyVersionAttribute("0.6.14")>]
[<assembly: AssemblyFileVersionAttribute("0.6.14")>]
[<assembly: AssemblyMetadataAttribute("githash","df7b34e897f30729c2b803436f1f7161efe65cb6")>]
do ()

module internal AssemblyVersionInformation =
    let [<Literal>] Version = "0.6.14"
