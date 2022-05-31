using System.Collections.Generic;
using System.Linq;
using iqraProject.DTOs;
using iqraProject.Entities;
using iqraProject.Interface.IRepository;

namespace iqraProject.Implementation.Repositories
{
    public class WordRepository : IWordRepository
    {
        private readonly ArabicContext _context;

        public WordRepository(ArabicContext context)
        {
            _context = context;
        }
        public Word Create(Word word)
        {
            _context.Words.Add(word);
            _context.SaveChanges();
            return word;
        }

        public void Delete(Word word)
        {
            _context.Words.Remove(word);
            _context.SaveChanges();
        }

        public Word Get(int id)
        {
            return _context.Words.Find(id);
        }

        public Word Update(Word word)
        {
            _context.Words.Update(word);
            _context.SaveChanges();
            return word;
        }

         public List<WordDto> GetAll()
        {
            return _context.Words.Select(y => new WordDto
            {
                Id = y.Id,
                WordName = y.WordName,
                Symbol = y.Symbol,
                Sound = y.Sound,
                Audio = y.Audio,
                
            }).ToList();
        }

        public WordDto Return(int id)
        {
            var word = _context.Words.SingleOrDefault(y => y.Id == id);
            var wordDto = new WordDto
            {
                Id = word.Id,
                WordName = word.WordName,
                Symbol = word.Symbol,
                Sound = word.Sound,
                Audio = word.Audio
            };
            return wordDto;
        }

        public IEnumerable<WordDto> GetByLessonId(int lessonId)
        {
            return _context.Words.Where(t => t.LessonId == lessonId).Select(wordDto => new WordDto
            {
                Id = wordDto.Id,
                WordName = wordDto.WordName,
                Symbol = wordDto.Symbol,
                Sound = wordDto.Sound,
                LessonId = wordDto.LessonId,
                Audio = wordDto.Audio
                
            }).ToList();
        }
    }
}