using JDP.Contracts.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Threading.Tasks;

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
            var exams =  await examService.GetListOfAvailableExams();
            exams.ToList().ForEach(exam =>
            {
                Console.WriteLine(exam.ExamName);
            });

            var studentService = serviceProvider.GetService<IStudentService>();
            var studentStatus = await studentService.GetListOfAvailableExamsToEnrollForStudent(4);
            var studentsExamsGrades = await studentService.GetStudentExamListWithGrades(4);

            var nesto = 0;
        }
    }
}
