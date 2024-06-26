﻿namespace BusinessLogic.RecipeLogic.Models.List
{
    internal class ListRecipeFilterModel
    {
        public int? CaloriesPer100 { get; set; }

        public int? CarbohydratesPer100 { get; set; }

        public string Description { get; set; }

        public int? FatsPer100 { get; set; }

        public bool IsModerated { get; set; }

        public bool? IsTest { get; set; }

        public string Name { get; set; }

        public int? ProteinsPer100 { get; set; }

        public Guid RecipeId { get; set; }

        public Guid UserId { get; set; }

        public int? Weight { get; set; }

        public IEnumerable<ProductListFilterModel> Products { get; set; } = Enumerable.Empty<ProductListFilterModel>();
    }

    internal class ProductListFilterModel
    {
        public Guid ProductId { get; set; }

        public string ProductName { get; set; }
    }
}
