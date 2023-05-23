using Azure;
using DataHR.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using WebSite.Models.Login;
using WebSite.OtherContent.Token;

namespace WebSite.Controllers
{
    public class LoginController : Controller
    {
        private readonly Uri url = new Uri("https://localhost:7278/api/Token/");
        //public static string Token = null;
        [HttpGet]
        public IActionResult Index()
        {
            if (TokenResponse.Token != null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Authenticate(LoginVM model)
        {
            using (var client = new HttpClient())
            {

                client.BaseAddress = url;
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var content = JsonConvert.SerializeObject(model);
                var buffer = System.Text.Encoding.UTF8.GetBytes(content);
                var byteContent = new ByteArrayContent(buffer);
                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");


                HttpResponseMessage response = await client.PostAsync("", byteContent);
                if (response.IsSuccessStatusCode)
                {
                    string jsonString = await response.Content.ReadAsStringAsync();
                    TokenResponse.Token = JsonConvert.DeserializeObject<string>(jsonString);
                    //CustomTokenAuthorizationAttribute.Token = responseData.Token;
                    return RedirectToAction("Index", "Home");
                }
                return RedirectToAction("Index", "Login");
            }

        }
        [CustomTokenAuthorization]
        public IActionResult Logout()
        {
            TokenResponse.Token = null;
            return RedirectToAction("Index", "Login");
        }
    }
}
