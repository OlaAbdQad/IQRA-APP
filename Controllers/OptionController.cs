using System;
using System.IO;
using System.Speech.Synthesis;
using iqraProject.DTOs;
using iqraProject.Interface.IServices;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace iqraProject.Controllers
{
    public class OptionController : Controller
    {
        private readonly IOptionService _optionService;
        private readonly IQuestionService _questionService;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public OptionController(IOptionService optionService, IQuestionService questionService, IWebHostEnvironment webHostEnvironment)
        {
            _optionService = optionService;
            _questionService = questionService;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            return View(_optionService.GetAllOption().Data);
        }
        public IActionResult TeachersIndex()
        {
            return View(_optionService.GetAllOption().Data);
        }

        // public IActionResult OptionList()
        // {
        //     return View(_optionService.GetAllOption().Data);
        // }

        public IActionResult Create()
        {
            var questions = _questionService.GetAllQuestion().Data;
            ViewData["Question"] = new SelectList(questions, "Id", "TextTest");

            return View();
        }

        [HttpPost]
        public IActionResult Create(AddOptionRequestModel model, IFormFile optionSound)
        {
            // if(optionSound != null)
            // {
            // var from = HttpContext.Request.Form.Files;
            // string optionSoundPath = Path.Combine(_webHostEnvironment.WebRootPath, "OptionSound");
            // var extension = optionSound.FileName.Substring(optionSound.FileName.LastIndexOf('.')+1);
            // Directory.CreateDirectory(optionSoundPath);
            // string optionAudio = $"STD{Guid.NewGuid()}.{extension}";
            // string fullPath = Path.Combine(optionSoundPath, optionAudio);
            // using (var fileStream = new FileStream(fullPath, FileMode.Create))
            // {
            //     optionSound.CopyTo(fileStream);
            // }
            // model.Sound = optionAudio;
            // }

            _optionService.AddOption(model);
            return RedirectToAction("Index");
            
        }

        public IActionResult CreateForTeacher()
        {
            var questions = _questionService.GetAllQuestion().Data;
            ViewData["Question"] = new SelectList(questions, "Id", "TextTest");

            return View();
        }

        [HttpPost]
        public IActionResult CreateForTeacher(AddOptionRequestModel model)
        {
            _optionService.AddOption(model);
            return RedirectToAction("TeachersIndex");
        }

        




        [HttpGet]
        public IActionResult Update(int id)
        {
            var option = _optionService.GetOption(id);
            if(option == null)
            {
                return NotFound();
            }
            return View();
        }

        [HttpPost]
        public IActionResult Update(UpdateOptionRequestModel model, int id)
        {
            var option = _optionService.UpdateOption(model, id);
            return RedirectToAction("Index");
        }

        public IActionResult Details(int id)
        {
            return View(_optionService.GetOption(id).Data);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var option = _optionService.GetOption(id).Data;
            if(option == null)
            {
                return NotFound();
            }
            return View(option);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            _optionService.DeleteOption(id);
            return RedirectToAction("Index");
        }



    }
}