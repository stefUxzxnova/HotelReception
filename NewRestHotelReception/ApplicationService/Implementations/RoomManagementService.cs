using ApplicationService.DTOs;
using DataHR.Entities;
using Repository.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.Implementations
{
	public class RoomManagementService
	{
		/*
		 repository.Get(
    filter: o => o.OrderDate >= startDate && o.OrderDate <= endDate,
    orderBy: q => q.OrderBy(o => o.OrderDate),
    includeProperties: "Customer, OrderItem");*/

		public List<RoomDTO> Get(string orderBy = "", string roomTypeFilter = "", int roomNumberFilter = 0,
        string includeProperties = ""
		)
		{
			List<RoomDTO> roomsDto = new List<RoomDTO>();
			Func<IQueryable<Room>, IOrderedQueryable<Room>> order = null;
			Expression<Func<Room, bool>> filter = null;
			if (roomTypeFilter == null)
			{
				filter = u =>
			  (roomNumberFilter == 0 || u.RoomNumber == roomNumberFilter);
			}
			else
			{
				filter = u =>
				  (string.IsNullOrEmpty(roomTypeFilter) || u.RoomType.Contains(roomTypeFilter)) &&
				  (roomNumberFilter == 0 || u.RoomNumber == roomNumberFilter);
			}
            if (orderBy != null && orderBy.Equals("priceAsc"))
			{
                order = rooms => rooms.OrderBy(room => room.PricePerDay);
            }
			if (orderBy != null && orderBy.Equals("priceDesc"))
			{
                order = rooms => rooms.OrderByDescending(room => room.PricePerDay);
            }
            if (orderBy != null && orderBy.Equals("roomNumberAsc"))
            {
                order = rooms => rooms.OrderBy(room => room.RoomNumber);
            }
            if (orderBy != null && orderBy.Equals("roomNumberDesc"))
            {
                order = rooms => rooms.OrderByDescending(room => room.RoomNumber);
            }


            using (UnitOfWork unitOfWork = new UnitOfWork())
			{
				CheckOccupancy();
				foreach (var item in unitOfWork.RoomRepository.Get(filter, order, includeProperties))
				{
					roomsDto.Add(new RoomDTO
					{
						ID = item.ID,
						RoomNumber = item.RoomNumber,
						RoomType = item.RoomType,
						RoomDescription = item.RoomDescription,
						PricePerDay = item.PricePerDay,
						MaxOccupancy = item.MaxOccupancy,
						Floor = item.Floor,
						isOccupied = item.isOccupied,
						ImageLink = item.ImageLink,
					}); ;
				}
			}

			return roomsDto;
		}

		public RoomDTO GetById(int id)
		{
			RoomDTO roomDTO = new RoomDTO();

			using (UnitOfWork unitOfWork = new UnitOfWork())
			{
				Room room = unitOfWork.RoomRepository.GetByID(id);

				if (room != null)
				{
					roomDTO.ID = room.ID;
					roomDTO.RoomNumber = room.RoomNumber;
					roomDTO.RoomType = room.RoomType;
					roomDTO.RoomDescription = room.RoomDescription;
					roomDTO.PricePerDay = room.PricePerDay;
					roomDTO.MaxOccupancy = room.MaxOccupancy;
					roomDTO.Floor = room.Floor;
					roomDTO.isOccupied = room.isOccupied;
					roomDTO.ImageLink = room.ImageLink;
				}

				return roomDTO;
			}
		}
		public bool Save(RoomDTO roomDTO)
		{
			Room room = new Room
			{
				ID = roomDTO.ID,
				RoomNumber = roomDTO.RoomNumber,
				RoomType = roomDTO.RoomType,
				RoomDescription = roomDTO.RoomDescription,
				PricePerDay = roomDTO.PricePerDay,
				MaxOccupancy = roomDTO.MaxOccupancy,
				Floor = roomDTO.Floor,
				isOccupied = roomDTO.isOccupied,
				ImageLink = roomDTO.ImageLink,
			};

			try
			{
				using (UnitOfWork unitOfWork = new UnitOfWork())
				{
					if (roomDTO.ID == 0)
					{
						room.isOccupied = false;
						room.CreatedOn = DateTime.Now;
						unitOfWork.RoomRepository.Insert(room);
					}
					else
					{
						room.UpdatedAt = DateTime.Now;
						unitOfWork.RoomRepository.Update(room);
					}

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
					unitOfWork.RoomRepository.Delete(id);
					unitOfWork.Save();
				}
				return true;
			}
			catch
			{
				return false;
			}
		}
		public void CheckOccupancy()
		{
			try
			{
				using (UnitOfWork unitOfWork = new UnitOfWork())
				{
					List<BookingDTO> bookingsDTOs = new List<BookingDTO>();
					foreach (var item in unitOfWork.BookingRepository.Get())
					{
						bookingsDTOs.Add(new BookingDTO
						{
							ID = item.ID,
							CheckInDate = item.CheckInDate,
							CheckOutDate = item.CheckOutDate,
							RoomID = item.RoomID,
							

						});
					}
					foreach (var item in bookingsDTOs)
					{
						if (item.CheckInDate <= DateTime.Now && item.CheckOutDate >= DateTime.Now)
						{
							Room room = unitOfWork.RoomRepository.GetByID(item.RoomID);
							room.isOccupied = true;
							unitOfWork.RoomRepository.Update(room);
						}
					}
					unitOfWork.Save();
				}
				
			}
			catch
			{
				throw new Exception();
			}
		}

	}
}
