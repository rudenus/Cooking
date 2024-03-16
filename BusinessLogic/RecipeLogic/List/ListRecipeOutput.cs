using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.RecipeLogic.List
{
    public class ListRecipeOutput
    {
        public int Calories { get; set; }

        public int Carbohydrates { get; set; }

        public string Description { get; set; }

        public int Fats { get; set; }

        public string Name { get; set; }

        public int Proteins { get; set; }

        public Guid RecipeId { get; set; }

        public string Products { get; set; }


    }
}
