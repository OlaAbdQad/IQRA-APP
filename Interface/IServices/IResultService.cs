using iqraProject.DTOs;
using iqraProject.Entities;

namespace iqraProject.Interface.IServices
{
    public interface IResultService
    {
        BaseResponse<StudentResultDto> GenerateResult(ResultRequestModel model);
        StudentDto CheckResult(double correctMarks, int studentId, int lessonId);
    }
}