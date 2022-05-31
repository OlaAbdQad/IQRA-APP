using System.Security.Claims;
using iqraProject.DTOs;
using iqraProject.Interface.IServices;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace iqraProject.Controllers
{
    public class StudentController : Controller
    {
        private readonly IStudentService _studentService;

        public StudentController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Index()
        {
            return View(_studentService.GetAll().Data);
        }

       public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        // [Authorize(Roles = "Student")]
        public IActionResult Create(AddStudentRequestModel model)
        {
            _studentService.Create(model);
            return RedirectToAction("Login", "User");
        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            var studentEmail = User.FindFirst(ClaimTypes.Email).Value;
            var student = _studentService.GetByEmail(studentEmail);
            if(student == null)
            {
                return NotFound();
            }
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Student")]
        public IActionResult Update(UpdateStudentRequestModel model)
        {
            var studentEmail = User.FindFirst(ClaimTypes.Email).Value;
            var student = _studentService.GetByEmail(studentEmail);
            var updatedStudent = _studentService.Update(model, student.Data.Id);
            return RedirectToAction("MyDetails");
        }

        public IActionResult Details(int id)
        {
            var studentEmail = User.FindFirst(ClaimTypes.Email).Value;
            var student = _studentService.ReturnById(id).Data;
            return View(student);
        }

        public IActionResult MyDetails(int id)
        {
            var studentEmail = User.FindFirst(ClaimTypes.Email).Value;
            var student = _studentService.GetByEmail(studentEmail);
            var studentsDetails = _studentService.ReturnById(student.Data.Id);
            return View(studentsDetails.Data);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var student = _studentService.ReturnById(id).Data;
            if(student == null)
            {
                return NotFound();
            }
            return View(student);
        }

        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "Admin")]
        public IActionResult DeleteConfirmed(int id)
        {
            _studentService.Delete(id);
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Student")]
        public IActionResult Profile()
        {
            return View();
        }
        
        public IActionResult Guide()
        {
            return View();
        }


    }
}