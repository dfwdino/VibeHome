CREATE TABLE [Recipe].[Recipes] (
    [RecipeId]                   INT             IDENTITY (1, 1) NOT NULL,
    [RecipeName]                 NVARCHAR (200)  NOT NULL,
    [PrepTimeMinutes]            INT             NULL,
    [CookTimeMinutes]            INT             NULL,
    [Servings]                   INT             NULL,
    [DifficultyLevel]            NVARCHAR (20)   NULL,
    [Notes]                      NVARCHAR (MAX)  NULL,
    [Calories]                   INT             NULL,
    [Protein]                    DECIMAL (10, 2) NULL,
    [Fat]                        DECIMAL (10, 2) NULL,
    [TotalCarbs]                 DECIMAL (10, 2) NULL,
    [Fiber]                      DECIMAL (10, 2) NULL,
    [Sugar]                      DECIMAL (10, 2) NULL,
    [Sodium]                     INT             NULL,
    [AdditionalNutritionDetails] NVARCHAR (500)  NULL,
    [Rating]                     DECIMAL (3, 1)  NULL,
    [IsDeleted]                  BIT             DEFAULT ((0)) NOT NULL,
    [CreatedAt]                  DATETIME2 (7)   DEFAULT (sysdatetime()) NOT NULL,
    [ModifiedAt]                 DATETIME2 (7)   DEFAULT (sysdatetime()) NOT NULL,
    PRIMARY KEY CLUSTERED ([RecipeId] ASC)
);

GO
CREATE NONCLUSTERED INDEX [IX_Recipes_IsDeleted]
    ON [Recipe].[Recipes]([IsDeleted] ASC);

GO
CREATE NONCLUSTERED INDEX [IX_Recipes_RecipeName]
    ON [Recipe].[Recipes]([RecipeName] ASC);
