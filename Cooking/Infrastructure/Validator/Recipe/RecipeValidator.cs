using Dal.Migrations;

namespace Cooking.Infrastructure.Validator.Recipe
{
    public class RecipeValidator
    {
        public ValidatorRecipeOutput Validate(ValidatorRecipeInput input)
        {
            if (input.Ingridients == null || !input.Ingridients.Any())
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

            if(input.Operations.Any(x => x.TimeInSeconds < 0))
            {
                throw new ArgumentException("Время шага не может быть отрицательным");
            }

            if(!isOperationsStepsValid(input.Operations.Select(x => x.Step)))//если каждый шаг уникален
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
                        Carbohydrates = x.NewProduct.Carbohydrates,
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

        //Шаги начинаться с 1 и увеличиваться на 1
        private bool isOperationsStepsValid(IEnumerable<int> steps)
        {
            int lastStep = 0;
            foreach(int step in steps.Order())
            {
                if(step - lastStep != 1)
                {
                    return false;
                }

                lastStep = step;
            }

            return true;
        }
    }
}
