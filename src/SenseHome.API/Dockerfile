#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 5000

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["src/SenseHome.API/SenseHome.API.csproj", "src/SenseHome.API/"]
COPY ["src/SenseHome.Services/SenseHome.Services.csproj", "src/SenseHome.Services/"]
COPY ["src/SenseHome.Repositories/SenseHome.Repositories.csproj", "src/SenseHome.Repositories/"]
RUN dotnet restore "src/SenseHome.API/SenseHome.API.csproj"
COPY . .
WORKDIR "/src/src/SenseHome.API"
RUN dotnet build "SenseHome.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SenseHome.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SenseHome.API.dll"]