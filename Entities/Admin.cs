namespace iqraProject.Entities
{
    public class Admin : BaseEntity
    {
        public User User {get;set;}
        public int UserId {get;set;}
    }
}