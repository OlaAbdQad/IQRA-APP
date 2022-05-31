using System.Collections.Generic;
using iqraProject.DTOs;
using iqraProject.Entities;

namespace iqraProject.Interface.IRepository
{
    public interface IAdminRepository
    {
        AdminDto Create (Admin admin);
        bool ExistByEmail(string email);
        public bool ExistById(int id);
        Admin Update (Admin admin);
        void Delete (Admin admin);
        Admin GetById (int id);
        Admin GetByEmail (string email);
        AdminDto ReturnById (int id);
        List<AdminDto> GetAll ();
    }
}