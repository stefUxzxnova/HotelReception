using ApplicationService.DTOs;
using ApplicationService.Implementations;
using DataHR.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;
using System.Net;
using System.Web.Http.Filters;
using WebApplication1.Content.Models;
using WebApplication1.Messages;

namespace WebApplication1.Controllers
{
	[Route("/api/[controller]")]

	public class RoomController : ControllerBase
	{
		#region Properties
		private readonly RoomManagementService service = null;
		#endregion

		#region Constructors
		public RoomController()
		{
			service = new RoomManagementService();
		}
		#endregion

		//[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
		[HttpGet]
		public IActionResult Get(string orderBy = null, string roomTypeFilter = null, int roomNumberFilter = 0)
		{
			List<RoomDTO> list = new List<RoomDTO>();
			foreach (var item in service.Get(orderBy, roomTypeFilter, roomNumberFilter).ToArray())
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
		public IActionResult Save([FromBody] RoomDTO roomDTO)
		{
			if (!roomDTO.Validate())
				return BadRequest(new ResponseMessage { Code = 400, Error = "Data is not valid!" });
			ResponseMessage response = new ResponseMessage();

			if (service.Save(roomDTO))
			{
				response.Code = 200;
				response.Body = "Room is saved.";
				return Ok(response);
			}
			else
			{
				response.Code = 400;
				response.Body = "Room is not saved.";
				return BadRequest(response);
			}
		
		}
		[HttpPost]
		[Route("upload")]
		public IActionResult Upload()
		{
			var file = Request.Form.Files.FirstOrDefault();

			if (file != null && file.Length > 0)
			{
				string path = @"C:\Users\User\OneDrive\Desktop\pregovor\distributedSystems\NewRestHotelReception\WebSite\wwwroot\images\";

				if (this.Request.Form.Files.Count > 0 && this.Request.Form.Files[0].Length > 0)
				{
					Stream readStream = this.Request.Form.Files[0].OpenReadStream();
					FileStream writeStream = new FileStream(path + this.Request.Form.Files[0].FileName, FileMode.Create);

					byte[] buffer = new byte[1024];
					while (true)
					{
						int lenght = readStream.Read(buffer, 0, buffer.Length);

						if (lenght == 0)
						{
							break;
						}
						writeStream.Write(buffer, 0, lenght);
					}

					readStream.Close();
					writeStream.Close();
					return Ok(new ResponseMessage { Code = 200, Body = "File uploaded successfully." });
				}	
			}
			return BadRequest(new ResponseMessage { Code = 400, Body = "Failed to upload file." });
		}

		[HttpDelete("{id}")]
		public IActionResult Delete(int id)
		{
			ResponseMessage response = new ResponseMessage();

			if (service.Delete(id))
			{
				response.Code = 200;
				response.Body = "Room is deleted.";
			}
			else
			{
				response.Code = 500;
				response.Body = "Room is not deleted.";
			}

			return Ok(response);
		}
	}
}
