using System.Collections.Generic;

namespace JDP.Dtos
{
    public class StudentExamListDto
    {
        public string StudentName { get; set; }

        public IEnumerable<ExamGradeDto> Exams { get; set; }
    }

    public class ExamGradeDto
    {
        public string ExamName { get; set; }
        public string Grade { get; set; }
    }
}