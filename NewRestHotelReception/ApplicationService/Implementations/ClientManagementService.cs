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
	public class ClientManagementService
	{
		public List<ClientDTO> Get(string firstName = "", string lastName = "")
		{
			List<ClientDTO> clientsDTO = new List<ClientDTO>();
			Expression<Func<Client, bool>> filter = u =>
				  (string.IsNullOrEmpty(firstName) || u.FirstName.Contains(firstName)) &&
				 (string.IsNullOrEmpty(lastName) || u.LastName.Contains(lastName));
			

			using (UnitOfWork unitOfWork = new UnitOfWork())
			{
				foreach (var item in unitOfWork.ClientRepository.Get(filter))
				{
					clientsDTO.Add(new ClientDTO
					{
						ClientID = item.ID,
						FirstName = item.FirstName,
						LastName = item.LastName,
						Email = item.Email,
						City = item.City,
						Phone = item.Phone

					});
				}
			}

			return clientsDTO;
		}

		public ClientDTO GetById(int id)
		{
			ClientDTO clientDTO = new ClientDTO();

			using (UnitOfWork unitOfWork = new UnitOfWork())
			{
				Client client = unitOfWork.ClientRepository.GetByID(id);

				if (client != null)
				{
					clientDTO.ClientID = client.ID;
					clientDTO.FirstName = client.FirstName;
					clientDTO.LastName = client.LastName;
					clientDTO.Email = client.Email;
					clientDTO.Phone = client.Phone;
					clientDTO.City = client.City;

				}

				return clientDTO;
			}
		}
		public bool Save(ClientDTO clientDTO)
		{
			Client client = new Client
			{
				ID = clientDTO.ClientID,
				FirstName = clientDTO.FirstName,
				LastName = clientDTO.LastName,
				Email = clientDTO.Email,
				Phone = clientDTO.Phone,
				City = clientDTO.City
				
			};

			try
			{
				using (UnitOfWork unitOfWork = new UnitOfWork())
				{
					if (clientDTO.ClientID == 0)
						unitOfWork.ClientRepository.Insert(client);
					else
						unitOfWork.ClientRepository.Update(client);
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
					unitOfWork.ClientRepository.Delete(id);
					unitOfWork.Save();
				}
				return true;
			}
			catch
			{
				return false;
			}
		}
	}
}
