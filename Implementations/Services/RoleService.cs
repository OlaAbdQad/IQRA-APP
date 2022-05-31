using System.Collections.Generic;
using iqraProject.DTOs;
using iqraProject.Entities;
using iqraProject.Interface.IRepository;
using iqraProject.Interface.IServices;

namespace iqraProject.Implementations.Services
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _roleRepository;

        public RoleService(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        public BaseResponse<RoleDto> Create(AddRoleRequestModel model)
        {
            var role = _roleRepository.GetByName(model.Name);
            
            if (role != null)
            {
                return new BaseResponse<RoleDto>
                {
                    IsSuccess = false,
                    Message = "Role already exists"
                };
            }
            
            var newRole = new Role
            {
                Name = model.Name,
                Description = model.Description
            };
            
            _roleRepository.Create(newRole);
            
            return new BaseResponse<RoleDto>
            {
                IsSuccess = true,
                Message = "User created successfully"
            };
        }

        public BaseResponse<RoleDto> Delete(int id)
        {
            var role = _roleRepository.Get(id);
            if(role == null)
            {
                return new BaseResponse<RoleDto>
                {
                    Message = "Role not found",
                    IsSuccess = false
                
                };
            }
            _roleRepository.Delete(role);
            return new BaseResponse<RoleDto>
            {
                Message = "Role successfully Deleted",
                IsSuccess = true
            };
        }

        public BaseResponse<IList<RoleDto>> GetAll()
        {
             var role = _roleRepository.GetAll();
            if(role == null)
            {
                return new BaseResponse<IList<RoleDto>>
                {
                    Message = "No Role Found",
                    IsSuccess = false
                };
            }
            return new BaseResponse<IList<RoleDto>>
            {
                Message = "Role successfully retrieved",
                IsSuccess = true,
                Data = role
            };
        }

        public BaseResponse<RoleDto> GetByEmail(string email)
        {
            throw new System.NotImplementedException();
        }

        public BaseResponse<RoleDto> ReturnById(int id)
        {
            if(!(_roleRepository.ExistById(id)))
            {
                return new BaseResponse<RoleDto>
                {
                    Message = "Role doesn't exist",
                    IsSuccess = false
                };
            }
            var role = _roleRepository.ReturnById(id);
            return new BaseResponse<RoleDto>
            {
                Message = "Role successfully retrieved",
                IsSuccess = true,
                Data = role
            }; 
        }

        public BaseResponse<RoleDto> Update(UpdateRoleRequestModel model, int id)
        {
            var role = _roleRepository.Get(id);
            if(role == null)
            {
                return new BaseResponse<RoleDto>
                {
                    Message = "Role doesn't exist",
                    IsSuccess = false
                };
            }

            role.Name = model.Name ?? role.Name;
            role.Description = model.Description ?? role.Description;
            _roleRepository.Update(role);
            return new BaseResponse<RoleDto>
            {
                Message = "Role successfully updated",
                IsSuccess = true,
                Data = new RoleDto
                {
                    Name = role.Name,
                    Description = role.Description
                }
            };
        }
    }
}