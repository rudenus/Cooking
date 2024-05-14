using Dal;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.ModeratorLogic
{
    public class ModeratorLogic
    {
        private Context context;
        public ModeratorLogic(Context context) 
        {
            this.context = context;
        }

        public async Task Approve(Guid recipeId)
        {
            var recipe = await context.Recipes
                .Include(x => x.Ingridients)
                .ThenInclude(x => x.Product)
                .FirstOrDefaultAsync(x => x.RecipeId == recipeId);

            if(recipe is null)
            {
                throw new ArgumentException($"Рецепт не найден {recipeId}");
            }

            recipe.IsModerated = true;

            foreach (var ingridient in recipe.Ingridients)
            {
                ingridient.Product.IsModerated = true;
            }

            await context.SaveChangesAsync();
        }
    }
}
