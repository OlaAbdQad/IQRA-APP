namespace iqraProject.Entities
{
    public class Teacher : BaseEntity
    {
        public User User { get; set; }
        public int UserId { get; set; }
    }
}