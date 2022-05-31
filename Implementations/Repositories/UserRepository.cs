using System.Collections.Generic;
using System.Linq;
using iqraProject;
using iqraProject.DTOs;
using iqraProject.Entities;
using iqraProject.Interface.IRepository;
using Microsoft.EntityFrameworkCore;

namespace iqraProject.Implementation.Repositories
{
    public class UserRepository : IUserRepository
    {
          private readonly ArabicContext _context;

        public UserRepository(ArabicContext context)
        {
            _context = context;
        }

        public UserDto Create(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
            return new UserDto
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email
            };
        }

        public void Delete(User user)
        {
            _context.Users.Remove(user);
            _context.SaveChanges();
        }

        public bool ExistByEmail(string email)
        {
            return _context.Users.Any(y => y.Email == email);
        }

        public bool ExistById(int id)
        {
            return _context.Users.Any(t => t.Id == id);
        }

        public User Get(int id)
        {
            return _context.Users.FirstOrDefault(a => a.Id == id);
        }

        public List<UserDto> GetAll()
        {
            return _context.Users.Select(user => new UserDto
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email
            }).ToList();
        }

        public User GetByEmail(string email)
        {
            return _context.Users.Include(u => u.UserRoles).ThenInclude(r => r.Role).FirstOrDefault(a => a.Email == email);
        }

        public UserDto ReturnById(int id)
        {
            var user = _context.Users.Include(u => u.UserRoles).ThenInclude(r => r.Role).FirstOrDefault(a => a.Id == id);
            return new UserDto
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Roles = user.UserRoles.Select(r => new RoleDto
                {
                    Id = r.RoleId,
                    Name = r.Role.Name,
                    Description = r.Role.Description
                }).ToList()
            };
        }

        public UserDto Update(User user)
        {
            _context.Users.Update(user);
            _context.SaveChanges();
            return new UserDto
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email
            };
        }
    }
}