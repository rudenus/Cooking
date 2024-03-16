using Dal.EfExtensions;
using Dal.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dal.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public virtual void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Products");

            builder.HasKey(x => x.ProductId);

            builder.HasNotNegativeCheckConstraint(x => x.Calories);
            builder.HasNotNegativeCheckConstraint(x => x.Carbohydrates);
            builder.HasNotNegativeCheckConstraint(x => x.Fats);
            builder.HasNotNegativeCheckConstraint(x => x.Proteins);
        }
    }
}
