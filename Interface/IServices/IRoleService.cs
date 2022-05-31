using System.Collections.Generic;
using iqraProject.DTOs;

namespace iqraProject.Interface.IServices
{
    public interface IRoleService
    {
        BaseResponse<RoleDto> Create (AddRoleRequestModel model);
        BaseResponse<RoleDto> Update (UpdateRoleRequestModel model , int id);
        BaseResponse<RoleDto> Delete (int id);
        BaseResponse<RoleDto> GetByEmail (string email);
        BaseResponse<RoleDto> ReturnById (int id);
        BaseResponse<IList<RoleDto>> GetAll ();
    }
}