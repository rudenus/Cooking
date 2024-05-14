namespace Cooking.Infrastructure.Validator.Recipe
{
    public class ValidatorRecipeInput
    {
        public IEnumerable<ValidatorRecipeInputOperation> Operations { get; set; }

        public IEnumerable<ValidatorRecipeInputIngridient> Ingridients { get; set; }

        public int? Weight { get; set; }
    }

    public class ValidatorRecipeInputIngridient
    {
        public ValidatorRecipeInputProduct NewProduct { get; set; }

        public Guid? ExistingProductId { get; set; }

        public int Weight { get; set; }
    }

    public class ValidatorRecipeInputOperation
    {
        public int Step { get; set; }

        public int TimeInSeconds { get; set; }
    }

    public class ValidatorRecipeInputProduct
    {
        public int Carbohydrates { get; set; }

        public int Fats { get; set; }

        public int Proteins { get; set; }
    }
}

