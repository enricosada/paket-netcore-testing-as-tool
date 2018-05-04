# Paket .NET Core

Both `paket` and `paket.bootstrapper` as .NET tool, trying to maintain a dev flow similar to current paket v5 (and hopefully minimal maintenance)

# Requirements

- [.NET Core Sdk 2.1.300-preview2](https://www.microsoft.com/net/download/dotnet-core/sdk-2.1.300-preview2)

`NOTE` install from zip/binaries (safe way), but you need to set `DOTNET_ROOT` env var to the dir of unzipped sdk (bug in `dotnet/cli` for preview https://github.com/dotnet/cli/issues/9114 )

# Try it

See the scenarios below.

The scenario 1 (`dotnet restore .paket`) maintain current flow with `paket.bootstrapper`

As .net tools so each one can be installed separately to specific dir, so can be run as normal native binaries. as

- `dotnet tool install paket.bootstrapper --version "[1.2.14-netcore]" --tool-path "mydir" --source-feed https://www.myget.org/F/paket-netcore-as-tool/api/v2`
- `dotnet tool install paket --version "[1.2.5-netcore]" --tool-path "mydir2" --source-feed https://www.myget.org/F/paket-netcore-as-tool/api/v2`

`NOTE` to both command you need to add the `--source-feed` because is in myget:

PRO:

- maintan the `paket.bootstrapper` (download from github, etc) but is not mandatory
- the `paket.bootstrapper` download the nupkg (from github or nuget feed) and .net core sdk install it from a local dir (so not using nuget client)
- you can install just `paket` as .net tool, no need for `paket.bootstrapper`
- native exe in `.paket`, as usual

CONS:

- some dirty in `.paket` dir (the `.store`, `.startupconfig.json`). while the `.json` may go away in favor of a convention (relative path).
  - we can restore in `paket-files\paket-bin` and symlink/shellscript the exe?
- no atm `dotnet paket`. this will be another PR for an helper global tool (`dotnet tool install -g`)
- need .net core sdk. But we can use paket repotool when ready for `paket` .net full too

Behaviour:

- install the bootstrapper as .NET Tool
- run `paket.bootstrapper` but download the nupkg and install `paket` as .NET tool

# Scenarios

Before each scenario do a `git clean -xdf`

# scenario 1, download bootstrapper and paket

Install the boostrapper and restore `paket` with

`dotnet restore .paket`

after that, as usual

`.paket\paket --help`

or

`.paket\paket restore`


# scenario 2, just downlaod the bootstrapper

To just download the bootstrapper, use

`dotnet msbuild .paket\paket.boostrapper.proj`
 
after that, as usual

`.paket\paket.bootstrapper.exe --help`

# scenario 3, integration with sdk

After the bootstrapping

```
dotnet restore .paket
```

NOTE this will update the `.paket/Paket.Restore.target` who should anyway be committed in repo, as usual

normal command like the following should work

```
dotnet run -p src/c1
```

or for a suave app

```
dotnet run -p src/c1 -- --port 8083
```
# scenario 4, docker

In `Dockerfile`, with multi steps to reuse layers to build a smaller `alpine`+`.net core runtime`

build the image with

```
docker build . -t paket-netcore-app
```

Run just the console app

```
docker run paket-netcore-app
```

Run suave webapp (after that is avaiable at http://localhost:8083/ )

```
docker run -p 8083:8083 paket-netcore-app --port 8083
```

# scenario 5, just paket

To just download the `paket` in `.paket` dir, use

`dotnet tool install paket --version "[1.2.5-netcore]" --tool-path ".paket" --source-feed https://www.myget.org/F/paket-netcore-as-tool/api/v2`

after that, as usual

`.paket\paket --help`

# EXPECTED TO WORK

- should work docker/win/osx/unix
- integration with sdk (with `dotnet build` AFTER paket exist with `dotnet restore .paket`)
- `.paket/paket` commands

# EXPECTED TO NOT WORK (WIP)

- `dotnet paket`. is not installed as global command. will do a workaround later. For now, use as before `.paket/paket --version`

# KNOW ERRORS

- sdk 2.1 doesnt use right packages from `/packages` (see with `dotnet build src/c1 -v n`)

# WIP

- remove the need for `dotnet restore .paket` after a fresh clone, so just `dotnet build` will do
- repeteable `dotnet restore .paket`
