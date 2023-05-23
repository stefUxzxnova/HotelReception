using ApplicationService.DTOs;
using ApplicationService.Implementations;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Messages;

namespace WebApplication1.Controllers
{
	
	[Route("/api/[controller]")]
	public class ClientController : ControllerBase
	{
		#region Properties
		private readonly ClientManagementService service = null;
		#endregion

		#region Constructors
		public ClientController()
		{
			service = new ClientManagementService();
		}
		#endregion

		[HttpGet]
		public IActionResult GetAll(string firstName = "", string lastName = "")
		{
			List<ClientDTO> list = new List<ClientDTO>();
			foreach (var item in service.Get(firstName, lastName).ToArray())
			{
				list.Add(item);
			}
			return Ok(list);
		}

		[HttpGet("{id}")]
		public IActionResult GetById(int id)
		{
			return Ok(service.GetById(id));
		}

		[HttpPost]
		public IActionResult Save([FromBody] ClientDTO clientDTO)
		{
			if (!clientDTO.Validate())
				return BadRequest(new ResponseMessage { Code = 500, Error = "Data is not valid!" });
			ResponseMessage response = new ResponseMessage();

			if (service.Save(clientDTO))
			{
				response.Code = 200;
				response.Body = "Client is saved.";
			}
			else
			{
				response.Code = 500;
				response.Body = "Client is not saved.";
			}

			return Ok(response);
		}

		[HttpDelete("{id}")]
		public IActionResult Delete(int id)
		{
			ResponseMessage response = new ResponseMessage();

			if (service.Delete(id))
			{
				response.Code = 200;
				response.Body = "Client is deleted.";
			}
			else
			{
				response.Code = 500;
				response.Body = "Client is not deleted.";
			}

			return Ok(response);
		}
	}
}
