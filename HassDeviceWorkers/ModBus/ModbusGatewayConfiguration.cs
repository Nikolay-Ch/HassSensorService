namespace HassDeviceWorkers.ModBus
{
    public class ModbusGatewayConfiguration
    {
        public int SendTimeout { get; set; }
        public string GatewayAddress { get; set; }
        public int GatewayPort { get; set; }
    }
}
