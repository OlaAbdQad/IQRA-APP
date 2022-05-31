using System.Linq;
using System.Security.Claims;
using System.Speech.Synthesis;
using iqraProject.DTOs;
using iqraProject.Interface.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace iqraProject.Controllers
{
    public class AssessmentController : Controller
    {
        private readonly IAssessmentService _assessmentService;
        private readonly ILessonService _lessonService;
        private readonly IStudentService _studentService;
        private readonly SpeechSynthesizer _speech;

        public AssessmentController(IAssessmentService assessmentService, ILessonService lessonService, SpeechSynthesizer speechSynthesizer, IStudentService studentService)
        {
            _assessmentService = assessmentService;
            _lessonService = lessonService;
            _speech = speechSynthesizer;
            _studentService = studentService;
        }

        public IActionResult Index()
        {
            return View(_assessmentService.GetAllAssessment().Data);
        }
        public IActionResult TeachersIndex()
        {
            return View(_assessmentService.GetAllAssessment().Data);
        }

        public IActionResult Create()
        {
            var lessons = _lessonService.GetAllLesson();
            ViewData["Lesson"] = new SelectList(lessons, "Id", "Name");

            return View();
        }

        [HttpPost]
        public IActionResult Create(AddAssessmentRequestModel model)
        {
            _assessmentService.AddAssessment(model);
            return RedirectToAction("Index");
        }
        public IActionResult CreateForTeacher()
        {
            var lessons = _lessonService.GetAllLesson();
            ViewData["Lesson"] = new SelectList(lessons, "Id", "Name");

            return View();
        }

        [HttpPost]
        public IActionResult CreateForTeacher(AddAssessmentRequestModel model)
        {
            _assessmentService.AddAssessment(model);
            return RedirectToAction("TeachersIndex");
        }

        public IActionResult ConvertToSound(string text)
        {
            // foreach(var voice in _speech.GetInstalledVoices())
            // {
            //    text = voice.VoiceInfo.Name;
            // }
                      
            // _speech.SelectVoice(text);
           
            var email = User.FindFirst(ClaimTypes.Email).Value;
            var student = _studentService.GetByEmail(email);
            var studentId = _studentService.ReturnById(student.Data.Id);
            var lesson = _lessonService.GetLesson(studentId.Data.Lessons.Last().Id);
            var assessmentId = lesson.Assessments.Last().Id;

            _speech.SetOutputToDefaultAudioDevice();
            _speech.Speak(text);

            return RedirectToAction("Details", new {id = assessmentId});

        }


        [HttpGet]
        public IActionResult Update(int id)
        {
            var assessment = _assessmentService.GetAssessment(id).Data;
            if(assessment == null)
            {
                return NotFound();
            }
            return View();
        }

        [HttpPost]
        public IActionResult Update(UpdateAssessmentRequestModel model, int id)
        {
            var assessment = _assessmentService.UpdateAssessment(model, id).Data;
            return RedirectToAction("Index") ;
        }

        public IActionResult Details(int id)
        {
            return View(_assessmentService.GetAssessment(id).Data);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var assessment = _assessmentService.GetAssessment(id).Data;
            if(assessment == null)
            {
                return NotFound();
            }
            return View(assessment);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            _assessmentService.DeleteAssessment(id);
            return RedirectToAction("Index");
        }



    }
}