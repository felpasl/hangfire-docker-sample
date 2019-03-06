FROM microsoft/dotnet:2.2-aspnetcore-runtime-stretch-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM microsoft/dotnet:2.2-sdk-stretch AS build
WORKDIR /src
COPY ["Hangfire.Docker.Web/Hangfire.Docker.Web.csproj", "Hangfire.Docker.Web/"]
RUN dotnet restore "Hangfire.Docker.Web/Hangfire.Docker.Web.csproj"
COPY . .
WORKDIR "/src/Hangfire.Docker.Web"
RUN dotnet build "Hangfire.Docker.Web.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "Hangfire.Docker.Web.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Hangfire.Docker.Web.dll"]