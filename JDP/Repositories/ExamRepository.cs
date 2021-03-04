using System.Collections.Generic;
using System.Threading.Tasks;
using JDP.Contracts.Repositories;
using JDP.Fixtures;
using JDP.Models;

namespace JDP.Repositories
{
    public class ExamRepository :
        IExamRepository
    {
        public async Task<IEnumerable<Exam>> GetExams()
        {
            return DataSource.Exams;
        }
    }
}