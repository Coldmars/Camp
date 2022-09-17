using Camp.DataAccess.Entities;
using Camp.DataAccess.EntityConfigurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Camp.DataAccess.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }

        public DbSet<Role> Roles { get; set; }

        public DbSet<Link> Links { get; set; }

        public DbSet<UserLink> UserLinks { get; set; }

        public DbSet<Image> Images { get; set; }

        public DbSet<Report> Reports { get; set; }

        public DbSet<Genre> Genres { get; set; }

        public DbSet<Entities.Type> Types { get; set; }

        public DbSet<ReportGenre> ReportGenres { get; set; }

        public static readonly ILoggerFactory ConsoleLoggerFactory =
            LoggerFactory.Create(builder =>
            {
                builder.AddConsole();
                builder.SetMinimumLevel(LogLevel.Information);
            });

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(UserConfiguration).Assembly);

            // Default values of Role
            modelBuilder
                .Entity<Role>()
                    .HasData(new Role
                    {
                        Id = ((int)Enums.Roles.Role.Curator),
                        Name = Enums.Roles.Role.Curator.ToString()
                    });

            modelBuilder
                .Entity<Role>()
                    .HasData(new Role
                    {
                        Id = ((int)Enums.Roles.Role.Squad),
                        Name = Enums.Roles.Role.Squad.ToString()
                    });

            modelBuilder
                .Entity<Role>()
                    .HasData(new Role
                    {
                        Id = ((int)Enums.Roles.Role.Volunteer),
                        Name = Enums.Roles.Role.Volunteer.ToString()
                    });

            // Default values of Genre
            modelBuilder
                .Entity<Genre>()
                    .HasData(new Genre
                    {
                        Id = ((int)Enums.GenresEnum.Genres.Roskomnadzor),
                        Name = Enums.GenresEnum.Genres.Roskomnadzor.ToString()
                    });

            modelBuilder
                .Entity<Genre>()
                    .HasData(new Genre
                    {
                        Id = ((int)Enums.GenresEnum.Genres.MVD),
                        Name = Enums.GenresEnum.Genres.MVD.ToString()
                    });

            modelBuilder
                .Entity<Genre>()
                    .HasData(new Genre
                    {
                        Id = ((int)Enums.GenresEnum.Genres.FSB),
                        Name = Enums.GenresEnum.Genres.FSB.ToString()
                    });

            modelBuilder
                .Entity<Genre>()
                    .HasData(new Genre
                    {
                        Id = ((int)Enums.GenresEnum.Genres.Prokuratura),
                        Name = Enums.GenresEnum.Genres.Prokuratura.ToString()
                    });

            modelBuilder
               .Entity<Genre>()
                   .HasData(new Genre
                   {
                       Id = ((int)Enums.GenresEnum.Genres.Rosmolodezh),
                       Name = Enums.GenresEnum.Genres.Rosmolodezh.ToString()
                   });

            // Deafult values of Types
            modelBuilder
               .Entity<Entities.Type>()
                   .HasData(new Entities.Type
                   {
                       Id = ((int)Enums.TypesEnum.Type.Extremism),
                       Name = Enums.TypesEnum.Type.Extremism.ToString()
                   });

            modelBuilder
               .Entity<Entities.Type>()
                   .HasData(new Entities.Type
                   {
                       Id = ((int)Enums.TypesEnum.Type.Violence),
                       Name = Enums.TypesEnum.Type.Violence.ToString()
                   });

            modelBuilder
               .Entity<Entities.Type>()
                   .HasData(new Entities.Type
                   {
                       Id = ((int)Enums.TypesEnum.Type.NarcoticDrugs),
                       Name = Enums.TypesEnum.Type.NarcoticDrugs.ToString()
                   });

            modelBuilder
               .Entity<Entities.Type>()
                   .HasData(new Entities.Type
                   {
                       Id = ((int)Enums.TypesEnum.Type.PersonalData),
                       Name = Enums.TypesEnum.Type.PersonalData.ToString()
                   });

            modelBuilder
               .Entity<Entities.Type>()
                   .HasData(new Entities.Type
                   {
                       Id = ((int)Enums.TypesEnum.Type.Recruitment),
                       Name = Enums.TypesEnum.Type.Recruitment.ToString()
                   });

            modelBuilder
               .Entity<Entities.Type>()
                   .HasData(new Entities.Type
                   {
                       Id = ((int)Enums.TypesEnum.Type.Columbine),
                       Name = Enums.TypesEnum.Type.Columbine.ToString()
                   });

            modelBuilder
               .Entity<Entities.Type>()
                   .HasData(new Entities.Type
                   {
                       Id = ((int)Enums.TypesEnum.Type.Criminal),
                       Name = Enums.TypesEnum.Type.Criminal.ToString()
                   });

            modelBuilder
               .Entity<Entities.Type>()
                   .HasData(new Entities.Type
                   {
                       Id = ((int)Enums.TypesEnum.Type.Antisocial),
                       Name = Enums.TypesEnum.Type.Antisocial.ToString()
                   });
        }
    }
}
