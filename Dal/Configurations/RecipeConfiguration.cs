using Dal.EfExtensions;
using Dal.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace Dal.Configurations
{
    public class RecipeConfiguration : IEntityTypeConfiguration<Recipe>
    {
        public virtual void Configure(EntityTypeBuilder<Recipe> builder)
        {
            builder.ToTable("Recipes");

            builder.HasKey(x => x.RecipeId);

            builder.Property(p => p.CaloriesPer100)
              .HasComputedColumnSql("\"Calories\" * 100 /\"Weight\"", stored: true);

            builder.Property(p => p.CarbohydratesPer100)
              .HasComputedColumnSql("\"Carbohydrates\" * 100 /\"Weight\"", stored: true);

            builder.Property(p => p.FatsPer100)
              .HasComputedColumnSql("\"Fats\" * 100 /\"Weight\"", stored: true);

            builder.Property(p => p.ProteinsPer100)
              .HasComputedColumnSql("\"Proteins\" * 100 /\"Weight\"", stored: true);

            builder.HasNotNegativeCheckConstraint(x => x.Calories);
            builder.HasNotNegativeCheckConstraint(x => x.Carbohydrates);
            builder.HasNotNegativeCheckConstraint(x => x.Fats);
            builder.HasNotNegativeCheckConstraint(x => x.Proteins);
            builder.HasNotNegativeCheckConstraint(x => x.ServingsNumber);
            builder.HasNotNegativeCheckConstraint(x => x.Weight);
        }
    }
}
