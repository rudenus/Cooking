using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.RecipeLogic.List
{
    public class ListRecipeOutput
    {
        public int CaloriesPer100 { get; set; }

        public int CarbohydratesPer100 { get; set; }

        public string Description { get; set; }

        public int FatsPer100 { get; set; }

        public string Name { get; set; }

        public int ProteinsPer100 { get; set; }

        public Guid RecipeId { get; set; }

        public string Products { get; set; }


    }
}
