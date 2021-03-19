using System;
using System.Collections.Generic;
using System.Text;

namespace Homework.DB.Entities
{
    //学生做好的作业
    public partial class StudentWork
    {
        public string StudentWorkId { get; set; }
        public string StudentId { get; set; }
        public string HomeworkId { get; set; }
        public DateTime SubmissionTime { get; set; }
        public double TotalScore { get; set; }
        //是否批阅
        public bool IsReview { get; set; }
        public string TeacherName { get; set; }
        public bool IsSubmited { get; set; }
        public Homework Homework { get; set; }
        public Student Student { get; set; }
        public ICollection<StudentAnswer> StudentAnswers { get; set; }
    }
}
