namespace System
open System.Reflection

[<assembly: AssemblyTitleAttribute("Exira.EventStore")>]
[<assembly: AssemblyProductAttribute("Exira.EventStore")>]
[<assembly: AssemblyDescriptionAttribute("Exira.EventStore is an F# implementation for EventStore (https://geteventstore.com/)")>]
[<assembly: AssemblyVersionAttribute("0.8.22")>]
[<assembly: AssemblyFileVersionAttribute("0.8.22")>]
[<assembly: AssemblyMetadataAttribute("githash","edb7eb23c87ebdc1dd9b9a4755863cbfe9dfb895")>]
do ()

module internal AssemblyVersionInformation =
    let [<Literal>] Version = "0.8.22"
