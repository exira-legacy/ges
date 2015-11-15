namespace System
open System.Reflection

[<assembly: AssemblyTitleAttribute("Exira.EventStore")>]
[<assembly: AssemblyProductAttribute("Exira.EventStore")>]
[<assembly: AssemblyDescriptionAttribute("Exira.EventStore is an F# implementation for EventStore (https://geteventstore.com/)")>]
[<assembly: AssemblyVersionAttribute("0.4.9")>]
[<assembly: AssemblyFileVersionAttribute("0.4.9")>]
[<assembly: AssemblyMetadataAttribute("githash","3164cd40330e213ff60317bd8e16ccd842257306")>]
do ()

module internal AssemblyVersionInformation =
    let [<Literal>] Version = "0.4.9"
