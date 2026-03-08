CREATE TABLE [Recipe].[RecipeIngredients] (
    [RecipeIngredientId] INT            IDENTITY (1, 1) NOT NULL,
    [RecipeId]           INT            NOT NULL,
    [IngredientId]       INT            NULL,
    [IngredientName]     NVARCHAR (200) NOT NULL,
    [Quantity]           NVARCHAR (50)  NULL,
    [UnitTypeId]         INT            NULL,
    [SortOrder]          INT            DEFAULT ((0)) NOT NULL,
    [IsDeleted]          BIT            DEFAULT ((0)) NOT NULL,
    [CreatedAt]          DATETIME2 (7)  DEFAULT (sysdatetime()) NOT NULL,
    [ModifiedAt]         DATETIME2 (7)  DEFAULT (sysdatetime()) NOT NULL,
    PRIMARY KEY CLUSTERED ([RecipeIngredientId] ASC),
    CONSTRAINT [FK_RecipeIngredients_Recipes]     FOREIGN KEY ([RecipeId])     REFERENCES [Recipe].[Recipes]     ([RecipeId]),
    CONSTRAINT [FK_RecipeIngredients_Ingredients] FOREIGN KEY ([IngredientId]) REFERENCES [Recipe].[Ingredients] ([IngredientId]),
    CONSTRAINT [FK_RecipeIngredients_UnitTypes]   FOREIGN KEY ([UnitTypeId])   REFERENCES [Recipe].[UnitTypes]   ([UnitTypeId])
);

GO
CREATE NONCLUSTERED INDEX [IX_RecipeIngredients_RecipeId]
    ON [Recipe].[RecipeIngredients]([RecipeId] ASC);

GO
CREATE NONCLUSTERED INDEX [IX_RecipeIngredients_IsDeleted]
    ON [Recipe].[RecipeIngredients]([IsDeleted] ASC);
