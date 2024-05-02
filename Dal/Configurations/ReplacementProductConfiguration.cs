using Dal.EfExtensions;
using Dal.Entities;
using Dal.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Dal.Configurations
{
    public class ReplacementProductConfiguration : IEntityTypeConfiguration<ReplacementProduct>
    {
        public virtual void Configure(EntityTypeBuilder<ReplacementProduct> builder)
        {
            builder.ToTable("ReplacementProducts");

            builder.HasKey(x => new {x.ReplacingId, x.ReplacementId});

            builder.HasOne(x => x.Replacing)
                .WithMany()
                .HasForeignKey(x => x.ReplacingId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.Replacement)
                .WithMany()
                .HasForeignKey(x => x.ReplacementId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Property(x => x.ReplacementLevel)
               .HasConversion<EnumToStringConverter<ReplacementLevel>>();

            builder.HasEnumStringValuesCheckConstraint(x => x.ReplacementLevel, new EnumToStringConverter<ReplacementLevel>());
        }
    }
}
