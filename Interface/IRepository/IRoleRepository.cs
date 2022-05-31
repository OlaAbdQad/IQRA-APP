using System.Collections.Generic;
using iqraProject.DTOs;
using iqraProject.Entities;

namespace iqraProject.Interface.IRepository
{
    public interface IRoleRepository
    {
        Role Create (Role role);
        Role Update (Role role);
        Role Get (int id);
        void Delete (Role role);
        RoleDto ReturnById (int id);
        List<RoleDto> GetAll ();
        Role GetByName (string name);
        bool ExistById (int id);
        bool ExistByName (string name);

    }
}