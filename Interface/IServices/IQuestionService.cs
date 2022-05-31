using System.Collections.Generic;
using iqraProject.DTOs;

namespace iqraProject.Interface.IServices
{
    public interface IQuestionService
    {
        BaseResponse<QuestionDto> AddQuestion(AddQuestionRequestModel model);
        BaseResponse<QuestionDto> UpdateQuestion(UpdateQuestionRequestModel model, int id);
        BaseResponse<QuestionDto> DeleteQuestion(int id);
        BaseResponse<QuestionDto> GetQuestion (int id);
        BaseResponse<IEnumerable<QuestionDto>> GetAllQuestion();
        BaseResponse<IEnumerable<QuestionDto>> GetQuestionByAssessmentId(int assessmentId);
    }
}