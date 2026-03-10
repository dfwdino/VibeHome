namespace VibeHome.Domain.Entities.Recipes
{
    public class Recipe
    {
        public int RecipeId { get; set; }
        public string RecipeName { get; set; } = string.Empty;
        public int? PrepTimeMinutes { get; set; }
        public int? CookTimeMinutes { get; set; }
        public int? Servings { get; set; }
        public string? DifficultyLevel { get; set; }
        public string? Notes { get; set; }
        public int? Calories { get; set; }
        public decimal? Protein { get; set; }
        public decimal? Fat { get; set; }
        public decimal? TotalCarbs { get; set; }
        public decimal? Fiber { get; set; }
        public decimal? Sugar { get; set; }
        public int? Sodium { get; set; }
        public string? AdditionalNutritionDetails { get; set; }
        public decimal? Rating { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }

        public ICollection<RecipeIngredient> RecipeIngredients { get; set; } = new List<RecipeIngredient>();
        public ICollection<RecipeInstruction> RecipeInstructions { get; set; } = new List<RecipeInstruction>();
        public ICollection<RecipeFavorite> RecipeFavorites { get; set; } = new List<RecipeFavorite>();
    }
}
