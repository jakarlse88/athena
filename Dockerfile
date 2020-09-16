FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY src/Athena.csproj Athena/
RUN dotnet restore "Athena/Athena.csproj"
WORKDIR /src/Athena
COPY . .
RUN dotnet build "Athena.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Athena.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
CMD dotnet Athena.dll
