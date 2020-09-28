using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StackOverflow.Models
{
    public class QuestionData
    {
        public int PostID { get; set; }
        public string UserID { get; set; }
        public string UserName { get; set; }
        public DateTime Date { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public int Likes { get; set; }
        public int Dislikes { get; set; }
    }
}