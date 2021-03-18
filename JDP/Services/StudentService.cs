using JDP.Contracts.Repositories;
using JDP.Contracts.Services;
using JDP.Dtos;
using JDP.Extensions;
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

        #region AvailableExamsToEnroll
        public async Task<IEnumerable<ExamDto>> GetListOfAvailableExamsToEnrollForStudent(int studentId)
        {
            // create extension method "ToDto" in which LINQ .Select method should be used

            var student = await _studentRepository.GetStudentBy(studentId);
            var alredyEnrolledExamsIds = student.Exams.Select(studentExam => studentExam.Exam.Id).ToList();
            var examsAvailableForEnrollment = await _examRepository.GetExams(exam => !alredyEnrolledExamsIds.Contains(exam.Id));

            var exams = examsAvailableForEnrollment.Select(exam => exam.ToExamDto()).ToList();

            return exams;
        }
        #endregion AvailableExamsToEnroll

        #region StudentExamListWithGrades
        public async Task<StudentExamListDto> GetStudentExamListWithGrades(int studentId)
        {
            // use LINQ .Aggregate method and .OrderBy grade

            var student = await _studentRepository.GetStudentBy(studentId);
            var exams = student.Exams.Aggregate(new List<ExamGradeDto>(), (list, examStatus) =>
            {
                list.Append(new ExamGradeDto
                {
                    ExamName = examStatus.Exam.Title,
                    Grade = examStatus.Grade.ToString()
                });
                return list;
            });

            return new StudentExamListDto
            {
                StudentName = student.FullName,
                Exams = GetExamsList(student.Exams).OrderBy(exam => exam.Grade)
            };

            //// Determine whether any string in the array is longer than "banana".
            //string[] fruits = { "apple", "mango", "orange", "passionfruit", "grape" };
            //string longestName = fruits.Aggregate("banana", (longest, next) => next.Length > longest.Length ? next : longest, fruit => fruit.ToUpper());

            //// Count the even numbers in the array, using a seed value of 0.
            //int[] ints = { 4, 8, 8, 3, 9, 0, 7, 8, 2 };
            //int numEven = ints.Aggregate(0, (total, next) => next % 2 == 0 ? total + 1 : total);
        }

        private IEnumerable<ExamGradeDto> GetExamsList(IEnumerable<Models.ExamStatus> examStatuses)
        {
            foreach (var examStatus in examStatuses)
            {
                yield return new ExamGradeDto
                {
                    ExamName = examStatus.Exam.Title,
                    Grade = examStatus.Grade.ToString()
                };
            }
        }
        #endregion StudentExamListWithGrades
    }
}