CREATE TABLE [Recipe].[RecipeFavorites] (
    [RecipeFavoriteId] INT           IDENTITY (1, 1) NOT NULL,
    [RecipeId]         INT           NOT NULL,
    -- TODO: Enable FK when auth is ready:
    -- CONSTRAINT [FK_RecipeFavorites_Users] FOREIGN KEY ([UserId]) REFERENCES [Kids].[Users] ([UserId])
    [UserId]           INT           NULL,
    [IsDeleted]        BIT           DEFAULT ((0)) NOT NULL,
    [CreatedAt]        DATETIME2 (7) DEFAULT (sysdatetime()) NOT NULL,
    [ModifiedAt]       DATETIME2 (7) DEFAULT (sysdatetime()) NOT NULL,
    PRIMARY KEY CLUSTERED ([RecipeFavoriteId] ASC),
    CONSTRAINT [FK_RecipeFavorites_Recipes] FOREIGN KEY ([RecipeId]) REFERENCES [Recipe].[Recipes] ([RecipeId])
);

GO
CREATE NONCLUSTERED INDEX [IX_RecipeFavorites_RecipeId]
    ON [Recipe].[RecipeFavorites]([RecipeId] ASC);

GO
CREATE NONCLUSTERED INDEX [IX_RecipeFavorites_IsDeleted]
    ON [Recipe].[RecipeFavorites]([IsDeleted] ASC);
