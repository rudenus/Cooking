using Dal.EfExtensions;
using Dal.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dal.Configurations
{
    public class OperationConfiguration : IEntityTypeConfiguration<Operation>
    {
        public virtual void Configure(EntityTypeBuilder<Operation> builder)
        {
            builder.ToTable("Operations");

            builder.HasKey(x => new {x.RecipeId, x.Step});

            builder.HasIndex(x => x.RecipeId);

            builder.HasNotNegativeCheckConstraint(x => x.Step);

            builder.HasNotNegativeCheckConstraint(x => x.TimeInSeconds);
        }
    }
}
