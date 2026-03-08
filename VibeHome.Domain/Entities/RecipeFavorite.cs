namespace VibeHome.Domain.Entities
{
    public class RecipeFavorite
    {
        public int RecipeFavoriteId { get; set; }
        public int RecipeId { get; set; }
        // TODO: Wire up to authenticated UserId from [Kids].[Users] when auth is enabled
        public int? UserId { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
    }
}
