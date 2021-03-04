namespace JDP.Models
{
    public enum ExamGrade
    {
        A,
        B,
        C,
        D,
        F,
        Unrated
    }
    public class ExamStatus
    {
        public Exam Exam { get; set; }

        public ExamGrade Grade { get; set; } = ExamGrade.Unrated;
    }
}