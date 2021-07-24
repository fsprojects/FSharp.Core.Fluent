// --------------------------------------------------------------------------------------
// FAKE build script
// --------------------------------------------------------------------------------------
#r "paket: groupref build //"
#load ".fake/build.fsx/intellisense.fsx"

open Fake.Core
open Fake.DotNet
open Fake.IO
open Fake.Core.TargetOperators

// --------------------------------------------------------------------------------------
// Information about the project to be used at NuGet and in AssemblyInfo files
// --------------------------------------------------------------------------------------

let summary = "Fluent extensions for FSharp.Core"
let authors = "Don Syme, Phillip Carter"
let tags = "f#, fsharp"

let gitOwner = "fsprojects"
let gitName = "FSharp.Core.Fluent"
let gitHome = "https://github.com/" + gitOwner
let gitUrl = gitHome + "/" + gitName
let siteUrl = "https://fsprojects.github.io/FSharp.Core.Fluent/"

// --------------------------------------------------------------------------------------
// Build variables
// --------------------------------------------------------------------------------------

let buildDir  = "./build/"

System.Environment.CurrentDirectory <- __SOURCE_DIRECTORY__
let changelogFilename = "RELEASE_NOTES.md"
let changelog = Changelog.load changelogFilename
let latestEntry = changelog.LatestEntry

let nugetVersion = latestEntry.NuGetVersion
let packageReleaseNotes = sprintf "%s/blob/%s/RELEASE_NOTES.md" gitUrl latestEntry.NuGetVersion

Target.create "Clean" (fun _ ->
    Shell.cleanDirs [buildDir]
)

Target.create "Build" (fun _ ->
    DotNet.build (fun p ->
        { p with
            Configuration = DotNet.BuildConfiguration.Release
            OutputPath = Some buildDir
            MSBuildParams = { p.MSBuildParams with Properties = [("Version", nugetVersion); ("PackageReleaseNotes", packageReleaseNotes)]}
        }
    ) "FSharp.Core.Fluent.sln"
)

Target.create "Test" (fun _ ->
    DotNet.test (fun p ->
        { p with
            Configuration = DotNet.BuildConfiguration.Release
            MSBuildParams = { p.MSBuildParams with Properties = [("Version", nugetVersion); ("PackageReleaseNotes", packageReleaseNotes)]}
        }
    ) "FSharp.Core.Fluent.sln"
)

Target.create "GenerateDocs" (fun _ ->
   Shell.cleanDir ".fsdocs"
   DotNet.exec id "fsdocs" "build --clean" |> ignore
)

Target.create "Pack" (fun _ ->
    let properties = [
        ("Version", nugetVersion)
        ("Authors", authors)
        ("PackageProjectUrl", siteUrl)
        ("PackageTags", tags)
        ("RepositoryType", "git")
        ("RepositoryUrl", gitUrl)
        ("PackageReleaseNotes", packageReleaseNotes)
        ("PackageDescription", summary)
    ]

    DotNet.pack (fun p ->
        { p with
            Configuration = DotNet.BuildConfiguration.Release
            OutputPath = Some buildDir
            MSBuildParams = { p.MSBuildParams with Properties = properties}
        }
    ) "FSharp.Core.Fluent.sln"
)

Target.create "All" ignore

"Clean"
  ==> "Build"
  ==> "Test"
  ==> "Pack"
  ==> "All"

"Clean"
  ==> "Build"
  ==> "GenerateDocs"
  ==> "All"
 
Target.runOrDefault "All"
