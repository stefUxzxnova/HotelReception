using Newtonsoft.Json;
using System.Net.Http.Headers;
using System;
using WebSite.Models.Client;
using WebSite.Models.Room;
using WebSite.Models.Booking;

namespace WebSite.OtherContent.DataUtil
{
    public class DataUtil
    {
        public int Items { get; set; }

        public async Task<int> GetItemsNumber(Uri uri)
        {

            using (var client = new HttpClient())
            {
                client.BaseAddress = uri;
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                // Add the Authorization header with the AccessToken.
                //client.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);

                // make the request
                HttpResponseMessage response = await client.GetAsync("");

                // parse the response and return the data.
                string jsonString = await response.Content.ReadAsStringAsync();
                var responseData = JsonConvert.DeserializeObject<List<BookingVM>>(jsonString);
                if (response.IsSuccessStatusCode)
                {
                    Items = responseData.Count();
                    return Items;
                }
                return 0;
            }
        }
    }
}
