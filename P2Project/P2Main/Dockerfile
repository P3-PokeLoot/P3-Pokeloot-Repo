# #See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
#EXPOSE 443
#ENV ASPNETCORE_URLS http://*:80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["P2Api/P3Api.csproj", "P2Api/"]
COPY ["P2DbContext/P3DbContext.csproj", "P2DbContext/"]
COPY ["P2Main/BusinessLayer.csproj", "P2Main/"]
RUN dotnet restore "P2Api/P3Api.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "P2Api/P3Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "P2Api/P3Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "P3Api.dll"]
