using System.Collections.Generic;
using System.Linq;
using iqraProject.DTOs;
using iqraProject.Entities;
using iqraProject.Interface.IRepository;
using Microsoft.EntityFrameworkCore;

namespace iqraProject.Implementation.Repositories
{
    public class OptionRepository : IOptionRepository
    {
        private readonly ArabicContext _context;

        public OptionRepository(ArabicContext context)
        {
            _context = context;
        }

        public Option Create(Option option)
        {
            _context.Options.Add(option);
            _context.SaveChanges();
            return option;
        }

        public void Delete(Option option)
        {
            _context.Options.Remove(option);
            _context.SaveChanges();
        }

        public Option Get(int id)
        {
            return _context.Options.Find(id);
        }

        public List<OptionDto> GetAll()
        {
            return _context.Options.Select(o => new OptionDto
            {
                Id = o.Id,
                Label = o.Label,
                Sound = o.Sound,
                Text = o.Text,
                OptionStatus = o.OptionStatus,
                OptionType = o.OptionType
            }).ToList();
        }

        public Option GetByLabel(string label)
        {
            return _context.Options.SingleOrDefault(o => o.Label == label);
        }

        public IEnumerable<OptionDto> GetByQuestionId(int questionId)
        {
            return _context.Options.Where(o => o.QuestionId == questionId).Select(optionDto => new OptionDto
            {
                Id = optionDto.Id,
                Label = optionDto.Label,
                OptionStatus = optionDto.OptionStatus,
                Sound = optionDto.Sound,
                Text = optionDto.Text,
                OptionType = optionDto.OptionType

            }).ToList();
        }

        public OptionDto Return(int id)
        {
            var option = _context.Options.SingleOrDefault(o => o.Id == id);
            var optionDto = new OptionDto
            {
                Id = option.Id,
                Label = option.Label,
                OptionStatus = option.OptionStatus,
                Sound = option.Sound,
                Text = option.Text,
                OptionType = option.OptionType
            };
            return optionDto;
        }

        public Option Update(Option option)
        {
            _context.Options.Update(option);
            _context.SaveChanges();
            return option;
        }

        public IEnumerable<Option> GetSelectedOptions(List<int> ids)
        {
            return _context.Options.Include(q => q.Question).ThenInclude(a => a.Assessment).Where(y => ids.Contains(y.Id)).ToList();

        }

    }
}