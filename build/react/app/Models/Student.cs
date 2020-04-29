using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProxyWorldApi.Models
{
    public class Student
    {
        public int StudentID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime EnrollmentDate { get; set; }

        public ICollection<Course> Enrollments { get; set; }
    }
}
