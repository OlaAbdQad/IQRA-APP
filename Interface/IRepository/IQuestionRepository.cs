using System.Collections.Generic;
using iqraProject.DTOs;
using iqraProject.Entities;

namespace iqraProject.Interface.IRepository
{
    public interface IQuestionRepository
    {
        Question Create(Question question);
        Question Update(Question question);
        void Delete(Question question);
        Question Get(int id);
        Question GetByTextTest(string textTest);
        List<QuestionDto> GetAll();
        QuestionDto Return(int id);
        IEnumerable<QuestionDto> GetByAssessmentId(int assessmentId);
    }
}