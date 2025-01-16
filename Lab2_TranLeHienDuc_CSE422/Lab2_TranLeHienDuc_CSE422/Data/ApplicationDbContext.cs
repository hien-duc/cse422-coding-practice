using Microsoft.EntityFrameworkCore;
using Lab2_TranLeHienDuc_CSE422.Models;

namespace Lab2_TranLeHienDuc_CSE422.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Device> Devices { get; set; }
        public DbSet<DeviceCategory> DeviceCategories { get; set; }
        public DbSet<Role> Roles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure one-to-many relationship between Role and User
            modelBuilder.Entity<User>()
                .HasOne(u => u.Role)
                .WithMany()
                .HasForeignKey(u => u.RoleId)
                .OnDelete(DeleteBehavior.Restrict);

            // Configure one-to-many relationship between User and Device
            modelBuilder.Entity<Device>()
                .HasOne(d => d.User)
                .WithMany(u => u.Devices)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.SetNull);

            // Seed initial roles
            modelBuilder.Entity<Role>().HasData(
                new Role 
                { 
                    Id = 1, 
                    Type = RoleType.Admin, 
                    Name = "Administrator", 
                    Description = "Full system access" 
                },
                new Role 
                { 
                    Id = 2, 
                    Type = RoleType.Manager, 
                    Name = "Manager", 
                    Description = "Manage users and devices" 
                },
                new Role 
                { 
                    Id = 3, 
                    Type = RoleType.User, 
                    Name = "User", 
                    Description = "Regular user access" 
                }
            );
        }
    }
}
