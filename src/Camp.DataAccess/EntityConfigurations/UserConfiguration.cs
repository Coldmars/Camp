using Camp.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Camp.DataAccess.EntityConfigurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder
                .Property(s => s.Id)
                .IsRequired()
                .ValueGeneratedOnAdd();

            builder.Property(s => s.Name)
                .IsRequired();

            builder.Property(s => s.Login)
                .IsRequired();

            builder.Property(s => s.PasswordHash)
                .IsRequired();

            builder.Property(s => s.CreateDate)
                .IsRequired();

            builder.Property(s => s.ParentId);

            builder.Property(s => s.PhoneNumber);

            builder.Property(s => s.RefreshToken);

            builder.Property(s => s.RefreshTokenExpiryTime);

            builder
                .HasOne(p => p.Parent)
                .WithMany(c => c.Children)
                .HasForeignKey(c => c.Id);

            builder
                .HasOne(user => user.Role)
                .WithMany(role => role.Users)
                .HasForeignKey(user => user.RoleId);
        }
    }
}
