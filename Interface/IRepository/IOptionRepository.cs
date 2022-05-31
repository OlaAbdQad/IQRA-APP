using System.Collections.Generic;
using iqraProject.DTOs;
using iqraProject.Entities;

namespace iqraProject.Interface.IRepository
{
    public interface IOptionRepository
    {
        Option Create(Option option);
        Option Update(Option option);
        void Delete(Option option);
        Option Get(int id);
        Option GetByLabel(string label);
        List<OptionDto> GetAll();
        OptionDto Return(int id);
        IEnumerable<OptionDto> GetByQuestionId(int questionId);
        IEnumerable<Option> GetSelectedOptions(List<int> ids);
    }
}