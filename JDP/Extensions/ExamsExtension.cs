using JDP.Dtos;
using JDP.Models;

namespace JDP.Extensions
{
    public static class ExamsExtension
    {
        public static ExamDto ToExamDto(this Exam exam)
        {
            return new ExamDto
            {
                ExamName = exam.Title
            };
        }
    }
}
