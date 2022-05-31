using System.Collections.Generic;
using iqraProject.DTOs;

namespace iqraProject.Interface.IServices
{
    public interface ILessonService
    {
        bool AddLesson(AddLessonRequestModel model);
        bool UpdateLesson(UpdateLessonRequestModel model, int id);
        void DeleteLesson(int id);
        LessonDto GetLesson (int id);
        List<LessonDto> GetAllLesson();
    }
}