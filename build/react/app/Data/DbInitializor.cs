using ProxyWorldApi.Data;
using ProxyWorldApi.Models;
using System;
using System.Linq;

namespace ProxyWorldApi.Data
{
    public static class DbInitializer
    {
        public static void Initialize(ApplicationDbContext context)
        {
            context.Database.EnsureCreated();
            if (context.Students.Any())
            {
                return;
            }

            var catalogs = new Catalog[]
            {
                //this was good
                new Catalog{CatalogID=1436, Name="Programming Fundamentals I", Description="This course is an introduction to programming. Topics include fundamental concepts of computer programming and software development methodology, including data types, control structures, functions, arrays, and the mechanics of programming running, testing, and debugging."},
                new Catalog{CatalogID=1437, Name="Programming Fundamentals II", Description="This course is a continuation of COSC 1436 and focuses on design, implementation, and re-usability of computer programs with abstract data types."},
                new Catalog{CatalogID=2327, Name="Intro to Computer Networks", Description="Students are introduced to installation, usage, and management of computer hardware and operating systems for business."},
                new Catalog{CatalogID=2329, Name="Comp Organiz & Machine Lang.", Description="Students are introduced to instruction set architectures, emphasizing central processor organization and operations."},
                new Catalog{CatalogID=2347, Name="Special Topics/Programming.", Description="Students engage in an in-depth study of a programming language used to implement information systems."},
                new Catalog{CatalogID=3312, Name="Numerical Methods", Description="Student study the concepts underlying the use of the computer for interpolation, approximations, solutions of equations and the solution of both linear and nonlinear systems equations."},
                new Catalog{CatalogID=3318, Name="Data Base Management Systems", Description="Students explore the design of information systems using database software and query language/programming interfaces."},
                new Catalog{CatalogID=3319, Name="Data Structures and Algorithms", Description="Student are introduced to such topics as orthogonal lists, strings, arrays, linked lists, multilinked structures, indexed and direct files, and generalized data management and database management systems."},
                new Catalog{CatalogID=3321, Name="Digital System Design", Description="This course is an introduction to Boolean Algebra and graph theory with emphasis on their applications in the design of digital computer software and hardware."},
                new Catalog{CatalogID=3327, Name="Computer Architecture", Description="This course is a continuation of COSC 2329, exploring computer organization and architectures in more depth and breadth."},
                new Catalog{CatalogID=3331, Name="Human-Computer Interaction", Description="Students are provided a comprehensive introduction to the principles and techniques of human-computer interaction. "},
                new Catalog{CatalogID=4314, Name="Data Mining", Description="Students are provided an introduction to the newly-emerging field of data mining."},
                new Catalog{CatalogID=4316, Name="Compiler Design & Construction", Description="Students study the design and implementation of assemblers, interpreters and compilers"},
                new Catalog{CatalogID=4318, Name="Advanced Language Concepts", Description="Students study programming languages which support the Object-Oriented Programming (OOP) paradigm"},
                new Catalog{CatalogID=4319, Name="Software Engineering", Description="This course is an introduction to formal methods of specifying, designing, implementing and testing software for large programming projects"},
                new Catalog{CatalogID=4320, Name="System Modeling and Simulation", Description="This course is an introduction to modeling and simulation for analysis of computer software and hardware"},
                new Catalog{CatalogID=4326, Name="Network Theory", Description="Students examine the theoretical basis for data communication together with an examination of the structures and protocols associated with the control of error, congestion and routing"},
                new Catalog{CatalogID=4327, Name="Computer Operating Systems", Description="This course is concerned with software organization of computer systems"},
                new Catalog{CatalogID=4332, Name="Computer Graphics", Description="Students are introduced to graphical APIis used in developing graphical user interfaces and multimedia applications"},
                new Catalog{CatalogID=4340, Name="Spc Tpcs in Computer Sci", Description="Topics of general interest are offered on a timely basis. Previous topics include Cognitive Computing, Embedded Linux Systems, Visual Graphics/Component Systems"},
                new Catalog{CatalogID=4349, Name="Professionalism and Ethics", Description="Students examine the nature, need and value of well-formed ethical constructs within the digital forensics profession"},
            };
            foreach (Catalog catalog in catalogs)
            {
                context.Catalogs.Add(catalog);
            }
            context.SaveChanges();

            var instructors = new Instructor[]
            {
                //Added InstructorID and fixed titles and changes employment dates
                new Instructor{InstructorID=1, FirstName="Peter",LastName="Cooper", Title="Professor and Department Chair", EmploymentDate=DateTime.Parse("1990-01-15")},
                new Instructor{InstructorID=2, FirstName="Narasimha",LastName="Karpoor-Shashidhar", Title="Associate Professor and Assistant Chair", EmploymentDate=DateTime.Parse("2005-01-15")},
                new Instructor{InstructorID=3, FirstName="Brad",LastName="Glisson", Title="Director of the Cyber Forensics Intelligence Center and Associate Professor", EmploymentDate=DateTime.Parse("2001-01-15")},
                new Instructor{InstructorID=4, FirstName="David",LastName="Burris", Title="Professor and University Articulation Coordinator", EmploymentDate=DateTime.Parse("1980-01-15")},
                new Instructor{InstructorID=5, FirstName="Cihan",LastName="Varol", Title="Associate Professor and Graduate Advisor", EmploymentDate=DateTime.Parse("2010-01-15")},
                new Instructor{InstructorID=6, FirstName="Jenny",LastName="Bing-Zhou", Title="Associate Professor and Graduate Advisor", EmploymentDate=DateTime.Parse("2009-01-15")},
                new Instructor{InstructorID=7, FirstName="Gary",LastName="Smith", Title="Associate Professor", EmploymentDate=DateTime.Parse("2011-01-15")},
                new Instructor{InstructorID=8, FirstName="Frank",LastName="Liu", Title="Associate Professor", EmploymentDate=DateTime.Parse("2015-01-15")},
                new Instructor{InstructorID=9, FirstName="Hyuk",LastName="Cho", Title="Associate Professor", EmploymentDate=DateTime.Parse("2012-01-15")},
                new Instructor{InstructorID=10, FirstName="Li-Jen",LastName="Lester", Title="Associate Professor", EmploymentDate=DateTime.Parse("2014-01-15")},
                new Instructor{InstructorID=11, FirstName="Mingkui",LastName="Wei", Title="Associate Professor", EmploymentDate=DateTime.Parse("2016-01-15")},
                new Instructor{InstructorID=12, FirstName="Min Kyung",LastName="An", Title="Associate Professor", EmploymentDate=DateTime.Parse("2017-01-15")},
                new Instructor{InstructorID=13, FirstName="ABM",LastName="Islam", Title="Associate Professor", EmploymentDate=DateTime.Parse("2020-01-15")},
                new Instructor{InstructorID=14, FirstName="Hacer",LastName="Varol", Title="Instructor", EmploymentDate=DateTime.Parse("2018-01-15")},
                new Instructor{InstructorID=15, FirstName="Pat",LastName="Ko", Title="Assistant Professor", EmploymentDate=DateTime.Parse("2019-01-15")},
                new Instructor{InstructorID=16, FirstName="Amar",LastName="Rasheed", Title="Assistant Professor", EmploymentDate=DateTime.Parse("2014-01-15")},
            };
            foreach (Instructor instructor in instructors)
            {
                context.Instructors.Add(instructor);
            }
            context.SaveChanges();

            var students = new Student[]
            {
                //I fixed the Enrollment Dates to be different
                new Student{StudentID=1, FirstName="Melvin",LastName="Glover", EnrollmentDate=DateTime.Parse("2015-08-12")},
                new Student{StudentID=2, FirstName="Paul",LastName="Ortiz", EnrollmentDate=DateTime.Parse("2017-01-15")},
                new Student{StudentID=3, FirstName="Eddie",LastName="Mcdonald", EnrollmentDate=DateTime.Parse("2016-01-15")},
                new Student{StudentID=4, FirstName="David",LastName="Mann", EnrollmentDate=DateTime.Parse("2019-08-15")},
                new Student{StudentID=5, FirstName="Danny",LastName="Fuller", EnrollmentDate=DateTime.Parse("2015-01-15")},
                new Student{StudentID=6, FirstName="Karla",LastName="Fields", EnrollmentDate=DateTime.Parse("2016-01-12")},
                new Student{StudentID=7, FirstName="Albert",LastName="Curtis", EnrollmentDate=DateTime.Parse("2019-08-01")},
                new Student{StudentID=8, FirstName="Velma",LastName="Pierce", EnrollmentDate=DateTime.Parse("2017-01-15")},
                new Student{StudentID=9, FirstName="Theodore",LastName="Bush", EnrollmentDate=DateTime.Parse("2020-08-15")},
                new Student{StudentID=10, FirstName="Sam",LastName="Rodriguez", EnrollmentDate=DateTime.Parse("2015-01-15")},
                new Student{StudentID=11, FirstName="Carrie",LastName="Johnston", EnrollmentDate=DateTime.Parse("2016-01-12")},
                new Student{StudentID=12, FirstName="Courtney",LastName="Webster", EnrollmentDate=DateTime.Parse("2017-08-15")},
                new Student{StudentID=13, FirstName="Tami",LastName="Reed", EnrollmentDate=DateTime.Parse("2019-08-15")},
                new Student{StudentID=14, FirstName="Francis",LastName="Ramsey", EnrollmentDate=DateTime.Parse("2015-01-01")},
                new Student{StudentID=15, FirstName="Alyssa",LastName="Griffith", EnrollmentDate=DateTime.Parse("2016-08-15")},
                new Student{StudentID=16, FirstName="Alberto",LastName="Blake", EnrollmentDate=DateTime.Parse("2017-01-15")},
                new Student{StudentID=17, FirstName="Ernestine",LastName="Kim", EnrollmentDate=DateTime.Parse("2018-08-12")},
                new Student{StudentID=18, FirstName="Conelius",LastName="Rivera", EnrollmentDate=DateTime.Parse("2018-08-01")},
                new Student{StudentID=19, FirstName="Clifton",LastName="Gutierrez", EnrollmentDate=DateTime.Parse("2019-08-12")},
                new Student{StudentID=20, FirstName="Cecelia",LastName="Owen", EnrollmentDate=DateTime.Parse("2015-08-15")},
                new Student{StudentID=21, FirstName="Laurie",LastName="Gardner", EnrollmentDate=DateTime.Parse("2020-01-15")},
                new Student{StudentID=22, FirstName="Guy",LastName="Wood", EnrollmentDate=DateTime.Parse("2020-01-15")},
                new Student{StudentID=23, FirstName="Viola",LastName="Mcgee", EnrollmentDate=DateTime.Parse("2018-08-01")},
                new Student{StudentID=24, FirstName="Elsie",LastName="Price", EnrollmentDate=DateTime.Parse("2015-01-15")},
                new Student{StudentID=25, FirstName="Nadine",LastName="Maxwell", EnrollmentDate=DateTime.Parse("2017-01-12")},
                new Student{StudentID=26, FirstName="Rodolfo",LastName="Harrington", EnrollmentDate=DateTime.Parse("2016-08-15")},
                new Student{StudentID=27, FirstName="Terrence",LastName="Hodges", EnrollmentDate=DateTime.Parse("2019-01-15")},
                new Student{StudentID=28, FirstName="Gordon",LastName="Gomez", EnrollmentDate=DateTime.Parse("2015-08-15")},
                new Student{StudentID=29, FirstName="Lorene",LastName="Ballard", EnrollmentDate=DateTime.Parse("2017-01-15")},
                new Student{StudentID=30, FirstName="Alicia",LastName="Collins", EnrollmentDate=DateTime.Parse("2016-06-12")},
                new Student{StudentID=31, FirstName="Lora",LastName="Phelps", EnrollmentDate=DateTime.Parse("2015-06-01")},
                new Student{StudentID=32, FirstName="Raul",LastName="Foster", EnrollmentDate=DateTime.Parse("2018-06-15")},
                new Student{StudentID=33, FirstName="Al",LastName="Carr", EnrollmentDate=DateTime.Parse("2020-01-05")},
                new Student{StudentID=34, FirstName="Michael",LastName="Shelton", EnrollmentDate=DateTime.Parse("2020-01-12")},
                new Student{StudentID=35, FirstName="Doreen",LastName="Kennedy", EnrollmentDate=DateTime.Parse("2015-06-15")},
                new Student{StudentID=36, FirstName="Rochelle",LastName="Ramirez", EnrollmentDate=DateTime.Parse("2016-01-15")},
                new Student{StudentID=37, FirstName="Debbie",LastName="Diaz", EnrollmentDate=DateTime.Parse("2019-06-15")},
                new Student{StudentID=38, FirstName="Elisa",LastName="Keller", EnrollmentDate=DateTime.Parse("2017-01-15")},
                new Student{StudentID=39, FirstName="Lynda",LastName="Bell", EnrollmentDate=DateTime.Parse("2018-08-15")},
                new Student{StudentID=40, FirstName="Edith",LastName="Bowman", EnrollmentDate=DateTime.Parse("2020-08-05")},
                new Student{StudentID=41, FirstName="Janice",LastName="Campbell", EnrollmentDate=DateTime.Parse("2015-01-15")},
                new Student{StudentID=42, FirstName="Mario",LastName="Schultz", EnrollmentDate=DateTime.Parse("2017-01-15")},
                new Student{StudentID=43, FirstName="Adrienne",LastName="Moore", EnrollmentDate=DateTime.Parse("2016-08-15")},
                new Student{StudentID=44, FirstName="Pete",LastName="Robinson", EnrollmentDate=DateTime.Parse("2019-01-15")},
                new Student{StudentID=45, FirstName="Sheryl",LastName="Page", EnrollmentDate=DateTime.Parse("2018-08-12")},
                new Student{StudentID=46, FirstName="Marcus",LastName="Brown", EnrollmentDate=DateTime.Parse("2017-01-15")},
                new Student{StudentID=47, FirstName="Rosemary",LastName="Saunders", EnrollmentDate=DateTime.Parse("2019-08-05")},
                new Student{StudentID=48, FirstName="Kendra",LastName="Poole", EnrollmentDate=DateTime.Parse("2016-01-15")},
                new Student{StudentID=49, FirstName="Israel",LastName="Goodman", EnrollmentDate=DateTime.Parse("2017-08-12")},
                new Student{StudentID=50, FirstName="David",LastName="Hall", EnrollmentDate=DateTime.Parse("2018-01-15")},
                new Student{StudentID=51, FirstName="Cameron",LastName="Wade", EnrollmentDate=DateTime.Parse("2019-08-15")},
            };
            foreach (Student student in students)
            {
                context.Students.Add(student);
            }
            context.SaveChanges();

            var courses = new Course[]
            {
                new Course{CourseID=1, CatalogID=1436, InstructorID=10, Title="Programming Fundamentals I", Credit=4},
                new Course{CourseID=2, CatalogID=1436, InstructorID=14, Title="Programming Fundamentals I", Credit=4},
                new Course{CourseID=3, CatalogID=1437, InstructorID=15, Title="Programming Fundamentals II", Credit=4},
                new Course{CourseID=4, CatalogID=1437, InstructorID=9, Title="Programming Fundamentals II", Credit=4},
                new Course{CourseID=5, CatalogID=2327, InstructorID=12, Title="Intro to Computer Networks", Credit=3},
                new Course{CourseID=6, CatalogID=2329, InstructorID=15, Title="Comp Organiz & Machine Lang", Credit=3},
                new Course{CourseID=7, CatalogID=2329, InstructorID=16, Title="Comp Organiz & Machine Lang", Credit=3},
                new Course{CourseID=8, CatalogID=2347, InstructorID=4, Title="Special Topics/Programming", Credit=3},
                new Course{CourseID=9, CatalogID=3318, InstructorID=6, Title="Data Base Management", Credit=3},
                new Course{CourseID=10, CatalogID=3319, InstructorID=4, Title="Data Structures and Algorithms", Credit=3},
                new Course{CourseID=11, CatalogID=3321, InstructorID=11, Title="Digital System Design", Credit=3},
                new Course{CourseID=12, CatalogID=3327, InstructorID=16, Title="Computer Architecture", Credit=3},
                new Course{CourseID=13, CatalogID=3331, InstructorID=6, Title="Human-Computer Interaction", Credit=3},
                new Course{CourseID=14, CatalogID=4316, InstructorID=4, Title="Compiler Design & Construction", Credit=3},
                new Course{CourseID=15, CatalogID=4318, InstructorID=7, Title="Advanced Language Concepts", Credit=3},
                new Course{CourseID=16, CatalogID=4319, InstructorID=13, Title="Software Engineering", Credit=3},
                new Course{CourseID=17, CatalogID=4320, InstructorID=13, Title="System Modeling and Simulation", Credit=3},
                new Course{CourseID=18, CatalogID=4326, InstructorID=5, Title="Network Theory", Credit=3},
                new Course{CourseID=19, CatalogID=4327, InstructorID=7, Title="Computer Operating Systems", Credit=3},
                new Course{CourseID=20, CatalogID=4332, InstructorID=1, Title="Computer Graphics", Credit=3},
                new Course{CourseID=21, CatalogID=4340, InstructorID=1, Title="Spc Tpcs in Computer Sci", Credit=3},
                new Course{CourseID=22, CatalogID=4349, InstructorID=10, Title="Professionalism and Ethics", Credit=3},
            };
            foreach (Course course in courses)
            {
                context.Courses.Add(course);
            }
            context.SaveChanges();

            var enrollments = new Enrollment[]
            {
                new Enrollment{EnrollmentID=1, StudentID=1, CourseID=1, Grade=Grade.A},
                new Enrollment{EnrollmentID=2, StudentID=2, CourseID=1, Grade=Grade.A},
                new Enrollment{EnrollmentID=3, StudentID=3, CourseID=1, Grade=Grade.A},
                new Enrollment{EnrollmentID=4, StudentID=4, CourseID=2, Grade=Grade.C},
                new Enrollment{EnrollmentID=5, StudentID=5, CourseID=2, Grade=Grade.D},
                new Enrollment{EnrollmentID=6, StudentID=6, CourseID=2, Grade=Grade.A},
                new Enrollment{EnrollmentID=7, StudentID=7, CourseID=3, Grade=null},
                new Enrollment{EnrollmentID=8, StudentID=8, CourseID=3, Grade=null},
                new Enrollment{EnrollmentID=9, StudentID=9, CourseID=3, Grade=null},
                new Enrollment{EnrollmentID=10, StudentID=10, CourseID=4, Grade=null},
                new Enrollment{EnrollmentID=11, StudentID=11, CourseID=4, Grade=null},
                new Enrollment{EnrollmentID=12, StudentID=12, CourseID=4, Grade=Grade.A},
                new Enrollment{EnrollmentID=13, StudentID=13, CourseID=5, Grade=Grade.B},
                new Enrollment{EnrollmentID=14, StudentID=14, CourseID=5, Grade=Grade.D},
                new Enrollment{EnrollmentID=15, StudentID=15, CourseID=5, Grade=Grade.C},
                new Enrollment{EnrollmentID=16, StudentID=16, CourseID=6, Grade=Grade.C},
                new Enrollment{EnrollmentID=17, StudentID=17, CourseID=6, Grade=Grade.C},
                new Enrollment{EnrollmentID=18, StudentID=18, CourseID=6, Grade=Grade.B},
                new Enrollment{EnrollmentID=19, StudentID=19, CourseID=7, Grade=Grade.B},
                new Enrollment{EnrollmentID=20, StudentID=20, CourseID=7, Grade=Grade.B},
                new Enrollment{EnrollmentID=21, StudentID=21, CourseID=7, Grade=Grade.D},
                new Enrollment{EnrollmentID=22, StudentID=22, CourseID=8, Grade=Grade.D},
                new Enrollment{EnrollmentID=23, StudentID=23, CourseID=8, Grade=Grade.D},
                new Enrollment{EnrollmentID=24, StudentID=24, CourseID=8, Grade=Grade.B},
                new Enrollment{EnrollmentID=25, StudentID=25, CourseID=9, Grade=Grade.B},
                new Enrollment{EnrollmentID=26, StudentID=26, CourseID=9, Grade=Grade.B},
                new Enrollment{EnrollmentID=27, StudentID=27, CourseID=9, Grade=Grade.B},
                new Enrollment{EnrollmentID=28, StudentID=28, CourseID=10, Grade=Grade.C},
                new Enrollment{EnrollmentID=29, StudentID=29, CourseID=10, Grade=Grade.D},
                new Enrollment{EnrollmentID=30, StudentID=30, CourseID=10, Grade=Grade.C},
                new Enrollment{EnrollmentID=31, StudentID=31, CourseID=11, Grade=Grade.F},
                new Enrollment{EnrollmentID=32, StudentID=32, CourseID=11, Grade=Grade.F},
                new Enrollment{EnrollmentID=33, StudentID=33, CourseID=11, Grade=Grade.F},
                new Enrollment{EnrollmentID=34, StudentID=34, CourseID=12, Grade=Grade.F},
                new Enrollment{EnrollmentID=35, StudentID=35, CourseID=12, Grade=Grade.A},
                new Enrollment{EnrollmentID=36, StudentID=36, CourseID=13, Grade=Grade.A},
                new Enrollment{EnrollmentID=37, StudentID=37, CourseID=13, Grade=Grade.A},
                new Enrollment{EnrollmentID=38, StudentID=38, CourseID=13, Grade=Grade.B},
                new Enrollment{EnrollmentID=39, StudentID=39, CourseID=14, Grade=Grade.B},
                new Enrollment{EnrollmentID=40, StudentID=40, CourseID=14, Grade=Grade.B},
                new Enrollment{EnrollmentID=41, StudentID=41, CourseID=15, Grade=Grade.B},
                new Enrollment{EnrollmentID=42, StudentID=42, CourseID=16, Grade=Grade.B},
                new Enrollment{EnrollmentID=43, StudentID=43, CourseID=17, Grade=null},
                new Enrollment{EnrollmentID=44, StudentID=44, CourseID=18, Grade=null},
                new Enrollment{EnrollmentID=45, StudentID=45, CourseID=18, Grade=null},
                new Enrollment{EnrollmentID=46, StudentID=46, CourseID=19, Grade=null},
                new Enrollment{EnrollmentID=47, StudentID=47, CourseID=20, Grade=null},
                new Enrollment{EnrollmentID=48, StudentID=48, CourseID=21, Grade=null},
                new Enrollment{EnrollmentID=49, StudentID=49, CourseID=22, Grade=null},
                new Enrollment{EnrollmentID=50, StudentID=50, CourseID=22, Grade=Grade.A},
                new Enrollment{EnrollmentID=51, StudentID=51, CourseID=22, Grade=Grade.A},

            };
            foreach (Enrollment enrollment in enrollments)
            {
                context.Enrollments.Add(enrollment);
            }
            context.SaveChanges();
        }
    }
}