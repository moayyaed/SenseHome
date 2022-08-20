#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 4000

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["src/SenseHome.Agent/SenseHome.Agent.csproj", "src/SenseHome.Agent/"]
RUN dotnet restore "src/SenseHome.Agent/SenseHome.Agent.csproj"
COPY . .
WORKDIR "/src/src/SenseHome.Agent"
RUN dotnet build "SenseHome.Agent.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SenseHome.Agent.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SenseHome.Agent.dll"]