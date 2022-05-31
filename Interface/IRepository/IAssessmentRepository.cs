using System.Collections.Generic;
using iqraProject.DTOs;
using iqraProject.Entities;

namespace iqraProject.Interface.IRepository
{
    public interface IAssessmentRepository
    {
        Assessment Create(Assessment assessment);
        Assessment Update(Assessment assessment);
        void Delete(Assessment assessment);
        Assessment Get(int id);
        Assessment GetByName(string name);
        IEnumerable<AssessmentDto> GetAll();
        AssessmentDto Return(int id);
        IEnumerable<AssessmentDto> GetByLessonId(int lessonId);
    }
}