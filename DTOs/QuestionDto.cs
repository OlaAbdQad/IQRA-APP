using System.Collections.Generic;
using iqraProject.Enums;

namespace iqraProject.DTOs
{
    public class QuestionDto
    {
        public int Id { get; set; }
        public int AssessmentId { get; set; }
        public string AudioTest { get; set; }
        public string TextTest { get; set; }
        public QuestionType QuestionType { get; set; }
        public ICollection<OptionDto> Options { get; set; } = new List<OptionDto>();
    }

    public class AddQuestionRequestModel
    {
        public int AssessmentId { get; set; }
        public string AudioTest { get; set; }
        public string TextTest { get; set; }
        public QuestionType QuestionType { get; set; }
    }
    public class UpdateQuestionRequestModel
    {
        public string AudioTest { get; set; }
        public string TextTest { get; set; }
        public QuestionType QuestionType { get; set; }
    }
}