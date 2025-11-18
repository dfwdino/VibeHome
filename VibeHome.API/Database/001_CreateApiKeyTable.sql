-- Migration: Create ApiKeys table
-- Date: 2025-11-18
-- Description: Creates the ApiKeys table in the Kids schema for API key authentication

USE HomeAppsV3;
GO

-- Create ApiKeys table
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Kids].[ApiKeys]') AND type in (N'U'))
BEGIN
    CREATE TABLE [Kids].[ApiKeys] (
        [ApiKeyId] INT IDENTITY(1,1) NOT NULL,
        [KeyName] NVARCHAR(100) NOT NULL,
        [KeyValue] NVARCHAR(500) NOT NULL,
        [Description] NVARCHAR(1000) NULL,
        [IsActive] BIT NOT NULL DEFAULT 1,
        [ExpiresAt] DATETIME2 NULL,
        [LastUsedAt] DATETIME2 NULL,
        [IsDeleted] BIT NOT NULL DEFAULT 0,
        [CreatedAt] DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
        [ModifiedAt] DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
        CONSTRAINT [PK_ApiKeys] PRIMARY KEY CLUSTERED ([ApiKeyId] ASC)
    );

    PRINT 'ApiKeys table created successfully.';
END
ELSE
BEGIN
    PRINT 'ApiKeys table already exists.';
END
GO

-- Create index on KeyValue for faster lookups
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_ApiKeys_KeyValue' AND object_id = OBJECT_ID('Kids.ApiKeys'))
BEGIN
    CREATE NONCLUSTERED INDEX [IX_ApiKeys_KeyValue]
    ON [Kids].[ApiKeys] ([KeyValue])
    WHERE [IsDeleted] = 0 AND [IsActive] = 1;

    PRINT 'Index IX_ApiKeys_KeyValue created successfully.';
END
GO

-- Create index on IsActive and IsDeleted for faster filtering
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_ApiKeys_Active' AND object_id = OBJECT_ID('Kids.ApiKeys'))
BEGIN
    CREATE NONCLUSTERED INDEX [IX_ApiKeys_Active]
    ON [Kids].[ApiKeys] ([IsActive], [IsDeleted])
    INCLUDE ([ExpiresAt]);

    PRINT 'Index IX_ApiKeys_Active created successfully.';
END
GO

PRINT 'Migration completed successfully.';
GO
