using System.Collections.Generic;
using JDP.Models;

namespace JDP.Dtos
{
    public class StudentStatusInfoDto
    {
        public IEnumerable<StudentDto> ActiveStudents { get; set; }
        public IEnumerable<StudentDto> InactiveStudents { get; set; }
    }
}