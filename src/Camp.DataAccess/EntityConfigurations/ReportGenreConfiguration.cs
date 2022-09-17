using Camp.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Camp.DataAccess.EntityConfigurations
{
    public class ReportGenreConfiguration : IEntityTypeConfiguration<ReportGenre>
    {
        public void Configure(EntityTypeBuilder<ReportGenre> builder)
        {
            builder
                .Property(x => x.Id)
                .IsRequired()
                .ValueGeneratedOnAdd();

            builder
                .HasOne(rg => rg.Report)
                .WithMany(report => report.ReportGenres)
                .HasForeignKey(rg => rg.ReportId);

            builder
                .HasOne(rg => rg.Genre)
                .WithMany(genre => genre.ReportGenres)
                .HasForeignKey(rg => rg.GenreId);
        }
    }
}
