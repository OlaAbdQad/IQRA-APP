using System.Collections.Generic;

namespace iqraProject.DTOs
{
    public class StudentResultDto
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public int AssessmentId { get; set; }
        public double CorrectMarks { get; set; }
        public double TotalScore { get; set; }
        public int LessonId { get; set; }
        public ICollection<LessonDto> Lessons { get; set; } = new List<LessonDto>();
    }
}