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
            studentResult.Should().HaveCount(7);
            //studentResult.FirstOrDefault()?.ExamName.Should().Be("Exam 3");
            //studentResult.ToList()[1]?.ExamName.Should().Be("Exam 4");
            //studentResult.ToList()[2]?.ExamName.Should().Be("Exam 5");
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
            studentStatusInfo.ActiveStudents.Should().HaveCountLessThan(5);
            studentStatusInfo.InactiveStudents.Should().HaveCountLessThan(5);

            //studentStatusInfo.ActiveStudents.FirstOrDefault()?.FullName.Should().Be("Student 1");
            //studentStatusInfo.InactiveStudents.FirstOrDefault()?.FullName.Should().Be("Student 3");

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
            studentExams.Exams.Should().HaveCount(3);
            //studentExams.Exams.FirstOrDefault()?.ExamName.Should().Be("Exam 1");

            //studentExams.StudentName.Should().Be("Sofija Spasic");
        }

        private Student GetMockStudentBy(int studentId)
        {
            var faker = new Faker();
            var exams = GetExamStatuses(3);

            return new Student
            {
                Id = studentId,
                FullName = faker.Person.FullName,
                ActiveStatusExpirationDateTime = GetRandomDateNearToday(10, 3),
                Exams = exams
            };
        }

        private List<Exam> GetMockExams()
        {
            var autoFaker = new AutoFaker<Exam>();
            var exams1 = Enumerable.Range(0, 10).Select(_ => autoFaker.Generate()).ToList();

            var faker = new Faker();
            int examId = 0;
            var exams = Enumerable.Range(0, 10)
                //.Select(_ => new Exam() { Id = ++examId, Title = faker.Hacker.Phrase() }).ToList();
                .Select(_ => new Exam() { Id = ++examId, Title = faker.Music.Genre() }).ToList();

            return exams;
        }

        private IEnumerable<Student> GetMockStudents(Expression<Func<Student, bool>> predicate)
        {
            var studentId = 1;
            var students = Enumerable.Range(0, 4)
                .Select(_ => new Student
                {
                    Id = studentId++,
                    FullName = GetRandomPersonFullName(),
                    ActiveStatusExpirationDateTime = GetRandomDateNearToday(20, 20),
                    Exams = GetExamStatuses(2)
                }).ToList();

            var response = students.AsQueryable().Where(predicate);
            return response;
        }

        private List<ExamStatus> GetExamStatuses(int numberOfStatuses)
        {
            var faker = new Faker();
            var examId = 1;

            var examStatuses = Enumerable.Range(0, numberOfStatuses)
                .Select(_ => new ExamStatus
                {
                    Exam = new Exam
                    {
                        Id = examId++,
                        Title = faker.Company.CatchPhrase()
                    },
                    Grade = (ExamGrade)examId
                })
                .ToList();

            return examStatuses;
        }

        private string GetRandomPersonFullName()
        {
            var faker = new Faker();
            return faker.Person.FullName;
        }

        private DateTime GetRandomDateNearToday(int numberOfDaysBeforeToday, int numberOfDaysAfterToday)
        {
            var faker = new Faker();
            return faker.Date.Between(DateTime.Now.AddDays(-numberOfDaysBeforeToday), DateTime.Now.AddDays(numberOfDaysAfterToday));
        }
    }
}
