namespace VibeHome.Domain.Entities.Recipes
{
    public class RecipeIngredient
    {
        public int RecipeIngredientId { get; set; }
        public int RecipeId { get; set; }
        public int? IngredientId { get; set; }
        public string IngredientName { get; set; } = string.Empty;
        public string? Quantity { get; set; }
        public int? UnitTypeId { get; set; }
        public int SortOrder { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }

        public UnitType? UnitType { get; set; }
    }
}
