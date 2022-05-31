using System.Linq;
using iqraProject.DTOs;
using iqraProject.Entities;
using iqraProject.Interface.IRepository;
using Microsoft.EntityFrameworkCore;

namespace iqraProject.Implementations.Repositories
{
    public class ResultRepository : IResultRepository
    {
        private readonly ArabicContext _context;

        public ResultRepository(ArabicContext context)
        {
            _context = context;
        }

        public Result Create(Result result)
        {
           var res =  _context.Results.Add(result);
            _context.SaveChanges();
            return result;
        }

        public Result Get(int id)
        {
            return _context.Results.Find(id);
        }

        public ResultDto Return(int id)
        {
            var result = _context.Results.Include(a => a.Assessment).ThenInclude(q => q.Questions).ThenInclude(o => o.Options).SingleOrDefault(r => r.Id == id);
            var resultDto = new ResultDto
            {
                Id = result.Id,
                AssessmentId = result.AssessmentId,
                StudentId = result.StudentId,
                

            };

            return resultDto;

        }
    }
}