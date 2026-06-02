using System;
using Microsoft.EntityFrameworkCore;
using WebApp.Entities;

namespace WebApp.Data
{
	public class AppDbContext : DbContext
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

			modelBuilder.Entity<PlayTable>(entity =>
			{
				entity.HasKey(t => t.Id);
			});

			modelBuilder.Entity<PlayTableBooking>(entity =>
			{
				entity.HasKey(b => b.BookingID);

				entity.HasOne(b => b.PlayTable)
					  .WithMany(t => t.Bookings)
					  .HasForeignKey(b => b.PlayTableID)
					  .OnDelete(DeleteBehavior.Cascade);
			});

			//admin login preste
            modelBuilder.Entity<UserAccount>().HasData(
				new UserAccount
				{
                    Id = 1,
					FirstName = "admin",
					LastName = "account",
                    UserName = "admin",
					Email = "admin@example.com",
					Password = "admin",
					Role = "Admin"
				}
			);
        }


		/*
		// 3. Korrektsed algandmed (Seed Data) testitavate Guididega
		modelBuilder.Entity<PlayTable>().HasData(
			new PlayTable
			{
				Id = Guid.Parse("11111111-1111-1111-1111-111111111111"),
				TableName = "Corner Table",
				LocationStoreName = "North",
				TableDescription = "A cozy corner table",
				CreatedAt = DateTime.Now,
				ModifiedAt = DateTime.Now,
				LastVisitAt = DateTime.Now
			},
			new PlayTable
			{
				Id = Guid.Parse("22222222-2222-2222-2222-222222222222"),
				TableName = "Center Table",
				LocationStoreName = "Center",
				TableDescription = "A table in the center of the room",
				CreatedAt = DateTime.Now,
				ModifiedAt = DateTime.Now,
				LastVisitAt = DateTime.Now
			},
			new PlayTable
			{
				Id = Guid.Parse("33333333-3333-3333-3333-333333333333"),
				TableName = "VIP Table",
				LocationStoreName = "South",
				TableDescription = "A VIP table with exclusive access",
				CreatedAt = DateTime.Now,
				ModifiedAt = DateTime.Now,
				LastVisitAt = DateTime.Now
			}	
		);
		*/
	}
	
}
