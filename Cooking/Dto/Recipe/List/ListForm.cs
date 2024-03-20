namespace Cooking.Dto.Recipe.List
{
    internal class ListForm
    {
        public IEnumerable<Guid> Products { get; set; } = new List<Guid>();

        public int? CaloriesMin { get; set; }

        public int? CaloriesMax { get; set; }

        public int? CarbohydratesMin { get; set; }
        
        public int? CarbohydratesMax { get; set; }

        public int? FatsMin { get; set; }

        public int? FatsMax { get; set; }

        public int? ProteinsMin { get; set; }

        public int? ProteinsMax { get; set; }

        public int? PageNumber { get; set; }
        public int? PageSize { get; set; }

    }
}
