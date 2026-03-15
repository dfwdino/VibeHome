CREATE TABLE [HomeAppSettings].[UsersRole] (
    [UsersRoleID]    INT           IDENTITY (1, 1) NOT NULL,
    [UserID]         INT           NOT NULL,
    [RoleID]         INT           NOT NULL,
    [ControllerName] NVARCHAR (50) NULL,
    [FunctionName]   NVARCHAR (50) NULL,
    CONSTRAINT [PK_UsersPermissions] PRIMARY KEY CLUSTERED ([UsersRoleID] ASC),
    CONSTRAINT [FK_UsersPermissions_UsersPermissions] FOREIGN KEY ([UsersRoleID]) REFERENCES [HomeAppSettings].[UsersRole] ([UsersRoleID])
);

