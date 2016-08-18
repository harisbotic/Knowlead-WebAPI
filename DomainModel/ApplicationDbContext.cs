using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Knowlead.DomainModel.TestModels;

namespace Knowlead.DomainModel
{
    public class ApplicationDbContext : DbContext
    {
        private IConfigurationRoot _config;
        public ApplicationDbContext(IConfigurationRoot config, DbContextOptions options) : base(options) 
        {
            _config = config;
        }

        public DbSet<Test> Tests {get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            
            optionsBuilder.UseNpgsql(_config["ConnectionStrings:DefaultContextConnection"]);

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
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
