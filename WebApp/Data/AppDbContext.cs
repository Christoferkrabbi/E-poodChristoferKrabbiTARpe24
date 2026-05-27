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

            modelBuilder.Entity<PlayTable>().HasKey(p => p.Id);

            modelBuilder.Entity<PlayTable>().HasData(
                new PlayTable {Id=Guid.NewGuid(), TableName = "Corner Table", LocationStoreName = "North", TableDescription = "A cozy corner table" },
                new PlayTable {Id=Guid.NewGuid(), TableName = "Center Table", LocationStoreName = "Center", TableDescription = "A table in the center of the room" },
                new PlayTable {Id=Guid.NewGuid(), TableName = "VIP Table", LocationStoreName = "South", TableDescription = "A VIP table with exclusive access" }
            );
        }
    }
}
