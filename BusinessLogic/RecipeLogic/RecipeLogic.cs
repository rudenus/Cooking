using BusinessLogic.RecipeLogic.List;
using Dal;
using Microsoft.EntityFrameworkCore;

namespace BusinessLogic.RecipeLogic
{
    public class RecipeLogic
    {
        private readonly Context context;
        public RecipeLogic(Context context)
        {
            this.context = context;
        }

        public async Task<ListRecipeOutput[]> List(ListRecipeInput input)
        {
            var list = context.Recipes
                .Select(x => new ListFilterModel
                {
                    Calories = x.Calories,
                    Carbohydrates = x.Carbohydrates,
                    Description = x.Description,
                    Fats = x.Fats,
                    Name = x.Name,
                    Proteins = x.Proteins,
                    Weight = x.Weight,
                    RecipeId = x.RecipeId,
                    Products = x.Ingridients.Select(x => new ProductListFilterModel
                    {
                        ProductId = x.ProductId,
                        ProductName = x.Product.Name,
                    })
                });

            list = FilterList(list, input);

            list.Skip(input.PageNumber * input.PageSize);
            list.Take(input.PageSize);

            return list.Select(x => new ListRecipeOutput()
            {
                Calories = x.Calories,
                Carbohydrates = x.Carbohydrates,
                Description = x.Description,
                Fats = x.Fats,
                Products = string.Join(", ", x.Products.Select(x => x.ProductName)),
                Name = x.Name,
                Proteins = x.Proteins,
                RecipeId = x.RecipeId,
            }).ToArray();

        }

        private IQueryable<ListFilterModel> FilterList(IQueryable<ListFilterModel> list, ListRecipeInput input)
        {
            if (input.Products.Any())
            {
                list = list.Where(x => x.Products.All(y => input.Products.Any(z => z == y.ProductId)));
            }

            if (input.CaloriesMin.HasValue)
            {
                list = list.Where(x => x.Calories * 100 / x.Weight >= input.CaloriesMin.Value);
            }

            if (input.CaloriesMax.HasValue)
            {
                list = list.Where(x => x.Calories * 100 / x.Weight <= input.CaloriesMax.Value);
            }

            if (input.CarbohydratesMin.HasValue)
            {
                list = list.Where(x => x.Calories * 100 / x.Weight >= input.CarbohydratesMin.Value);
            }

            if (input.CarbohydratesMax.HasValue)
            {
                list = list.Where(x => x.Calories * 100 / x.Weight <= input.CarbohydratesMax.Value);
            }

            if (input.FatsMin.HasValue)
            {
                list = list.Where(x => x.Calories * 100 / x.Weight >= input.FatsMin.Value);
            }

            if (input.FatsMax.HasValue)
            {
                list = list.Where(x => x.Calories * 100 / x.Weight <= input.CaloriesMax.Value);
            }

            if (input.ProteinsMin.HasValue)
            {
                list = list.Where(x => x.Calories * 100 / x.Weight >= input.ProteinsMin.Value);
            }

            if (input.ProteinsMax.HasValue)
            {
                list = list.Where(x => x.Calories * 100 / x.Weight <= input.ProteinsMax.Value);
            }

            return list;
        }
    }
}
