using Dal.EfExtensions;
using Dal.Entities;
using Dal.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Dal.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public virtual void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");

            builder.HasKey(x => x.UserId);

            builder.HasIndex(x => x.Login)
                .IncludeProperties(x => new {x.UserId, x.PasswordHash})
                .IsUnique();

            builder.HasIndex(x => x.Email)
                .IsUnique();

            builder.HasIndex(x => x.Phone)
                .IsUnique();

            builder.Property(x => x.Role)
               .HasConversion<EnumToStringConverter<Role>>();

            builder.HasEnumStringValuesCheckConstraint(x => x.Role, new EnumToStringConverter<Role>());
        }
    }
}
