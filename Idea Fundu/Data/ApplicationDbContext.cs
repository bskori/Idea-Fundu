using Idea_Fundu.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Idea_Fundu.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<ApplicationUser> Users { get; set; }
        public DbSet<Idea> Ideas { get; set; }
        public DbSet<Document> Documents { get; set; }
        public DbSet<Investment> Investments { get; set; }
        public DbSet<ProgressUpdate> ProgressUpdates { get; set; }
        public DbSet<Agreement> Agreements { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Investment>()
                .HasOne(i => i.Idea)
                .WithMany(i => i.Investments)
                .HasForeignKey(i => i.IdeaId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<Investment>()
                .HasOne(i => i.Investor)
                .WithMany(u => u.Investments)
                .HasForeignKey(i => i.InvestorId)
                .OnDelete(DeleteBehavior.NoAction);
        }



    }
}
