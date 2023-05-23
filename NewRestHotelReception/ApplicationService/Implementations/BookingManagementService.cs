using ApplicationService.DTOs;
using DataHR.Entities;
using Microsoft.IdentityModel.Tokens;
using Repository.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.Implementations
{
	public class BookingManagementService
	{
		public List<BookingDTO> Get(int itemsPerPage = 0, int page = 0)
		{
			List<BookingDTO> bookingsDTO = new List<BookingDTO>();

			using (UnitOfWork unitOfWork = new UnitOfWork())
			{
				foreach (var item in unitOfWork.BookingRepository.Get(null, null, "", itemsPerPage, page))
				{
					bookingsDTO.Add(new BookingDTO
					{
						ID = item.ID,
						CheckInDate = item.CheckInDate,
						CheckOutDate = item.CheckOutDate,
						TotalCost = item.TotalCost,
						PaymentStatus = item.PaymentStatus,
						Client = new ClientDTO()
						{
							ClientID = item.ClientID,
							FirstName = item.Client.FirstName,
							LastName = item.Client.LastName,
							Email = item.Client.Email,
							City = item.Client.City,
							Phone = item.Client.Phone

						},
						Room = new RoomDTO()
						{
							ID = item.RoomID,
							RoomNumber = item.Room.RoomNumber,
							RoomType = item.Room.RoomType,
							RoomDescription = item.Room.RoomDescription,
							MaxOccupancy = item.Room.MaxOccupancy,
							Floor = item.Room.Floor,
							PricePerDay = item.Room.PricePerDay

						}

					});
				}
			}

			return bookingsDTO;
		}

		public BookingDTO GetById(int id)
		{
			BookingDTO bookingDTO = new BookingDTO();

			using (UnitOfWork unitOfWork = new UnitOfWork())
			{
				Booking booking = unitOfWork.BookingRepository.GetByID(id);

				if (booking != null)
				{
					bookingDTO.ID = booking.ID;
					bookingDTO.CheckInDate = booking.CheckInDate;
					bookingDTO.CheckOutDate = booking.CheckOutDate;
					bookingDTO.PaymentStatus = booking.PaymentStatus;
					bookingDTO.TotalCost = booking.TotalCost;
					bookingDTO.ClientID = booking.ClientID;
					bookingDTO.RoomID = booking.RoomID;
					bookingDTO.Client = new ClientDTO()
					{
						ClientID = booking.ClientID,
						FirstName = booking.Client.FirstName,
						LastName = booking.Client.LastName,
						Email = booking.Client.Email,
						City = booking.Client.City,
						Phone = booking.Client.Phone

					};
					bookingDTO.Room = new RoomDTO()
					{
						ID = booking.RoomID,
						RoomNumber = booking.Room.RoomNumber,
						RoomType = booking.Room.RoomType,
						RoomDescription = booking.Room.RoomDescription,
						MaxOccupancy = booking.Room.MaxOccupancy,
						Floor = booking.Room.Floor,
						PricePerDay = booking.Room.PricePerDay

					};


                }

				return bookingDTO;
			}
		}
		public bool Save(BookingDTO bookingDTO)
		{
			//calculate the days of the booking
			int calculateDays(DateTime checkIn, DateTime checkOut)
			{
				TimeSpan duration = checkOut - checkIn;
				int days = duration.Days;
				return days;
			}
			Booking booking = new Booking();
			if (bookingDTO!=null)
			{
				booking.ID = bookingDTO.ID;
				booking.CheckInDate = bookingDTO.CheckInDate;
				booking.CheckOutDate = bookingDTO.CheckOutDate;
				booking.ClientID = bookingDTO.ClientID;
				booking.RoomID = bookingDTO.RoomID;
				booking.PaymentStatus = bookingDTO.PaymentStatus;
				booking.TotalCost = bookingDTO.PricePerDay * calculateDays((DateTime)bookingDTO.CheckInDate, (DateTime)bookingDTO.CheckOutDate);
			
			}
				

			try
			{
				using (UnitOfWork unitOfWork = new UnitOfWork())
				{
					if (bookingDTO.ID == 0)
						unitOfWork.BookingRepository.Insert(booking);
					else
						unitOfWork.BookingRepository.Update(booking);
					
					unitOfWork.Save();
				}
                
                return true;
			}
			catch
			{
				return false;
			}
		}
		public bool Delete(int id)
		{
			try
			{
				using (UnitOfWork unitOfWork = new UnitOfWork())
				{
					unitOfWork.BookingRepository.Delete(id);
					unitOfWork.Save();
				}
				return true;
			}
			catch
			{
				return false;
			}
		}

		public bool CheckAvailability(int id, DateTime? checkInDate, DateTime? checkOutDate) 
		{
			List<BookingDTO> bookingsDTOs = new List<BookingDTO>();

			using (UnitOfWork unitOfWork = new UnitOfWork())
			{
				foreach (var item in unitOfWork.BookingRepository.Get(filter: m => m.RoomID == id))
				{
					bookingsDTOs.Add(new BookingDTO
					{
						ID = item.ID,
						CheckInDate = item.CheckInDate,
						CheckOutDate = item.CheckOutDate,
						Room = new RoomDTO()
						{
							ID = item.RoomID
						}

					});
				}
			}
			if (!bookingsDTOs.IsNullOrEmpty())
			{
				bool isAvailable = bookingsDTOs.Any(booking => (checkInDate <= booking.CheckInDate && checkOutDate >= booking.CheckInDate && checkOutDate <= booking.CheckOutDate) 
																|| (checkInDate >= booking.CheckInDate && checkOutDate <= booking.CheckOutDate)
																|| ((checkInDate >= booking.CheckInDate && checkInDate < booking.CheckOutDate) && checkOutDate >= booking.CheckOutDate));

				if (isAvailable)
				{
					return false;
				}
				
			}
			return true;

		}

	}
}
