using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Homework.DB.Entities
{
    //题目答案
    public partial class StudentAnswer
    {

        public string StudentWorkId { get; set; }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string StudentAnswerId { get; set; }
        public string Answer { get; set; }
        public double Score { get; set; }
        public StudentWork StudentWork { get; set; }
    }
}
