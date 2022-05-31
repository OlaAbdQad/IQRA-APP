using System.Collections.Generic;
using iqraProject.Enums;

namespace iqraProject.Entities
{
    public class Question
    {
        public int Id { get; set; }
        public int AssessmentId { get; set; }
        public Assessment Assessment { get; set; }
        public string AudioTest { get; set; }
        public string TextTest { get; set; }
        public QuestionType QuestionType { get; set; }
        public ICollection<Option> Options { get; set; } = new List<Option>();
    }
}