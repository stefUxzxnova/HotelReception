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
	public class UserManagementService
	{
		public List<UserDTO> Get()
		{
			List<UserDTO> usersDto = new List<UserDTO>();

			using (UnitOfWork unitOfWork = new UnitOfWork())
			{
				foreach (var item in unitOfWork.UserRepository.Get())
				{
					usersDto.Add(new UserDTO
					{
						ID = item.ID,
						FirstName = item.FirstName,
						LastName = item.LastName,
						Username = item.Username,
						Password = item.Password,
						
					}); ;
				}
			}

			return usersDto;
		}

		public UserDTO GetById(int id)
		{
			UserDTO userDTO = new UserDTO();

			using (UnitOfWork unitOfWork = new UnitOfWork())
			{
				User user = unitOfWork.UserRepository.GetByID(id);

				if (user != null)
				{
					userDTO.ID = user.ID;
					userDTO.FirstName = user.FirstName;
					userDTO.LastName = user.LastName;
					userDTO.Username = user.Username;
					userDTO.Password = user.Password;
				}

				return userDTO;
			}
		}
		public bool Save(UserDTO userDTO)
		{
			User user = new User
			{
				ID = userDTO.ID,
				FirstName = userDTO.FirstName,
				LastName = userDTO.LastName,
				Username = userDTO.Username,
				Password = userDTO.Password,
			};

			try
			{
				using (UnitOfWork unitOfWork = new UnitOfWork())
				{
					if (userDTO.ID == 0)
					{
						user.CreatedOn = DateTime.Now;
						unitOfWork.UserRepository.Insert(user);
					}
					else
					{
						user.UpdatedAt = DateTime.Now;
						unitOfWork.UserRepository.Update(user);
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
					unitOfWork.UserRepository.Delete(id);
					unitOfWork.Save();
				}
				return true;
			}
			catch
			{
				return false;
			}
		}

		public UserDTO Find(Func<User, bool> predicate)
		{
			UserDTO item = new UserDTO();	
			using (UnitOfWork unitOfWork = new UnitOfWork())
			{
				User user = unitOfWork.UserRepository.Find(predicate);
				if (user != null)
				{
					item.ID = user.ID;
					item.FirstName = user.FirstName;
					item.LastName = user.LastName;
					item.Username = user.Username;
					item.Password = user.Password;
				}

				return item;
			}
		}
	}
}
