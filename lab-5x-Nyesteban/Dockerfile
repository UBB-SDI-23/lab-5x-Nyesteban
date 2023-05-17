#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
RUN apt-get update && \
apt-get install -y wget && \
apt-get install -y gnupg2 && \
wget -qO- https://deb.nodesource.com/setup_12.x | bash - && \
apt-get install -y build-essential nodejs
WORKDIR /src
COPY ["lab-5x-Nyesteban.csproj", "."]
RUN dotnet restore "./lab-5x-Nyesteban.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "lab-5x-Nyesteban.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "lab-5x-Nyesteban.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "lab-5x-Nyesteban.dll"]