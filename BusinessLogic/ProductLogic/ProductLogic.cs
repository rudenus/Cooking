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

        public async Task<ListProductOutput[]> List()
        {
            var list = await context.Products
                .Where(x => x.IsModerated == true)
                .Select(x => new ListProductOutput()
                {
                    ProductId = x.ProductId,
                    Calories = x.Calories,
                    Carbohydrates = x.Carbohydrates,
                    Fats = x.Fats,
                    Name = x.Name,
                    Proteins = x.Proteins,
                })
                .ToArrayAsync();

            return list;
        }
    }
}
