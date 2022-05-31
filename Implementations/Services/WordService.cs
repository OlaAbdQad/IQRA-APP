using System.Collections.Generic;
using System.Linq;
using iqraProject.DTOs;
using iqraProject.Entities;
using iqraProject.Interface.IRepository;
using iqraProject.Interface.IServices;


namespace iqraProject.Implementation.Services
{
    public class WordService : IWordService
    {
        private readonly IWordRepository _wordRepository;
        private readonly ILessonRepository _lessonRepository;

        public WordService(IWordRepository wordRepository, ILessonRepository lessonRepository)
        {
            _wordRepository = wordRepository;
            _lessonRepository = lessonRepository;
        }

        public bool AddWord(AddWordRequestModel model)
        {
             var word = new Word
            {
                WordName = model.WordName,
                Symbol = model.Symbol,
                Sound = model.Sound,
                LessonId = model.LessonId
            };
            _wordRepository.Create(word);
            return true;
        }

        public void DeleteWord(int id)
        {
            var word = _wordRepository.Get(id);
            _wordRepository.Delete(word);
        }

        public List<WordDto> GetAllWord()
        {
            return _wordRepository.GetAll().ToList();
        }

        public WordDto GetWord(int id)
        {
            return _wordRepository.Return(id);
        }

        public bool UpdateWord(UpdateWordRequestModel model, int id)
        {
            var word = _wordRepository.Get(id);
            if(word == null)
            {
                throw new NotFoundException($"word with {id} not found ");
            }
            word.WordName = model.WordName ?? word.WordName;
            word.Symbol = model.Symbol ?? word.Symbol;
            word.Sound = model.Sound ?? word.Sound;
            _wordRepository.Update(word);

            return true;
        }

        public IEnumerable<WordDto> GetWordByLessonId(int lessonId)
        {
            return _wordRepository.GetByLessonId(lessonId);
        }
    }
}