#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 1433
EXPOSE 5001

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["TnR_SS.API/TnR_SS.API.csproj", "TnR_SS.API/"]
COPY ["TnR_SS.Entity/TnR_SS.Entity.csproj", "TnR_SS.Entity/"]
RUN dotnet restore "TnR_SS.Entity/TnR_SS.Entity.csproj"
RUN dotnet restore "TnR_SS.API/TnR_SS.API.csproj"
COPY . .
WORKDIR "/src/TnR_SS.API"
RUN dotnet build "TnR_SS.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TnR_SS.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TnR_SS.API.dll"]