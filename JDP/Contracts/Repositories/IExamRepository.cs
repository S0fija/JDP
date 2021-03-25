using JDP.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace JDP.Contracts.Repositories
{
    public interface IExamRepository
    {
        Task<IEnumerable<Exam>> GetExams();
        //Task<IEnumerable<Exam>> GetExams(Expression<Func<Exam, bool>> predicate);
    }
}