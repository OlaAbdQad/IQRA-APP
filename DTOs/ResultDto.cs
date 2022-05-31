using System.Collections.Generic;

namespace iqraProject.DTOs
{
    public class ResultDto
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public int AssessmentId { get; set; }
        public double TotalScore { get; set; }
        public double CorrectMarks { get; set; }
        
    }

    public class ResultRequestModel
    {
        public string StudentEmail { get; set; }
        public int AssessmentId { get; set; }
        public List<int> OptionsIds { get; set; } = new List<int>();
    }
}


