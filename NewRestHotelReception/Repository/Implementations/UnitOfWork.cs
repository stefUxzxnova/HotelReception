using DataHR.Context;
using DataHR.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Implementations
{
	public class UnitOfWork : IDisposable
	{
		private HotelReceptionDbContext context = new HotelReceptionDbContext();
		private GenericRepository<Room> roomRepository;
		private GenericRepository<Client> clientRepository;
		private GenericRepository<Booking> bookingRepository;
		private GenericRepository<User> userRepository;


		public GenericRepository<Room> RoomRepository
		{
			get
			{

				if (this.roomRepository == null)
				{
					this.roomRepository = new GenericRepository<Room>(context);
				}
				return roomRepository;
			}
		}
		public GenericRepository<Client> ClientRepository
		{
			get
			{

				if (this.clientRepository == null)
				{
					this.clientRepository = new GenericRepository<Client>(context);
				}
				return clientRepository;
			}
		}
		public GenericRepository<Booking> BookingRepository
		{
			get
			{

				if (this.bookingRepository == null)
				{
					this.bookingRepository = new GenericRepository<Booking>(context);
				}
				return bookingRepository;
			}
		}

		public GenericRepository<User> UserRepository
		{
			get
			{

				if (this.userRepository == null)
				{
					this.userRepository = new GenericRepository<User>(context);
				}
				return userRepository;
			}
		}

		public void Save()
		{
			context.SaveChanges();
		}

		private bool disposed = false;

		protected virtual void Dispose(bool disposing)
		{
			if (!this.disposed)
			{
				if (disposing)
				{
					context.Dispose();
				}
			}
			this.disposed = true;
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}
	}
}
