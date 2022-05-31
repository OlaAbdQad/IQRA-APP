using System.Collections.Generic;

namespace iqraProject.Entities
{
    public class Lesson
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<Word> Words { get; set; } = new List<Word>();
        public ICollection<Assessment> Assessments { get; set; } = new List<Assessment>();
        public ICollection<StudentLesson> StudentLessons { get; set; } = new List<StudentLesson>();
    }
}