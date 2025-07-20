-- KidsChore Project: Kids Schema SQL
-- Ensure the Kids schema exists
IF NOT EXISTS (SELECT * FROM sys.schemas WHERE name = 'Kids')
    EXEC('CREATE SCHEMA Kids');
GO

-- Users table for authentication
CREATE TABLE Kids.Users (
    UserId INT IDENTITY(1,1) PRIMARY KEY,
    Username NVARCHAR(100) NOT NULL UNIQUE,
    PasswordHash NVARCHAR(256) NOT NULL,
    Role NVARCHAR(20) NOT NULL, -- 'Parent' or 'Child'
    IsDeleted BIT NOT NULL DEFAULT 0,
    CreatedAt DATETIME2 NOT NULL DEFAULT SYSDATETIME(),
    ModifiedAt DATETIME2 NOT NULL DEFAULT SYSDATETIME()
);
GO

-- Kids table
CREATE TABLE Kids.Kids (
    KidId INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(100) NOT NULL,
    Age INT NULL,
    Grade NVARCHAR(20) NULL,
    IsDeleted BIT NOT NULL DEFAULT 0,
    CreatedAt DATETIME2 NOT NULL DEFAULT SYSDATETIME(),
    ModifiedAt DATETIME2 NOT NULL DEFAULT SYSDATETIME()
);
GO

-- Locations table
CREATE TABLE Kids.Locations (
    LocationId INT IDENTITY(1,1) PRIMARY KEY,
    LocationName NVARCHAR(100) NOT NULL,
    Description NVARCHAR(255) NULL,
    IsDeleted BIT NOT NULL DEFAULT 0,
    CreatedAt DATETIME2 NOT NULL DEFAULT SYSDATETIME(),
    ModifiedAt DATETIME2 NOT NULL DEFAULT SYSDATETIME()
);
GO

-- ChoreItems table
CREATE TABLE Kids.ChoreItems (
    ChoreItemId INT IDENTITY(1,1) PRIMARY KEY,
    ChoreName NVARCHAR(100) NOT NULL,
    Price DECIMAL(10,2) NOT NULL,
    Description NVARCHAR(255) NULL,
    IsDeleted BIT NOT NULL DEFAULT 0,
    CreatedAt DATETIME2 NOT NULL DEFAULT SYSDATETIME(),
    ModifiedAt DATETIME2 NOT NULL DEFAULT SYSDATETIME()
);
GO

-- ChoreCompletions table
CREATE TABLE Kids.ChoreCompletions (
    ChoreCompletionId INT IDENTITY(1,1) PRIMARY KEY,
    KidId INT NOT NULL,
    ChoreItemId INT NOT NULL,
    LocationId INT NOT NULL,
    CompletionDateTime DATETIME2 NOT NULL,
    Notes NVARCHAR(255) NULL,
    IsDeleted BIT NOT NULL DEFAULT 0,
    CreatedAt DATETIME2 NOT NULL DEFAULT SYSDATETIME(),
    ModifiedAt DATETIME2 NOT NULL DEFAULT SYSDATETIME(),
    CONSTRAINT FK_ChoreCompletions_Kid FOREIGN KEY (KidId) REFERENCES Kids.Kids(KidId),
    CONSTRAINT FK_ChoreCompletions_ChoreItem FOREIGN KEY (ChoreItemId) REFERENCES Kids.ChoreItems(ChoreItemId),
    CONSTRAINT FK_ChoreCompletions_Location FOREIGN KEY (LocationId) REFERENCES Kids.Locations(LocationId)
);
GO

-- Indexes for performance
CREATE INDEX IX_Kids_IsDeleted ON Kids.Kids(IsDeleted);
CREATE INDEX IX_Locations_IsDeleted ON Kids.Locations(IsDeleted);
CREATE INDEX IX_ChoreItems_IsDeleted ON Kids.ChoreItems(IsDeleted);
CREATE INDEX IX_ChoreCompletions_KidId ON Kids.ChoreCompletions(KidId);
CREATE INDEX IX_ChoreCompletions_ChoreItemId ON Kids.ChoreCompletions(ChoreItemId);
CREATE INDEX IX_ChoreCompletions_LocationId ON Kids.ChoreCompletions(LocationId);
CREATE INDEX IX_ChoreCompletions_CompletionDateTime ON Kids.ChoreCompletions(CompletionDateTime);
CREATE INDEX IX_Users_IsDeleted ON Kids.Users(IsDeleted);
GO

-- Triggers to update ModifiedAt on update for all tables
CREATE TRIGGER Kids.trg_Kids_ModifiedAt
ON Kids.Kids
AFTER UPDATE
AS
BEGIN
    SET NOCOUNT ON;
    UPDATE Kids.Kids
    SET ModifiedAt = SYSDATETIME()
    FROM Inserted
    WHERE Kids.Kids.KidId = Inserted.KidId;
END
GO

CREATE TRIGGER Kids.trg_Locations_ModifiedAt
ON Kids.Locations
AFTER UPDATE
AS
BEGIN
    SET NOCOUNT ON;
    UPDATE Kids.Locations
    SET ModifiedAt = SYSDATETIME()
    FROM Inserted
    WHERE Kids.Locations.LocationId = Inserted.LocationId;
END
GO

CREATE TRIGGER Kids.trg_ChoreItems_ModifiedAt
ON Kids.ChoreItems
AFTER UPDATE
AS
BEGIN
    SET NOCOUNT ON;
    UPDATE Kids.ChoreItems
    SET ModifiedAt = SYSDATETIME()
    FROM Inserted
    WHERE Kids.ChoreItems.ChoreItemId = Inserted.ChoreItemId;
END
GO

CREATE TRIGGER Kids.trg_ChoreCompletions_ModifiedAt
ON Kids.ChoreCompletions
AFTER UPDATE
AS
BEGIN
    SET NOCOUNT ON;
    UPDATE Kids.ChoreCompletions
    SET ModifiedAt = SYSDATETIME()
    FROM Inserted
    WHERE Kids.ChoreCompletions.ChoreCompletionId = Inserted.ChoreCompletionId;
END
GO

CREATE TRIGGER Kids.trg_Users_ModifiedAt
ON Kids.Users
AFTER UPDATE
AS
BEGIN
    SET NOCOUNT ON;
    UPDATE Kids.Users
    SET ModifiedAt = SYSDATETIME()
    FROM Inserted
    WHERE Kids.Users.UserId = Inserted.UserId;
END
GO 