using System.Collections.Generic;
using iqraProject.DTOs;
using iqraProject.Entities;
using iqraProject.Interface.IRepository;
using iqraProject.Interface.IServices;

namespace iqraProject.Implementations.Services
{
    public class AssessmentService : IAssessmentService
    {
        private readonly IAssessmentRepository _assessmentRepository;

        public AssessmentService(IAssessmentRepository assessmentRepository)
        {
            _assessmentRepository = assessmentRepository;
        }

        public BaseResponse<AssessmentDto> AddAssessment(AddAssessmentRequestModel model)
        {
            var assessment = _assessmentRepository.GetByName(model.Name);
            if(assessment != null)
            {
                return new BaseResponse<AssessmentDto>
                {
                    Message = "Assessment already exist",
                    IsSuccess = false
                };
            }
            var newAssessment = new Assessment
            {
                Name = model.Name,
                Description = model.Description,
                LessonId = model.LessonId

            };
            var assessmentDetails = _assessmentRepository.Create(newAssessment);
            return new BaseResponse<AssessmentDto>
            {
                Message = "Assessment successfully created",
                IsSuccess = true,
                Data = new AssessmentDto
                {
                    Name = assessmentDetails.Name,
                    Description = assessmentDetails.Description,
                    LessonId = assessmentDetails.LessonId
                }
            };
        }

        public BaseResponse<AssessmentDto> DeleteAssessment(int id)
        {
            var assessment = _assessmentRepository.Get(id);
            if(assessment == null)
            {
                return new BaseResponse<AssessmentDto>
                {
                    Message = "Assessment not found",
                    IsSuccess = false
                };
            }
            _assessmentRepository.Delete(assessment);
            return new BaseResponse<AssessmentDto>
            {
                Message = "Assessment deleted successfully",
                IsSuccess = true
            };
        }

        public BaseResponse<IEnumerable<AssessmentDto>> GetAllAssessment()
        {
            var assessments = _assessmentRepository.GetAll();
            if(assessments == null)
            {
                return new BaseResponse<IEnumerable<AssessmentDto>>
                {
                    Message = "No Assessment found",
                    IsSuccess = false
                };
            }
            return new BaseResponse<IEnumerable<AssessmentDto>>
            {
                Message = "Assessments have been found",
                IsSuccess = true,
                Data = assessments
            };
        }

        public BaseResponse<AssessmentDto> GetAssessment(int id)
        {
            var assessment = _assessmentRepository.Return(id);
            if(assessment == null)
            {
                return new BaseResponse<AssessmentDto>
                {
                    Message = "No Assessment found",
                    IsSuccess = false
                };
            }
            return new BaseResponse<AssessmentDto>
            {
                Message = "Assessment has been found",
                IsSuccess = true,
                Data = assessment
            };
        }

        public BaseResponse<IEnumerable<AssessmentDto>> GetAssessmentByLessonId(int lessonId)
        {
            var assessments = _assessmentRepository.GetByLessonId(lessonId);
            if(assessments == null)
            {
                return new BaseResponse<IEnumerable<AssessmentDto>>
                {
                    Message = "No Assessment found",
                    IsSuccess = false
                };
            }
            return new BaseResponse<IEnumerable<AssessmentDto>>
            {
                Message = "Assessments have been found",
                IsSuccess = true,
                Data = assessments
            };
        }

        public BaseResponse<AssessmentDto> UpdateAssessment(UpdateAssessmentRequestModel model, int id)
        {
            var assessment = _assessmentRepository.Get(id);
            if(assessment == null)
            {
                return new BaseResponse<AssessmentDto>
                {
                    Message = "Assessment not found",
                    IsSuccess = false
                };
            }
            assessment.Name = model.Name ?? assessment.Name;
            assessment.Description = model.Description ?? assessment.Description;
            var assessmentDetails = _assessmentRepository.Update(assessment);
            return new BaseResponse<AssessmentDto>
            {
                Message = "Assessment successfully updated",
                IsSuccess = true,
                Data = new AssessmentDto
                {
                    Name = assessmentDetails.Name,
                    Description = assessmentDetails.Description
                }
            };

        }
    }
}