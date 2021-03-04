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
    public class ExamRepository : IExamRepository
    {
        public async Task<IEnumerable<Exam>> GetExams()
        {
            return DataSource.Exams;
        }

        public async Task<IEnumerable<Exam>> GetExams(Expression<Func<Exam, bool>> predicate)
        {
            var students = DataSource.Exams.AsQueryable().Where(predicate);
            return students;
        }
    }
}