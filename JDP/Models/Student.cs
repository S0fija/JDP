using System;
using System.Collections.Generic;

namespace JDP.Models
{
    public class Student
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public DateTime ActiveStatusExpirationDateTime { get; set; }

        public IEnumerable<ExamStatus> Exams { get; set; }

        public bool IsStudentActive()
        {
            var isActive = DateTime.Now < ActiveStatusExpirationDateTime;

            return isActive;
        }
    }
}