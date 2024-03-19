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

            list = FilterList(list, input);

            list.Skip(input.PageNumber * input.PageSize);
            list.Take(input.PageSize);

            return list.Select(x => new ListRecipeOutput()
            {
                CaloriesPer100 = x.CaloriesPer100,
                CarbohydratesPer100 = x.CarbohydratesPer100,
                Description = x.Description,
                FatsPer100 = x.FatsPer100,
                Products = string.Join(", ", x.Products.Select(x => x.ProductName)),
                Name = x.Name,
                ProteinsPer100 = x.ProteinsPer100,
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
                list = list.Where(x => x.CaloriesPer100 >= input.CaloriesMin.Value);
            }

            if (input.CaloriesMax.HasValue)
            {
                list = list.Where(x => x.CaloriesPer100 <= input.CaloriesMax.Value);
            }

            if (input.CarbohydratesMin.HasValue)
            {
                list = list.Where(x => x.CarbohydratesPer100 >= input.CarbohydratesMin.Value);
            }

            if (input.CarbohydratesMax.HasValue)
            {
                list = list.Where(x => x.CarbohydratesPer100 <= input.CarbohydratesMax.Value);
            }

            if (input.FatsMin.HasValue)
            {
                list = list.Where(x => x.FatsPer100 >= input.FatsMin.Value);
            }

            if (input.FatsMax.HasValue)
            {
                list = list.Where(x => x.FatsPer100 <= input.FatsMax.Value);
            }

            if (input.ProteinsMin.HasValue)
            {
                list = list.Where(x => x.ProteinsPer100 >= input.ProteinsMin.Value);
            }

            if (input.ProteinsMax.HasValue)
            {
                list = list.Where(x => x.ProteinsPer100 <= input.ProteinsMax.Value);
            }

            return list;
        }
    }
}
