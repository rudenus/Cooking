using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.RecipeLogic.Models.Get
{
    public class GetRecipeOutput
    {
        public string Description { get; set; }

        public string Name { get; set; }

        public int Calories { get; set; }

        public int Carbohydrates { get; set; }

        public int Fats { get; set; }

        public int? Weight { get; set; }

        public bool IsModerated { get; set; }

        public IEnumerable<GetRecipeOutputIngridient> Ingridients { get; set; }

        public IEnumerable<GetRecipeOutputOperation> Operations { get; set; }
    }

    public class GetRecipeOutputOperation
    {
        public int Step { get; set; }

        public byte[]? File { get; set; }

        public string Description { get; set; }
    }

    public class GetRecipeOutputIngridient
    {
        public int? Calories { get; set; }

        public int Carbohydrates { get; set; }

        public int Fats { get; set; }

        public string Name { get; set; }

        public int Proteins { get; set; }

        public int Weight { get; set; }
    }
}
