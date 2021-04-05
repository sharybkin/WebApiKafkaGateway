FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build-env
WORKDIR /solution/src/app

COPY src/ProductApi/ProductApi.csproj ./
COPY src/Common/Common.csproj ./
RUN dotnet restore ProductApi.csproj

COPY src/Common/Common.xml out/Common.xml

# Copy everything else and build
COPY src/ProductApi ./
COPY src/Common ./
RUN dotnet publish ProductApi.csproj -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:5.0

WORKDIR /app
COPY --from=build-env /solution/src/app/out .
ENTRYPOINT ["dotnet", "ProductApi.dll"]