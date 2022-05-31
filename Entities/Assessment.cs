using System.Collections.Generic;

namespace iqraProject.Entities
{
    public class Assessment
    {
        public int Id { get; set; }
        public int LessonId { get; set; }
        public Lesson Lesson { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<Question> Questions { get; set; }  = new List<Question>();
        public ICollection<Result> Results { get; set; }  = new List<Result>();
    }
}