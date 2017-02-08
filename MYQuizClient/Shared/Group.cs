using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MYQuizClient
{
   public class Group
    {
        public string id { get; set; }
        public string title { get; set; }
        public string enterGroupPin { get; set; }
        public List<SingleTopic> singleTopics { get; set; }
    }
}
