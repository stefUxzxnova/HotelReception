using ApplicationService.DTOs;
using ApplicationService.Implementations;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Messages;

namespace WebApplication1.Controllers
{
	[Route("api/[controller]")]
	public class UserController : Controller
	{
		#region Properties
		private readonly UserManagementService service = null;
		#endregion

		#region Constructors
		public UserController()
		{
			service = new UserManagementService();
		}
		#endregion

		[HttpGet]
		public IActionResult GetAll()
		{
			List<UserDTO> list = new List<UserDTO>();
			foreach (var item in service.Get().ToArray())
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
		public IActionResult Save(UserDTO userDTO)
		{
			if (!userDTO.Validate())
				return BadRequest(new ResponseMessage { Code = 500, Error = "Data is not valid!" });
			ResponseMessage response = new ResponseMessage();

			if (service.Save(userDTO))
			{
				response.Code = 200;
				response.Body = "User is saved.";
			}
			else
			{
				response.Code = 500;
				response.Body = "User is not saved.";
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
				response.Body = "User is deleted.";
			}
			else
			{
				response.Code = 500;
				response.Body = "User is not deleted.";
			}

			return Ok(response);
		}
	}
}
