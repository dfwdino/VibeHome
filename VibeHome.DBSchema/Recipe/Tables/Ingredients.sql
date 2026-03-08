CREATE TABLE [Recipe].[Ingredients] (
    [IngredientId]   INT            IDENTITY (1, 1) NOT NULL,
    [IngredientName] NVARCHAR (200) NOT NULL,
    [IsDeleted]      BIT            DEFAULT ((0)) NOT NULL,
    [CreatedAt]      DATETIME2 (7)  DEFAULT (sysdatetime()) NOT NULL,
    [ModifiedAt]     DATETIME2 (7)  DEFAULT (sysdatetime()) NOT NULL,
    PRIMARY KEY CLUSTERED ([IngredientId] ASC)
);

GO
CREATE NONCLUSTERED INDEX [IX_Ingredients_IsDeleted]
    ON [Recipe].[Ingredients]([IsDeleted] ASC);

GO
CREATE NONCLUSTERED INDEX [IX_Ingredients_IngredientName]
    ON [Recipe].[Ingredients]([IngredientName] ASC);
