FROM microsoft/dotnet:2.1.300-preview2-sdk AS build-env
WORKDIR /app

# bootstrap as separate layer
COPY .paket/paket.boostrapper.proj .paket/
RUN dotnet restore .paket

# paket restore as separate layer
COPY paket.dependencies .
COPY paket.lock .

RUN .paket/paket restore

# now copy everything and build
COPY src src/
RUN dotnet publish src/c1 -c Release -o /app/out

# build runtime image
FROM microsoft/dotnet:2.1.0-preview2-runtime-alpine
WORKDIR /app
COPY --from=build-env /app/out ./
ENTRYPOINT ["dotnet", "c1.dll"]
