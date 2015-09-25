namespace System
open System.Reflection

[<assembly: AssemblyTitleAttribute("Exira.EventStore")>]
[<assembly: AssemblyProductAttribute("Exira.EventStore")>]
[<assembly: AssemblyDescriptionAttribute("Exira.EventStore is an F# implementation for EventStore (https://geteventstore.com/)")>]
[<assembly: AssemblyVersionAttribute("0.0.10")>]
[<assembly: AssemblyFileVersionAttribute("0.0.10")>]
do ()

module internal AssemblyVersionInformation =
    let [<Literal>] Version = "0.0.10"
