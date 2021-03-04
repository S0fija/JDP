using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JDP.Contracts.Repositories;
using JDP.Contracts.Services;
using JDP.Dtos;

namespace JDP.Services
{
    public class ExamService : IExamService
    {
        private readonly IExamRepository _examRepository;

        public ExamService(IExamRepository examRepository)
        {
            _examRepository = examRepository;
        }

        public async Task<IEnumerable<ExamDto>> GetListOfAvailableExams()
        {
            var exams = (await _examRepository
                .GetExams())
                .Select(exam => new ExamDto
                {
                    ExamName =  exam.Title
                });

            return exams;
        }
    }
}