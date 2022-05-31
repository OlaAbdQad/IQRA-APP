using System;
using System.Collections.Generic;
using System.Linq;
using iqraProject.DTOs;
using iqraProject.Entities;
using iqraProject.Enums;
using iqraProject.Interface.IRepository;
using iqraProject.Interface.IServices;

namespace iqraProject.Implementations.Services
{
    public class ResultService : IResultService
    {
        private readonly IResultRepository _resultRepository;
        private readonly IOptionRepository _optionRepository;
        private readonly IAssessmentRepository _assessmentRepository;
        private readonly IStudentRepository _studentRepository;
        private readonly IQuestionRepository _questionRepository;
        private readonly ILessonRepository _lessonRepository;

        public ResultService(IResultRepository resultRepository, IOptionRepository optionRepository, IAssessmentRepository assessmentRepository, IStudentRepository studentRepository, IQuestionRepository questionRepository, ILessonRepository lessonRepository)
        {
            _resultRepository = resultRepository;
            _optionRepository = optionRepository;
            _studentRepository = studentRepository;
            _questionRepository = questionRepository;
            _assessmentRepository = assessmentRepository;
            _lessonRepository  = lessonRepository;
        }

        public BaseResponse<StudentResultDto> GenerateResult(ResultRequestModel model)
        {
            List<Question> questions = new List<Question>();
            double totalScore = 0;
            double correctMarks = 0;
            var options = _optionRepository.GetSelectedOptions(model.OptionsIds).ToList();
            var assessment = _assessmentRepository.Get(options[0].Question.AssessmentId);
            var student = _studentRepository.GetByEmail(model.StudentEmail);
            var presentLesson = student.StudentLessons.Last();
            
            foreach(var option in options)
            {
                var question = _questionRepository.Get(option.QuestionId);
                questions.Add(question);
            }

            for(int c = 0; c < questions.Count; c++)
            {
                totalScore++;
                if(options[c].OptionStatus == OptionStatus.Correct)
                {
                    correctMarks++; 
                }
            }

            var studentScore = Math.Round(correctMarks / totalScore * 100);

            var result = new Result
            {
                Assessment = assessment,
                AssessmentId = assessment.Id,
                Student = student,
                StudentId = student.Id,
                CorrectMarks = studentScore,
                TotalScore = 100
            };
            var studentMark = _resultRepository.Create(result);

            var checkResult = CheckResult(studentMark.CorrectMarks, student.Id, presentLesson.Lesson.Id + 1);

            if(checkResult == null)
            {
                return new BaseResponse<StudentResultDto>
                {
                    Message = $"Sorry, You scored {result.CorrectMarks}%\nKeep calm and endeavor to take the lesson again",
                    IsSuccess = false,
                };
            }

            return new BaseResponse<StudentResultDto>
            {
                Message = $"Congrat! You scored {result.CorrectMarks}%\nWell Done! Do well to proceed your lesson",
                IsSuccess = true,
                Data = new StudentResultDto
                {
                    Id = studentMark.Id,
                    AssessmentId = studentMark.AssessmentId,
                    CorrectMarks = studentMark.CorrectMarks,
                    StudentId = studentMark.StudentId,
                    Lessons = checkResult.Lessons.Select(l => new LessonDto
                    {
                        Id = l.Id,
                        Name = l.Name,
                        Description = l.Description
                    }).ToList()
                }
            };

        }                                   


        public StudentDto CheckResult(double correctMarks, int studentId, int lessonId)
        {
            // bool check = false;
            // var allLesson = _lessonRepository.GetAll();

            // for(int i = 0; i < allLesson.Count; i++)
            // {
            //     if(allLesson[i].Id == lessonId)
            //     {
            //         check = true;
            //     }

            //     if(check == true)
            //     {
                    if(correctMarks > 85)
                    {
                        var student = _studentRepository.GetById(studentId);
                        var lesson = _lessonRepository.Get(lessonId);
                        var studentLesson = new StudentLesson
                        {
                            Student = student,
                            StudentId = student.Id,
                            Lesson = lesson,
                            LessonId = lesson.Id,
                        }; 

                        student.StudentLessons.Add(studentLesson);
                        _studentRepository.Update(student);
                        var studentDto = new StudentDto
                        {
                            Id = student.Id,
                            FirstName = student.FirstName,
                            LastName = student.LastName,
                            Email = student.Email,
                            Lessons = student.StudentLessons.Select(s => new LessonDto
                            {
                                Id = s.Lesson.Id,
                                Name = s.Lesson.Name,
                                Description = s.Lesson.Description,

                            }).ToList()
                        };
                        return studentDto;
                    }
                    else
                    {
                        return null;
                    }

                // }
                // else
                // {
                //     return null;
                // }


            // }

            
        }

        
    }
}