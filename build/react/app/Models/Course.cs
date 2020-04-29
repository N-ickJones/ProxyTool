using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProxyWorldApi.Models
{
    public class Course
    {
        public int CourseID { get; set; }
        public int CatalogID { get; set; }
        public int InstructorID { get; set; }
        public string Title { get; set; }
        public int Credit { get; set; }
        public ICollection<Enrollment> Enrollments { get; set; }
    }
}
