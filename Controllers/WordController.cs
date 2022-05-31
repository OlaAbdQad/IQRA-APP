using System;
using System.IO;
using iqraProject.DTOs;
using iqraProject.Interface.IServices;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
// using Microsoft.CognitiveServices.Speech;
using System.Speech.Synthesis;

namespace iqraProject.Controllers
{
    public class WordController : Controller
    {
        private readonly IWordService _wordService;
        private readonly ILessonService _lessonService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly SpeechSynthesizer _speech;


        public WordController(IWordService wordService, ILessonService lessonService, IWebHostEnvironment webHostEnvironment, SpeechSynthesizer speech)
        {
            _wordService = wordService;
            _lessonService = lessonService;
            _webHostEnvironment = webHostEnvironment;
            _speech = speech;
        }

        public IActionResult Index()
        {
            return View(_wordService.GetAllWord());
        }
        public IActionResult TeachersIndex()
        {
            return View(_wordService.GetAllWord());
        }

        public IActionResult Create()
        {
            var lessons = _lessonService.GetAllLesson();
            ViewData["Lesson"] = new SelectList(lessons, "Id", "Name");

            return View();
        }

        [HttpPost]
        public IActionResult Create(AddWordRequestModel model)
        {
            // foreach(var voice in _speech.GetInstalledVoices())
            // {
            //    model.Sound = voice.VoiceInfo.Name;
            // }
                      
            // _speech.SelectVoice(model.Sound);

            // _speech.SetOutputToDefaultAudioDevice();
            // _speech.Speak(model.Sound);

            _wordService.AddWord(model);
            return RedirectToAction("Index");
        }

        public IActionResult CreateForTeacher()
        {
            var lessons = _lessonService.GetAllLesson();
            ViewData["Lesson"] = new SelectList(lessons, "Id", "Name");

            return View();
        }

        [HttpPost]
        public IActionResult CreateForTeacher(AddWordRequestModel model)
        {
            _wordService.AddWord(model);
            return RedirectToAction("TeachersIndex");
        }





        [HttpGet]
        public IActionResult Update(int id)
        {
            var word = _wordService.GetWord(id);
            if(word == null)
            {
                return NotFound();
            }
            return View();
        }

        [HttpPost]
        public IActionResult Update(UpdateWordRequestModel model, int id)
        {
            var word = _wordService.UpdateWord(model, id);
            return RedirectToAction("Index") ;
        }

        public IActionResult Details(int id)
        {
            return View(_wordService.GetWord(id));
        }
        

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var word = _wordService.GetWord(id);
            if(word == null)
            {
                return NotFound();
            }
            return View(word);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            _wordService.DeleteWord(id);
            return RedirectToAction("Index");
        }

        

        // var from = HttpContext.Request.Form.Files;
            // string wordSoundPath = Path.Combine(_webHostEnvironment.WebRootPath, "WordSound");
            // var extension = wordSound.FileName.Substring(wordSound.FileName.LastIndexOf('.')+1);
            // Directory.CreateDirectory(wordSoundPath);
            // string wordAudio = $"STD{Guid.NewGuid()}.{extension}";
            // string fullPath = Path.Combine(wordSoundPath, wordAudio);
            // using (var fileStream = new FileStream(fullPath, FileMode.Create))
            // {
            //     wordSound.CopyTo(fileStream);
            // }
            // model.Sound = wordAudio;




        // foreach(var voice in _speech.GetInstalledVoices())
            // {
            //    model.Audio = voice.VoiceInfo.Name;
            // }

                      
            // _speech.SelectVoice(model.Audio);

    }
}

