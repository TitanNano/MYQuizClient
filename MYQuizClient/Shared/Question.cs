using System;
using System.Collections.Generic;

namespace MYQuizClient
{
    public partial class Question
    {
        public long Id { get; set; }
        public string Category { get; set; }
        public string MultipleChoice { get; set; }
        public bool MultipleChoiceBool { get { return MultipleChoice.ToLower() == "true"; } }
        public string Text { get; set; }
        public List<AnswerOption> AnswerOptions { get; set; } = new List<AnswerOption>();

    }
}