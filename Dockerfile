FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

EXPOSE 80
EXPOSE 5024

# Copy csproj and restore as distinct layers
COPY WebApp/*.csproj ./WebApp/
RUN dotnet restore ./WebApp/WebApp.csproj

# Copy everything else and build
COPY . ./
RUN dotnet publish ./WebApp/WebApp.csproj -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/sdk:8.0
WORKDIR /app
COPY --from=build /app/out .
ENTRYPOINT ["dotnet", "WebApp.dll"]