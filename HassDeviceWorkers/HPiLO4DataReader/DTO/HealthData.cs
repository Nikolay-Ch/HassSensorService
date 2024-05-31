using System;
using System.Collections.Generic;

namespace HassDeviceWorkers.HPiLO4DataReader.DTO
{
    public readonly record struct HealthData
    {
        public IEnumerable<Fan> Fans { get; init; }
        public IEnumerable<Temperature> Temperatures { get; init; }
        public IEnumerable<PowerSupply> PowerSupplies { get; init; }
        public HealthAtAGlance HealthAtAGlance { get; init; }
        public DateTime ServerUptime { get; init; }
    }
}
