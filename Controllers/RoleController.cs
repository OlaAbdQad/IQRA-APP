using iqraProject.DTOs;
using iqraProject.Interface.IServices;
using Microsoft.AspNetCore.Mvc;

namespace iqraProject.Controllers
{
    public class RoleController : Controller
    {
        private readonly IRoleService _roleService;

        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        public IActionResult Index()
        {
            return View(_roleService.GetAll().Data);
        }

       public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(AddRoleRequestModel model)
        {
            _roleService.Create(model);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            var role = _roleService.ReturnById(id).Data;
            if(role == null)
            {
                return NotFound();
            }
            return View();
        }

        [HttpPost]
        public IActionResult Update(UpdateRoleRequestModel model, int id)
        {
            var student = _roleService.Update(model, id).Data;
            return RedirectToAction("Index");
        }

        public IActionResult Details(int id)
        {
            return View(_roleService.ReturnById(id).Data);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var role = _roleService.ReturnById(id).Data;
            if(role == null)
            {
                return NotFound();
            }
            return View(role);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            _roleService.Delete(id);
            return RedirectToAction("Index");
        }

    }
}