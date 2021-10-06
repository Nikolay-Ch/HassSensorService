#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:5.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["SensorService/SensorService.csproj", "SensorService/"]
COPY ["WorkerSensors/WorkerSensors.csproj", "WorkerSensors/"]
COPY ["MqttIntegration/MqttIntegration.csproj", "MqttIntegration/"]
COPY ["HassSensorConfiguration/HassSensorConfiguration.csproj", "HassSensorConfiguration/"]
RUN dotnet restore "SensorService/SensorService.csproj"
COPY . .
WORKDIR "/src/SensorService"
RUN dotnet build "SensorService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SensorService.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SensorService.dll"]