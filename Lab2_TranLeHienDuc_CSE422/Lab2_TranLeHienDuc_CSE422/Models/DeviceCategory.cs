using System.ComponentModel.DataAnnotations;

namespace Lab2_TranLeHienDuc_CSE422.Models;

public class DeviceCategory
{
    public DeviceCategory()
    {
        Devices = new List<Device>();
    }

    public int Id { get; set; }

    [Required(ErrorMessage = "Category name is required")]
    [StringLength(50, ErrorMessage = "Category name cannot exceed 50 characters")]
    public string Name { get; set; }

    [StringLength(200, ErrorMessage = "Description cannot exceed 200 characters")]
    public string Description { get; set; }

    public virtual ICollection<Device> Devices { get; set; }
}