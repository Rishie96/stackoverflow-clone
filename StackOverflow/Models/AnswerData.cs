using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StackOverflow.Models
{
    public class AnswerData
    {
        public int AnswerID { get; set; }
        public int PostID { get; set; }
        public string UserID { get; set; }
        public string UserName { get; set; }
        public DateTime Date { get; set; }
        public string Answer { get; set; }
        public int Likes { get; set; }
        public int Dislikes { get; set; }
    }
}