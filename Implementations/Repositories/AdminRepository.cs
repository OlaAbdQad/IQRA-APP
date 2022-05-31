using System.Collections.Generic;
using System.Linq;
using iqraProject;
using iqraProject.DTOs;
using iqraProject.Entities;
using iqraProject.Interface.IRepository;

namespace iqraProject.Implementation.Repositories
{
    public class AdminRepository : IAdminRepository
    {
        private readonly ArabicContext _context;

        public AdminRepository(ArabicContext context)
        {
            _context = context;
        }

         public AdminDto Create(Admin admin)
        {
            _context.Admins.Add(admin);
            _context.SaveChanges();
            return new AdminDto
            {
                Id = admin.Id,
                FirstName = admin.FirstName,
                LastName = admin.LastName,
                Email = admin.Email
            };
        }

        public bool ExistByEmail(string email)
        {
            return _context.Admins.Any(e => e.Email == email);
        }

        public bool ExistById(int id)
        {
            return _context.Admins.Any(e => e.Id == id);
        }

        public Admin Update(Admin admin)
        {
            _context.Admins.Update(admin);
            _context.SaveChanges();
            return admin;
        }

        public void Delete(Admin admin)
        {
            _context.Admins.Remove(admin);
            _context.SaveChanges();
        }

        public Admin GetById(int id)
        {
            return _context.Admins.FirstOrDefault(a => a.Id == id);
          
        }

        public Admin GetByEmail(string email)
        {
            return _context.Admins.FirstOrDefault(a => a.Email == email);
         
        }

        public AdminDto ReturnById(int id)
        {
            var admin = _context.Admins.FirstOrDefault(a => a.Id == id);
            return new AdminDto
            {
                 Id = admin.Id,
                FirstName = admin.FirstName,
                LastName = admin.LastName,
                Email = admin.Email
            };
        }

        public List<AdminDto> GetAll()
        {
            return _context.Admins.Select(admin => new AdminDto
            {
                Id = admin.Id,
                FirstName = admin.FirstName,
                LastName = admin.LastName,
                Email = admin.Email
            }).ToList();
        }
    }
}