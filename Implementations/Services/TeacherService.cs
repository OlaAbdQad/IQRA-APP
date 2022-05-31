using System.Collections.Generic;
using iqraProject.DTOs;
using iqraProject.Entities;
using iqraProject.Interface.IRepository;
using iqraProject.Interface.IServices;

namespace iqraProject.Implementations.Services
{
    public class TeacherService : ITeacherService
    {
        private readonly ITeacherRepository _teacherRepository;
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;

        public TeacherService(ITeacherRepository teacherRepository, IUserRepository userRepository, IRoleRepository roleRepository)
        {
            _teacherRepository = teacherRepository;
            _userRepository = userRepository;
            _roleRepository = roleRepository;
        }

        public BaseResponse<TeacherDto> Create(AddTeacherRequestModel model)
        {
            var teacher = _teacherRepository.ExistByEmail(model.Email);
            if(teacher != false)
            {
                return new BaseResponse<TeacherDto>
                {
                    Message = "Teacher already exists",
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
            var role = _roleRepository.GetByName("Teacher");
            var userRole = new UserRole
            {
                User = user,
                UserId = user.Id,
                Role = role,
                RoleId = role.Id
            };
            user.UserRoles.Add(userRole);
            var newTeacher = new Teacher
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                User = user,
                UserId = user.Id
                
            };
            // _userRepository.Create(user);
            var teacherInfo = _teacherRepository.Create(newTeacher);
            return new BaseResponse<TeacherDto>
            {
                Message = "Teacher successfully created",
                IsSuccess  = true,
                Data = teacherInfo
            };
        }

        public BaseResponse<TeacherDto> Delete(int id)
        {
            var teacher = _teacherRepository.GetById(id);
            if(teacher == null)
            {
                return new BaseResponse<TeacherDto>
                {
                    Message = "Teacher not found",
                    IsSuccess = false
                
                };
            }
            _teacherRepository.Delete(teacher);
            return new BaseResponse<TeacherDto>
            {
                Message = "Teacher successfully Deleted",
                IsSuccess = true
            };
        }

        public BaseResponse<IList<TeacherDto>> GetAll()
        {
            var teachers = _teacherRepository.GetAll();
            if(teachers == null)
            {
                return new BaseResponse<IList<TeacherDto>>
                {
                    Message = "No Teacher Found",
                    IsSuccess = false
                };
            }
            return new BaseResponse<IList<TeacherDto>>
            {
                Message = "Teacher successfully retrieved",
                IsSuccess = true,
                Data = teachers
            };
        }

        public BaseResponse<TeacherDto> GetByEmail(string email)
        {
            var teacher = _teacherRepository.ExistByEmail(email);
            if(teacher == false)
            {
                return new BaseResponse<TeacherDto>
                {
                    Message = "Teacher doesn't exist",
                    IsSuccess = false
                };
            }
            var newTeacher = _teacherRepository.GetByEmail(email);
            return new BaseResponse<TeacherDto>
            {
                Message = "Teacher successfully retrieved",
                IsSuccess = true,
                Data = new TeacherDto
                {
                    Id = newTeacher.Id,
                    FirstName = newTeacher.FirstName,
                    LastName = newTeacher.LastName,
                    Email = newTeacher.Email
                }
            };
        }

        public BaseResponse<TeacherDto> ReturnById(int id)
        {
            var teacher = _teacherRepository.ExistById(id);
            if(teacher == false)
            {
                return new BaseResponse<TeacherDto>
                {
                    Message = "Teacher doesn't exist",
                    IsSuccess = false
                };
            }
            var newTeacher = _teacherRepository.ReturnById(id);
            return new BaseResponse<TeacherDto>
            {
                Message = "Teacher successfully retrieved",
                IsSuccess = true,
                Data = newTeacher
            }; 
        }

        public BaseResponse<TeacherDto> Update(UpdateTeacherRequestModel model, int id)
        { 
            var teacher = _teacherRepository.ExistById(id);
            if(teacher == false)
            {
                return new BaseResponse<TeacherDto>
                {
                    Message = "Teacher doesn't exist",
                    IsSuccess = false
                };
            }
            var teacherInfo = _teacherRepository.GetById(id);
            var user = _userRepository.Get(teacherInfo.UserId);
            teacherInfo.FirstName = model.FirstName ?? teacherInfo.FirstName;
            teacherInfo.LastName = model.LastName ?? teacherInfo.LastName;
            user.Password = model.Password ?? user.Password;
            var newTeacher = _teacherRepository.Update(teacherInfo);
            _userRepository.Update(user);
            return new BaseResponse<TeacherDto>
            {
                Message = "Teacher successfully updated",
                IsSuccess = true,
                Data = new TeacherDto
                {
                    FirstName = newTeacher.FirstName,
                    LastName = newTeacher.LastName,
                    Email = newTeacher.Email
                }
            };
        }
    }
}