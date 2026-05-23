using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using UNWE_LaegueApp.Models;

namespace UNWE_LaegueApp.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext(options)
    {
        public DbSet<League> Leagues { get; set; } = null!;
        public DbSet<Team> Teams { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<Season> Seasons { get; set; }
        public DbSet<JoinRequest> JoinRequests { get; set; }

        public DbSet<LogItem> log_22180023 { get; set; }
        public DbSet<UserProfile> UserProfiles { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=MITAKA\SQLEXPRESS;Database=22180023;Trusted_Connection=True;TrustServerCertificate=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("22180023");

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<League>().HasOne(l => l.SeasonOne).WithMany().HasForeignKey(l => l.SeasonOneId).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<League>().HasOne(l => l.SeasonTwo).WithMany().HasForeignKey(l => l.SeasonTwoId).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Team>().HasOne(t => t.Captain).WithMany().HasForeignKey(t => t.CaptainId).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Player>().HasOne(p => p.Team).WithMany(t => t.Players).HasForeignKey(p => p.TeamId).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Game>().HasOne(g => g.TeamOne).WithMany().HasForeignKey(g => g.TeamOneId).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Game>().HasOne(g => g.TeamTwo).WithMany().HasForeignKey(g => g.TeamTwoId).OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Team>().ToTable("Teams", tb => tb.HasTrigger("TRG_Teams_Audit"));
            modelBuilder.Entity<Player>().ToTable("Players", tb => tb.HasTrigger("TRG_Players_Audit"));
            modelBuilder.Entity<Game>().ToTable("Games", tb => tb.HasTrigger("TRG_Games_Audit"));
            modelBuilder.Entity<League>().ToTable("Leagues", tb => tb.HasTrigger("TRG_Leagues_Audit"));
            modelBuilder.Entity<Season>().ToTable("Seasons", tb => tb.HasTrigger("TRG_Seasons_Audit"));
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var entries = ChangeTracker.Entries<CommonEntity>()
                .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified);

            foreach (var entry in entries)
            {
                entry.Entity.ModifiedOn_22180023 = DateTime.UtcNow;
            }

            return base.SaveChangesAsync(cancellationToken);
        }
    }
}