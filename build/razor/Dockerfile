
FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /app

COPY app/razor.csproj .
RUN dotnet restore -r linux-musl-x64

COPY app/. .
RUN dotnet publish -c Release -o out

