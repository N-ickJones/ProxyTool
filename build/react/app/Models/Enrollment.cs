using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProxyWorldApi.Models
{
    public class Enrollment
    {
        public int EnrollmentID { get; set; }
        public int CourseID { get; set; }
        public int StudentID { get; set; }
        public Grade? Grade { get; set; }

        public Course Courses { get; set; }
        public Student Students { get; set; }
    }
    public enum Grade
    {
        A, B, C, D, F
    }
}
