using System.Collections.Generic;
using System.Linq;
using iqraProject;
using iqraProject.DTOs;
using iqraProject.Entities;
using iqraProject.Interface.IRepository;
using Microsoft.EntityFrameworkCore;

namespace iqraProject.Implementation.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        private readonly ArabicContext _context;

        public StudentRepository(ArabicContext context)
        {
            _context = context;
        }

        public StudentDto Create(Student student)
        {
            _context.Students.Add(student);
            _context.SaveChanges();
            return new StudentDto
            {
                Id = student.Id,
                FirstName = student.FirstName,
                LastName = student.LastName,
                Email = student.Email
            };
        }

        public void Delete(Student student)
        {
            _context.Students.Remove(student);
            _context.SaveChanges();
        }

        public bool ExistByEmail(string email)
        {
            return _context.Students.Any(e => e.Email == email);
        }

        public bool ExistById(int id)
        {
            return _context.Students.Any(e => e.Id == id);
        }

        public List<StudentDto> GetAll()
        {
            return _context.Students.Include(s => s.StudentLessons).ThenInclude(l => l.Lesson).Select(student => new StudentDto
            {
                Id = student.Id,
                FirstName = student.FirstName,
                LastName = student.LastName,
                Email = student.Email,
                Lessons = student.StudentLessons.Select(l => new LessonDto
                {
                    Id = l.Lesson.Id,
                    Name = l.Lesson.Name,
                    Description = l.Lesson.Description
                }).ToList()
                
            }).ToList();
        }

        public Student GetByEmail(string email)
        {
            return _context.Students.Include(s => s.StudentLessons).ThenInclude(l => l.Lesson).FirstOrDefault(a => a.Email == email);
        }

        public Student GetById(int id)
        {
            return _context.Students.Include(s => s.StudentLessons).ThenInclude(l => l.Lesson).FirstOrDefault(a => a.Id == id);
        }

        public StudentDto ReturnById(int id)
        {
            var student = _context.Students.Include(s => s.StudentLessons).ThenInclude(l => l.Lesson).FirstOrDefault(a => a.Id == id);
            return new StudentDto
            {
                Id = student.Id,
                FirstName = student.FirstName,
                LastName = student.LastName,
                Email = student.Email,
                Lessons = student.StudentLessons.Select(l => new LessonDto
                {
                    Id = l.Lesson.Id,
                    Name = l.Lesson.Name,
                    Description = l.Lesson.Description
                }).ToList()
            };
        }

        public Student Update(Student student)
        {
            _context.Students.Update(student);
            _context.SaveChanges();
            return student;
        }
    }
}