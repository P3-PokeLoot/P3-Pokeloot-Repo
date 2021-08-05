<<<<<<< HEAD
FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
#create a dir called 'app' inside the image/container that serves as the 'dest' in commands
WORKDIR /app
#for http/// the container will be listening on this port
EXPOSE 80

#if you want https - 443 is the defauilt port for https in the container
# EXPOSE 443
#leave this in so that any port the app is listening on will be overwritten to by 80 (instead of 5000, 5001, et ).
ENV ASPNETCORE_URLS http://*:80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /app
COPY ["P2Api/P2Api.csproj", "P2Api/"]
COPY ["P2DbContext/P2DbContext.csproj", "P2DbContext/"]
COPY ["P2Main/BusinessLayer.csproj", "P2Main/"]


#run uses the relative path to the last WORKDIR stated
RUN dotnet restore "P2Api/P2Api.csproj"

#left arg refers to the build context ( my file system)
# the rt side arg refers to the last WORKDIR loc
COPY . .

# WORKDIR "/src/RpsApi"
# RUN dotnet build "RpsGameApi.csproj" -c Release -o /app/build

#FROM build AS publish
RUN dotnet publish "P2Api/P2Api.csproj" -c Release -o out

#this uses the last layer (line 20) of the base stage as base image for this stage and calls it prod
# creating a stage here is an example of self-documentation
FROM base as prod
# --from refers to the 'build' stage
# it's needed bc 
COPY --from=build /app/out .
=======
#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
#EXPOSE 443
ENV ASPNETCORE_URLS http://*:80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["P2Api/P2Api.csproj", "P2Api/"]
COPY ["P2DbContext/P2DbContext.csproj", "P2DbContext/"]
COPY ["P2Main/BusinessLayer.csproj", "P2Main/"]
RUN dotnet restore "P2Api/P2Api.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "P2Api/P2Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "P2Api/P2Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
>>>>>>> main
ENTRYPOINT ["dotnet", "P2Api.dll"]