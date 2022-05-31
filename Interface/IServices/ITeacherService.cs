using System.Collections.Generic;
using iqraProject.DTOs;

namespace iqraProject.Interface.IServices
{
    public interface ITeacherService
    {
        BaseResponse<TeacherDto> Create (AddTeacherRequestModel model);
        BaseResponse<TeacherDto> Update (UpdateTeacherRequestModel model , int id);
        BaseResponse<TeacherDto> Delete (int id);
        BaseResponse<TeacherDto> GetByEmail (string email);
        BaseResponse<TeacherDto> ReturnById (int id);
        BaseResponse<IList<TeacherDto>> GetAll ();
    }
}