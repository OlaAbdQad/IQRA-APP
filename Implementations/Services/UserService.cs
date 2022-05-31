using System.Collections.Generic;
using System.Linq;
using iqraProject.DTOs;
using iqraProject.Entities;
using iqraProject.Interface.IRepository;
using iqraProject.Interface.IServices;

namespace iqraProject.Implementations.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public BaseResponse<IList<UserDto>> GetAll()
        {
            var users = _userRepository.GetAll();
            if(users == null)
            {
                return new BaseResponse<IList<UserDto>>
                {
                    Message = "No User Found",
                    IsSuccess = false
                };
            }
            return new BaseResponse<IList<UserDto>>
            {
                Message = "User successfully retrieved",
                IsSuccess = true,
                Data = users
            };
        }

        public BaseResponse<UserDto> LogInUser(UserLoginDto model)
        {
            var user = _userRepository.GetByEmail(model.Email);
            if(user == null || user.Password != model.Password)
            {
                return new BaseResponse<UserDto>
                {
                    Message = "Email or Password is not correct",
                    IsSuccess = false,
                };
            }
            return new BaseResponse<UserDto>
            {
                Message = "User successfully logged in",
                IsSuccess = true,
                Data = new UserDto
                {
                    Email = user.Email,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Roles = user.UserRoles.Select(u => new RoleDto
                    {
                        Name = u.Role.Name
                    }).ToList()
                }
            };
        }

        public BaseResponse<UserDto> ReturnById(int id)
        {
            if(!(_userRepository.ExistById(id)))
            {
                return new BaseResponse<UserDto>
                {
                    Message = "User doesn't exist",
                    IsSuccess = false
                };
            }
            var user = _userRepository.ReturnById(id);
            return new BaseResponse<UserDto>
            {
                Message = "User successfully retrieved",
                IsSuccess = true,
                Data = user
            }; 
        }


        public BaseResponse<bool> DeleteUser(int id)
        {
             var user = _userRepository.Get(id);
            if (user == null)
            {
                return new BaseResponse<bool>
                {
                    Message = "User not found",
                    IsSuccess = false
                };
            }
            _userRepository.Delete(user);
            return new BaseResponse<bool>
            {
                Message = "User successfully Deleted",
                IsSuccess = true,
            };
        }
    }
}