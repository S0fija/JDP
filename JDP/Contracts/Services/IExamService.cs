using System.Collections.Generic;
using System.Threading.Tasks;
using JDP.Dtos;

namespace JDP.Contracts.Services
{
    public interface IExamService
    {
        Task<IEnumerable<ExamDto>> GetListOfAvailableExams();
    }
}