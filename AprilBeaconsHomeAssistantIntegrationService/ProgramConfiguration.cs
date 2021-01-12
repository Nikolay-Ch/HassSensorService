using System.Collections.Generic;

namespace AprilBeaconsHomeAssistantIntegrationService
{
    public class ProgramConfiguration
    {
        public List<string> AprilBeaconDevicesList { get; set; } = new List<string>();
        public string ServiceName { get; set; }
    }
}
