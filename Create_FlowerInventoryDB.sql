-- Drop database if it already exists (optional)
IF DB_ID('FlowerInventoryDB') IS NOT NULL
BEGIN
    ALTER DATABASE FlowerInventoryDB SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
    DROP DATABASE FlowerInventoryDB;
END
GO

-- Create database
CREATE DATABASE FlowerInventoryDB;
GO

USE FlowerInventoryDB;
GO

-- Create Category table
CREATE TABLE Categories (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(100) NOT NULL
);
GO

-- Create Flower table
CREATE TABLE Flowers (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(100) NOT NULL,
    Price DECIMAL(10,2) NOT NULL,
    CategoryId INT NOT NULL,
    FOREIGN KEY (CategoryId) REFERENCES Categories(Id) ON DELETE CASCADE
);
GO

-- Seed Categories
INSERT INTO Categories (Name) VALUES
('Roses'),
('Tulips');
GO

-- Seed Flowers
INSERT INTO Flowers (Name, Price, CategoryId) VALUES
('Red Rose', 3.50, 1),
('White Rose', 4.00, 1),
('Yellow Tulip', 2.50, 2),
('Pink Tulip', 3.00, 2);
GO

-- Verify seed data
SELECT * FROM Categories;
SELECT * FROM Flowers;
