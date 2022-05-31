using System.Collections.Generic;

namespace iqraProject.Entities
{
    public class Student : BaseEntity
    {
        public User User {get;set;}
        public int UserId {get;set;}
        public ICollection<Result> Results { get; set; } = new List<Result>();
        public ICollection<StudentLesson> StudentLessons { get; set; } = new List<StudentLesson>();
    }
}