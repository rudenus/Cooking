namespace Dal.Entities
{
    public class Recipe
    {
        public Guid RecipeId { get; set; }

        public int Calories { get; set; }

        public int Carbohydrates { get; set; }

        public string Description { get; set; }

        public int Fats { get; set; }

        public Guid? FileId { get; set; }

        public bool IsModerated { get; set; }

        public string Name { get; set; }

        public int Proteins { get; set; }

        public int ServingsNumber { get; set; }

        public int Weight { get; set; }

        public Guid UserId { get; set; }

        public File? File { get; set; }

        public User User { get; set; }

        public ICollection<Ingridient> Ingridients { get; set; } = new List<Ingridient>();

        public ICollection<Operation> Operations { get; set; } = new List<Operation>();

        public ICollection<Publication> Publications { get; set; } = new List<Publication>();
    }
}
