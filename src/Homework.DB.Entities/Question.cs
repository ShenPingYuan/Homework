using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using static Homework.Core.HomeworkConstants;

namespace Homework.DB.Entities
{
    //题目实体
    public partial class Question
    {
        private ICollection<Choice> _choices;
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string QuestionId { get; set; }
        public string HomeworkId { get; set; }
        public string QuestionTitle { get; set; }
        public string Answer { get; set; }
        public WorkType WorkType { get; set; }
        public double Score { get; set; }
        public Homework Homework { get; set; }
        public ICollection<Choice> Choices
        {
            get
            {
                if (WorkType == WorkType.QuestionAndAnswer)
                {
                    return null;
                }
                else
                {
                    return _choices;
                }
            }
            set
            {
                _choices = value;
            }
        }

    }
}
