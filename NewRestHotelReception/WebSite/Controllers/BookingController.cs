using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http.Headers;
using System.Text;
using WebSite.Models.Booking;
using WebSite.Models.Room;
using WebSite.Models.Client;
using WebSite.OtherContent.Token;
using WebSite.Models.Shared;
using static WebSite.OtherContent.Enums.Ordering;
using WebSite.OtherContent.DataUtil;

namespace WebSite.Controllers
{
    [CustomTokenAuthorization]
    public class BookingController : Controller
	{
        private readonly Uri url = new Uri("https://localhost:7278/api/Booking/");
        public async Task<IActionResult> Index(BookingViewVM model)
        {
            model.Pager = model.Pager ?? new PagerVM();
            model.Pager.Page = model.Pager.Page <= 0
                                   ? 1
                                   : model.Pager.Page;

            model.Pager.ItemsPerPage = model.Pager.ItemsPerPage <= 0
                                        ? 10
                                        : model.Pager.ItemsPerPage;


            using (var client = new HttpClient())
            {
                client.BaseAddress = url;
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                // Add the Authorization header with the AccessToken.
                //client.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);

                // make the request
                HttpResponseMessage response = await client.GetAsync("?itemsPerPage="
                    + model.Pager.ItemsPerPage + "&page=" + model.Pager.Page);

                // parse the response and return the data.
                string jsonString = await response.Content.ReadAsStringAsync();
                var responseData = JsonConvert.DeserializeObject<List<BookingVM>>(jsonString);

                if (response.IsSuccessStatusCode)
                {
                    DataUtil data = new DataUtil();
                    model.Pager.PageCount = (int)Math.Ceiling(await data.GetItemsNumber(url) / (double)model.Pager.ItemsPerPage);
                    BookingViewVM viewRoomVM = new BookingViewVM()
                    {
                        List = responseData,
                        Pager = model.Pager,
                        
                    };
                    return View(viewRoomVM);
                }
                return View();
            }
        }

        public async Task<ActionResult> Details(int id)
        {
            //string accessToken = await GetAccessToken();

            using (var client = new HttpClient())
            {
                client.BaseAddress = url;
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                // make the request
                HttpResponseMessage response = await client.GetAsync("" + id);

                // parse the response and return the data.
               
                if (response.IsSuccessStatusCode)
                {
					var body = response.Content.ReadAsStringAsync().Result;
					JObject data = JObject.Parse(body);

					//we take child nodes of "locales"
					JToken bookingRoom = data.GetValue("room");
					JToken bookingClient = data.GetValue("client");

					BookingVM model = JsonConvert.DeserializeObject<BookingVM>(body);
                    model.RoomVM = bookingRoom.ToObject<RoomVM>(); 
                    model.ClientVM = bookingClient.ToObject<ClientVM>();
					return View(model);
                }

                return View();
            }
        }

        public static List<RoomVM> list = new List<RoomVM>();
		public static Dictionary<int, decimal> roomsPrice = new Dictionary<int, decimal>();
		[HttpGet]
        public IActionResult Create(CheckAvailabilityVM model)
        {
            BookingVM createModel = new BookingVM()
            {
                RoomID = model.RoomID,
                ClientID = model.ClientID,
                CheckInDate = model.CheckInDate,
                CheckOutDate = model.CheckOutDate,
                
            };
            return View(createModel);
		}
		

        [HttpPost]
        public async Task<ActionResult> Create(BookingVM model)
        {
            
			var excludedFields = new string[] { nameof(BookingVM.roomVMs), nameof(BookingVM.TotalCost), nameof(BookingVM.PricePerDay), 
                nameof(BookingVM.ClientVM), nameof(BookingVM.RoomVM) };

			// Remove the excluded fields from the model state
			foreach (var field in excludedFields)
			{
				ModelState.Remove(field);
			}
			if (!ModelState.IsValid)
            {
                return View(model);
            }
            if (roomsPrice.ContainsKey(model.RoomID))
            {
                model.PricePerDay = roomsPrice[model.RoomID];
            }
            using (var client = new HttpClient())
            {
                client.BaseAddress = url;
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
				//if the dictionary already constains the choosen roomid
				
                var content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
                // make the request
                HttpResponseMessage response = await client.PostAsync("", content);
            }

            return RedirectToAction("Index");

        }

        [HttpGet]
        public async Task<IActionResult> CheckRoomAvailability(int clientId)
        {

			CheckAvailabilityVM model = new CheckAvailabilityVM();
			using (var client = new HttpClient())
			{
                client.BaseAddress = new Uri("https://localhost:7278/api/Room/");
				client.DefaultRequestHeaders.Accept.Clear();
				client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

				// Add the Authorization header with the AccessToken.
				//client.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);

				// make the request
				HttpResponseMessage response = await client.GetAsync("");

				// parse the response and return the data.
				string jsonString = await response.Content.ReadAsStringAsync();
				var responseData = JsonConvert.DeserializeObject<List<RoomVM>>(jsonString);
				if (responseData != null)
				{
					list.Clear();
					roomsPrice.Clear();
					foreach (var item in responseData)
					{
						roomsPrice.Add(item.ID, item.PricePerDay);
					}
					list = responseData;
					model.roomVMs = responseData;
					return View(model);
				}
				else
				{
					return View();
				}
			}
		}

        [HttpPost]
		public async Task<ActionResult> CheckRoomAvailability(CheckAvailabilityVM model)
		{
			model.roomVMs = list;
			if (!model.IsValidBooking())
			{
				ViewBag.InvalidData = "Enter valid data!";
				return View(model);

			}
			var excludedFields = new string[] { nameof(BookingVM.roomVMs)};

			// Remove the excluded fields from the model state
			foreach (var field in excludedFields)
			{
				ModelState.Remove(field);
			}
           
			if (!ModelState.IsValid)
			{
				ViewBag.InvalidData = "Enter valid data!";
				return View(model);
			}
			using (var client = new HttpClient())
			{
				client.BaseAddress = new Uri("https://localhost:7278/api/Booking/checkRoomAvailability");
				client.DefaultRequestHeaders.Accept.Clear();
				client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
               
				var content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");

				// make the request
				HttpResponseMessage response = await client.PostAsync("", content);
				string jsonString = await response.Content.ReadAsStringAsync();
                var jsonObject = JsonConvert.DeserializeObject<dynamic>(jsonString);
                string p = (string)jsonObject.body;
                
                if (response.IsSuccessStatusCode && p.Equals("Free"))
                {
                    return RedirectToAction("Create", "Booking", model);
                }
                ViewBag.Response = "The room is not available for this period";
                return View(model);
            }

		}

        // GET: Client/Edit/5
        [HttpGet]
		public async Task<ActionResult> Edit(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = url;
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

               
                // make the request
                HttpResponseMessage response = await client.GetAsync("" + id);

                // parse the response and return the data.
                string jsonString = await response.Content.ReadAsStringAsync();
                var responseData = JsonConvert.DeserializeObject<BookingVM>(jsonString);
                return View(responseData);
            }
        }

        // POST: Client/Edit/5
        [HttpPost]
        public async Task<ActionResult> Edit(BookingVM bookingVM)
        {
			var excludedFields = new string[] { nameof(BookingVM.roomVMs), nameof(BookingVM.TotalCost), nameof(BookingVM.PricePerDay),
				nameof(BookingVM.ClientVM), nameof(BookingVM.RoomVM) };

			// Remove the excluded fields from the model state
			foreach (var field in excludedFields)
			{
				ModelState.Remove(field);
			}
			if (!ModelState.IsValid)
            {
                return View(bookingVM);
            }
			if (roomsPrice.ContainsKey(bookingVM.RoomID))
			{
				bookingVM.PricePerDay = roomsPrice[bookingVM.RoomID];
			}
			try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = url;
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));


                    var content = JsonConvert.SerializeObject(bookingVM);
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

        public async Task<ActionResult> Delete(int id)
        {

            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = url;
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                   
                    // make the request
                    HttpResponseMessage response = await client.DeleteAsync("" + id);

                    return RedirectToAction("Index", "Booking");
                }
            }
            catch
            {

                return View();
            }



        }
    }
}
