# Paket .NET Core

Both `paket` and `paket.bootstrapper` as .NET tool

Behaviour:

- install the bootstrapper as .NET Tool
- run `paket.bootstrapper` but download the nupkg and install `paket` as .NET tool

# Requirements

- [.NET Core Sdk 2.1.300-preview2](https://www.microsoft.com/net/download/dotnet-core/sdk-2.1.300-preview2)

`NOTE` install from zip (safe way), but you need to set `DOTNET_ROOT` env var to the dir of unzipped sdk (bug in `dotnet/cli` for preview)

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
