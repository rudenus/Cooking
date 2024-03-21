namespace Cooking.Infrastructure.Validator.Recipe
{
    public class ValidatorRecipeOutput
    {
        public IEnumerable<ValidatorRecipeOutputOperation> Operations { get; set; }

        public IEnumerable<ValidatorRecipeOutputIngridient> Ingridients { get; set; }
    }

    public class ValidatorRecipeOutputIngridient
    {
        public ValidatorRecipeOutputProduct NewProduct { get; set; }

        public Guid? ExistingProductId { get; set; }

        public int Weight { get; set; }
    }

    public class ValidatorRecipeOutputOperation
    {
        public int Step { get; set; }

        public int TimeInSeconds { get; set; }
    }

    public class ValidatorRecipeOutputProduct
    {
        public int Calories { get; set; }

        public int Carbohydrates { get; set; }

        public int Fats { get; set; }

        public int Proteins { get; set; }
    }
}
