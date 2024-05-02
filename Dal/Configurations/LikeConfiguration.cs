using Dal.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dal.Configurations
{
    public class LikeConfiguration : IEntityTypeConfiguration<Like>
    {
        public virtual void Configure(EntityTypeBuilder<Like> builder)
        {
            builder.ToTable("Likes");

            builder.HasKey(x => new { x.PublicationId, x.UserId });
        }
    }
}
