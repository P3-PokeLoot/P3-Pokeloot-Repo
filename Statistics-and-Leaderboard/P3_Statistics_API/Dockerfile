#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
#EXPOSE 443
ENV ASPNETCORE_URLS http://*:80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["P3_Statistics_API/P3_Statistics_API.csproj", "P3_Statistics_API/"]
COPY ["BuisinessLayerMethods/BuisinessLayerMethods.csproj", "BuisinessLayerMethods/"]
COPY ["RepositoryModels/RepositoryModels.csproj", "RepositoryModels/"]
RUN dotnet restore "P3_Statistics_API/P3_Statistics_API.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "P3_Statistics_API/P3_Statistics_API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "P3_Statistics_API/P3_Statistics_API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "P3_Statistics_API.dll"]