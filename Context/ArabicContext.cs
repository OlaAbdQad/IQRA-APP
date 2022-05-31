using iqraProject.Entities;
using Microsoft.EntityFrameworkCore;


namespace iqraProject
{
    public class ArabicContext : DbContext
    {
        
        public ArabicContext(DbContextOptions<ArabicContext> option)  : base(option)
       {
       }

       public DbSet<Lesson> Lessons { get; set; }
       public DbSet<Word> Words { get; set; }
       public DbSet<Assessment> Assessments { get; set; }
       public DbSet<Question> Questions { get; set; }
       public DbSet<Option> Options { get; set; }
       public DbSet<User> Users { get; set; }
       public DbSet<Admin> Admins { get; set; }
       public DbSet<Student> Students { get; set; }
       public DbSet<Teacher> Teachers { get; set; }
       public DbSet<Role> Roles { get; set; }
       public DbSet<UserRole> UserRoles { get; set; }
       public DbSet<Result> Results { get; set; }


    }
}