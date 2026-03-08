CREATE TABLE [Recipe].[UnitTypes] (
    [UnitTypeId]   INT           IDENTITY (1, 1) NOT NULL,
    [UnitName]     NVARCHAR (50) NOT NULL,
    [Abbreviation] NVARCHAR (20) NULL,
    [IsDeleted]    BIT           DEFAULT ((0)) NOT NULL,
    [CreatedAt]    DATETIME2 (7) DEFAULT (sysdatetime()) NOT NULL,
    [ModifiedAt]   DATETIME2 (7) DEFAULT (sysdatetime()) NOT NULL,
    PRIMARY KEY CLUSTERED ([UnitTypeId] ASC)
);

GO
CREATE NONCLUSTERED INDEX [IX_UnitTypes_IsDeleted]
    ON [Recipe].[UnitTypes]([IsDeleted] ASC);
