# Use Microsoft's official .NET image.
# https://hub.docker.com/r/microsoft/dotnet
FROM mcr.microsoft.com/dotnet/core/sdk:3.0

# Install production dependencies.
# Copy csproj and restore as distinct layers.
WORKDIR /app
COPY *.csproj .
RUN dotnet restore

# Copy local code to the container image.
COPY . .

# Build a release artifact.
RUN dotnet publish -c Release -o out

# Make sure the app binds to port 8080
ENV ASPNETCORE_URLS http://*:80

# Run the web service on container startup.
CMD ["dotnet", "out/IpLocator.dll"]
