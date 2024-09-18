using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Schools.MVC.Models;
using Schools.MVC.Resources;
using Schools.MVC.Services;
using System.Diagnostics;

namespace Schools.MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly IStringLocalizer<HomeController> _localizer;
        private readonly LanguageService _sharedLanguageLocalizer;

        public HomeController(ILogger<HomeController> logger, IStringLocalizer<HomeController> localizer, LanguageService sharedLanguageLocalizer)
        {
            _logger = logger;
            _localizer = localizer;
            _sharedLanguageLocalizer = sharedLanguageLocalizer;
        }

        public IActionResult Index()
        {
            //var localizedTitle = _localizer["home"];
            //var localizedTitle = _sharedLanguageLocalizer.GetKey(LocalizationGlobals.welcome);

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
