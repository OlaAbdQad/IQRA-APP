using System.Collections.Generic;
using iqraProject.DTOs;
using iqraProject.Entities;

namespace iqraProject.Interface.IRepository
{
    public interface IStudentRepository
    {
        StudentDto Create (Student student);
        bool ExistByEmail(string email);
        public bool ExistById(int id);
        Student Update (Student student);
        void Delete (Student student);
        Student GetById (int id);
        Student GetByEmail (string email);
        StudentDto ReturnById (int id);
        List<StudentDto> GetAll ();
    }
}