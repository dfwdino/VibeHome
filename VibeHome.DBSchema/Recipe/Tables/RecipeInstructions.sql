CREATE TABLE [Recipe].[RecipeInstructions] (
    [RecipeInstructionId] INT            IDENTITY (1, 1) NOT NULL,
    [RecipeId]            INT            NOT NULL,
    [StepNumber]          INT            NOT NULL,
    [InstructionText]     NVARCHAR (MAX) NOT NULL,
    [IconCode]            NVARCHAR (10)  NULL,
    [IsDeleted]           BIT            DEFAULT ((0)) NOT NULL,
    [CreatedAt]           DATETIME2 (7)  DEFAULT (sysdatetime()) NOT NULL,
    [ModifiedAt]          DATETIME2 (7)  DEFAULT (sysdatetime()) NOT NULL,
    PRIMARY KEY CLUSTERED ([RecipeInstructionId] ASC),
    CONSTRAINT [FK_RecipeInstructions_Recipes] FOREIGN KEY ([RecipeId]) REFERENCES [Recipe].[Recipes] ([RecipeId])
);

GO
CREATE NONCLUSTERED INDEX [IX_RecipeInstructions_RecipeId]
    ON [Recipe].[RecipeInstructions]([RecipeId] ASC);

GO
CREATE NONCLUSTERED INDEX [IX_RecipeInstructions_IsDeleted]
    ON [Recipe].[RecipeInstructions]([IsDeleted] ASC);
