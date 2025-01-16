using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lab2_TranLeHienDuc_CSE422.Models;

public enum UserRole
{
    Admin,
    Manager,
    User
}

public class User
{
    public User()
    {
        Devices = new HashSet<Device>();
    }

    public int Id { get; set; }

    [Required]
    [StringLength(100)]
    public string FullName { get; set; } = string.Empty;

    [Required]
    [StringLength(100)]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Required]
    [Phone]
    public string PhoneNumber { get; set; } = string.Empty;

    [Required]
    [StringLength(100)]
    public string Password { get; set; } = string.Empty;

    [Required]
    public int RoleId { get; set; }

    [ForeignKey("RoleId")]
    public virtual Role Role { get; set; }

    // Navigation properties
    public virtual ICollection<Device> Devices { get; set; }
}