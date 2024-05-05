using BusinessLogic.RecipeLogic.Models.Create;
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

        public async Task Get()
        {

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
            
            var recipe = await GetRecipe(input, recipeId);

            context.Add(recipe);

            var listIngridients = GetIngridients(input.Ingridients, recipeId);
            context.AddRange(listIngridients);

            var listOperations = GetOperations(input.Operations, recipeId);
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
                    ProteinsPer100 = x.FatsPer100,
                    Weight = x.Weight,
                    RecipeId = x.RecipeId,
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

        private async Task<Recipe> GetRecipe(CreateRecipeInput input, Guid recipeId)
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

            var listAllProducts = await GetAllProducts(input.Ingridients);

            recipe.Calories = listAllProducts.Sum(x => x.Calories);
            recipe.Carbohydrates = listAllProducts.Sum(x => x.Carbohydrates);
            recipe.Fats = listAllProducts.Sum(x => x.Fats);
            recipe.Proteins = listAllProducts.Sum(x => x.Proteins);

            return recipe;
        }

        private async Task<List<CreateRecipeInputProduct>> GetAllProducts(IEnumerable<CreateRecipeInputIngridient> ingridients)
        {
            var listAllProducts = await context.Products
                .Where(x => ingridients.Select(x => x.ExistingProductId).Distinct().Contains(x.ProductId))
                .Select(x => new CreateRecipeInputProduct()
                {
                    Calories = x.Calories,
                    Carbohydrates = x.Carbohydrates,
                    Fats = x.Fats,
                    Proteins = x.Proteins,
                })
                .ToListAsync();

            listAllProducts.AddRange(ingridients.Where(x => !x.ExistingProductId.HasValue).Select(x => x.NewProduct));

            return listAllProducts;
        }

        private List<Ingridient> GetIngridients(IEnumerable<CreateRecipeInputIngridient> ingridients, Guid recipeId)
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
                        Calories = ingridient.NewProduct.Calories,
                        Carbohydrates = ingridient.NewProduct.Carbohydrates,
                        Fats = ingridient.NewProduct.Fats,
                        IsModerated = false,
                        Name = ingridient.NewProduct.Name,
                        Proteins = ingridient.NewProduct.Proteins,
                    }
                });
            }

            return listIngridients;
        }

        public List<Operation> GetOperations(IEnumerable<CreateRecipeInputOperation> operations, Guid recipeId)
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
    }
}
