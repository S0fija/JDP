using System.Collections.Generic;
using System.Threading.Tasks;
using JDP.Models;

namespace JDP.Contracts.Repositories
{
    public interface IExamRepository
    {
        Task<IEnumerable<Exam>> GetExams();
    }
}