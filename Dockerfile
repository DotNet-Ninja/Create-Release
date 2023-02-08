# Set the base image as the .NET 7.0 SDK (this includes the runtime)
FROM mcr.microsoft.com/dotnet/sdk:7.0 as build-env

# Copy everything and publish the release (publish implicitly restores and builds)
WORKDIR /app
COPY . ./
RUN dotnet publish ./src/CreateRelease/CreateRelease.csproj -c Release -o out --no-self-contained

# Label the container
LABEL maintainer=".Net Ninja"
LABEL repository="https://github.com/dotnet-ninja/Create-Release"
LABEL homepage="https://github.com/dotnet-ninja/Create-Releas"

# Label as GitHub action
LABEL com.github.actions.name="Create-Release"

# Relayer the .NET SDK, anew with the build output
FROM mcr.microsoft.com/dotnet/runtime:7.0
COPY --from=build-env /app/out .
ENTRYPOINT [ "dotnet", "/CreateRelease.dll" ]