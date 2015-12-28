namespace System
open System.Reflection

[<assembly: AssemblyTitleAttribute("Exira.EventStore")>]
[<assembly: AssemblyProductAttribute("Exira.EventStore")>]
[<assembly: AssemblyDescriptionAttribute("Exira.EventStore is an F# implementation for EventStore (https://geteventstore.com/)")>]
[<assembly: AssemblyVersionAttribute("0.7.16")>]
[<assembly: AssemblyFileVersionAttribute("0.7.16")>]
[<assembly: AssemblyMetadataAttribute("githash","691837363e63cd4c5ce9235c2fa605eddba6686f")>]
do ()

module internal AssemblyVersionInformation =
    let [<Literal>] Version = "0.7.16"
