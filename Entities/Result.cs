namespace iqraProject.Entities
{
    public class Result
    {
        public int Id { get; set; }
        public int AssessmentId { get; set; }
        public Assessment Assessment { get; set; }
        public int StudentId { get; set; }
        public Student Student { get; set; }
        public double TotalScore { get; set; }
        public double CorrectMarks { get; set; }
    }
}