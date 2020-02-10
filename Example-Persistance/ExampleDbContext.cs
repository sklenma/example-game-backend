using System.Numerics;
using Example_Persistance.Model;
using Microsoft.EntityFrameworkCore;

namespace Example_Persistance
{
    public class ExampleDbContext : DbContext
    {

        public DbSet<Player> Players { get; set; }
        public DbSet<PlayerAward> PlayerAwards { get; set; }
        public DbSet<AwardArticle> AwardArticles { get; set; }
        public DbSet<PlayerArticle> PlayerArticles { get; set; }
        public DbSet<Storage> Storages { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            optionsBuilder.UseSqlite(@"DataSource=e:\mydatabase.db;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Player>().HasOne(x => x.Storage).WithOne(x => x.Player)
                .HasForeignKey<Storage>(x => x.PlayerId);
            modelBuilder.Entity<AwardArticle>().HasOne(x => x.PlayerAward).WithMany(x => x.AwardArticles);
            modelBuilder.Entity<PlayerAward>().HasOne(x => x.Player).WithMany().HasForeignKey(x => x.PlayerId);
            modelBuilder.Entity<Storage>().HasMany<PlayerArticle>(p => p.Items).WithOne(x => x.Storage);

        }
    }
}