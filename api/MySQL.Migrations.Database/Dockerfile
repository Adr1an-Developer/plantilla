FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY . .
RUN dotnet build MySQL.Migrations.Database.csproj --configuration Release -o "./"
