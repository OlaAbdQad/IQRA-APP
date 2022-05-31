using System.Collections.Generic;
using iqraProject.DTOs;

namespace iqraProject.Interface.IServices
{
    public interface IAdminService
    {
      BaseResponse<AdminDto> Create (AddAdminRequestModel model);
      BaseResponse<AdminDto> Update (UpdateAdminRequestModel model , int id);
      BaseResponse<AdminDto> Delete (int id);
      BaseResponse<AdminDto> GetByEmail (string email);
      BaseResponse<AdminDto> ReturnById (int id);
      BaseResponse<IList<AdminDto>> GetAll ();
    }
}