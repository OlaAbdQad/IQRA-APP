using System.Security.Claims;
using iqraProject.DTOs;
using iqraProject.Interface.IServices;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace iqraProject.Controllers
{
    public class TeacherController : Controller
    {
        private readonly ITeacherService _teacherService;

        public TeacherController(ITeacherService teacherService)
        {
            _teacherService = teacherService;
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Index()
        {
            return View(_teacherService.GetAll().Data);
        }

       public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult Create(AddTeacherRequestModel model)
        {
            _teacherService.Create(model);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Update()
        {
            var teacherEmail = User.FindFirst(ClaimTypes.Email).Value;
            var teacher = _teacherService.GetByEmail(teacherEmail);
            if(teacher == null)
            {
                return NotFound();
            }
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Teacher")]
        public IActionResult Update(UpdateTeacherRequestModel model)
        {
            var teacherEmail = User.FindFirst(ClaimTypes.Email).Value;
            var teacher = _teacherService.GetByEmail(teacherEmail);
            var updatedTeacher = _teacherService.Update(model, teacher.Data.Id);
            return RedirectToAction("MyDetails");
        }

        [Authorize(Roles = "Admin, Teacher")]
        public IActionResult Details(int id)
        {
            var teacherEmail = User.FindFirst(ClaimTypes.Email).Value;
            var teacher = _teacherService.ReturnById(id).Data;
            return View(teacher);
        }

        [Authorize(Roles = "Teacher")]
        public IActionResult MyDetails(int id)
        {
            var teacherEmail = User.FindFirst(ClaimTypes.Email).Value;
            var teacher = _teacherService.GetByEmail(teacherEmail);
            var teachersDetails = _teacherService.ReturnById(teacher.Data.Id);
            return View(teachersDetails.Data);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
           var teacher = _teacherService.ReturnById(id).Data;
            if(teacher == null)
            {
                return NotFound();
            }
            return View(teacher);
        }

        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "Admin")]
        public IActionResult DeleteConfirmed(int id)
        {
            _teacherService.Delete(id);
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Teacher")]
        public IActionResult Profile()
        {
            return View();
        }

       

    }
}