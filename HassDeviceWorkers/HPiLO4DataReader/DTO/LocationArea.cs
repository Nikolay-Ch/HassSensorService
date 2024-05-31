using System.ComponentModel.DataAnnotations;

namespace HassDeviceWorkers.HPiLO4DataReader.DTO
{
    public enum LocationArea
    {
        Undefined = 0,
        [Display(Name = "System")]
        System = 1,
        [Display(Name = "System Board")]
        SystemBoard = 2,
        [Display(Name = "I/O Board")]
        IOBoard = 3,
        [Display(Name = "CPU")]
        CPU = 4,
        [Display(Name = "Memory")]
        Memory = 5,
        [Display(Name = "Storage")]
        Storage = 6,
        [Display(Name = "Removable Media")]
        RemovableMedia = 7,
        [Display(Name = "Power Supply")]
        PowerSupply = 8,
        [Display(Name = "Ambient")]
        Ambient = 9,
        [Display(Name = "Chassis")]
        Chassis = 10,
        [Display(Name = "Bridge Board")]
        BridgeBoard = 11,
        [Display(Name = "Exhaust")]
        Exhaust = 12,
        [Display(Name = "Processor Bay")]
        ProcessorBay = 13,
        [Display(Name = "IO Bay")]
        IOBay = 14,
        [Display(Name = "Blade Slot")]
        BladeSlot = 15,
        [Display(Name = "Virtual")]
        Virtual = 16
    }
}
