using BusinessLogic.RecipeLogic.Models.Create;
using BusinessLogic.RecipeLogic.Models.Get;
using BusinessLogic.RecipeLogic.Models.List;
using Dal;
using Dal.Entities;
using Dal.Migrations;
using Microsoft.EntityFrameworkCore;

namespace BusinessLogic.RecipeLogic
{
    public class RecipeLogic
    {
        private const int averageServingSize = 375;

        private const string recipeFileName = "Приложение к рецепту";

        private const string operationFileName = "Приложение к шагу рецепта рецепту";

        private readonly Context context;

        private readonly Filtrator filtrator;
        public RecipeLogic(Context context)
        {
            this.context = context;
            this.filtrator = new Filtrator(context);
        }

        public async Task<GetRecipeOutput> Get(Guid recipeId)
        {
            var recipe = await context.Recipes
                .Where(x => x.RecipeId == recipeId)
                .Select(x => new GetRecipeOutput()
                {
                    Description = x.Description,
                    Name = x.Name,
                    Weight = x.Weight,
                    Calories = x.Calories,
                    Carbohydrates = x.Carbohydrates,
                    Fats = x.Fats,
                    IsModerated = x.IsModerated,
                    Ingridients = x.Ingridients.Select(y => new GetRecipeOutputIngridient()
                    {
                        Calories = y.Product.Calories,
                        Carbohydrates = y.Product.Carbohydrates,
                        Fats = y.Product.Fats,
                        Name = y.Product.Name,
                        Proteins = y.Product.Proteins,
                        Weight = y.Weight
                    }),
                    Operations = x.Operations.Select(y => new GetRecipeOutputOperation()
                    {
                        Description = y.Description,
                        File = y.File.Content,
                        Step = y.Step,
                    })
                })
                .FirstOrDefaultAsync();

            return recipe;
        }

        public async Task Update()
        {

        }

        public async Task Delete()
        {

        }

        public async Task Create(CreateRecipeInput input)
        {
            Guid recipeId = Guid.NewGuid();
            
            var recipe = await CreateRecipeBody(input, recipeId);

            context.Add(recipe);

            var listIngridients = CreateIngridients(input.Ingridients, recipeId);
            context.AddRange(listIngridients);

            var listOperations = CreateOperations(input.Operations, recipeId);
            context.AddRange(listOperations);

            await context.SaveChangesAsync();
        }

        public async Task<ListRecipeOutput[]> List(ListRecipeInput input)
        {
            var list = context.Recipes
                .Select(x => new ListRecipeFilterModel
                {
                    CaloriesPer100 = x.CaloriesPer100,
                    CarbohydratesPer100 = x.CarbohydratesPer100,
                    Description = x.Description,
                    FatsPer100 = x.FatsPer100,
                    Name = x.Name,
                    IsModerated = x.IsModerated,
                    ProteinsPer100 = x.FatsPer100,
                    Weight = x.Weight,
                    RecipeId = x.RecipeId,
                    UserId = x.UserId,
                    Products = x.Ingridients.Select(x => new ProductListFilterModel
                    {
                        ProductId = x.ProductId,
                        ProductName = x.Product.Name,
                    })
                });

            list = await filtrator.Filter(list, input);

            return await list.Select(x => new ListRecipeOutput()
            {
                CaloriesPer100 = x.CaloriesPer100,
                CarbohydratesPer100 = x.CarbohydratesPer100,
                Description = x.Description,
                FatsPer100 = x.FatsPer100,
                Products = string.Join(", ", x.Products.Select(x => x.ProductName)),
                Name = x.Name,
                ProteinsPer100 = x.ProteinsPer100,
                RecipeId = x.RecipeId,
            }).ToArrayAsync();

        }

        private async Task<Recipe> CreateRecipeBody(CreateRecipeInput input, Guid recipeId)
        {
            Recipe recipe = new Recipe()
            {
                Description = input.Description,
                File = input.File != null ? new Dal.Entities.File()
                {
                    Content = input.File,
                    FileId = Guid.NewGuid(),
                    Name = recipeFileName,
                    Type = Dal.Enums.FileType.Photo,
                } : null,
                IsModerated = false,
                Name = input.Name,
                RecipeId = recipeId,
                UserId = input.UserId,
            };

            if (input.Weight.HasValue)
            {
                recipe.Weight = input.Weight.Value;

                if (input.ServingsNumber.HasValue)
                {
                    recipe.ServingsNumber = input.ServingsNumber.Value;
                }

                else
                {
                    recipe.ServingsNumber = (int)Math.Ceiling((double)recipe.Weight / (double)averageServingSize);
                }
            }

            else
            {
                recipe.Weight = input.Ingridients.Sum(x => x.Weight);

                if (input.ServingsNumber.HasValue)
                {
                    recipe.ServingsNumber = input.ServingsNumber.Value;
                }

                else
                {
                    recipe.ServingsNumber = recipe.Weight / averageServingSize;
                }
            }

            var listAllIngridients = await FillExistingIngridients(input.Ingridients);

            recipe.Calories = 0;
            recipe.Carbohydrates = 0;
            recipe.Proteins = 0;
            recipe.Fats = 0;

            foreach (var ingridient in listAllIngridients)
            {
                recipe.Fats += (int)Math.Round(ingridient.Product.Fats * ingridient.Weight / 100d);
                recipe.Proteins += (int)Math.Round(ingridient.Product.Proteins * ingridient.Weight / 100d);
                recipe.Carbohydrates += (int)Math.Round(ingridient.Product.Carbohydrates * ingridient.Weight / 100d);

                int calories = ingridient.Product.Calories ?? CalculateCalories(ingridient.Product.Proteins, ingridient.Product.Fats, ingridient.Product.Carbohydrates);
                recipe.Calories += (int)Math.Round(calories * ingridient.Weight / 100d);
            }

            return recipe;
        }

        private async Task<List<CreateRecipeInputIngridient>> FillExistingIngridients(IEnumerable<CreateRecipeInputIngridient> ingridients)
        {
            var productIds = ingridients.Where(x => x.ExistingProductId.HasValue).Select(x => x.ExistingProductId);
            
            var productsDictionary = await context.Products
                .Where(x => productIds.Contains(x.ProductId))
                .Select(x => new CreateRecipeInputIngridient()
                {
                    ExistingProductId = x.ProductId,
                    Product = new CreateRecipeInputProduct()
                    {
                        Calories = x.Calories,
                        Carbohydrates = x.Carbohydrates,
                        Fats = x.Fats,
                        Proteins = x.Proteins,
                    }
                })
                .ToDictionaryAsync(x => x.ExistingProductId!.Value, y => y.Product);

            var listResult = new List<CreateRecipeInputIngridient>(ingridients.Count());

            foreach(var ingridient in ingridients)
            {
                var newElement = new CreateRecipeInputIngridient();

                newElement.Weight = ingridient.Weight;

                if (ingridient.ExistingProductId.HasValue)
                {
                    newElement.Product = productsDictionary[ingridient.ExistingProductId!.Value];
                }

                else
                {
                    newElement.Product = ingridient.Product;
                }

                listResult.Add(newElement);
            }

            return listResult;
        }

        private List<Ingridient> CreateIngridients(IEnumerable<CreateRecipeInputIngridient> ingridients, Guid recipeId)
        {
            var listIngridients = new List<Ingridient>(ingridients.Count());

            listIngridients.AddRange(ingridients.Where(x => x.ExistingProductId.HasValue).Select(x => new Ingridient()
            {
                IngridientId = Guid.NewGuid(),
                ProductId = x.ExistingProductId!.Value,
                RecipeId = recipeId,
                Weight = x.Weight,
            }));

            foreach (var ingridient in ingridients.Where(x => !x.ExistingProductId.HasValue))
            {
                Guid productId = Guid.NewGuid();
                listIngridients.Add(new Ingridient()
                {
                    IngridientId = Guid.NewGuid(),
                    RecipeId = recipeId,
                    Weight = ingridient.Weight,
                    ProductId = productId,
                    Product = new Product()
                    {
                        ProductId = productId,
                        Calories = CalculateCalories(ingridient.Product.Proteins, ingridient.Product.Fats, ingridient.Product.Carbohydrates) ,
                        Carbohydrates = ingridient.Product.Carbohydrates,
                        Fats = ingridient.Product.Fats,
                        IsModerated = false,
                        Name = ingridient.Product.Name,
                        Proteins = ingridient.Product.Proteins,
                    }
                });
            }

            return listIngridients;
        }

        private List<Operation> CreateOperations(IEnumerable<CreateRecipeInputOperation> operations, Guid recipeId)
        {
            return operations.Select(x => new Operation()
            {
                Description = x.Description,
                File = new Dal.Entities.File()
                {
                    Content = x.File,
                    FileId = Guid.NewGuid(),
                    Name = operationFileName,
                    Type = Dal.Enums.FileType.Photo,
                },
                RecipeId = recipeId,
                Step = x.Step,
                TimeInSeconds = x.TimeInSeconds,
            }).ToList();
        }

        private int CalculateCalories(int proteins, int fats, int carbohydrates)
        {
            return proteins * 4 + carbohydrates * 4 + fats * 9;
        }
    }
}
