using Camp.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Camp.DataAccess.EntityConfigurations
{
    public class UserLinkConfiguration : IEntityTypeConfiguration<UserLink>
    {
        public void Configure(EntityTypeBuilder<UserLink> builder)
        {
            builder
              .Property(l => l.Id)
              .IsRequired()
              .ValueGeneratedOnAdd();

            builder
              .Property(l => l.CheckDate)
              .IsRequired();

            builder
              .Property(l => l.CheckedByRoleId)
              .IsRequired();

            builder
                .HasOne(ul => ul.User)
                .WithMany(u => u.UserLinks)
                .HasForeignKey(ul => ul.UserId);

            builder
                .HasOne(ul => ul.Link)
                .WithMany(u => u.UserLinks)
                .HasForeignKey(ul => ul.LinkId);
        }
    }
}
