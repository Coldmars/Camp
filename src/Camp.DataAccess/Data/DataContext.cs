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
        }
    }
}
