namespace BusinessLogic.RecipeLogic.List
{
    internal class ListFilterModel
    {
        public int Calories { get; set; }

        public int Carbohydrates { get; set; }

        public string Description { get; set; }

        public int Fats { get; set; }

        public string Name { get; set; }

        public int Proteins { get; set; }

        public Guid RecipeId { get; set; }

        public int Weight { get; set; }

        public IEnumerable<ProductListFilterModel> Products { get; set; } = Enumerable.Empty<ProductListFilterModel>();
    }

    internal class ProductListFilterModel
    {
        public Guid ProductId { get; set; }

        public string ProductName { get; set; }
    }
}
