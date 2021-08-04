#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
#EXPOSE 443
ENV ASPNETCORE_URLS http://*:80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["P3Api/P3Api.csproj", "P3Api/"]
COPY ["BusinessLayer/BusinessLayer.csproj", "BusinessLayer/"]
COPY ["P3Database/P3Database.csproj", "P3Database/"]
RUN dotnet restore "P3Api/P3Api.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "P3Api/P3Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "P3Api/P3Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "P3Api.dll"]