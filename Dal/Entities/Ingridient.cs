namespace Dal.Entities
{
    public class Ingridient
    {
        public Guid IngridientId { get; set; }

        public Guid ProductId { get; set; }

        public Guid RecipeId { get; set; }

        public int? Weight { get; set; }

        public Product Product { get; set; }

        public Recipe Recipe { get; set; }
    }
}
