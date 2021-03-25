using AutoBogus;
using Bogus;
using FluentAssertions;
using JDP.Contracts.Repositories;
using JDP.Models;
using JDP.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Xunit;

namespace JDP.Tests.Services
{
    public class StudentServiceShould
    {
        private Mock<IStudentRepository> _studentRepository;
        private Mock<IExamRepository> _examRepository;
        private StudentService _systemUnderTest;

        public StudentServiceShould()
        {
            _studentRepository = new Mock<IStudentRepository>();
            _examRepository = new Mock<IExamRepository>();
            _systemUnderTest = new StudentService(_studentRepository.Object, _examRepository.Object);
        }

        [Fact]
        public async Task GetListOfAvailableExamsToEnrollForStudent_ReturnCollection()
        {
            // Arrange
            int studentId = 1;
            var student = GetMockStudentBy(studentId);
            var exams = GetMockExams();

            _studentRepository
                .Setup(instance => instance.GetStudentBy(studentId))
                .ReturnsAsync(student);
            _examRepository
                .Setup(instance => instance.GetExams())
                .ReturnsAsync(exams);

            // Act
            var studentResult = await _systemUnderTest.GetListOfAvailableExamsToEnrollForStudent(studentId);

            // Assert
            studentResult.Should().HaveCount(3);
            studentResult.FirstOrDefault()?.ExamName.Should().Be("Exam 3");
            studentResult.ToList()[1]?.ExamName.Should().Be("Exam 4");
            studentResult.ToList()[2]?.ExamName.Should().Be("Exam 5");
            studentResult.Should().NotContainNulls();
        }
        
        [Fact]
        public async Task GetStudentsGroupedByStatus_ReturnObject()
        {
            // Arrange
            var studentsList = GetMockStudents(student => true);

            _studentRepository
                .Setup(instance => instance.GetStudents(student => true))
                .ReturnsAsync(studentsList);

            // Act
            var studentStatusInfo = await _systemUnderTest.GetStudentsGroupedByStatus();

            // Assert
            studentStatusInfo.ActiveStudents.Should().HaveCount(3);
            studentStatusInfo.InactiveStudents.Should().HaveCount(1);

            studentStatusInfo.ActiveStudents.FirstOrDefault()?.FullName.Should().Be("Student 1");
            studentStatusInfo.InactiveStudents.FirstOrDefault()?.FullName.Should().Be("Student 3");

            //studentResult.Should().NotContainNulls();
        }
        
        [Fact]
        public async Task GetStudentExamListWithGrades_ReturnObject()
        {
            // Arrange
            int studentId = 1;
            var student = GetMockStudentBy(studentId);

            _studentRepository
                .Setup(instance => instance.GetStudentBy(studentId))
                .ReturnsAsync(student);

            // Act
            var studentExams = await _systemUnderTest.GetStudentExamListWithGrades(studentId);

            // Assert
            studentExams.Exams.Should().HaveCount(2);
            studentExams.Exams.FirstOrDefault()?.ExamName.Should().Be("Exam 1");

            studentExams.StudentName.Should().Be("Sofija Spasic");
        }

        private Student GetMockStudentBy(int studentId)
        {
            var exams = new List<ExamStatus>()
                {
                    new ExamStatus()
                    {
                        Exam = new Exam
                        {
                            Id = 1,
                            Title = "Exam 1"
                        },
                        Grade = ExamGrade.A
                    },
                    new ExamStatus()
                    {
                        Exam = new Exam
                        {
                            Id = 2,
                            Title = "Exam 2"
                        },
                        Grade = ExamGrade.B
                    }
                };

            return new Student
            {
                Id = studentId,
                FullName = "Sofija Spasic",
                ActiveStatusExpirationDateTime = new System.DateTime(2021, 12, 31),
                Exams = exams
            };
        }

        private List<Exam> GetMockExams()
        {
            //https://github.com/nickdodd79/AutoBogus
            var autoFaker = new AutoFaker<Exam>();
            var exams1 = Enumerable.Range(0, 3).Select(_ => autoFaker.Generate()).ToList();

            //https://github.com/bchavez/Bogus
            var faker = new Faker();
            int examId = 0;
            var exams2 = Enumerable.Range(0, 3)
                .Select(_ => new Exam() { Id = ++examId, Title = faker.Hacker.Phrase() }).ToList();

            var exams = new List<Exam>()
                {
                    new Exam
                    {
                        Id = 1,
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
                        Id = 4,
                        Title = "Exam 4"
                    },
                    new Exam
                    {
                        Id = 5,
                        Title = "Exam 5"
                    }
                };
            return exams;
        }

        private IEnumerable<Student> GetMockStudents(Expression<Func<Student, bool>> predicate)
        {
            var exams = GetMockExams();
            List<Student> students = new List<Student>()
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
                            Exam = exams[0] ,
                            Grade = ExamGrade.A
                        },
                        new ExamStatus()
                        {
                            Exam = exams[1] ,
                            Grade = ExamGrade.B
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
                            Exam = exams[0] ,
                            Grade = ExamGrade.B
                        },
                        new ExamStatus()
                        {
                            Exam = exams[1] ,
                            Grade = ExamGrade.C
                        },
                        new ExamStatus()
                        {
                            Exam = exams[2] ,
                            Grade = ExamGrade.D
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
                            Exam = exams[4] ,
                            Grade = ExamGrade.A
                        },
                        new ExamStatus()
                        {
                            Exam = exams[2] ,
                            Grade = ExamGrade.Unrated
                        },
                        new ExamStatus()
                        {
                            Exam = exams[3] ,
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
                            Exam = exams[4] ,
                            Grade = ExamGrade.A
                        },
                        new ExamStatus()
                        {
                            Exam = exams[1] ,
                            Grade = ExamGrade.B
                        },
                        new ExamStatus()
                        {
                            Exam = exams[2] ,
                            Grade = ExamGrade.C
                        },
                        new ExamStatus()
                        {
                            Exam = exams[3] ,
                            Grade = ExamGrade.A
                        }
                    }
                }
            };

            var response = students.AsQueryable().Where(predicate);
            return response;
        }
    }
}
