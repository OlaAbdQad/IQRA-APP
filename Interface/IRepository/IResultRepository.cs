using iqraProject.DTOs;
using iqraProject.Entities;

namespace iqraProject.Interface.IRepository
{
    public interface IResultRepository
    {
        Result Create(Result result);
        Result Get(int id);
        ResultDto Return (int id);
        
    }
}