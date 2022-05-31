using System.Collections.Generic;

namespace iqraProject.DTOs
{
    public class StudentDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public ICollection<LessonDto> Lessons { get; set; } = new List<LessonDto>();
        public ICollection<ResultDto> Results { get; set; } = new List<ResultDto>();
    }

     public class AddStudentRequestModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class UpdateStudentRequestModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
    }
}