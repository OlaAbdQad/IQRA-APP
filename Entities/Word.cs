namespace iqraProject.Entities
{
    public class Word
    {
        public int Id { get; set; }
        public int LessonId { get; set; }
        public Lesson Lesson { get; set; }
        public string WordName { get; set; }
        public string Symbol { get; set; }
        public string Sound { get; set; }
        public string Audio { get; set; }
    }
}