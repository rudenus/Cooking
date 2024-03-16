namespace Cooking.Dto.Recipe.List
{
    public class ListResult
    {
        public int Calories { get; set; }

        public int Carbohydrates { get; set; }

        public string Description { get; set; }

        public int Fats { get; set; }

        public string Name { get; set; }

        public int Proteins { get; set; }

        public Guid RecipeId { get; set; }

        public string Products { get; set; }
    }
}
