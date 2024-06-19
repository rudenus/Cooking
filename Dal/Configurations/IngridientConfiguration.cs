using Dal.EfExtensions;
using Dal.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dal.Configurations
{
    public class IngridientConfiguration : IEntityTypeConfiguration<Ingridient>
    {
        public virtual void Configure(EntityTypeBuilder<Ingridient> builder)
        {
            builder.ToTable("Ingridients");

            builder.HasKey(x => x.IngridientId);

            builder.HasNotNegativeCheckConstraint(x => (int?)x.Weight);
        }
    }
}
