namespace System
open System.Reflection

[<assembly: AssemblyTitleAttribute("Exira.EventStore")>]
[<assembly: AssemblyProductAttribute("Exira.EventStore")>]
[<assembly: AssemblyDescriptionAttribute("Exira.EventStore is an F# implementation for EventStore (https://geteventstore.com/)")>]
[<assembly: AssemblyVersionAttribute("0.4.12")>]
[<assembly: AssemblyFileVersionAttribute("0.4.12")>]
[<assembly: AssemblyMetadataAttribute("githash","85af95f026039486fd298319a7558d0451a448f3")>]
do ()

module internal AssemblyVersionInformation =
    let [<Literal>] Version = "0.4.12"
