using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using JDP.Models;

namespace JDP.Contracts.Repositories
{
    public interface IStudentRepository
    {
        Task<Student> GetStudentBy(int Id);

        Task<Student> AssignExamToStudent(int studentId, Exam exam);

        Task<IEnumerable<Student>> GetStudents(Expression<Func<Student,bool>> predicate);
    }
}