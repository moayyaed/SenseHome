#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 1883
EXPOSE 1884

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["src/SenseHome.Broker/SenseHome.Broker.csproj", "src/SenseHome.Broker/"]
RUN dotnet restore "src/SenseHome.Broker/SenseHome.Broker.csproj"
COPY . .
WORKDIR "/src/src/SenseHome.Broker"
RUN dotnet build "SenseHome.Broker.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SenseHome.Broker.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SenseHome.Broker.dll"]