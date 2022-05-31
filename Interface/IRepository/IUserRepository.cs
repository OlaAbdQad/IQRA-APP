using System.Collections.Generic;
using iqraProject.DTOs;
using iqraProject.Entities;

namespace iqraProject.Interface.IRepository
{
    public interface IUserRepository
    {
        UserDto Create (User user);
        UserDto Update (User user);
        User Get (int id);
        bool ExistByEmail(string email);
        public bool ExistById(int id);
        void Delete (User user);
        User GetByEmail (string email);
        UserDto ReturnById (int id);
        List<UserDto> GetAll ();
    }
}