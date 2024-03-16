using Dal.EfExtensions;
using Dal.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dal.Configurations
{
    public class RecipeConfiguration : IEntityTypeConfiguration<Recipe>
    {
        public virtual void Configure(EntityTypeBuilder<Recipe> builder)
        {
            builder.ToTable("Recipes");

            builder.HasKey(x => x.RecipeId);

            builder.HasNotNegativeCheckConstraint(x => x.Calories);
            builder.HasNotNegativeCheckConstraint(x => x.Carbohydrates);
            builder.HasNotNegativeCheckConstraint(x => x.Fats);
            builder.HasNotNegativeCheckConstraint(x => x.Proteins);
            builder.HasNotNegativeCheckConstraint(x => x.ServingsNumber);
            builder.HasNotNegativeCheckConstraint(x => x.Weight);
        }
    }
}
