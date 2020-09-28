using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace StackOverflow.Models
{
    public class AnswerVotes
    {
        [Key]
        public int ID { get; set; }
        public string UserID { get; set; }
        public int AnswerID { get; set; }
        public string Type { get; set; }
    }
}