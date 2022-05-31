using System.Linq;
using System.Security.Claims;
using iqraProject.DTOs;
using iqraProject.Interface.IServices;
using Microsoft.AspNetCore.Mvc;

namespace iqraProject
{
    public class ResultController : Controller
    {
        private readonly IResultService _resultService;

        public ResultController(IResultService resultService)
        {
            _resultService = resultService;
        }

        public IActionResult GenerateLessonResult()
        {
            return View();
        }

        [HttpPost ]

        public IActionResult GenerateLessonResult(ResultRequestModel model)
        {
            var form = HttpContext.Request.Form.Keys.ToList();
            for(int i = 0; i < form.Count - 1 ; i++)
            {
                model.OptionsIds.Add(int.Parse(HttpContext.Request.Form[form[i]]));
            }

            model.StudentEmail = User.FindFirst(ClaimTypes.Email).Value;
            var result = _resultService.GenerateResult(model);  
            ViewBag.message = result.Message;

            return View();

            // if(result.IsSuccess)
            // {
            //     return RedirectToAction("Details", "Lesson");
            // }
            // else
            // {
            //     return RedirectToAction("Details", "Lesson");
            // }
            
            
            
        }
    }
}