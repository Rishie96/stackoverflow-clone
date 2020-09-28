using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace StackOverflow.Models
{
    public class QuestionVotes
    {
        [Key]
        public int ID { get; set; }
        public string UserID { get; set; }
        public int PostID { get; set; }
        public string Type { get; set; }
    }
}