using System.Collections.Generic;
using iqraProject.DTOs;
using iqraProject.Entities;
using iqraProject.Interface.IRepository;
using iqraProject.Interface.IServices;

namespace iqraProject.Implementations.Services
{
    public class OptionService : IOptionService
    {
        private readonly IOptionRepository _optionRepository;

        public OptionService(IOptionRepository optionRepository)
        {
            _optionRepository = optionRepository;
        }

        public BaseResponse<OptionDto> AddOption(AddOptionRequestModel model)
        {
            // var option = _optionRepository.GetByLabel(model.Label);
            // if(option != null)
            // {
            //     return new BaseResponse<OptionDto>
            //     {
            //         Message = "Option already exist",
            //         IsSuccess = false
            //     };
            // }
            var newOption = new Option
            {
                Label = model.Label,
                Sound = model.Sound,
                Text = model.Text,
                OptionStatus = model.OptionStatus,
                OptionType = model.OptionType,
                QuestionId = model.QuestionId

            };
            var optionDetails = _optionRepository.Create(newOption);
            return new BaseResponse<OptionDto>
            {
                Message = "Option successfully created",
                IsSuccess = true,
                Data = new OptionDto
                {
                    Label = optionDetails.Label,
                    Sound = optionDetails.Sound,
                    Text = optionDetails.Text,
                    OptionStatus = optionDetails.OptionStatus,
                    OptionType = optionDetails.OptionType,
                    QuestionId = optionDetails.QuestionId
                }
            };
        }

        public BaseResponse<OptionDto> DeleteOption(int id)
        {
            var option = _optionRepository.Get(id);
            if(option == null)
            {
                return new BaseResponse<OptionDto>
                {
                    Message = "Option not found",
                    IsSuccess = false
                };
            }
            _optionRepository.Delete(option);
            return new BaseResponse<OptionDto>
            {
                Message = "Option deleted successfully",
                IsSuccess = true
            };
        }

        public BaseResponse<IEnumerable<OptionDto>> GetAllOption()
        {
            var options = _optionRepository.GetAll();
            if(options == null)
            {
                return new BaseResponse<IEnumerable<OptionDto>>
                {
                    Message = "No Option found",
                    IsSuccess = false
                };
            }
            return new BaseResponse<IEnumerable<OptionDto>>
            {
                Message = "Options have been found",
                IsSuccess = true,
                Data = options
            };
        }

        public BaseResponse<OptionDto> GetOption(int id)
        {
            var option = _optionRepository.Return(id);
            if(option == null)
            {
                return new BaseResponse<OptionDto>
                {
                    Message = "No Option found",
                    IsSuccess = false
                };
            }
            return new BaseResponse<OptionDto>
            {
                Message = "Option has been found",
                IsSuccess = true,
                Data = option
            };
        }

        public BaseResponse<IEnumerable<OptionDto>> GetOptionByQuestonId(int questionId)
        {
            var options = _optionRepository.GetByQuestionId(questionId);
            if(options == null)
            {
                return new BaseResponse<IEnumerable<OptionDto>>
                {
                    Message = "No Options found",
                    IsSuccess = false
                };
            }

            return new BaseResponse<IEnumerable<OptionDto>>
            {
                Message = "Options have been found",
                IsSuccess = true,
                Data = options
            };
        }

        public BaseResponse<OptionDto> UpdateOption(UpdateOptionRequestModel model, int id)
        {
            var option = _optionRepository.Get(id);
            if(option == null)
            {
                return new BaseResponse<OptionDto>
                {
                    Message = "Option not found",
                    IsSuccess = false
                };
            }
            option.Text = model.Text ?? option.Text;
            option.Sound = model.Sound ?? option.Sound;
            option.Label = model.Label ?? option.Label;
            option.OptionStatus = model.OptionStatus;
            option.OptionType = model.OptionType;
            var optionDetails = _optionRepository.Update(option);
            return new BaseResponse<OptionDto>
            {
                Message = "Option successfully updated",
                IsSuccess = true,
                Data = new OptionDto
                {
                    Label = optionDetails.Label,
                    Sound = optionDetails.Sound,
                    Text = optionDetails.Text,
                    OptionStatus = optionDetails.OptionStatus,
                    OptionType = optionDetails.OptionType
                }
            };
        }
    }
}