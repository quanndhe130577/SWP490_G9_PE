#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["SWP_G9_PE/SWP_G9_PE.csproj", "SWP_G9_PE/"]
RUN dotnet restore "SWP_G9_PE/SWP_G9_PE.csproj"
COPY . .
WORKDIR "/src/SWP_G9_PE"
RUN dotnet build "SWP_G9_PE.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SWP_G9_PE.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SWP_G9_PE.dll"]