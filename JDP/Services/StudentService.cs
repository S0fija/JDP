﻿using JDP.Contracts.Repositories;
using JDP.Contracts.Services;
using JDP.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JDP.Services
{
    public class StudentService :
        IStudentService
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IExamRepository _examRepository;
        private const bool StudentIsActive = true;
        private const bool StudentIsInactive = false;

        public StudentService(IStudentRepository studentRepository, IExamRepository examRepository)
        {
            _studentRepository = studentRepository;
            _examRepository = examRepository;
        }

        #region StudentsGroupedByStatus
        public async Task<StudentStatusInfoDto> GetStudentsGroupedByStatus()
        {
            //Should be checked if student is active or not
            // use LINQ .GroupBy method

            var allStudents = await _studentRepository.GetStudents(student => true);

            var studentsGroupedByStatus = allStudents.GroupBy(student => student.IsStudentActive());

            var active = studentsGroupedByStatus.First(group => group.Key == StudentIsActive);
            var inactive = studentsGroupedByStatus.First(group => group.Key == StudentIsInactive);

            var studentStatusInfo = new StudentStatusInfoDto
            {
                ActiveStudents = MakeStudentList(active).ToList(),
                InactiveStudents = MakeStudentList(inactive).ToList()
            };

            return studentStatusInfo;
        }

        private IEnumerable<StudentDto> MakeStudentList(IGrouping<bool, Models.Student> studentGroup)
        {
            foreach (var student in studentGroup)
            {
                yield return new StudentDto { FullName = student.FullName };
            }
        }
        #endregion StudentsGroupedByStatus

        public async Task<ExamDto> GetListOfAvailableExamsToEnrollFroStudent(int studentId)
        {
            // create extension method "ToDto" in which LINQ .Select method should be used
            throw new NotImplementedException();
        }

        public async Task<StudentExamListDto> GetStudentExamListWithGrades(int studentId)
        {
            // use LINQ .Aggregate method and .OrderBy grade
            throw new NotImplementedException();
        }

    }
}