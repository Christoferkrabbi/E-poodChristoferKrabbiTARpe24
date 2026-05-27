using System;
using System.Globalization;
using System.Net;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using WebApp.Entities;
using WebApp.Services;

namespace WebApp
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// Register typed HttpClient for IOrderService.
			// Base address is read from configuration key "Services:OrderServiceUrl" or environment variable "ORDER_SERVICE_URL".
			builder.Services.AddHttpClient<IOrderService, OrderServiceHttpClient>(client =>
			{
				var url = builder.Configuration["Services:OrderServiceUrl"] ?? Environment.GetEnvironmentVariable("ORDER_SERVICE_URL");
				if (!string.IsNullOrEmpty(url))
				{
					// ensure trailing slash isn't required; relative paths will combine correctly
					client.BaseAddress = new Uri(url);
				}
			});

			builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("Default")));

			// optional: global euro culture (keeps currency formatting consistent)
			var euroCulture = new CultureInfo("en-IE");
			CultureInfo.DefaultThreadCurrentCulture = euroCulture;
			CultureInfo.DefaultThreadCurrentUICulture = euroCulture;

			builder.Services.AddControllersWithViews();
			builder.Services.AddSession();

			builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie();

			var app = builder.Build();

			if (!app.Environment.IsDevelopment())
			{
				app.UseExceptionHandler("/Home/Error");
				app.UseHsts();
			}

			app.UseHttpsRedirection();
			app.UseStaticFiles();
			app.UseSession();
			app.UseRouting();

			app.UseAuthentication();
			app.UseAuthorization();

			app.MapControllerRoute(
				name: "default",
				pattern: "{controller=Store}/{action=Index}/{id?}");

			app.Run();
		}
	}
}