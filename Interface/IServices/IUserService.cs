using System.Collections.Generic;
using iqraProject.DTOs;

namespace iqraProject.Interface.IServices
{
    public interface IUserService
    {
        BaseResponse<UserDto> ReturnById (int id);
        BaseResponse<IList<UserDto>> GetAll ();
        BaseResponse<UserDto> LogInUser(UserLoginDto model);
        BaseResponse<bool> DeleteUser(int id);
    }
}