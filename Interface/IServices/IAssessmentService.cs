using System.Collections.Generic;
using iqraProject.DTOs;

namespace iqraProject.Interface.IServices
{
    public interface IAssessmentService
    {
        BaseResponse<AssessmentDto> AddAssessment(AddAssessmentRequestModel model);
        BaseResponse<AssessmentDto> UpdateAssessment(UpdateAssessmentRequestModel model, int id);
        BaseResponse<AssessmentDto> DeleteAssessment(int id);
        BaseResponse<AssessmentDto> GetAssessment (int id);
        BaseResponse<IEnumerable<AssessmentDto>> GetAllAssessment();
        BaseResponse<IEnumerable<AssessmentDto>> GetAssessmentByLessonId(int lessonId);
    }
}