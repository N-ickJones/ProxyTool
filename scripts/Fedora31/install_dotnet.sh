sudo rpm --import https://packages.microsoft.com/keys/microsoft.asc
sudo wget -O /etc/yum.repos.d/microsoft-prod.repo https://packages.microsoft.com/config/fedora/31/prod.repo
sudo dnf install -y dotnet-sdk-3.1
sudo dnf install -y aspnetcore-runtime-3.1
sudo dnf install -y dotnet-runtime-3.1
