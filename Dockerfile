FROM microsoft/dotnet:2.1.300-preview2-sdk AS build-env
WORKDIR /app

# bootstrap as separate layer
COPY .paket/paket.boostrapper.proj .paket/
RUN dotnet restore .paket

# paket restore as separate layer
COPY paket.dependencies .
COPY paket.lock .

RUN .paket/paket restore

# run it
ENTRYPOINT [".paket/paket"]
