using System;
using System.Linq;
using System.Threading.Tasks;
using JDP.Contracts.Services;
using Microsoft.Extensions.DependencyInjection;

namespace JDP
{
    class Program
    {
        // - Implement methods of IStudentService
        // - Create method for assigning exam to student
        // - Write unit tests for all services public methods
        // - Optimize DI where it is possible
        static async Task Main(string[] args)
        {
            var serviceProvider = AppStartup.BuildServiceProvider();
            var examService = serviceProvider.GetService<IExamService>();
            var studentService = serviceProvider.GetService<IStudentService>();
            var exams =  await examService.GetListOfAvailableExams();

            studentService.GetStudentsGroupedByStatus();

            exams.ToList().ForEach(exam =>
            {
                Console.WriteLine(exam.ExamName);
            });
            
        }
    }
}
