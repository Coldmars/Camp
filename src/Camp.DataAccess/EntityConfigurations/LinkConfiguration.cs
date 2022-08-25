using Camp.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Camp.DataAccess.EntityConfigurations
{
    public class LinkConfiguration : IEntityTypeConfiguration<Link>
    {
        public void Configure(EntityTypeBuilder<Link> builder)
        {
            builder
               .Property(l => l.Id)
               .IsRequired()
               .ValueGeneratedOnAdd();

            builder
                .Property(l => l.Url)
                .IsRequired();

            builder
                .Property(l => l.IsLock)
                .IsRequired();

            builder
               .Property(l => l.LockDate);
        }
    }
}
