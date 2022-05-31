using System.Collections.Generic;

namespace iqraProject.DTOs
{
    public class LessonDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<WordDto> Words { get; set; } = new List<WordDto>();
        public ICollection<AssessmentDto> Assessments { get; set; } = new List<AssessmentDto>();
        public ICollection<StudentDto> Students { get; set; } = new List<StudentDto>();
    }

    public class AddLessonRequestModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
    public class UpdateLessonRequestModel
    {
        public string Name { get; set; }
        public string Description { get; set; }

    }
}