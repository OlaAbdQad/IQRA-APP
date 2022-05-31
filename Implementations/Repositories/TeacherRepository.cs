using System.Collections.Generic;
using System.Linq;
using iqraProject;
using iqraProject.DTOs;
using iqraProject.Entities;
using iqraProject.Interface.IRepository;

namespace iqraProject.Implementation.Repositories
{
    public class TeacherRepository : ITeacherRepository
    {
        private readonly ArabicContext _context;

        public TeacherRepository(ArabicContext context)
        {
            _context = context;
        }

        public TeacherDto Create(Teacher teacher)
        {
            _context.Teachers.Add(teacher);
            _context.SaveChanges();
            return new TeacherDto
            {
                Id = teacher.Id,
                FirstName = teacher.FirstName,
                LastName = teacher.LastName,
                Email = teacher.Email
            };
        }

        public void Delete(Teacher teacher)
        {
            _context.Teachers.Remove(teacher);
            _context.SaveChanges();
        }

        public bool ExistByEmail(string email)
        {
            return _context.Teachers.Any(t => t.Email == email);
        }

        public bool ExistById(int id)
        {
            return _context.Teachers.Any(t => t.Id == id);
        }

        public List<TeacherDto> GetAll()
        {
            return _context.Teachers.Select(teacher => new TeacherDto
            {
                Id = teacher.Id,
                FirstName = teacher.FirstName,
                LastName = teacher.LastName,
                Email = teacher.Email
            }).ToList();
        }

        public Teacher GetByEmail(string email)
        {
            return _context.Teachers.FirstOrDefault(a => a.Email == email);
        }

        public Teacher GetById(int id)
        {
            return _context.Teachers.FirstOrDefault(g => g.Id == id);
        }

        public TeacherDto ReturnById(int id)
        {
            var teacher = _context.Teachers.FirstOrDefault(a => a.Id == id);
            return new TeacherDto
            {
                Id = teacher.Id,
                FirstName = teacher.FirstName,
                LastName = teacher.LastName,
                Email = teacher.Email
            };
        }

        public Teacher Update(Teacher teacher)
        {
            _context.Teachers.Update(teacher);
            _context.SaveChanges();
            return teacher;
        }
    }
}