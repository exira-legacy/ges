namespace System
open System.Reflection

[<assembly: AssemblyTitleAttribute("Exira.EventStore")>]
[<assembly: AssemblyProductAttribute("Exira.EventStore")>]
[<assembly: AssemblyDescriptionAttribute("Exira.EventStore is an F# implementation for EventStore (https://geteventstore.com/)")>]
[<assembly: AssemblyVersionAttribute("0.8.24")>]
[<assembly: AssemblyFileVersionAttribute("0.8.24")>]
[<assembly: AssemblyMetadataAttribute("githash","25063b25643266e1aa99b6c70d237cece81095d6")>]
do ()

module internal AssemblyVersionInformation =
    let [<Literal>] Version = "0.8.24"
