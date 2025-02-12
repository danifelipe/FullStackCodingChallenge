FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["FullStackCodingChallenge/FullStackCodingChallenge.csproj", "FullStackCodingChallenge/"]
COPY ["Domain/Domain.csproj", "Domain/"]
COPY ["Infrastructure/Infrastructure.csproj", "Infrastructure/"]
COPY ["Services/Services.csproj", "Services/"]

RUN dotnet restore "FullStackCodingChallenge/FullStackCodingChallenge.csproj"

COPY . .
WORKDIR "/src/FullStackCodingChallenge"
RUN dotnet build -c Release -o /app/build

FROM build AS publish
RUN dotnet publish -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FullStackCodingChallenge.dll"]
