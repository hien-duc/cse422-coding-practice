-- Create Database
CREATE DATABASE DeviceManagement;
GO

USE DeviceManagement;
GO

-- Create DeviceCategories table
CREATE TABLE DeviceCategories (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(50) NOT NULL,
    Description NVARCHAR(200)
);
GO

-- Create Users table
CREATE TABLE Users (
    Id INT PRIMARY KEY IDENTITY(1,1),
    FullName NVARCHAR(100) NOT NULL,
    Email NVARCHAR(100) NOT NULL UNIQUE,
    PhoneNumber NVARCHAR(20) NOT NULL
);
GO

-- Create Devices table
CREATE TABLE Devices (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(100) NOT NULL,
    Code NVARCHAR(50) NOT NULL UNIQUE,
    DeviceCategoryId INT NOT NULL,
    Status INT NOT NULL, -- 0: InUse, 1: Broken, 2: UnderMaintenance
    EntryDate DATETIME NOT NULL DEFAULT GETDATE(),
    FOREIGN KEY (DeviceCategoryId) REFERENCES DeviceCategories(Id)
);
GO

-- Insert sample data for DeviceCategories
INSERT INTO DeviceCategories (Name, Description) VALUES
('Computer', 'Desktop computers and laptops'),
('Printer', 'Office printers and scanners'),
('Phone', 'Mobile phones and desk phones'),
('Network Equipment', 'Routers, switches, and network devices');
GO

-- Insert sample data for Users
INSERT INTO Users (FullName, Email, PhoneNumber) VALUES
(N'Nguyễn Văn An', 'van.an@example.com', '0912345678'),
(N'Trần Thị Bình', 'thi.binh@example.com', '0923456789'),
(N'Lê Hoàng Cường', 'hoang.cuong@example.com', '0934567890'),
(N'Phạm Minh Đức', 'minh.duc@example.com', '0945678901'),
(N'Võ Thị Em', 'thi.em@example.com', '0956789012');
GO

-- Insert sample data for Devices
INSERT INTO Devices (Name, Code, DeviceCategoryId, Status, EntryDate) VALUES
('Dell XPS 15', 'COMP001', 1, 0, GETDATE()),
('HP LaserJet Pro', 'PRINT001', 2, 0, GETDATE()),
('iPhone 13', 'PHONE001', 3, 0, GETDATE()),
('Cisco Switch', 'NET001', 4, 0, GETDATE()),
('Lenovo ThinkPad', 'COMP002', 1, 1, GETDATE()),
('Canon PIXMA', 'PRINT002', 2, 2, GETDATE());
GO

-- Create index for faster searching
CREATE INDEX IX_Devices_Code ON Devices(Code);
CREATE INDEX IX_Devices_Name ON Devices(Name);
CREATE INDEX IX_Devices_CategoryId ON Devices(DeviceCategoryId);
GO

-- Create view for device details
CREATE VIEW vw_DeviceDetails AS
SELECT 
    d.Id,
    d.Name AS DeviceName,
    d.Code AS DeviceCode,
    d.Status,
    d.EntryDate,
    dc.Name AS CategoryName,
    dc.Description AS CategoryDescription
FROM Devices d
INNER JOIN DeviceCategories dc ON d.DeviceCategoryId = dc.Id;
GO

-- Create stored procedure for searching devices
CREATE PROCEDURE sp_SearchDevices
    @SearchTerm NVARCHAR(100) = NULL,
    @CategoryId INT = NULL,
    @Status INT = NULL
AS
BEGIN
    SELECT 
        d.Id,
        d.Name,
        d.Code,
        d.Status,
        d.EntryDate,
        dc.Name AS CategoryName
    FROM Devices d
    INNER JOIN DeviceCategories dc ON d.DeviceCategoryId = dc.Id
    WHERE 
        (@SearchTerm IS NULL OR (d.Name LIKE '%' + @SearchTerm + '%' OR d.Code LIKE '%' + @SearchTerm + '%'))
        AND (@CategoryId IS NULL OR d.DeviceCategoryId = @CategoryId)
        AND (@Status IS NULL OR d.Status = @Status)
    ORDER BY d.EntryDate DESC;
END;
GO
