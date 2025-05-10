open System.IO

let gitOwner = "fsprojects"
let gitName = "FSharp.Core.Fluent"
let gitHome = "https://github.com/" + gitOwner
let gitUrl = gitHome + "/" + gitName

// --------------------------------------------------------------------------------------
// Build variables
// --------------------------------------------------------------------------------------

let buildDir  = "./bin/"

System.Environment.CurrentDirectory <- __SOURCE_DIRECTORY__

let runCommand cmd args =
    printf "Running command: %s %s\n" cmd args
    let proc = new System.Diagnostics.Process()
    proc.StartInfo.FileName <- cmd
    proc.StartInfo.Arguments <- args
    proc.StartInfo.UseShellExecute <- false
    proc.StartInfo.RedirectStandardOutput <- true
    proc.StartInfo.RedirectStandardError <- true
    proc.Start() |> ignore
    let output = proc.StandardOutput.ReadToEnd()
    let error = proc.StandardError.ReadToEnd()
    proc.WaitForExit()
    if not (System.String.IsNullOrEmpty error) then
        failwithf "Error: %s" error
    printfn $"{output}"

let cleanDirectory dir =
    if Directory.Exists dir then Directory.Delete(dir, true)
    Directory.CreateDirectory dir |> ignore

let getLatestVersionFromChangelog filename =
    File.ReadAllLines filename
    |> Seq.filter (fun line -> line.StartsWith "## ")
    |> Seq.head
    |> fun line -> (line.Split(' ')[1]).Trim() 

let changelogFilename = "RELEASE_NOTES.md"
let nugetVersion = getLatestVersionFromChangelog changelogFilename 
let packageReleaseNotes = sprintf "%s/blob/%s/RELEASE_NOTES.md" gitUrl nugetVersion

printfn "Building version: %s" nugetVersion

runCommand "dotnet" "clean"
runCommand "dotnet" $"build FSharp.Core.Fluent.sln -c Release /p:Version={nugetVersion}"
runCommand "dotnet" $"test FSharp.Core.Fluent.sln -c Release --no-build"

cleanDirectory ".fsdocs"
runCommand "dotnet" "fsdocs build --clean --properties Configuration=Release --eval"

cleanDirectory buildDir
runCommand "dotnet" $"pack --no-build -o {buildDir} /p:Version={nugetVersion} /p:PackageReleaseNotes={packageReleaseNotes}"   // Nuget properties set in project file
