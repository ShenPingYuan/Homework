using System;
using System.Collections.Generic;
using System.Text;

namespace Homework.DB.Entities
{
    ////the student entity class
    public partial class Student
    {
        public Student()
        {
            StudentCourses = new HashSet<StudentCourse>();
        }

        public string StudentId { get; set; }
        public string StudentName { get; set; }
        //public string EnglishName { get; set; }
        //public string Sex { get; set; }
        //public DateTime Birthday { get; set; }
        //public int? Year { get; set; }
        //public string Character { get; set; }
        //public string Pwd { get; set; }
        /// <summary>
        /// 家庭地址/省
        /// </summary>
        //public string Province { get; set; }
        ///// <summary>
        ///// 家庭地址/市
        ///// </summary>
        //public string City { get; set; }
        ///// <summary>
        ///// 家庭地址/区
        ///// </summary>
        //public string Area { get; set; }
        
        public virtual ICollection<StudentCourse> StudentCourses { get; set; }
    }
}
