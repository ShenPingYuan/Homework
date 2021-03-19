using System;
using System.Collections.Generic;
using System.Text;

namespace Homework.DB.Entities
{
    //course entity class
    public partial class Course
    {
        public Course()
        {
            Homeworks = new HashSet<Homework>();
            StudentCourses = new HashSet<StudentCourse>();
        }

        public string CourseId { get; set; }
        public string CourseName { get; set; }

        //public string EnglishName { get; set; }
        //public string CScore { get; set; }
        public virtual ICollection<Homework> Homeworks { get; set; }
        public virtual Teacher Teacher { get; set; }
        public virtual ICollection<StudentCourse> StudentCourses { get; set; }
    }
}
