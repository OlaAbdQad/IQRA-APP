using System.Collections.Generic;
using System.Linq;
using iqraProject.DTOs;
using iqraProject.Entities;
using iqraProject.Interface.IRepository;
using iqraProject.Interface.IServices;



namespace iqraProject.Implementation.Services
{
    public class LessonService : ILessonService
    {
        private readonly ILessonRepository _lessonRepository;

        public LessonService(ILessonRepository lessonRepository)
        {
            _lessonRepository = lessonRepository;
        }

        public bool AddLesson(AddLessonRequestModel model)
        {
            var lesson = new Lesson
            {
                Name = model.Name,
                Description = model.Description
            };
            _lessonRepository.Create(lesson);
            return true;
        }


        public void DeleteLesson(int id)
        {
            var lesson = _lessonRepository.Get(id);
            _lessonRepository.Delete(lesson);
        }

        public List<LessonDto> GetAllLesson()
        {
            return _lessonRepository.GetAll().ToList();
        }

        public LessonDto GetLesson(int id)
        {
            return _lessonRepository.Return(id);
        }

        public bool UpdateLesson(UpdateLessonRequestModel model, int id)
        {
            var lesson = _lessonRepository.Get(id);
            if(lesson == null)
            {
                throw new NotFoundException($"word with {id} not found ");
            }
            lesson.Name = model.Name ?? lesson.Name;
            lesson.Description = model.Description ?? lesson.Description;
            _lessonRepository.Update(lesson);
            return true;
            
        }

    }
}