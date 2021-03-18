using FluentAssertions;
using JDP.Contracts.Repositories;
using JDP.Models;
using JDP.Services;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace JDP.Test.Services
{
    public class ExamServiceShould
    {
        private Mock<IExamRepository> _repository;
        private ExamService _systemUnderTest;

        public ExamServiceShould()
        {
            _repository = new Mock<IExamRepository>();
            _systemUnderTest = new ExamService(_repository.Object);
        }

        [Fact]
        public async Task GetListOfAvailableExams_ReturnCollection()
        {
            // Arrange
            var exams = GetMockExams();
            _repository.Setup(instance => instance.GetExams())
                .ReturnsAsync(exams);

            // Act
            var examsResults = await _systemUnderTest.GetListOfAvailableExams();

            // Assert
            examsResults.Should().HaveCount(3);
            examsResults.FirstOrDefault()?.ExamName.Should().Be("Exam 1");
            examsResults.ToList()[1]?.ExamName.Should().Be("Exam 2");
            examsResults.ToList()[2]?.ExamName.Should().Be("Exam 3");
            examsResults.Should().NotContainNulls();
            /// ...

            //OR
            Assert.Equal(3, examsResults.Count());
            Assert.Equal("Exam 1", examsResults.FirstOrDefault()?.ExamName);
            /// ...
        }

        private List<Exam> GetMockExams()
        {
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
                    }
                };
            return exams;
        }
    }
}