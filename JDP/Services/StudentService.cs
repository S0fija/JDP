using System.Linq;
using System.Threading.Tasks;
using JDP.Contracts.Repositories;
using JDP.Contracts.Services;
using JDP.Dtos;

namespace JDP.Services
{
    public class StudentService :
        IStudentService
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IExamRepository _examRepository;

        public StudentService(IStudentRepository studentRepository, IExamRepository examRepository)
        {
            _studentRepository = studentRepository;
            _examRepository = examRepository;
        }
        
        public async Task<StudentStatusInfoDto> GetStudentsGroupedByStatus()
        {
            //Should be checked if student is active or not
            // use LINQ .GroupBy method
            throw new System.NotImplementedException();
        }

        public async Task<ExamDto> GetListOfAvailableExamsToEnrollFroStudent(int studentId)
        {
            // create extension method "ToDto" in which LINQ .Select method should be used
            throw new System.NotImplementedException();
        }

        public async Task<StudentExamListDto> GetStudentExamListWithGrades(int studentId)
        {
            // use LINQ .Aggregate method and .OrderBy grade
            throw new System.NotImplementedException();
        }
    }
}