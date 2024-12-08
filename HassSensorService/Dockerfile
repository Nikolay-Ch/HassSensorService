#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:9.0-noble-chiseled AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:9.0.101-noble AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Directory.Build.props", "."]
COPY ["HassSensorService/HassSensorService.csproj", "HassSensorService/"]
COPY ["HassClassExtensions/HassClassExtensions.csproj", "HassClassExtensions/"]
COPY ["HassDeviceBaseWorkers/HassDeviceBaseWorkers.csproj", "HassDeviceBaseWorkers/"]
COPY ["HassMqttIntegration/HassMqttIntegration.csproj", "HassMqttIntegration/"]
COPY ["HassSensorConfiguration/HassSensorConfiguration.csproj", "HassSensorConfiguration/"]
COPY ["HassDeviceWorkers/HassDeviceWorkers.csproj", "HassDeviceWorkers/"]
COPY ["Common/Common.csproj", "Common/"]
RUN dotnet restore "./HassSensorService/./HassSensorService.csproj"
COPY . .
WORKDIR "/src/HassSensorService"
RUN dotnet build "./HassSensorService.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
RUN groupadd -g 1638 sensor_service
RUN useradd -u 1638 -g 1638 sensor_service

ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./HassSensorService.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

RUN mkdir -p /app/publish/logs

FROM base AS final
COPY --from=publish /etc/passwd etc/passwd
COPY --from=publish /etc/group etc/group

USER sensor_service

WORKDIR /app
COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "HassSensorService.dll"]
