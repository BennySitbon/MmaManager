using System.Data.Entity;
using Domain.Models;

namespace Domain.DAL
{
    public class UfcContext : ApplicationDbContext
    {
        public DbSet<Fighter> Fighters { get; set; }
        public DbSet<FightListing> FightListings { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Ownership> Ownerships { get; set; }
        public DbSet<Event> Events { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<FightListing>()
                .HasRequired(c => c.BlueFighter)
                .WithMany()
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<FightListing>()
                .HasRequired(c => c.RedFighter)
                .WithMany()
                .WillCascadeOnDelete(false);
           // modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();

        }
    }
}