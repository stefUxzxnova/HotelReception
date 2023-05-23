using ApplicationService.DTOs;
using ApplicationService.Implementations;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Content.Models;
using WebApplication1.Messages;

namespace WebApplication1.Controllers
{
	
	[Route("api/[controller]")]
	public class BookingController : ControllerBase
	{
		#region Properties
		private readonly BookingManagementService service = null;
		#endregion

		#region Constructors
		public BookingController()
		{
			service = new BookingManagementService();
		}
		#endregion

		[HttpGet]
		public IActionResult GetAll(PagerVM model = null)
		{
			List<BookingDTO> list = new List<BookingDTO>();
			foreach (var item in service.Get(model.ItemsPerPage, model.Page).ToArray())
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
		public IActionResult Save([FromBody]BookingDTO bookingDTO)
		{
			//if (!bookingDTO.Validate())
			//	return BadRequest(new ResponseMessage { Code = 500, Error = "Data is not valid!" });
			ResponseMessage response = new ResponseMessage();

			if (service.Save(bookingDTO))
			{
				response.Code = 200;
				response.Body = "Booking is saved.";
				
			}
			else
			{
				response.Code = 500;
				response.Body = "Booking is not saved.";
				
			}

			return Ok(response);
		}

		[HttpPost("checkRoomAvailability")]
		public IActionResult CheckRoomAvailability([FromBody] BookingDTO bookingDTO)
		{
			//if (!bookingDTO.Validate())
			//	return BadRequest(new ResponseMessage { Code = 500, Error = "Data is not valid!" });
			//Random random = new Random();
			//int p = 1;
			ResponseMessage response = new ResponseMessage();
			
			
			if (service.CheckAvailability(bookingDTO.RoomID, bookingDTO.CheckInDate, bookingDTO.CheckOutDate))
			{
				response.Code = 200;
				response.Body = "Free";

			}
			else
			{
				response.Code = 200;
				response.Body = "Not free";

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
				response.Body = "Booking is deleted.";
			}
			else
			{
				response.Code = 500;
				response.Body = "Booking is not deleted.";
			}

			return Ok(response);
		}
	}
}
