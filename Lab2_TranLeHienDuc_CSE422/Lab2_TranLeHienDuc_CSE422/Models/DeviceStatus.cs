using System.ComponentModel.DataAnnotations;

namespace Lab2_TranLeHienDuc_CSE422.Models;

public enum DeviceStatus
{
    [Display(Name = "Available")]
    Available = 0,
    
    [Display(Name = "In Use")]
    InUse = 1,
    
    [Display(Name = "Under Maintenance")]
    UnderMaintenance = 2,
    
    [Display(Name = "Retired")]
    Retired = 3
}