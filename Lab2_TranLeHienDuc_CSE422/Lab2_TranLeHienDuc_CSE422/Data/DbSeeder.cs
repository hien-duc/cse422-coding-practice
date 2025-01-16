using Lab2_TranLeHienDuc_CSE422.Models;
using Microsoft.EntityFrameworkCore;

namespace Lab2_TranLeHienDuc_CSE422.Data
{
    public static class DbSeeder
    {
        public static void Seed(ApplicationDbContext context)
        {
            // Seed Roles if they don't exist
            if (!context.Roles.Any())
            {
                var roles = new List<Role>
                {
                    new Role { 
                        Id = 1,
                        Type = RoleType.Admin,
                        Name = "Administrator",
                        Description = "Full system access and management capabilities"
                    },
                    new Role { 
                        Id = 2,
                        Type = RoleType.Manager,
                        Name = "Manager",
                        Description = "Device and user management capabilities"
                    },
                    new Role { 
                        Id = 3,
                        Type = RoleType.User,
                        Name = "User",
                        Description = "Basic device usage and viewing capabilities"
                    }
                };
                context.Roles.AddRange(roles);
                context.SaveChanges();
            }

            // Seed Users if they don't exist
            if (!context.Users.Any())
            {
                var adminRole = context.Roles.First(r => r.Type == RoleType.Admin);
                var managerRole = context.Roles.First(r => r.Type == RoleType.Manager);
                var userRole = context.Roles.First(r => r.Type == RoleType.User);

                var users = new List<User>
                {
                    new User { 
                        FullName = "Nguyễn Văn An",
                        Email = "nguyenvanan@example.com",
                        PhoneNumber = "0912345678",
                        Password = "Admin@123", // In production, this should be hashed
                        RoleId = adminRole.Id
                    },
                    new User { 
                        FullName = "Trần Thị Bình",
                        Email = "tranthibinh@example.com",
                        PhoneNumber = "0923456789",
                        Password = "Manager@123",
                        RoleId = managerRole.Id
                    },
                    new User { 
                        FullName = "Lê Văn Cường",
                        Email = "levancuong@example.com",
                        PhoneNumber = "0934567890",
                        Password = "User@123",
                        RoleId = userRole.Id
                    },
                    new User { 
                        FullName = "Phạm Thị Dung",
                        Email = "phamthidung@example.com",
                        PhoneNumber = "0945678901",
                        Password = "User@123",
                        RoleId = userRole.Id
                    },
                    new User { 
                        FullName = "Hoàng Văn Em",
                        Email = "hoangvanem@example.com",
                        PhoneNumber = "0956789012",
                        Password = "User@123",
                        RoleId = userRole.Id
                    },
                    new User { 
                        FullName = "Vũ Thị Phương",
                        Email = "vuthiphuong@example.com",
                        PhoneNumber = "0967890123",
                        Password = "User@123",
                        RoleId = userRole.Id
                    },
                    new User { 
                        FullName = "Đặng Văn Giang",
                        Email = "dangvangiang@example.com",
                        PhoneNumber = "0978901234",
                        Password = "Manager@123",
                        RoleId = managerRole.Id
                    },
                    new User { 
                        FullName = "Bùi Thị Hoa",
                        Email = "buithihoa@example.com",
                        PhoneNumber = "0989012345",
                        Password = "User@123",
                        RoleId = userRole.Id
                    }
                };
                context.Users.AddRange(users);
                context.SaveChanges();
            }

            // Seed Device Categories if they don't exist
            if (!context.DeviceCategories.Any())
            {
                var categories = new List<DeviceCategory>
                {
                    new DeviceCategory { 
                        Name = "Máy Tính Xách Tay",
                        Description = "Các loại laptop và notebook"
                    },
                    new DeviceCategory { 
                        Name = "Máy Tính Để Bàn",
                        Description = "Các loại máy tính để bàn"
                    },
                    new DeviceCategory { 
                        Name = "Máy In",
                        Description = "Thiết bị in ấn"
                    },
                    new DeviceCategory { 
                        Name = "Máy Chiếu",
                        Description = "Thiết bị trình chiếu"
                    },
                    new DeviceCategory { 
                        Name = "Thiết Bị Mạng",
                        Description = "Router, Switch và các thiết bị mạng khác"
                    }
                };
                context.DeviceCategories.AddRange(categories);
                context.SaveChanges();
            }

            // Seed Devices if they don't exist
            if (!context.Devices.Any())
            {
                var categories = context.DeviceCategories.ToList();
                var users = context.Users.ToList();

                var laptopCategory = categories.First(c => c.Name == "Máy Tính Xách Tay");
                var desktopCategory = categories.First(c => c.Name == "Máy Tính Để Bàn");

                var devices = new List<Device>
                {
                    new Device {
                        Name = "Laptop Dell XPS 13",
                        Code = "LAP001",
                        Description = "High-end laptop for development",
                        PurchaseDate = DateTime.Now,
                        Status = DeviceStatus.Available,
                        DeviceCategoryId = laptopCategory.Id,
                        EntryDate = DateTime.Now
                    },
                    new Device {
                        Name = "iPhone 13 Pro",
                        Code = "PHN001",
                        Description = "Mobile device for testing",
                        PurchaseDate = DateTime.Now,
                        Status = DeviceStatus.Available,
                        DeviceCategoryId = desktopCategory.Id,
                        EntryDate = DateTime.Now
                    }
                };
                context.Devices.AddRange(devices);
                context.SaveChanges();
            }
        }
    }
}
