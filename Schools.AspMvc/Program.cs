using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Razor;
using Schools.MVC.Models;
using Schools.MVC.Services;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddHttpClient();

//builder.Services.AddIdentityCore<AppUser>(options =>
//{
//    options.User.RequireUniqueEmail = false;
//})
//.AddDefaultTokenProviders();

//builder.Services.AddAuthentication();


builder.Services.AddAuthentication(
    Microsoft.AspNetCore.Authentication.Cookies.CookieAuthenticationDefaults.AuthenticationScheme  // "Cookies authentication scheme"
    ).AddCookie(s =>
    {
        s.Cookie.Name = "logincookie";
        s.LoginPath = "/account/login";
        s.LogoutPath = "/account/logout";
    });


#region Localization

builder.Services.AddSingleton<LanguageService>();
builder.Services.AddLocalization(options => { options.ResourcesPath = "Resources"; });

builder.Services.AddControllersWithViews() // declare template  
    .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
    .AddDataAnnotationsLocalization(options =>
    {
        options.DataAnnotationLocalizerProvider = (type, factory) =>
        {
            var assembyName = new AssemblyName(typeof(SharedResources).GetTypeInfo().Assembly.FullName);
            return factory.Create("SharedResources", assembyName.Name);
        };
    }); //adds support for localized view files | support for localized DataAnnotations validation messages

var supportedCultures = new[] { "en-US", "fr" };
var localizationOptions = new RequestLocalizationOptions()
    .SetDefaultCulture(supportedCultures[0])
    .AddSupportedCultures(supportedCultures)
    .AddSupportedUICultures(supportedCultures);

#endregion

var app = builder.Build();

app.UseRequestLocalization(localizationOptions);

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
