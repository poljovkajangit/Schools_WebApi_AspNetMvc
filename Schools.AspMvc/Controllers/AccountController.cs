using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Schools.MVC.Models;
using System.Reflection;
using System.Security.Claims;
using System.Text;

namespace Schools.MVC.Controllers
{
    public class AccountController : Controller
    {
        private readonly HttpClient _client;

        //private readonly SignInManager<AppUser> _signInManager;

        public AccountController(/*SignInManager<AppUser> signInManager*/ IConfiguration config)
        {
            var baseAddressFromSettings = config.GetSection("AppSettings:BaseAddress").Value;

            if (string.IsNullOrWhiteSpace(baseAddressFromSettings))
            {
                throw new ArgumentNullException("baseAddressFromSettings");
            }

            _client = new HttpClient();
            _client.BaseAddress = new Uri(uriString: baseAddressFromSettings);
            //_signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult Login()
        {
            var response = new LoginViewModel();
            return View(response);
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel loginViewModel)
        {
            if (!ModelState.IsValid)
            {
                TempData["errorMessage"] = "Model is not valid.";
                return View(loginViewModel);
            }

            try
            {

                //check user credentials
                string data = JsonConvert.SerializeObject(loginViewModel);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                HttpResponseMessage response = _client.PostAsync(_client.BaseAddress + "/account/login", content).Result;

                if (!response.IsSuccessStatusCode)
                {
                    TempData["errorMessage"] = "Some error occured.";
                    return RedirectToAction("Account", "Login");
                }

                //login
                List<Claim> claims = new()
                {
                    new Claim(ClaimTypes.Role, "Claim_Role"),
                    new Claim(ClaimTypes.NameIdentifier, loginViewModel.Username),
                    new Claim(ClaimTypes.Name,  loginViewModel.Username),
                    new Claim("IdentificationValidated", "IdentificationValidated"),
                };

                var claimsIdentity = new ClaimsIdentity(claims,
                    Microsoft.AspNetCore.Authentication.Cookies.CookieAuthenticationDefaults.AuthenticationScheme);

                var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

                var authProperties = new AuthenticationProperties
                {
                    ExpiresUtc = DateTime.Now.AddMinutes(10)
                };

                HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal, authProperties);

                TempData["successMessage"] = "User is signed in.";
                return RedirectToAction("Index", "Home");

            }
            catch (Exception e)
            {
                TempData["errorMessage"] = e.Message;
                return View(loginViewModel);
            }
        }

        [HttpGet]
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
