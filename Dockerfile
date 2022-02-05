#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["HassSensorService/HassSensorService.csproj", "HassSensorService/"]
COPY ["HassDeviceBaseWorkers/HassDeviceBaseWorkers.csproj", "HassDeviceBaseWorkers/"]
COPY ["HassMqttIntegration/HassMqttIntegration.csproj", "HassMqttIntegration/"]
COPY ["HassSensorConfiguration/HassSensorConfiguration.csproj", "HassSensorConfiguration/"]
COPY ["HassDeviceWorkers/HassDeviceWorkers.csproj", "HassDeviceWorkers/"]
RUN dotnet restore "HassSensorService/HassSensorService.csproj"
COPY . .
WORKDIR "/src/HassSensorService"
RUN dotnet build "HassSensorService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "HassSensorService.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "HassSensorService.dll"]