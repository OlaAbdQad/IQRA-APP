using System.Collections.Generic;
using System.Linq;
using iqraProject.DTOs;
using iqraProject.Entities;
using iqraProject.Interface.IRepository;
using Microsoft.EntityFrameworkCore;

namespace iqraProject.Implementation.Repositories
{
    public class QuestionRepository : IQuestionRepository
    {
        private readonly ArabicContext _context;

        public QuestionRepository(ArabicContext context)
        {
            _context = context;
        }

        public Question Create(Question question)
        {
            _context.Questions.Add(question);
            _context.SaveChanges();
            return question;
        }

        public void Delete(Question question)
        {
            _context.Questions.Remove(question);
            _context.SaveChanges();
        }

        public Question Get(int id)
        {
            return _context.Questions.Find(id);
        }

        public List<QuestionDto> GetAll()
        {
            return _context.Questions.Include(a => a.Options).Select(q => new QuestionDto
            {
                Id = q.Id,
                AudioTest = q.AudioTest,
                TextTest = q.TextTest,
                QuestionType = q.QuestionType,
                Options = q.Options.Select(op => new OptionDto
                {
                    Id = op.Id,
                    Label = op.Label,
                    OptionType = op.OptionType,
                    OptionStatus = op.OptionStatus,
                    Text = op.Text,
                    Sound = op.Sound
                }).ToList()
            }).ToList();
        }

        public IEnumerable<QuestionDto> GetByAssessmentId(int assessmentId)
        {
            return _context.Questions.Where(q => q.AssessmentId == assessmentId).Select(questionDto => new QuestionDto
            {
                Id = questionDto.Id,
                AudioTest = questionDto.AudioTest,
                TextTest = questionDto.TextTest,
                AssessmentId = questionDto.AssessmentId,
                QuestionType = questionDto.QuestionType,
                
            }).ToList();
        }

        public Question GetByTextTest(string textTest)
        {
            return _context.Questions.SingleOrDefault(q => q.TextTest == textTest);
        }

        public QuestionDto Return(int id)
        {
            var question = _context.Questions.Include(a => a.Options).SingleOrDefault(q => q.Id == id);
            var questionDto = new QuestionDto
            {
                Id = question.Id,
                AudioTest = question.AudioTest,
                TextTest = question.TextTest,
                QuestionType = question.QuestionType,
                Options = question.Options.Select(op => new OptionDto
                {
                    Id = op.Id,
                    Label = op.Label,
                    OptionType = op.OptionType,
                    OptionStatus = op.OptionStatus,
                    Text = op.Text,
                    Sound = op.Sound
                }).ToList()
            };
            return questionDto;
        }

        public Question Update(Question question)
        {
            _context.Questions.Update(question);
            _context.SaveChanges();
            return question;
        }
    }
}