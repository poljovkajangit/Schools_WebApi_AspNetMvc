using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;

namespace Schools.MVC.Controllers
{
    public class LanguageController : Controller
    {
        [HttpGet]
        public IActionResult ChangeLanguage()
        {
            string? urlCulture = Request.Query["culture"];
            if (urlCulture != null)
            {
                var culture = new RequestCulture(urlCulture);
                Response.Cookies.Append(
                    CookieRequestCultureProvider.DefaultCookieName,
                    CookieRequestCultureProvider.MakeCookieValue(culture),
                    new CookieOptions { Expires = DateTimeOffset.UtcNow.AddDays(1) }
                );
            }

            string returnUrl = Request.Headers["Referer"].ToString();
            if (string.IsNullOrWhiteSpace(returnUrl))
            {
                returnUrl = "/Home/Index";
            }

            return Redirect(returnUrl);
        }
    }
}
