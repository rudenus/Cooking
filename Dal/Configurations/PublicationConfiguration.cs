using Dal.EfExtensions;
using Dal.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dal.Configurations
{
    public class PublicationConfiguration : IEntityTypeConfiguration<Publication>
    {
        public virtual void Configure(EntityTypeBuilder<Publication> builder)
        {
            builder.ToTable("Publications");

            builder.HasKey(x => x.PublicationId);

            builder.HasNotNegativeCheckConstraint(x => (int?)x.CommentsNumber);
            builder.HasNotNegativeCheckConstraint(x => (int?)x.LikesNumber);
        }
    }
}
