using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Newtonsoft.Json;
using Schools.MVC.Models;
using System.Security.Claims;
using System.Text;

namespace Schools.MVC.Controllers
{
    public class CityController : Controller
    {
        private readonly HttpClient _client;
        private readonly IConfiguration _config;

        public CityController(HttpClient client, IStringLocalizer<CityController> localizer, IConfiguration config)
        {
            var baseAddressFromSettings = config.GetSection("AppSettings:BaseAddress").Value;

            if (string.IsNullOrWhiteSpace(baseAddressFromSettings))
            {
                throw new ArgumentNullException("baseAddressFromSettings");
            }

            _client = new HttpClient();
            _client.BaseAddress = new Uri(uriString: baseAddressFromSettings);
            _config = config;
        }

        [HttpGet]
        //[Authorize(Roles = "Claim_Role")]
        public IActionResult Index()
        {


            var user = User;

            var cityList = new List<CityViewModel>();

            //call to web api micro-service endpoint
            HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/city/GetAll").Result;

            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                cityList = JsonConvert.DeserializeObject<List<CityViewModel>>(data);
            }

            return View(cityList);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(CityViewModel model)
        {
            try
            {
                string data = JsonConvert.SerializeObject(model);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                HttpResponseMessage response = _client.PostAsync(_client.BaseAddress + "/City", content).Result;

                if (response.IsSuccessStatusCode)
                {
                    TempData["successMessage"] = "City created.";
                    return RedirectToAction("Index");
                }

            }
            catch (Exception e)
            {
                TempData["errorMessage"] = e.Message;
                return View();
            }
            return View();
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            try
            {
                CityViewModel cityViewModel = null;

                var response = _client.GetAsync(_client.BaseAddress + "/City/GetById/" + id).Result;

                if (response.IsSuccessStatusCode)
                {
                    string data = response.Content.ReadAsStringAsync().Result;
                    cityViewModel = JsonConvert.DeserializeObject<CityViewModel>(data);
                }
                else
                {
                    TempData["errorMessage"] = "Some error occured.";
                }
                return View(cityViewModel);

            }
            catch (Exception e)
            {
                TempData["errorMessage"] = e.Message;
                return View();
            }
        }

        [HttpPost]
        public IActionResult Edit(CityViewModel model)
        {
            string data = JsonConvert.SerializeObject(model);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
            HttpResponseMessage response = _client.PutAsync(_client.BaseAddress + "/City/" + model.Id, content).Result;

            if (response.IsSuccessStatusCode)
            {
                TempData["successMessage"] = "City updated.";
                return RedirectToAction("Index");
            }
            else
            {
                TempData["errorMessage"] = "Some error occured.";
                return View();
            }
        }

        [HttpGet]
        [Route("{id:int}")]
        public IActionResult Delete(int id)
        {
            HttpResponseMessage response = _client.DeleteAsync(_client.BaseAddress + "/city/" + id).Result;

            if (response.IsSuccessStatusCode)
            {
                TempData["successMessage"] = "City deleted.";
                return RedirectToAction("Index");
            }
            else
            {
                TempData["errorMessage"] = "Some error occured.";
                return View();
            }
        }
    }
}
