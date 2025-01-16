using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lab2_TranLeHienDuc_CSE422.Models;

public class Device
{
    public int Id { get; set; }

    [Required]
    [StringLength(100)]
    public string Name { get; set; } = string.Empty;

    [Required]
    [StringLength(50)]
    public string Code { get; set; } = string.Empty;

    [Required]
    [StringLength(500)]
    public string Description { get; set; } = string.Empty;

    public DateTime PurchaseDate { get; set; }

    [Required]
    public int DeviceCategoryId { get; set; }

    [ForeignKey("DeviceCategoryId")]
    public virtual DeviceCategory? DeviceCategory { get; set; }

    public int? UserId { get; set; }

    [ForeignKey("UserId")]
    public virtual User? User { get; set; }

    public DeviceStatus Status { get; set; }
    public DateTime EntryDate { get; set; }
}