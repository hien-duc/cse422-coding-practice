using System.ComponentModel.DataAnnotations;

namespace Lab2_TranLeHienDuc_CSE422.Models
{
    public enum RoleType
    {
        Admin,
        Manager,
        User
    }

    public class Role
    {
        public int Id { get; set; }

        [Required]
        public RoleType Type { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; } = string.Empty;

        [StringLength(200)]
        public string Description { get; set; } = string.Empty;
    }
}
