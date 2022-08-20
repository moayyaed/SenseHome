#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 4100

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["src/SenseHome.DataSync/SenseHome.DataSync.csproj", "src/SenseHome.DataSync/"]
COPY ["src/SenseHome.Repositories/SenseHome.Repositories.csproj", "src/SenseHome.Repositories/"]
RUN dotnet restore "src/SenseHome.DataSync/SenseHome.DataSync.csproj"
COPY . .
WORKDIR "/src/src/SenseHome.DataSync"
RUN dotnet build "SenseHome.DataSync.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SenseHome.DataSync.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SenseHome.DataSync.dll"]