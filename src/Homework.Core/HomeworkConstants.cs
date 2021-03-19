using System;
using System.Collections.Generic;
using System.Text;

namespace Homework.Core
{
    public static class HomeworkConstants
    {
        public static class WorkTypes
        {
            //选择题
            public const string ChoiceQuestion = "choice_question";
            //问答题
            public const string QuestionAndAnswer = "question_and_answer";
        }
        public enum WorkType
        {
            ChoiceQuestion,
            QuestionAndAnswer
        }
    }
}
