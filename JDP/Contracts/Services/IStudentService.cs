using System.Threading.Tasks;
using JDP.Dtos;
using JDP.Models;

namespace JDP.Contracts.Services
{
    public interface IStudentService
    {
        Task<StudentStatusInfoDto> GetStudentsGroupedByStatus();

        Task<ExamDto> GetListOfAvailableExamsToEnrollFroStudent(int studentId);

        Task<StudentExamListDto> GetStudentExamListWithGrades(int studentId);
    }
}