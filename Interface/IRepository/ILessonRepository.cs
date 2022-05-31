using System.Collections.Generic;
using iqraProject.DTOs;
using iqraProject.Entities;


namespace iqraProject.Interface.IRepository
{
    public interface ILessonRepository
    {
        Lesson Create(Lesson lesson);
        Lesson Update(Lesson lesson);
        void Delete(Lesson lesson);
        Lesson Get(int id);
        List<LessonDto> GetAll();
        LessonDto Return(int id);
        Lesson GetLessonByName(string name);
    }
}