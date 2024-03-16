namespace Dal.Entities
{
    public class Operation
    {
        public Guid RecipeId { get; set; }

        public int Step {  get; set; }

        public string Description { get; set; }

        public int TimeInSeconds { get; set; }

        public string Title { get; set; }

        public Recipe Recipe { get; set; }
    }
}
