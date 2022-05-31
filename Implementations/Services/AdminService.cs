using System.Collections.Generic;
using iqraProject.DTOs;
using iqraProject.Entities;
using iqraProject.Interface.IRepository;
using iqraProject.Interface.IServices;

namespace iqraProject.Implementations.Services
{
    public class AdminService : IAdminService
    {
        private readonly IAdminRepository _adminRepository;
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;

        public AdminService(IAdminRepository adminRepository, IRoleRepository roleRepository, IUserRepository userRepository)
        {
            _adminRepository = adminRepository;
            _roleRepository = roleRepository;
            _userRepository = userRepository;
        }

        public BaseResponse<AdminDto> Create(AddAdminRequestModel model)
        {
            var admin = _adminRepository.ExistByEmail(model.Email);
            if(admin != false)
            {
                return new BaseResponse<AdminDto>
                {
                    Message = "Admin already exists",
                    IsSuccess = false
                };
            }
            var user = new User
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Password = model.Password,
                Email = model.Email
            };
             var userInfo =  _userRepository.Create(user);
            var role = _roleRepository.GetByName("Admin");
            var userRole = new UserRole
            {
                User = user,
                UserId = user.Id,
                Role = role,
                RoleId = role.Id
            };
            user.UserRoles.Add(userRole);
            var newAdmin = new Admin
            {
              FirstName = model.FirstName,
              LastName = model.LastName,
              Email = model.Email,
                User = user,
                UserId = user.Id
                
            };

            var adminInfo = _adminRepository.Create(newAdmin);
            return new BaseResponse<AdminDto>
            {
                Message = "Admin successfully created",
                IsSuccess  = true,
                Data = adminInfo
            };
        }

        public BaseResponse<AdminDto> Delete(int id)
        {
            var admin = _adminRepository.GetById(id);
            if(admin == null)
            {
                return new BaseResponse<AdminDto>
                {
                    Message = "Admin not found",
                    IsSuccess = false
                
                };
            }
            _adminRepository.Delete(admin);
            return new BaseResponse<AdminDto>
            {
                Message = "Admin successfully Deleted",
                IsSuccess = true
            };
        }

        public BaseResponse<IList<AdminDto>> GetAll()
        {
            var admins = _adminRepository.GetAll();
            if(admins == null)
            {
                return new BaseResponse<IList<AdminDto>>
                {
                    Message = "No admin Found",
                    IsSuccess = false
                };
            }
            return new BaseResponse<IList<AdminDto>>
            {
                Message = "Admin successfully retrieved",
                IsSuccess = true,
                Data = admins
            };
        }

        public BaseResponse<AdminDto> GetByEmail(string email)
        {
            var admin = _adminRepository.ExistByEmail(email);
            if(admin == false)
            {
                return new BaseResponse<AdminDto>
                {
                    Message = "Admin doesn't exist",
                    IsSuccess = false
                };
            }
            var newAdmin = _adminRepository.GetByEmail(email);
            return new BaseResponse<AdminDto>
            {
                Message = "Admin successfully retrieved",
                IsSuccess = true,
                Data = new AdminDto
                {
                    Id = newAdmin.Id,
                    FirstName = newAdmin.FirstName,
                    LastName = newAdmin.LastName,
                    Email = newAdmin.Email
                }
            };
        }

        public BaseResponse<AdminDto> ReturnById(int id)
        {
            var admin = _adminRepository.ExistById(id);
           if(admin == false)
            {
                return new BaseResponse<AdminDto>
                {
                    Message = "Admin doesn't exist",
                    IsSuccess = false
                };
            }
            var newAdmin = _adminRepository.ReturnById(id);
            return new BaseResponse<AdminDto>
            {
                Message = "Admin successfully retrieved",
                IsSuccess = true,
                Data = newAdmin
               
            }; 
        }

        public BaseResponse<AdminDto> Update(UpdateAdminRequestModel model, int id)
        {
            var admin = _adminRepository.ExistById(id);
            if(admin != true)
            {
                return new BaseResponse<AdminDto>
                {
                    Message = "Admin doesn't exist",
                    IsSuccess = false
                };
            }
            var adminInfo = _adminRepository.GetById(id);
            var user = _userRepository.Get(adminInfo.UserId);
            adminInfo.FirstName = model.FirstName ?? adminInfo.FirstName;
            adminInfo.LastName =  model.LastName ?? adminInfo.LastName;
            user.Password = model.Password ?? user.Password;
            var newAdmin = _adminRepository.Update(adminInfo);
            _userRepository.Update(user);
            return new BaseResponse<AdminDto>
            {
                Message = "Admin successfully updated",
                IsSuccess = true,
                Data = new AdminDto
                {
                    FirstName = newAdmin.FirstName,
                    LastName = newAdmin.LastName,
                    Email = newAdmin.Email
                }
            };
        }
    }
}