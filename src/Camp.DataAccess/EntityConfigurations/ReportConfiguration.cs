using Camp.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Camp.DataAccess.EntityConfigurations
{
    public class ReportConfiguration : IEntityTypeConfiguration<Report>
    {
        public void Configure(EntityTypeBuilder<Report> builder)
        {
            builder
                .Property(x => x.Id)
                .IsRequired()
                .ValueGeneratedOnAdd();

            builder
                .Property(x => x.Comment)
                .IsRequired(false);

            builder
                .Property(x => x.Url)
                .IsRequired();

            builder
                .Property(x => x.Time)
                .IsRequired();

            builder
                .HasOne(report => report.User)
                .WithMany(user => user.Reports)
                .HasForeignKey(report => report.UserId);

            builder
                .HasOne<Image>(report => report.Image)
                .WithOne(image => image.Report)
                .HasForeignKey<Report>(report => report.ImageId);

            builder
                .HasOne(report => report.Type)
                .WithMany(type => type.Reports)
                .HasForeignKey(report => report.TypeId);
        }
    }
}
