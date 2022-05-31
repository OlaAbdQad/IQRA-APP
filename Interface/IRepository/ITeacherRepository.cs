using System.Collections.Generic;
using iqraProject.DTOs;
using iqraProject.Entities;

namespace iqraProject.Interface.IRepository
{
    public interface ITeacherRepository
    {
        TeacherDto Create (Teacher teacher);
        bool ExistByEmail(string email);
        public bool ExistById(int id);
        Teacher Update (Teacher teacher);
        void Delete (Teacher teacher);
        Teacher GetById (int id);
        Teacher GetByEmail (string email);
        TeacherDto ReturnById (int id);
        List<TeacherDto> GetAll ();
    }
}