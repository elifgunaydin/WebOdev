﻿CREATE TABLE Users
(
    UserID INT PRIMARY KEY IDENTITY,
    Ad NVARCHAR(50) NOT NULL,
    Soyad NVARCHAR(50) NOT NULL,
    Email NVARCHAR(100) UNIQUE NOT NULL,
    Telefon NVARCHAR(20),
    Sifre NVARCHAR(256) NOT NULL
);
CREATE TABLE Roles
(
    RoleID INT PRIMARY KEY IDENTITY,
    RoleName NVARCHAR(50) UNIQUE NOT NULL
);
INSERT INTO Roles (RoleName) VALUES ('Admin');
INSERT INTO Roles (RoleName) VALUES ('Calisan');
INSERT INTO Roles (RoleName) VALUES ('Musteri');
CREATE TABLE UserRoles
(
    UserRoleID INT PRIMARY KEY IDENTITY,
    UserID INT NOT NULL,
    RoleID INT NOT NULL,
    FOREIGN KEY (UserID) REFERENCES Users(UserID),
    FOREIGN KEY (RoleID) REFERENCES Roles(RoleID)
);
