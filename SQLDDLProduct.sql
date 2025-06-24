CREATE DATABASE ProductDB;
GO

USE ProductDB;
GO

CREATE TABLE Products (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(100) NOT NULL,
    Price DECIMAL(18,2) NOT NULL,
    CreatedDate DATETIME DEFAULT GETDATE()
);
GO

-- Chèn dữ liệu mẫu
INSERT INTO Products (Name, Price) 
VALUES 
    ('Laptop', 1500.00),
    ('Smartphone', 800.00),
    ('Headphones', 120.50);
GO