using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace MYQuizClient
{
    public partial class QuestionBlock
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public List<Question> Questions { get; set; } = new List<Question>();
    }
}