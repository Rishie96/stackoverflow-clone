using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace StackOverflow.Models
{
    public class Answers
    {
        [Key]
        public int AnswerID { get; set; }
        public int PostID { get; set; }
        public string UserID { get; set; }
        public DateTime Date { get; set; }
        public string Answer { get; set; }
        public int Likes { get; set; }
        public int Dislikes { get; set; }
    }
}