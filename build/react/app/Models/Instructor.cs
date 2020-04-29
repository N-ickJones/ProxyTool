using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProxyWorldApi.Models
{
    public class Instructor
    {
        public int InstructorID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Title { get; set; }
        public DateTime EmploymentDate { get; set; }
        public DateTime TerminationDate { get; set; }
        public ICollection<Course> Courses { get; set; }
    }
}
