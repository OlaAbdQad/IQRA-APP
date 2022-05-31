using System.Collections.Generic;
using System.Security.Claims;
using iqraProject.DTOs;
using iqraProject.Interface.IServices;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;

namespace iqraProject.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        public IActionResult Index()
        {
            var users = _userService.GetAll().Data;
            return View(users);
        }

        public IActionResult LogIn()
        {
            return View();
        }
        [HttpPost]
        public IActionResult LogIn(UserLoginDto model)
        {
            var user = _userService.LogInUser(model);
            if(user.Data != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Data.Id.ToString()),
                    new Claim(ClaimTypes.Email, user.Data.Email)                                    
                };
                foreach (var item in user.Data.Roles)
                {
                    claims.Add(new Claim(ClaimTypes.Role, item.Name));
                }
                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var authenticationProperties = new AuthenticationProperties();
                var principal = new ClaimsPrincipal(claimsIdentity);
                HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, authenticationProperties);

                foreach (var role in user.Data.Roles)
                {
                    if (role.Name == "Admin")
                    {
                        return RedirectToAction("Profile", "Admin");
                    }
                    if (role.Name == "Teacher")
                    {
                        return RedirectToAction("Profile", "Teacher");
                    }
                    if (role.Name == "Student")
                    {
                        return RedirectToAction("Profile", "Student");
                    }
                    
                }

            }
            
            ViewBag.error = "Invalid username or password!";
            return View();
        }

         public IActionResult LogOut()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index","Home");
        }
    }


}