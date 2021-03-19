using System;
using System.Collections.Generic;
using System.Text;

namespace Homework.DB.Entities
{
    public partial class StudentCourse
    {
        public string CourseId { get; set; }
        public string StudentId { get; set; }
        public Student Student { get; set; }
        public Course Course { get; set; }
    }
}
