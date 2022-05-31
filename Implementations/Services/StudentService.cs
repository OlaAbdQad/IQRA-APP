using System.Collections.Generic;
using System.Linq;
using iqraProject.DTOs;
using iqraProject.Entities;
using iqraProject.Interface.IRepository;
using iqraProject.Interface.IServices;

namespace iqraProject.Implementations.Services
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly ILessonRepository _lessonRepository;

        public StudentService(IStudentRepository studentRepository, IUserRepository userRepository, IRoleRepository roleRepository, ILessonRepository lessonRepository)
        {
            _studentRepository = studentRepository;
            _userRepository = userRepository;
            _roleRepository = roleRepository;
            _lessonRepository = lessonRepository;
        }

        public BaseResponse<StudentDto> Create(AddStudentRequestModel model)
        {
            var student = _studentRepository.GetByEmail(model.Email);
            if(student != null)
            {
                return new BaseResponse<StudentDto>
                {
                    Message = "Student already exists",
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
            var role = _roleRepository.GetByName("Student");
            var userRole = new UserRole
            {
                User = user,
                UserId = user.Id,
                Role = role,
                RoleId = role.Id
            };
            user.UserRoles.Add(userRole);
            var newStudent = new Student
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                User = user,
                UserId = user.Id
                
            };

            var lesson = _lessonRepository.GetLessonByName("lesson1");
            var studentLesson = new StudentLesson
            {
                Student = newStudent,
                StudentId = newStudent.Id,
                Lesson = lesson,
                LessonId = lesson.Id,  
            };
            newStudent.StudentLessons.Add(studentLesson);

            _userRepository.Create(user);
            var studentInfo = _studentRepository.Create(newStudent);
            return new BaseResponse<StudentDto>
            {
                Message = "Student successfully created",
                IsSuccess  = true,
                Data = studentInfo
            };
        }

        public BaseResponse<StudentDto> GetRecentLesson(int id, string name)
        {
            var student = _studentRepository.GetById(id);
            var lesson = _lessonRepository.GetLessonByName(name);
            var studentLesson = new StudentLesson
            {
                Student = student,
                StudentId = student.Id,
                Lesson = lesson,
                LessonId = lesson.Id,  
            };
            student.StudentLessons.Add(studentLesson);
            return new BaseResponse<StudentDto>
            {
                Message = "Recent Lesson Achieved",
                IsSuccess = true
            };
        }

        public BaseResponse<StudentDto> Delete(int id)
        {
            var student = _studentRepository.GetById(id);
            if(student == null)
            {
                return new BaseResponse<StudentDto>
                {
                    Message = "Student not found",
                    IsSuccess = false
                
                };
            }
            _studentRepository.Delete(student);
            return new BaseResponse<StudentDto>
            {
                Message = "Student successfully Deleted",
                IsSuccess = true
            };
        }

        public BaseResponse<IList<StudentDto>> GetAll()
        {
            var students = _studentRepository.GetAll();
            if(students == null)
            {
                return new BaseResponse<IList<StudentDto>>
                {
                    Message = "No Student Found",
                    IsSuccess = false
                };
            }
            return new BaseResponse<IList<StudentDto>>
            {
                Message = "Student successfully retrieved",
                IsSuccess = true,
                Data = students
            };
        }

        public BaseResponse<StudentDto> GetByEmail(string email)
        {
            var student = _studentRepository.ExistByEmail(email);
            if(student == false)
            {
                return new BaseResponse<StudentDto>
                {
                    Message = "Student doesn't exist",
                    IsSuccess = false
                };
            }
            var newStudent = _studentRepository.GetByEmail(email);
            return new BaseResponse<StudentDto>
            {
                Message = "Student successfully retrieved",
                IsSuccess = true,
                Data = new StudentDto
                {
                    Id = newStudent.Id,
                    FirstName = newStudent.FirstName,
                    LastName = newStudent.LastName,
                    Email = newStudent.Email,
                    Lessons = newStudent.StudentLessons.Select(l => new LessonDto
                    {
                        Id = l.Id,
                        Name = l.Lesson.Name,
                        Description = l.Lesson.Description
                        
                    }).ToList()
                }
            };
        }

        public BaseResponse<StudentDto> ReturnById(int id)
        {
            var student = _studentRepository.ExistById(id);
            if(student == false)
            {
                return new BaseResponse<StudentDto>
                {
                    Message = "Student doesn't exist",
                    IsSuccess = false
                };
            }
            var newStudent = _studentRepository.ReturnById(id);
            return new BaseResponse<StudentDto>
            {
                Message = "Student successfully retrieved",
                IsSuccess = true,
                Data = newStudent
            }; 
        }

        public BaseResponse<StudentDto> Update(UpdateStudentRequestModel model, int id)
        {
            var student = _studentRepository.ExistById(id);
            if(student != true)
            {
                return new BaseResponse<StudentDto>
                {
                    Message = "Student doesn't exist",
                    IsSuccess = false
                };
            }
            var studentInfo = _studentRepository.GetById(id);
            var user = _userRepository.Get(studentInfo.UserId);
            studentInfo.FirstName = model.FirstName ?? studentInfo.FirstName;
            studentInfo.LastName = model.LastName ?? studentInfo.LastName;
            user.Password = model.Password ?? user.Password;
            var newStudent = _studentRepository.Update(studentInfo);
            _userRepository.Update(user);
            return new BaseResponse<StudentDto>
            {
                Message = "Student successfully updated",
                IsSuccess = true,
                Data = new StudentDto
                {
                    FirstName = newStudent.FirstName,
                    LastName = newStudent.LastName,
                    Email = newStudent.Email
                }
            };
        }
    }
}