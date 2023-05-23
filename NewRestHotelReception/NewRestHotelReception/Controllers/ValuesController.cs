using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace NewRestHotelReception.Controllers
{
	public class ValuesController : ApiController
	{
		#region Properties
		private readonly RoomManagementService service = null;
		#endregion

		#region Constructors
		public ClientsController()
		{
			service = new RoomManagementService();
		}
		#endregion

		[HttpGet]
		public IHttpActionResult GetAll()
		{
			List<RoomDTO> list = new List<RoomDTO>();
			foreach (var item in service.Get().ToArray())
			{

				list.Add(item);
			}
			return Json(list);
		}

		[HttpGet]
		public IHttpActionResult GetById(int id)
		{
			return Json(service.GetById(id));
		}

		//[HttpPost]
		//public IHttpActionResult Save(ClientDTO clientDTO)
		//{
		//	if (!clientDTO.Validate())
		//		return Json(new ResponseMessage { Code = 500, Error = "Data is not valid!" });
		//	ResponseMessage response = new ResponseMessage();

		//	if (service.Save(clientDTO))
		//	{
		//		response.Code = 200;
		//		response.Body = "Client is save.";
		//	}
		//	else
		//	{
		//		response.Code = 500;
		//		response.Body = "Client is not save.";
		//	}

		//	return Json(response);
		//}

		//[HttpDelete]
		//public IHttpActionResult Delete(int id)
		//{
		//	ResponseMessage response = new ResponseMessage();

		//	if (service.Delete(id))
		//	{
		//		response.Code = 200;
		//		response.Body = "Director is save.";
		//	}
		//	else
		//	{
		//		response.Code = 500;
		//		response.Body = "Director is not save.";
		//	}

		//	return Json(response);
		//}
	}
}
