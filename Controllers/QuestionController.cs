using System;
using System.IO;
using iqraProject.DTOs;
using iqraProject.Interface.IServices;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace iqraProject.Controllers
{
    public class QuestionController : Controller
    {
        private readonly IQuestionService _questionService;
        private readonly IAssessmentService _assessmentService;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public QuestionController(IQuestionService questionService, IAssessmentService assessmentService, IWebHostEnvironment webHostEnvironment)
        {
            _questionService = questionService;
            _assessmentService = assessmentService;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            return View(_questionService.GetAllQuestion().Data);
        }
        public IActionResult TeachersIndex()
        {
            return View(_questionService.GetAllQuestion().Data);
        }


        public IActionResult Create()
        {
            var assessments = _assessmentService.GetAllAssessment();
            ViewData["Assessment"] = new SelectList(assessments.Data, "Id", "Name");

            return View();
        }

        [HttpPost]
        public IActionResult Create(AddQuestionRequestModel model, IFormFile questionSound)
        {
            if (questionSound != null)
            {
            var from = HttpContext.Request.Form.Files;
            string questionSoundPath = Path.Combine(_webHostEnvironment.WebRootPath, "QuestionSound");
            var extension = questionSound.FileName.Substring(questionSound.FileName.LastIndexOf('.')+1);
            Directory.CreateDirectory(questionSoundPath);
            string questionAudio = $"STD{Guid.NewGuid()}.{extension}";
            string fullPath = Path.Combine(questionSoundPath, questionAudio);
            using (var fileStream = new FileStream(fullPath, FileMode.Create))
            {
                questionSound.CopyTo(fileStream);
            }
            model.AudioTest = questionAudio;
            }

            _questionService.AddQuestion(model);
            return RedirectToAction("Index");
        }

        public IActionResult CreateForTeacher()
        {
            var assessments = _assessmentService.GetAllAssessment();
            ViewData["Assessment"] = new SelectList(assessments.Data, "Id", "Name");

            return View();
        }

        [HttpPost]
        public IActionResult CreateForTeacher(AddQuestionRequestModel model)
        {
            _questionService.AddQuestion(model);
            return RedirectToAction("TeachersIndex");
        }


        [HttpGet]
        public IActionResult Update(int id)
        {
            var question = _questionService.GetQuestion(id).Data;
            if(question == null)
            {
                return NotFound();
            }
            return View();
        }

        [HttpPost]
        public IActionResult Update(UpdateQuestionRequestModel model, int id)
        {
            var question = _questionService.UpdateQuestion(model, id).Data;
            return RedirectToAction("Index");
        }

        public IActionResult Details(int id)
        {
            return View(_questionService.GetQuestion(id).Data);
        }


        [HttpGet]
        public IActionResult Delete(int id)
        {
            var question = _questionService.GetQuestion(id).Data;
            if(question == null)
            {
                return NotFound();
            }
            return View(question);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            _questionService.DeleteQuestion(id);
            return RedirectToAction("Index");
        }


    }
}