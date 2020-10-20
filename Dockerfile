FROM mcr.microsoft.com/dotnet/aspnet:5.0.0-rc.2-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0.100-rc.2-buster-slim AS build
WORKDIR /src
COPY src/Athena.csproj Athena/
RUN dotnet restore "Athena/Athena.csproj"
WORKDIR /src/Athena
COPY src/. .
RUN dotnet build "Athena.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Athena.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
CMD dotnet Athena.dll
