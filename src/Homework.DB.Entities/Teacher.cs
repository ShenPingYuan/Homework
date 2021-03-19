using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Homework.DB.Entities
{
    //the teacher entity class
    public partial class Teacher
    {
        public Teacher()
        {
        }
        [Key]
        public string TeacherId { get; set; }
        public string TeacherName { get; set; }
        //public string NickName { get; set; }
        //public string EnglishName { get; set; }
        //public string Sex { get; set; }
        //public string Birthday { get; set; }
        public string CourseId { get; set; }
        public virtual Course Course { get; set; }
        //public string Province { get; set; }
        //public string City { get; set; }
        //public string Area { get; set; }
    }
}
