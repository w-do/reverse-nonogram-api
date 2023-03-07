# https://hub.docker.com/_/microsoft-dotnet
FROM mcr.microsoft.com/dotnet/sdk:6.0-bullseye-slim AS build
WORKDIR /source

# copy csproj and restore as distinct layers
COPY *.sln .
COPY ReverseNonogramApi/*.csproj ./ReverseNonogramApi/
COPY ReverseNonogramApi.Tests/*.csproj ./ReverseNonogramApi.Tests/
RUN dotnet restore

# copy everything else and build app
COPY ReverseNonogramApi/. ./ReverseNonogramApi/
WORKDIR /source/ReverseNonogramApi
RUN dotnet publish -c release -o /app --no-restore

# final stage/image
FROM mcr.microsoft.com/dotnet/aspnet:6.0-bullseye-slim
WORKDIR /app
COPY --from=build /app ./
ENTRYPOINT ["dotnet", "ReverseNonogramApi.dll"]
