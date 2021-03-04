using JDP.Contracts.Repositories;
using JDP.Contracts.Services;
using JDP.Dtos;
using JDP.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JDP.Services
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IExamRepository _examRepository;
        private const bool StudentIsActive = true;
        private const bool StudentIsInactive = false;

        public StudentService(IStudentRepository studentRepository, IExamRepository examRepository)
        {
            _studentRepository = studentRepository;
            _examRepository = examRepository;
        }

        #region StudentsGroupedByStatus

        public async Task<StudentStatusInfoDto> GetStudentsGroupedByStatus()
        {
            //Should be checked if student is active or not
            // use LINQ .GroupBy method

            var allStudents = await _studentRepository.GetStudents(student => true);
            var studentsGroupedByStatus = allStudents.GroupBy(student => student.IsStudentActive());

            var activeStudentsGroup = studentsGroupedByStatus.First(group => group.Key == StudentIsActive);
            var inactiveStudentsGroup = studentsGroupedByStatus.First(group => group.Key == StudentIsInactive);

            var studentStatusInfo = new StudentStatusInfoDto
            {
                ActiveStudents = GetStudentsFromGroup(activeStudentsGroup).ToList(),
                InactiveStudents = GetStudentsFromGroup(inactiveStudentsGroup).ToList()
            };

            return studentStatusInfo;
        }

        private IEnumerable<StudentDto> GetStudentsFromGroup(IGrouping<bool, Models.Student> studentGroup)
        {
            foreach (var student in studentGroup)
            {
                yield return new StudentDto { FullName = student.FullName };
            }
        }

        #endregion StudentsGroupedByStatus

        public async Task<IEnumerable<ExamDto>> GetListOfAvailableExamsToEnrollForStudent(int studentId)
        {
            // create extension method "ToDto" in which LINQ .Select method should be used

            var student = await _studentRepository.GetStudentBy(studentId);
            var alredyEnrolledExamsIds = student.Exams.Select(studentExam => studentExam.Exam.Id).ToList();
            var examsAvailableForEnrollment = await _examRepository.GetExams(exam => !alredyEnrolledExamsIds.Contains(exam.Id));

            var exams = examsAvailableForEnrollment.Select(exam => exam.ToExamDto()).ToList();

            return exams;
        }

        public async Task<StudentExamListDto> GetStudentExamListWithGrades(int studentId)
        {
            // use LINQ .Aggregate method and .OrderBy grade
            throw new NotImplementedException();
        }
    }
}