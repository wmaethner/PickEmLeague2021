#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["PickEmLeagueAPI/PickEmLeagueAPI.csproj", "PickEmLeagueAPI/"]
COPY ["PickEmLeagueServices/PickEmLeagueServices.csproj", "PickEmLeagueServices/"]
COPY ["PickEmLeagueDatabase/PickEmLeagueDatabase.csproj", "PickEmLeagueDatabase/"]
COPY ["PickEmLeagueDomain/PickEmLeagueDomain.csproj", "PickEmLeagueDomain/"]
COPY ["PickEmLeague.Global.Shared/PickEmLeague.Global.Shared.csproj", "PickEmLeague.Global.Shared/"]
COPY ["PickEmLeague.Global.StaticData/PickEmLeague.Global.StaticData.csproj", "PickEmLeague.Global.StaticData/"]
COPY ["PickEmLeagueUtils/PickEmLeagueUtils.csproj", "PickEmLeagueUtils/"]
COPY ["PickEmLeagueIOC/PickEmLeagueIOC.csproj", "PickEmLeagueIOC/"]
RUN dotnet restore "PickEmLeagueAPI/PickEmLeagueAPI.csproj"
COPY . .
WORKDIR "/src/PickEmLeagueAPI"
RUN dotnet build "PickEmLeagueAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PickEmLeagueAPI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PickEmLeagueAPI.dll"]
