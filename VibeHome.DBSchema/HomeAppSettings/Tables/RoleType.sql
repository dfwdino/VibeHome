CREATE TABLE [HomeAppSettings].[RoleType] (
    [RoleTypeID]   INT           IDENTITY (1, 1) NOT NULL,
    [RoleTypeName] NVARCHAR (50) NOT NULL,
    [IsDeleted]    BIT           CONSTRAINT [DF_RoleType_IsDeleted] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_RoleType] PRIMARY KEY CLUSTERED ([RoleTypeID] ASC)
);

