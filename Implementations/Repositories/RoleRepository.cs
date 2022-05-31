using System.Collections.Generic;
using System.Linq;
using iqraProject;
using iqraProject.DTOs;
using iqraProject.Entities;
using iqraProject.Interface.IRepository;

namespace iqraProject.Implementation.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private readonly ArabicContext _context;

        public RoleRepository(ArabicContext context)
        {
            _context = context;
        }

        public Role Create(Role role)
        {
            _context.Roles.Add(role);
            _context.SaveChanges();
            return role;
        }

        public void Delete(Role role)
        {
            _context.Roles.Remove(role);
            _context.SaveChanges();
        }

        public bool ExistByName(string name)
        {
            return _context.Roles.Any(e => e.Name == name);
        }

        public Role Get(int id)
        {
            return _context.Roles.FirstOrDefault(a => a.Id == id);
        }

        public List<RoleDto> GetAll()
        {
            return _context.Roles.Select(role => new RoleDto
            {
                Id = role.Id,
                Name = role.Name,
                Description = role.Description
            }).ToList();
        }

        public bool ExistById(int id)
        {
            return _context.Roles.Any(e => e.Id == id);
        }

        public Role GetByName(string name)
        {
            return _context.Roles.FirstOrDefault(a => a.Name == name);
        }

        public RoleDto ReturnById(int id)
        {
            var role = _context.Roles.FirstOrDefault(r => r.Id == id);
            return new RoleDto
            {
                Id = role.Id,
                Name = role.Name,
                Description = role.Description
            };
        }

        public Role Update(Role role)
        {
            _context.Roles.Add(role);
            _context.SaveChanges();
            return role;
        }

        
    

    }
}