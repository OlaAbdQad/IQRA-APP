using System.Collections.Generic;
using System.Linq;
using iqraProject.DTOs;
using iqraProject.Entities;
using iqraProject.Interface.IRepository;
using Microsoft.EntityFrameworkCore;

namespace iqraProject.Implementation.Repositories
{
    public class LessonRepository : ILessonRepository
    {
        private readonly ArabicContext _context;
        public LessonRepository(ArabicContext context)
        {
            _context = context;
        }

        public Lesson Create(Lesson lesson)
        {
            _context.Lessons.Add(lesson);
            _context.SaveChanges();
            return lesson;
        }

        public void Delete(Lesson lesson)
        {
            _context.Lessons.Remove(lesson);
            _context.SaveChanges();
        }


        public Lesson Get(int id)
        {
            return _context.Lessons.Include(sl => sl.StudentLessons).ThenInclude(s => s.Student).FirstOrDefault(f => f.Id == id);
        }

        public List<LessonDto> GetAll()
        {
             return _context.Lessons.Include(l => l.Words).Select(y => new LessonDto
            {
                Id = y.Id,
                Name = y.Name,
                Description = y.Description,
                Words = y.Words.Select(l =>  new WordDto
                {
                    Id = l.Id,
                    Symbol = l.Symbol,
                    Sound = l.Sound,
                    WordName = l.WordName,
                    Audio = l.Audio

                }).ToList()
                
            }).ToList();
        }

        public Lesson GetLessonByName(string name)
        {
            return _context.Lessons.SingleOrDefault(l => l.Name == name);
        }

        public LessonDto Return(int id)
        {
            var lesson = _context.Lessons.Include(l => l.Words).Include(a => a.Assessments).Include(s => s.StudentLessons).ThenInclude(l => l.Student).SingleOrDefault(y => y.Id == id);
            var lessonDto = new LessonDto
            {
                Id = lesson.Id,
                Name = lesson.Name,
                Description = lesson.Description,
                Words = lesson.Words.Select(l =>  new WordDto
                {
                    Id = l.Id,
                    Symbol = l.Symbol,
                    Sound = l.Sound,
                    WordName = l.WordName,
                    Audio = l.Audio
                }).ToList(),

                Assessments = lesson.Assessments.Select(a => new AssessmentDto
                {
                    Id = a.Id,
                    Name = a.Name,
                    Description = a.Description

                }).ToList(),
                Students = lesson.StudentLessons.Select(s => new StudentDto
                {
                    Id = s.Id,
                    FirstName = s.Student.FirstName,
                    LastName = s.Student.LastName,
                    Email = s.Student.Email,
                }).ToList()

            };
            return lessonDto;
        }

        public Lesson Update(Lesson lesson)
        {
            _context.Lessons.Update(lesson);
            _context.SaveChanges();
            return lesson;
        }

       
    }
}