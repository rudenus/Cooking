using BusinessLogic.ProductLogic.Models.List;
using Dal;
using Microsoft.EntityFrameworkCore;
namespace BusinessLogic.ProductLogic
{
    public class ProductLogic
    {
        private readonly Context context;
        public ProductLogic (Context context)
        {
            this.context = context;
        }

        public async Task<ListProductOutput[]> List(bool onlyModerated)
        {
            if (onlyModerated)
            {

                var list = await context.Products
                    .Where(x => x.IsModerated == true)
                    .Where(x => x.IsTest != true)
                    .Select(x => new ListProductOutput()
                    {
                        ProductId = x.ProductId,
                        Calories = x.Calories,
                        Carbohydrates = x.Carbohydrates,
                        Fats = x.Fats,
                        Name = x.Name,
                        Proteins = x.Proteins,
                    })
                    .OrderBy(x => x.Name)
                    .ToArrayAsync();

                return list;
            }

            else
            {
                var list = await context.Products
                    .Select(x => new ListProductOutput()
                    {
                        ProductId = x.ProductId,
                        Calories = x.Calories,
                        Carbohydrates = x.Carbohydrates,
                        Fats = x.Fats,
                        Name = x.Name,
                        Proteins = x.Proteins,
                    })
                    .OrderBy(x => x.Name)
                    .ToArrayAsync();

                return list;
            }
        }
    }
}
