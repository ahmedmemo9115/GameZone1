using GameZone.Models;

namespace GameZone.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {   
        
        
        }

        public DbSet<Game> Games { get; set; }
        public DbSet<Category> Cateogries { get; set; }
        public DbSet<Device> Device { get; set; }
        public DbSet<GameDevice> GameDevices { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>()
                .HasData (
                new Category[]
                {

                    new Category {Id = 1 , Name = "Sports"},
                    new Category {Id = 2 , Name = "Action"},
                    new Category {Id = 3 , Name = "Adventure"},
                    new Category {Id = 4 , Name = "Fighting"},
                    new Category {Id = 5 , Name = "Racing"},
                    new Category {Id = 6 , Name = "Film"}
                });

            modelBuilder.Entity<Device>()
               .HasData(
               new Device[]
               {

                    new Device {Id = 1 , Name = "PC",Icon = "bi-PC"},
                    new Device {Id = 2 , Name = "PlayStation",Icon = "bi-Playstation"},
                    new Device {Id = 3 , Name = "xbox",Icon = "bi-xbox"},
                    new Device {Id = 4 , Name = "Nentendo",Icon = "bi-Nentendo"}
               });


            modelBuilder.Entity<GameDevice>()
            .HasKey(e => new { e.GameId , e.DeviceId});
            base.OnModelCreating(modelBuilder);
        }
    }
    }

