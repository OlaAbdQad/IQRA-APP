using System.Collections.Generic;
using iqraProject.Entities;

namespace iqraProject.DTOs
{
    public class RoleDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<User> UserRoles { get; set; } = new List<User>();
    }

    public class AddRoleRequestModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
    public class UpdateRoleRequestModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}