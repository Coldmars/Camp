using Camp.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Camp.DataAccess.EntityConfigurations
{
    public class ImageConfiguration : IEntityTypeConfiguration<Image>
    {
        public void Configure(EntityTypeBuilder<Image> builder)
        {
            builder
                .Property(image => image.Id)
                .IsRequired()
                .ValueGeneratedOnAdd();

            builder
                .Property(image => image.Url)
                .IsRequired();
        }
    }
}
