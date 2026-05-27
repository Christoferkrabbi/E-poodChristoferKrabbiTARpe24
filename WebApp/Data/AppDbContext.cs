using Microsoft.EntityFrameworkCore;
using WebApp.Entities;

namespace WebApp.Data
{
    public class AppDbContext :DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            
        }

		public DbSet<PlayTableBooking> PlayTableBookings { get; set; }

		public DbSet<UserAccount> UserAccounts { get; set; }

		public DbSet<PlayTable> PlayTables { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<PlayTable>().HasKey(p => p.TableID);

            modelBuilder.Entity<PlayTable>().HasData(
                new PlayTable { TableID = "T1", Name = "Corner Table", Capacity = 4, Location = "North" },
                new PlayTable { TableID = "T2", Name = "Center Table", Capacity = 6, Location = "Center" },
                new PlayTable { TableID = "T3", Name = "VIP Table", Capacity = 2, Location = "South" }
            );
        }
    }
}
