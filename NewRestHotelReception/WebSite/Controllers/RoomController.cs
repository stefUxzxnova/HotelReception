using ApplicationService.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using WebSite.Models.Room;
using WebSite.Models.Shared;
using WebSite.OtherContent.DataUtil;
using WebSite.OtherContent.Token;
using static WebSite.OtherContent.Enums.Ordering;

namespace WebSite.Controllers
{
    [CustomTokenAuthorization]
    public class RoomController : Controller
	{
		private readonly Uri url = new Uri("https://localhost:7278/api/Room/");
		[HttpGet]
		public async Task<IActionResult> Index(string orderBy, RoomViewVM model)
		{	
			ViewBag.OrderBy = orderBy;
			model.Filter = model.Filter ?? new FilterVM();
			
			

            using (var client = new HttpClient())
			{
				client.BaseAddress = url;
				client.DefaultRequestHeaders.Accept.Clear();
				client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

				// Add the Authorization header with the AccessToken.
				//client.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);

				// make the request
				HttpResponseMessage response = await client.GetAsync("?orderBy=" + orderBy + "&roomTypeFilter=" 
					+ model.Filter.RoomType + "&roomNumberFilter=" + model.Filter.RoomNumber);

				// parse the response and return the data.
				string jsonString = await response.Content.ReadAsStringAsync();
                var responseData = JsonConvert.DeserializeObject<List<RoomVM>>(jsonString);


                if (response.IsSuccessStatusCode)
				{
                    RoomViewVM viewRoomVM = new RoomViewVM()
					{
						List = responseData,
						Filter = model.Filter,
						Orderby = orderBy,
					};
					return View(viewRoomVM);
				}
				return View();
			}
		}
		[HttpGet]
		public async Task<ActionResult> Details(int id)
		{
			//string accessToken = await GetAccessToken();

			using (var client = new HttpClient())
			{
				client.BaseAddress = url;
				client.DefaultRequestHeaders.Accept.Clear();
				client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

				// Add the Authorization header with the AccessToken.
				//client.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);

				// make the request
				HttpResponseMessage response = await client.GetAsync("" + id);

				// parse the response and return the data.
				string jsonString = await response.Content.ReadAsStringAsync();
				var responseData = JsonConvert.DeserializeObject<DetailSVM>(jsonString);
				return View(responseData);
			}
		}
		[HttpGet]
		public IActionResult Create()
		{
			return View();
		}

		[HttpPost]
		public async Task<ActionResult> Create(CreateVM createVM, IFormCollection formCollection)
		{
			
			var imageFile = formCollection.Files.FirstOrDefault();
			try
			{
				using (var client = new HttpClient())
				{
					client.BaseAddress = url;
					client.DefaultRequestHeaders.Accept.Clear();
					client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

					//check if there is 
					if (imageFile != null && imageFile.Length > 0)
					{
						var imageContent = new MultipartFormDataContent();
						var fileContent = new StreamContent(imageFile.OpenReadStream());

						// Add the file content to the multipart form-data content
						imageContent.Add(fileContent, "imageFile", imageFile.FileName);

						// Send the multipart form-data request to the RESTful service
						var ImageResponse = await client.PostAsync("https://localhost:7278/api/Room/upload", imageContent);
						if (ImageResponse.IsSuccessStatusCode)
						{
							createVM.ImageLink = imageFile.FileName;
						}
						else
						{
							createVM.ImageLink = "defaultPhoto.jpg";
						}
					}
					else
					{
						createVM.ImageLink = "defaultPhoto.jpg";
					}


					var content = JsonConvert.SerializeObject(createVM);
					var buffer = System.Text.Encoding.UTF8.GetBytes(content);
					var byteContent = new ByteArrayContent(buffer);
					byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

					// make the request
					HttpResponseMessage response = await client.PostAsync("", byteContent);

				}

				return RedirectToAction("Index");
			}
			catch
			{
				return View();
			}
		}

		// GET: Genres/Edit/5
		public async Task<ActionResult> Edit(int id)
		{
			

			using (var client = new HttpClient())
			{
				client.BaseAddress = url;
				client.DefaultRequestHeaders.Accept.Clear();
				client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

				// Add the Authorization header with the AccessToken.
				//client.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);

				// make the request
				HttpResponseMessage response = await client.GetAsync("" + id);

				// parse the response and return the data.
				string jsonString = await response.Content.ReadAsStringAsync();
				var responseData = JsonConvert.DeserializeObject<EditVM>(jsonString);
				return View(responseData);
			}
		}

		// POST: Genres/Edit/5
		[HttpPost]
		public async Task<ActionResult> Edit(EditVM editVM, IFormCollection collecton)
		{
            
            
   //         if (!ModelState.IsValid)
			//{
			//	return View(editVM);
			//}
            var imageFile = collecton.Files.FirstOrDefault();
            try
			{
				using (var client = new HttpClient())
				{
					client.BaseAddress = url;
					client.DefaultRequestHeaders.Accept.Clear();
					client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));


                    //check if there is 
                    if (imageFile != null && imageFile.Length > 0)
                    {
                        var imageContent = new MultipartFormDataContent();
                        var fileContent = new StreamContent(imageFile.OpenReadStream());

                        // Add the file content to the multipart form-data content
                        imageContent.Add(fileContent, "imageFile", imageFile.FileName);

                        // Send the multipart form-data request to the RESTful service
                        var ImageResponse = await client.PostAsync("https://localhost:7278/api/Room/upload", imageContent);
                        if (ImageResponse.IsSuccessStatusCode)
						{
                            editVM.ImageLink = imageFile.FileName;
                        }
                        
                    }
                    // Add the Authorization header with the AccessToken.
                    //client.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);

                    var content = JsonConvert.SerializeObject(editVM);
					var buffer = System.Text.Encoding.UTF8.GetBytes(content);
					var byteContent = new ByteArrayContent(buffer);
					byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

					// make the request
					HttpResponseMessage response = await client.PostAsync("", byteContent);

					// parse the response and return the data.
					//string jsonString = await response.Content.ReadAsStringAsync();
					//var responseData = JsonConvert.DeserializeObject<GenreVM>(jsonString);
					//return View(responseData);
				}

				return RedirectToAction("Index");
			}
			catch
			{
				return View();
			}
		}

		public async Task<ActionResult> Delete(int id)
		{
			//string accessToken = await GetAccessToken();

			try
			{
				using (var client = new HttpClient())
				{
					client.BaseAddress = url;
					client.DefaultRequestHeaders.Accept.Clear();
					client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

					// Add the Authorization header with the AccessToken.
					//client.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);

					// make the request
					HttpResponseMessage response = await client.DeleteAsync("" + id);

					return RedirectToAction("Index", "Room");
				}
			}
			catch
			{

				return View();
			}
		}


	}
}
