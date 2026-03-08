namespace VibeHome.Domain.Entities
{
    public class RecipeInstruction
    {
        public int RecipeInstructionId { get; set; }
        public int RecipeId { get; set; }
        public int StepNumber { get; set; }
        public string InstructionText { get; set; } = string.Empty;
        public string? IconCode { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
    }
}
