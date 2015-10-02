namespace System
open System.Reflection

[<assembly: AssemblyTitleAttribute("Exira.EventStore")>]
[<assembly: AssemblyProductAttribute("Exira.EventStore")>]
[<assembly: AssemblyDescriptionAttribute("Exira.EventStore is an F# implementation for EventStore (https://geteventstore.com/)")>]
[<assembly: AssemblyVersionAttribute("0.1.1")>]
[<assembly: AssemblyFileVersionAttribute("0.1.1")>]
[<assembly: AssemblyMetadataAttribute("githash","15b98eeae1e64835ca635ba0f5e7b386388f0208")>]
do ()

module internal AssemblyVersionInformation =
    let [<Literal>] Version = "0.1.1"
