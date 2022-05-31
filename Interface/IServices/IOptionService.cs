using System.Collections.Generic;
using iqraProject.DTOs;

namespace iqraProject.Interface.IServices
{
    public interface IOptionService
    {
        BaseResponse<OptionDto> AddOption(AddOptionRequestModel model);
        BaseResponse<OptionDto> UpdateOption(UpdateOptionRequestModel model, int id);
        BaseResponse<OptionDto> DeleteOption(int id);
        BaseResponse<OptionDto> GetOption (int id);
        BaseResponse<IEnumerable<OptionDto>> GetAllOption();
        BaseResponse<IEnumerable<OptionDto>> GetOptionByQuestonId(int questionId);
    }
}