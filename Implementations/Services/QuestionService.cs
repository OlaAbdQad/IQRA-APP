using System.Collections.Generic;
using iqraProject.DTOs;
using iqraProject.Entities;
using iqraProject.Interface.IRepository;
using iqraProject.Interface.IServices;

namespace iqraProject.Implementations.Services
{
    public class QuestionService : IQuestionService
    {
        private readonly IQuestionRepository _questionRepository;

        public QuestionService(IQuestionRepository questionRepository)
        {
            _questionRepository = questionRepository;
        }

        public BaseResponse<QuestionDto> AddQuestion(AddQuestionRequestModel model)
        {
            var question = _questionRepository.GetByTextTest(model.TextTest);
            if(question != null)
            {
                return new BaseResponse<QuestionDto>
                {
                    Message = "Question already exist",
                    IsSuccess = false
                };
            }
            var newQuestion = new Question
            {
                AudioTest = model.AudioTest,
                TextTest = model.TextTest,
                AssessmentId = model.AssessmentId
            };
            var questionDetails = _questionRepository.Create(newQuestion);
            return new BaseResponse<QuestionDto>
            {
                Message = "Question successfully created",
                IsSuccess = true,
                Data = new QuestionDto
                {
                    AudioTest = questionDetails.AudioTest,
                    TextTest = questionDetails.TextTest,
                    AssessmentId = questionDetails.AssessmentId

                }
            };
        }

        public BaseResponse<QuestionDto> DeleteQuestion(int id)
        {
            var question = _questionRepository.Get(id);
            if(question == null)
            {
                return new BaseResponse<QuestionDto>
                {
                    Message = "Question not found",
                    IsSuccess = false
                };
            }
            _questionRepository.Delete(question);
            return new BaseResponse<QuestionDto>
            {
                Message = "Question deleted successfully",
                IsSuccess = true
            };
        }

        public BaseResponse<IEnumerable<QuestionDto>> GetAllQuestion()
        {
            var questions = _questionRepository.GetAll();
            if(questions == null)
            {
                return new BaseResponse<IEnumerable<QuestionDto>>
                {
                    Message = "No Question found",
                    IsSuccess = false
                };
            }
            return new BaseResponse<IEnumerable<QuestionDto>>
            {
                Message = "Questions have been found",
                IsSuccess = true,
                Data = questions
            };
        }

        public BaseResponse<QuestionDto> GetQuestion(int id)
        {
            var question = _questionRepository.Return(id);
            if(question == null)
            {
                return new BaseResponse<QuestionDto>
                {
                    Message = "No Question found",
                    IsSuccess = false
                };
            }
            return new BaseResponse<QuestionDto>
            {
                Message = "Question has been found",
                IsSuccess = true,
                Data = question
            };
        }

        public BaseResponse<IEnumerable<QuestionDto>> GetQuestionByAssessmentId(int assessmentId)
        {
            var questions = _questionRepository.GetByAssessmentId(assessmentId);
            if(questions == null)
            {
                return new BaseResponse<IEnumerable<QuestionDto>>
                {
                    Message = "No Questions found",
                    IsSuccess = false
                };
            }

            return new BaseResponse<IEnumerable<QuestionDto>>
            {
                Message = "Questions have been found",
                IsSuccess = true,
                Data = questions
            };
        }

        public BaseResponse<QuestionDto> UpdateQuestion(UpdateQuestionRequestModel model, int id)
        {
            var question = _questionRepository.Get(id);
            if(question == null)
            {
                return new BaseResponse<QuestionDto>
                {
                    Message = "Question not found",
                    IsSuccess = false
                };
            }
            question.TextTest = model.TextTest ?? question.TextTest;
            question.AudioTest = model.AudioTest ?? question.AudioTest;
            var questionDetails = _questionRepository.Update(question);
            return new BaseResponse<QuestionDto>
            {
                Message = "Question successfully updated",
                IsSuccess = true,
                Data = new QuestionDto
                {
                    TextTest = questionDetails.TextTest,
                    AudioTest = questionDetails.AudioTest
                }
            };
        }
    }
}