using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Knowlead.DomainModel.UserModels;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Knowlead.DomainModel.LookupModels.Core;
using Knowlead.DomainModel.LookupModels.Geo;
using Knowlead.DomainModel.CoreModels;

namespace Knowlead.DomainModel
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        private IConfigurationRoot _config;
        public ApplicationDbContext(IConfigurationRoot config, DbContextOptions options) : base(options) 
        {
            _config = config;
        }

        public DbSet<Achievement> Achievements {get; set; }
        public DbSet<City> Cities {get; set; }
        public DbSet<Country> Countries {get; set; }
        public DbSet<FOS> Fos {get; set; }
        public DbSet<Language> Languages {get; set; }
        public DbSet<Image> Images {get; set; }
        



        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            
            optionsBuilder.UseNpgsql(_config["ConnectionStrings:DefaultContextConnection"]);

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
             base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<CoreLookup>()
                .HasDiscriminator<string>("Category")
                .HasValue<Achievement>("Achievement")
                .HasValue<FOS>("Fos")
                .HasValue<Language>("Language");

            modelBuilder.Entity<GeoLookup>()
                .HasDiscriminator<string>("Category")
                .HasValue<City>("City")
                .HasValue<Country>("Country");

            /*** SceneTask ***/ 
            // modelBuilder.Entity<SceneTask>()
            //     .HasKey(st => new {st.SceneId, st.TaskId});

            // modelBuilder.Entity<SceneTask>()
            //     .HasOne(fh => fh.Scene)
            //     .WithMany(ft => ft.SceneTasks)
            //     .HasForeignKey(fh => fh.SceneId);

            // modelBuilder.Entity<SceneTask>()
            //     .HasOne(th => th.Task)
            //     .WithMany(fts => fts.SceneTasks)
            //     .HasForeignKey(th => th.TaskId);

        }
    }
}
