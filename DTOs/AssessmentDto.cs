using System.Collections.Generic;

namespace iqraProject.DTOs
{
    public class AssessmentDto
    {
        public int Id { get; set; }
        public int LessonId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<QuestionDto> Questions { get; set; }  = new List<QuestionDto>();
        public ICollection<ResultDto> Results { get; set; }  = new List<ResultDto>();
    }

    public class AddAssessmentRequestModel
    {
        public int LessonId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }

    public class UpdateAssessmentRequestModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}