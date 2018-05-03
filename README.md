# Paket .NET Core

Both `paket` and `paket.bootstrapper` as .NET tool

so can be installed as

- `dotnet tool install paket.bootstrapper --version "[1.2.11-netcore]" --tool-path "mydir"`
- `dotnet tool install paket --version "[1.2.5-netcore]" --tool-path "mydir2"`

to both command you need to add:

- `--source-feed https://www.myget.org/F/paket-netcore-as-tool/api/v2` because is in myget atm

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

# Requirements

- [.NET Core Sdk 2.1.300-preview2](https://www.microsoft.com/net/download/dotnet-core/sdk-2.1.300-preview2)

`NOTE` install from zip (safe way), but you need to set `DOTNET_ROOT` env var to the dir of unzipped sdk (bug in `dotnet/cli` for preview https://github.com/dotnet/cli/issues/9114 )

# scenario 1, download bootstrapper and paket

Install the boostrapper and restore `paket` with

`dotnet restore .paket`

after that, as usual

`.paket\paket.exe --help`

# scenario 2, just downlaod the bootstrapper

To just download the bootstrapper, use

`dotnet msbuild .paket\paket.boostrapper.proj`
 
after that, as usual

`.paket\paket.bootstrapper.exe --help`

# KNOW ERRORS

- on mac `Authority/Host could not be parsed.` on `paket.bootstrapper (thx @vaskir)

    [Nuget] DownloadVersion...
    Starting download from https:/www.myget.org/F/paket-netcore-as-tool/api/v2/package/Paket/1.2.5-netcore
    [Nuget] DownloadVersion took 0.01 second(s) and failed with 'Invalid URI: The Authority/Host could not be parsed.'.
    Invalid URI: The Authority/Host could not be parsed. (Nuget)
