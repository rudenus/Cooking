namespace Cooking.Infrastructure.Validator.Recipe
{
    public class RecipeValidator
    {
        public ValidatorRecipeOutput Validate(ValidatorRecipeInput input)
        {
            if (input.Ingridients == null || input.Ingridients.Any())
            {
                throw new ArgumentException("Список ингридиентов не может быть пустым");
            }

            foreach(var ingridient in input.Ingridients)
            {
                if(ingridient.ExistingProductId != null)
                {
                    continue;
                }

                if(ingridient.NewProduct == null)
                {
                    throw new ArgumentException("Продукт не может быть пустым");
                }

                if(ingridient.NewProduct.Calories < 0)
                {
                    throw new ArgumentException("Количество калорий в продукте не может быть отрицательным");
                }

                if (ingridient.NewProduct.Carbohydrates < 0)
                {
                    throw new ArgumentException("Количество углеводов в продукте не может быть отрицательным");
                }

                if (ingridient.NewProduct.Fats < 0)
                {
                    throw new ArgumentException("Количество жиров в продукте не может быть отрицательным");
                }

                if (ingridient.NewProduct.Proteins < 0)
                {
                    throw new ArgumentException("Количество белков в продукте не может быть отрицательным");
                }
            }

            var incorrectOperation = input.Operations.FirstOrDefault(x => x.TimeInSeconds < 0);

            if(incorrectOperation != null)
            {
                throw new ArgumentException("Время одного шага не может быть отрицательным");
            }

            if(input.Operations.Count() != input.Operations.Max(x => x.Step) - input.Operations.Min(x => x.Step)//если разница равна количеству
                || input.Operations.Count() != input.Operations.DistinctBy(x => x.Step).Count()
                || input.Operations.Any(x => x.Step == 0))//если каждый шаг уникален
            {
                throw new ArgumentException("Последовательность шагов должна начинаться с нуля и всегда увеличиваться на единицу");
            }

            return new ValidatorRecipeOutput()
            {
                Ingridients = input.Ingridients.Select(x => new ValidatorRecipeOutputIngridient()
                {
                    ExistingProductId = x.ExistingProductId,
                    NewProduct = x.NewProduct != null ? new ValidatorRecipeOutputProduct()
                    {
                        Calories = x.NewProduct.Calories,
                        Carbohydrates = x.NewProduct.Calories,
                        Fats = x.NewProduct.Fats,
                        Proteins = x.NewProduct.Proteins
                    } : null,
                    Weight = x.Weight
                }),

                Operations = input.Operations.Select(x => new ValidatorRecipeOutputOperation()
                {
                    Step = x.Step,
                    TimeInSeconds = x.TimeInSeconds,
                })
            };
        }
    }
}
