using BusinessLogic.RecipeLogic.Models.List;
using Dal;
using Microsoft.EntityFrameworkCore;

namespace BusinessLogic.RecipeLogic
{
    internal class Filtrator
    {

        private readonly Context context;

        internal Filtrator(Context context)
        {
            this.context = context;
        }

        public async Task<IQueryable<ListRecipeFilterModel>> Filter(IQueryable<ListRecipeFilterModel> list, ListRecipeInput input)
        {
            list = BaseFiltration(list, input);

            list = await FiltrationWithReplacement(list, input);

            list = Pagination(list, input);

            return list;
        }

        private IQueryable<ListRecipeFilterModel> BaseFiltration(IQueryable<ListRecipeFilterModel> list, ListRecipeInput input)
        {

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

        private async Task<IQueryable<ListRecipeFilterModel>> FiltrationWithReplacement(IQueryable<ListRecipeFilterModel> list, ListRecipeInput input)
        {

            if (!input.Products.Any())
            {
                return list;
            }

            if (input.ReplacementLevel == null)
            {
                return list.Where(x => x.Products.Any(y => input.Products.Any(z => z == y.ProductId)));
            }

            var listReplacementProducts = context.ReplacementProducts
                .Where(x => input.Products.Contains(x.ReplacementId))
                .Select(x => new
                {
                    x.ReplacingId,
                    x.ReplacementLevel,
                });

            //криво
            if (input.ReplacementLevel.Value == Enums.ReplacementLevel.Medium)
            {
                listReplacementProducts = listReplacementProducts
                    .Where(x => x.ReplacementLevel == Dal.Enums.ReplacementLevel.Medium
                    || x.ReplacementLevel == Dal.Enums.ReplacementLevel.Low);
            }

            else if (input.ReplacementLevel.Value == Enums.ReplacementLevel.Low)
            {
                listReplacementProducts = listReplacementProducts
                    .Where(x => x.ReplacementLevel == Dal.Enums.ReplacementLevel.Low);
            }

            var listProductsForSearch = (await listReplacementProducts.Select(x => x.ReplacingId).ToArrayAsync())
                .Union(input.Products);

            list = list.Where(x => x.Products.Any(y => listProductsForSearch.Any(z => z == y.ProductId)));

            return list;
        }

        private IQueryable<ListRecipeFilterModel> Pagination(IQueryable<ListRecipeFilterModel> list, ListRecipeInput input)
        {
            list.Skip(input.PageNumber * input.PageSize);
            list.Take(input.PageSize);

            return list;
        }
    }
}
