#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 1911

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["TnR_SS.API/TnR_SS.API.csproj", "TnR_SS.API/"]
COPY ["TnR_SS.DataEFCore/TnR_SS.DataEFCore.csproj", "TnR_SS.DataEFCore/"]
COPY ["TnR_SS.Domain/TnR_SS.Domain.csproj", "TnR_SS.Domain/"]
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