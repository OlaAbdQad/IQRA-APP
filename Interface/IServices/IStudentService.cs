using System.Collections.Generic;
using iqraProject.DTOs;

namespace iqraProject.Interface.IServices
{
    public interface IStudentService
    {
        BaseResponse<StudentDto> Create (AddStudentRequestModel model);
        BaseResponse<StudentDto> Update (UpdateStudentRequestModel model , int id);
        BaseResponse<StudentDto> Delete (int id);
        BaseResponse<StudentDto> GetByEmail (string email);
        BaseResponse<StudentDto> ReturnById (int id);
        BaseResponse<IList<StudentDto>> GetAll ();
        BaseResponse<StudentDto> GetRecentLesson(int id, string name);
    }
}