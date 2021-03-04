using JDP.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JDP.Contracts.Services
{
    public interface IStudentService
    {
        Task<StudentStatusInfoDto> GetStudentsGroupedByStatus();

        Task<IEnumerable<ExamDto>> GetListOfAvailableExamsToEnrollForStudent(int studentId);

        Task<StudentExamListDto> GetStudentExamListWithGrades(int studentId);
    }
}