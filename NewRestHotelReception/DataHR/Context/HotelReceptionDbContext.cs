using DataHR.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataHR.Context
{
	public class HotelReceptionDbContext : DbContext
	{
		public DbSet<User> Users { get; set; }
		public DbSet<Room> Rooms { get; set; }
		public DbSet<Client> Clients { get; set; }
		public DbSet<Booking> Bookings { get; set; }



		public object HttpContext { get; internal set; }

		public HotelReceptionDbContext()
		{
			this.Users = this.Set<User>();
			this.Rooms = this.Set<Room>();
			this.Clients = this.Set<Client>();
			this.Bookings = this.Set<Booking>();
		}




		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseSqlServer("Server=localhost;Database=HotelReceptionDB; Trusted_Connection=True;TrustServerCertificate=True;")
						 .UseLazyLoadingProxies();

		}
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<User>().HasData(
				new User()
				{
					ID = 1,
					Username = "stefibrat",
					Password = "stefipass",
					FirstName = "Stefani",
					LastName = "Uzunova",

				});
			;
		}
	}
}
