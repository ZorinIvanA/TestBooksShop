FROM mcr.microsoft.com/dotnet/core/aspnet:3.0-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.0-buster AS build
WORKDIR /src
COPY ["Sirena.Books.Api/Sirena.Books.Api.csproj", "Sirena.Books.Api/"]
RUN dotnet restore "Sirena.Books.Api/Sirena.Books.Api.csproj"
COPY . .
WORKDIR "/src/Sirena.Books.Api"
RUN dotnet build "Sirena.Books.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Sirena.Books.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Sirena.Books.Api.dll"]