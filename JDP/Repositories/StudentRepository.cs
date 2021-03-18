using JDP.Contracts.Repositories;
using JDP.Fixtures;
using JDP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace JDP.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        public async Task<Student> GetStudentBy(int id)
        {
            var singleStudent = DataSource.Students.FirstOrDefault(student => student.Id == id);
            return singleStudent;
        }

        public async Task<Student> AssignExamToStudent(int studentId, Exam exam)
        {
            var singleStudent = DataSource.Students.FirstOrDefault(student => student.Id == studentId);

            (singleStudent?.Exams as List<ExamStatus>)?.Add(new ExamStatus
            {
                Exam = exam,
                Grade = ExamGrade.Unrated
            });

            return singleStudent;
        }

        public async Task<IEnumerable<Student>> GetStudents(Expression<Func<Student, bool>> predicate)
        {
            var students = DataSource.Students.AsQueryable().Where(predicate);
            return students;
        }
    }
}