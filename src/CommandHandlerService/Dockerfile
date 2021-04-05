FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["CommandHandlerService/CommandHandlerService.csproj", "CommandHandlerService/"]
COPY ["Common/Common.csproj", "Common/"]
COPY ["ProductApi.Data/ProductApi.Data.csproj", "ProductApi.Data/"]
RUN dotnet restore "CommandHandlerService/CommandHandlerService.csproj"
COPY . .
WORKDIR "/src/CommandHandlerService"
RUN dotnet build "CommandHandlerService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CommandHandlerService.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CommandHandlerService.dll"]