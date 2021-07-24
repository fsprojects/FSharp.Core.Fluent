# FSharp.Core.Fluent [![NuGet Badge](https://buildstats.info/nuget/FSharp.Core.Fluent)](https://www.nuget.org/packages/FSharp.Core.Fluent)

[![Open in Gitpod](https://gitpod.io/button/open-in-gitpod.svg)](https://gitpod.io/#https://github.com/fsprojects/fsharp.core.fluent)

Provides fluent members for FSharp.Core functions like so:

```fsharp
open FSharp.Core.Fluent

let xs = [ 1 .. 10 ]

xs.map(fun x -> x + 1).filter(fun x -> x > 4).sort()

xs.map(fun x -> x + 1)
  .filter(fun x -> x > 4)
  .sort()
```

## Contributing

This is how you build the repo after cloning:

```console
dotnet tool restore
dotnet paket restore
dotnet fake build
```

Docs are generated and deployed to [the docs site](https://fsprojects.github.io/FSharp.Core.Fluent/) after every successful push to this repo.

We accept pull requests!

## Current maintainers

- [@dsyme](https://github.com/dsyme)
- [@cartermp](https://github.com/cartermp)

The default maintainer account for projects under "fsprojects" is [@fsprojectsgit](https://github.com/fsprojectsgit) - F# Community Project Incubation Space (repo management)
