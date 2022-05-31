using System.Collections.Generic;
using System.Linq;
using iqraProject.DTOs;
using iqraProject.Entities;
using iqraProject.Interface.IRepository;
using iqraProject;
using Microsoft.EntityFrameworkCore;

namespace iqraProject.Implementation.Repositories
{
    public class AssessmentRepository : IAssessmentRepository
    {
        private readonly ArabicContext _context;

        public AssessmentRepository(ArabicContext context)
        {
            _context = context;
        }

        public Assessment Create(Assessment assessment)
        {
            _context.Assessments.Add(assessment);
            _context.SaveChanges();
            return assessment;
        }

        public void Delete(Assessment assessment)
        {
            _context.Assessments.Remove(assessment);
            _context.SaveChanges();
        }

        public Assessment Get(int id)
        {
            return _context.Assessments.Find(id);
        }

        public IEnumerable<AssessmentDto> GetAll()
        {
            return _context.Assessments.Include(q => q.Questions).Select(a => new AssessmentDto
            {
                Id = a.Id,
                Name = a.Name,
                Description = a.Description,
                Questions = a.Questions.Select(q => new QuestionDto
                {
                    Id = q.Id,
                    AudioTest = q.AudioTest,
                    TextTest = q.TextTest,
                    QuestionType = q.QuestionType
                }).ToList()
            }).ToList();
        }

        public IEnumerable<AssessmentDto> GetByLessonId(int lessonId)
        {
            return _context.Assessments.Where(l => l.LessonId == lessonId).Select(assessmentDto => new AssessmentDto
            {
                Id = assessmentDto.Id,
                Name = assessmentDto.Name,
                Description = assessmentDto.Description,
                LessonId = assessmentDto.LessonId,
                
                
            }).ToList();
        }

        public Assessment GetByName(string name)
        {
            return _context.Assessments.FirstOrDefault(a => a.Name == name);
        }

        public AssessmentDto Return(int id)
        {
            var assessment = _context.Assessments.Include(q => q.Questions).ThenInclude(o => o.Options).SingleOrDefault(a => a.Id == id);
            var assessmentDto = new AssessmentDto
            {
                Id = assessment.Id,
                Name = assessment.Name,
                Description = assessment.Description,
                Questions = assessment.Questions.Select(q => new QuestionDto
                {
                    Id = q.Id,
                    AudioTest = q.AudioTest,
                    TextTest = q.TextTest,
                    QuestionType = q.QuestionType,
                    Options = q.Options.Select(o => new OptionDto
                    {
                        Id = o.Id,
                        Label = o.Label,
                        Text = o.Text,
                        Sound = o.Sound,
                        OptionStatus = o.OptionStatus,
                        OptionType  = o.OptionType,
                        QuestionId = o.QuestionId
                    }).ToList()
                }).ToList()
            };
            return assessmentDto;
        }

        public Assessment Update(Assessment assessment)
        {
            _context.Assessments.Update(assessment);
            _context.SaveChanges();
            return assessment;
        }
    }
}