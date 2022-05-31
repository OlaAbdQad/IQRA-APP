using System.Linq;
using System.Security.Claims;
using System.Speech.Synthesis;
using iqraProject.DTOs;
using iqraProject.Interface.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace iqraProject.Controllers
{
    public class LessonController : Controller
    {
        private readonly ILessonService _lessonService;
        private readonly IStudentService _studentService;
        private readonly SpeechSynthesizer _speech;

        public LessonController(ILessonService lessonService, IStudentService studentService , SpeechSynthesizer speechSynthesizer)
        {
            _lessonService = lessonService;
            _studentService = studentService;
            _speech = speechSynthesizer;
        }

        public IActionResult Index()
        {
            return View(_lessonService.GetAllLesson());
        }

        public IActionResult TeachersIndex()
        {
            return View(_lessonService.GetAllLesson());
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(AddLessonRequestModel model)
        {
            _lessonService.AddLesson(model);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult CreateForTeacher()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateForTeacher(AddLessonRequestModel model)
        {
            _lessonService.AddLesson(model);
            return RedirectToAction("TeachersIndex");
        }

        [HttpGet]
        [Authorize]
        public IActionResult Details()
        {
            var email = User.FindFirst(ClaimTypes.Email).Value;
            var student = _studentService.GetByEmail(email);
            var studentId = _studentService.ReturnById(student.Data.Id);
            var lesson = _lessonService.GetLesson(studentId.Data.Lessons.Last().Id);
            
            return View(lesson);
        }

        public IActionResult GetLesssonDetails(int id)
        {
            var lesson = _lessonService.GetLesson(id);
            return View(lesson);
        }

         [HttpGet]
        public IActionResult Update(int id)
        {
            var lesson = _lessonService.GetLesson(id);
            if (lesson == null)
            {
                return NotFound();
            }
            return View();
        }

         [HttpPost]
        public IActionResult Update(UpdateLessonRequestModel model, int id)
        {
            _lessonService.UpdateLesson(model, id);
            return RedirectToAction("Index");
        }
        public IActionResult ConvertToSound(string symbol)
        {
            // foreach(var voice in _speech.GetInstalledVoices())
            // {
            //    symbol = voice.VoiceInfo.Name;
            // }
                      
            // _speech.SelectVoice(symbol);

            _speech.SetOutputToDefaultAudioDevice();
            _speech.Speak(symbol);

            
            return RedirectToAction("Details");
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {

            var lesson = _lessonService.GetLesson(id);
            if (lesson == null)
            {
                return NotFound();
            }
            return View(lesson);
        }

         [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            _lessonService.DeleteLesson(id);
            return RedirectToAction("Index");
        }





    }
}