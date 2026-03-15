CREATE TABLE [HomeAppSettings].[Roles] (
    [PermissionLevelID] INT           IDENTITY (1, 1) NOT NULL,
    [PermissionName]    NVARCHAR (50) NOT NULL,
    [IsDeleted]         CHAR (1)      CONSTRAINT [DEFAULT_PermissionLevels_IsDeleted] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_PermissionLevels] PRIMARY KEY CLUSTERED ([PermissionLevelID] ASC)
);

