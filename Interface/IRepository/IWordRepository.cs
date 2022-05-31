using System.Collections.Generic;
using iqraProject.DTOs;
using iqraProject.Entities;

namespace iqraProject.Interface.IRepository
{
    public interface IWordRepository
    {
        Word Create(Word word);
        Word Update(Word word);
        void Delete(Word word);
        Word Get(int id);
        List<WordDto> GetAll();
        WordDto Return(int id);
        IEnumerable<WordDto> GetByLessonId(int lessonId);
    }
}