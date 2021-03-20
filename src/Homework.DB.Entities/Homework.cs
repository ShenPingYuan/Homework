using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Homework.DB.Entities
{
    //the homework entity class
    public partial class Homework
    {
        public Homework()
        {
            StudentWorks = new HashSet<StudentWork>();
            Questions = new HashSet<Question>();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string HomeworkId { get; set; }
        public string CourseId { get; set; }
        //作业布置时间
        public DateTime AssignTime { get; set; }
        //作业截止时间
        public DateTime Deadline { get; set; }
        public Course Course { get; set; }
        //public WorkType Work { get; set; }
        public virtual ICollection<Question> Questions { get; set; }
        public virtual ICollection<StudentWork> StudentWorks { get; set; }
    }
}
