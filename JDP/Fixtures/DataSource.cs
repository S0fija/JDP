using System;
using System.Collections.Generic;
using JDP.Models;

namespace JDP.Fixtures
{
    public static class DataSource
    {
        public static List<Exam> Exams = new List<Exam>()
        {
            new Exam
            {
                Id =1 ,
                Title = "Exam 1"
            },
            new Exam
            {
                Id = 2,
                Title = "Exam 2"
            },
            new Exam
            {
                Id = 3,
                Title = "Exam 3"
            },
            new Exam
            {
                Id = 4 ,
                Title = "Exam 4"
            },
            new Exam
            {
                Id = 5,
                Title = "Exam 5"
            },
            new Exam
            {
                Id = 6 ,
                Title = "Exam 6"
            },
        };

        public static List<Student> Students = new List<Student>()
        {
            new Student
            {
                Id = 1,
                FullName = "Student 1",
                ActiveStatusExpirationDateTime = DateTime.Now.AddDays(10),
                Exams = new List<ExamStatus>()
                {
                    new ExamStatus()
                    {
                        Exam = Exams[0] ,
                        Grade = ExamGrade.A
                    },
                    new ExamStatus()
                    {
                        Exam = Exams[1] ,
                        Grade = ExamGrade.B
                    },
                    new ExamStatus()
                    {
                        Exam = Exams[2] ,
                        Grade = ExamGrade.C
                    },
                    new ExamStatus()
                    {
                        Exam = Exams[3] ,
                        Grade = ExamGrade.Unrated
                    }
                }
            },
            new Student
            {
                Id = 2,
                FullName = "Student 2",
                ActiveStatusExpirationDateTime = DateTime.Now.AddDays(10),
                Exams = new List<ExamStatus>()
                {
                    new ExamStatus()
                    {
                        Exam = Exams[0] ,
                        Grade = ExamGrade.B
                    },
                    new ExamStatus()
                    {
                        Exam = Exams[1] ,
                        Grade = ExamGrade.C
                    },
                    new ExamStatus()
                    {
                        Exam = Exams[2] ,
                        Grade = ExamGrade.D
                    },
                    new ExamStatus()
                    {
                        Exam = Exams[3] ,
                        Grade = ExamGrade.F
                    }
                }
            },
            new Student
            {
                Id = 3,
                FullName = "Student 3",
                ActiveStatusExpirationDateTime = DateTime.Now.AddDays(-10),
                Exams = new List<ExamStatus>()
                {
                    new ExamStatus()
                    {
                        Exam = Exams[4] ,
                        Grade = ExamGrade.A
                    },
                    new ExamStatus()
                    {
                        Exam = Exams[5] ,
                        Grade = ExamGrade.B
                    },
                    new ExamStatus()
                    {
                        Exam = Exams[2] ,
                        Grade = ExamGrade.Unrated
                    },
                    new ExamStatus()
                    {
                        Exam = Exams[3] ,
                        Grade = ExamGrade.Unrated
                    }
                }
            },
            new Student
            {
                Id = 4,
                FullName = "Student 4",
                ActiveStatusExpirationDateTime = DateTime.Now.AddDays(10),
                Exams = new List<ExamStatus>()
                {
                    new ExamStatus()
                    {
                        Exam = Exams[4] ,
                        Grade = ExamGrade.A
                    },
                    new ExamStatus()
                    {
                        Exam = Exams[5] ,
                        Grade = ExamGrade.B
                    },
                    new ExamStatus()
                    {
                        Exam = Exams[2] ,
                        Grade = ExamGrade.C
                    },
                    new ExamStatus()
                    {
                        Exam = Exams[3] ,
                        Grade = ExamGrade.A
                    }
                }
            }
        };
    }
}