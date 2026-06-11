using System.Security.Claims;
using Azure.Identity;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApp.Data;
using WebApp.Entities;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly AppDbContext _context;
        public AccountController(AppDbContext appDbContext)
        {
            _context = appDbContext;
        }
        [HttpGet]
        public IActionResult Index()
        {
            // 1. Get the username of the user who is logged in
            string currentUserName = User.Identity?.Name;

            if (string.IsNullOrEmpty(currentUserName))
            {
                // If they aren't logged in, redirect them out onto your Login screen page
                return RedirectToAction("Login");
            }

            // 2. Fetch the entity straight from your context table (No mapping required)
            var userProfile = _context.UserAccounts
                .FirstOrDefault(u => u.UserName == currentUserName);

            if (userProfile == null)
            {
                return NotFound("User profile not found in database.");
            }

            // 3. Pass your regular entity object straight over onto your Account view
            return View(userProfile);
        }
        public IActionResult Registration()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Registration(RegistrationViewModel model)
        {
            if (ModelState.IsValid)
            {
                UserAccount account = new UserAccount();
                account.Email = model.Email;
                account.FirstName = model.FirstName;
                account.LastName = model.LastName;
                account.Password = model.Password;
                account.UserName = model.UserName;
                if (account.Email == "christoferkrabbi@gmail.com" ||
                    account.UserName == "Admin")


                {
                    account.Role = "Admin";
                }

                try
                {
					bool emailExists = _context.UserAccounts.Any(x => x.Email == model.Email);
					bool userExists = _context.UserAccounts.Any(x => x.UserName == model.UserName);

					if (emailExists)
					{
						ModelState.AddModelError("Email", "This email is already being used");
						return View(model);
					}

					if (userExists)
					{
						ModelState.AddModelError("UserName", "Username is already being used");
						return View(model);
					}

					// 2. Kui kõik on korras, siis alles salvesta
					_context.UserAccounts.Add(account);
					_context.SaveChanges();

					ModelState.Clear();
                    //ViewBag.Message = $"{account.FirstName} {account.LastName} registered successfully. Please login";
                    ViewBag.Message = "registered successfully.";
                    ViewBag.ShowLink = true;
                    //continues at the beginning of registartion view...
                }
                catch (DbUpdateException ex)
                {
                    ModelState.AddModelError("", "Please enter unique Email or Password.");
                    return View(model);
                }
                return View();
            }
            return View(model);
        }

        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            if(ModelState.IsValid)
            {
                var user = _context.UserAccounts.Where(x => (x.UserName == model.UserNameOrEmail || x.Email == model.UserNameOrEmail) && x.Password == model.Password).FirstOrDefault(); 
                if(user != null)
                {
                    //success, create cookie
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, user.Email),
                        new Claim("Name", user.FirstName),
						new Claim(ClaimTypes.Role, user.Role),
					};

                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

                    return RedirectToAction("SecurePage"); 
                }
                else
                {
                    ModelState.AddModelError("", "Username/Email or password is not correct");

                }
            }
            return View(model);
        }

        public IActionResult LogOut()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index");
        }
        [Authorize] 
        public IActionResult SecurePage()
        {
            ViewBag.Name = HttpContext.User.Identity.Name;
            return View();
        }

		[HttpGet]
		public IActionResult AccessDenied()
		{
			// This will look for a view named "AccessDenied.cshtml"
			return View();
		}

        [HttpGet]
        public IActionResult Profile()
        {
            // Grabs your cookie identity authentication name (which holds your user Email string)
            string currentUserEmail = User.Identity?.Name ?? "";

            if (string.IsNullOrEmpty(currentUserEmail))
            {
                return RedirectToAction("Login");
            }

            // Fetches your single user account entity directly from your AppDbContext table
            var loggedInUser = _context.UserAccounts
                .FirstOrDefault(u => u.Email == currentUserEmail || u.UserName == currentUserEmail);

            if (loggedInUser == null)
            {
                return NotFound("Kasutajaprofiili ei leitud.");
            }

            // Gathers your transactions from your in-memory list by looking for both usernames and emails
            ViewBag.Orders = OrderStorage.Orders
                .Where(o => string.Equals(o.UserName, loggedInUser.UserName, StringComparison.OrdinalIgnoreCase) ||
                            string.Equals(o.UserName, loggedInUser.Email, StringComparison.OrdinalIgnoreCase))
                .OrderByDescending(o => o.CreatedAt)
                .ToList();

            return View(loggedInUser);
        }

    }
}

        
