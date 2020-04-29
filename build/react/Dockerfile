
# Dotnet
FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS buildapi
WORKDIR /app
COPY app/ProxyWorldApi.csproj .
RUN dotnet restore -r linux-musl-x64
COPY app/. .
RUN dotnet publish -c Release -o out

# React
RUN curl -sL https://deb.nodesource.com/setup_10.x |  bash -
RUN apt-get install -y nodejs
WORKDIR /app/ProxyWorldClient
RUN npm install --silent
RUN npm install react-scripts@3.0.1 -g --silent
RUN npm run build --silent
WORKDIR /app
