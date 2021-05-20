using JDP.Contracts.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace JDP
{
    class Program
    {
        // - Implement methods of IStudentService DONE
        // - Create method for assigning exam to student DONE
        // - Write unit tests for all services public methods DONE
        // - Optimize DI where it is possible ?

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

            var availableExamsForStudent = await studentService.GetListOfAvailableExamsToEnrollForStudent(4);
            
            var studentsExamsWithGradesList = await studentService.GetStudentExamListWithGrades(4);
        }
    }
}
