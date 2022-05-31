using System.Collections.Generic;

namespace iqraProject.Entities
{
    public class User : BaseEntity
    {
        public Admin Admin { get; set; }
        public Teacher Teacher { get; set; }
        public Student Student { get; set; } 
        public string Password { get; set; }
        public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
    }
}