namespace HassDeviceWorkers.ModBus
{
    public record class ModbusGatewayConfiguration
    {
        public required int SendTimeout { get; set; } = 30000;
        public required string GatewayAddress { get; set; }
        public required int GatewayPort { get; set; }
    }
}
