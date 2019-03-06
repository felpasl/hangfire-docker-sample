FROM microsoft/dotnet:2.2-runtime-stretch-slim AS base
WORKDIR /app

FROM microsoft/dotnet:2.2-sdk-stretch AS build
WORKDIR /src
COPY ["Hangfire.Docker/Hangfire.Docker.csproj", "Hangfire.Docker/"]
RUN dotnet restore "Hangfire.Docker/Hangfire.Docker.csproj"
COPY . .
WORKDIR "/src/Hangfire.Docker"
RUN dotnet build "Hangfire.Docker.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "Hangfire.Docker.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Hangfire.Docker.dll"]
