using System.Collections.Generic;
using iqraProject.DTOs;

namespace iqraProject.Interface.IServices
{
    public interface IWordService
    {
        bool AddWord(AddWordRequestModel model);
        bool UpdateWord(UpdateWordRequestModel model, int id);
        void DeleteWord(int id);
        WordDto GetWord (int id);
        List<WordDto> GetAllWord();
        IEnumerable<WordDto> GetWordByLessonId(int lessonId);
    }
}