using System;
using System.Collections.Generic;

namespace MYQuizClient
{
    public partial class GivenAnswer
    {
        public long Id { get; set; }
        public long? GroupId { get; set; }
        public long? QuestionId { get; set; }
        public long? FinalQuestionId { get; set; }
        public string DateNow { get; set; }
    }
}