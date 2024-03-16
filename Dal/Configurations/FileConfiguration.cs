using Dal.EfExtensions;
using Dal.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Dal.Configurations
{
    public class FileConfiguration : IEntityTypeConfiguration<Entities.File>
    {
        public virtual void Configure(EntityTypeBuilder<Entities.File> builder)
        {
            builder.ToTable("Files");

            builder.HasKey(x => x.FileId);

            builder.Property(x => x.Type)
               .HasConversion<EnumToStringConverter<FileType>>();

            builder.HasEnumStringValuesCheckConstraint(x => x.Type, new EnumToStringConverter<FileType>());
        }
    }
}
