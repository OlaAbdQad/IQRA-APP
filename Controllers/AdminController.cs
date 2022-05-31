using System.Security.Claims;
using iqraProject.DTOs;
using iqraProject.Interface.IServices;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace iqraProject.Controllers
{
    public class AdminController : Controller
    {
        private readonly IAdminService _adminService;

        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
        }

        // [Authorize(Roles = "Admin")]
        public IActionResult Index()
        {
            return View(_adminService.GetAll().Data);
        }

       public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        // [Authorize(Roles = "Admin")]
        public IActionResult Create(AddAdminRequestModel model)
        {
            _adminService.Create(model);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            var adminEmail = User.FindFirst(ClaimTypes.Email).Value;
            var admin = _adminService.GetByEmail(adminEmail);
            if(admin == null)
            {
                return NotFound();
            }
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult Update(UpdateAdminRequestModel model, int id)
        {
            var adminEmail = User.FindFirst(ClaimTypes.Email).Value;
            var admin = _adminService.GetByEmail(adminEmail);
            var updatedAdmin = _adminService.Update(model, admin.Data.Id);
            return RedirectToAction("MyDetails");
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Details(int id)
        {
            var adminEmail = User.FindFirst(ClaimTypes.Email).Value;
            var admin = _adminService.ReturnById(id).Data;
            return View(admin);
        }

        public IActionResult MyDetails(int id)
        {
            var adminEmail = User.FindFirst(ClaimTypes.Email).Value;
            var admin = _adminService.GetByEmail(adminEmail);
            var adminsDetails = _adminService.ReturnById(admin.Data.Id);
            return View(adminsDetails.Data);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var admin = _adminService.ReturnById(id).Data;
            if(admin == null)
            {
                return NotFound();
            }
            return View(admin);
        }

        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "Admin")]
        public IActionResult DeleteConfirmed(int id)
        {
            _adminService.Delete(id);
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Profile()
        {
            return View();
        }

        // public IActionResult Logout()
        // {
        //     HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        //     return RedirectToAction("Index","Home");
        // }

    }
}