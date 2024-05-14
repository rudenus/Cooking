using BusinessLogic.RecipeLogic.Enums;

namespace BusinessLogic.RecipeLogic.Models.List
{
    public class ListRecipeInput
    {
        public IEnumerable<Guid> Products { get; set; }

        public ReplacementLevel? ReplacementLevel { get; set; }

        public Guid? UserId { get; set; }

        public int? CaloriesMin { get; set; }

        public int? CaloriesMax { get; set; }

        public int? CarbohydratesMin { get; set; }

        public int? CarbohydratesMax { get; set; }

        public int? FatsMin { get; set; }

        public int? FatsMax { get; set; }

        public bool? OnlyUnModerated { get; set; }

        public bool? OnlyTheirOwn { get; set; }

        public int? ProteinsMin { get; set; }

        public int? ProteinsMax { get; set; }

        public int PageNumber { get; set; }

        public int PageSize { get; set; }
    }
}
