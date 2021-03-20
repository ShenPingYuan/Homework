using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Homework.DB.Entities
{
    //Choices of multiple choice questions
    public partial class Choice
    {
        public string QuestionId { get; set; }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string ChoiceId { get; set; }
        public char ChoiceOrder { get; set; }
        public string Content { get; set; }
        public Question Question { get; set; }
    }
}
