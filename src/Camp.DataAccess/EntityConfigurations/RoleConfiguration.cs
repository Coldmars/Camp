using Camp.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Camp.DataAccess.EntityConfigurations
{
    public class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder
                .Property(r => r.Id)
                .IsRequired();

            builder
                .Property(r => r.Name)
                .IsRequired();
        }
    }
}
