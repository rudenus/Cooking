namespace BusinessLogic.RecipeLogic.Models.Create
{
    public class CreateRecipeInput
    {
        public string Description { get; set; }

        public byte[]? File {  get; set; }

        public string Name { get; set; }

        public int? ServingsNumber { get; set; }

        public Guid UserId { get; set; }

        public int? Weight { get; set; }

        public IEnumerable<CreateRecipeInputIngridient> Ingridients { get; set; }

        public IEnumerable<CreateRecipeInputOperation> Operations { get; set; }
    }

    public class CreateRecipeInputProduct
    {
        public int Calories { get; set; }

        public int Carbohydrates { get; set; }

        public int Fats { get; set; }

        public string Name { get; set; }

        public int Proteins { get; set; }
    }

    public class CreateRecipeInputOperation
    {
        public int Step { get; set; }
        
        public byte[]? File { get; set; }

        public string Description { get; set; }

        public int TimeInSeconds { get; set; }

        public string Title { get; set; }
    }

    public class CreateRecipeInputIngridient
    {
        public CreateRecipeInputProduct NewProduct { get; set; }

        public Guid? ExistingProductId { get; set; }

        public int Weight { get; set; }
    }
}
