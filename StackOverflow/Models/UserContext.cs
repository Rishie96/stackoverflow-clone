using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace StackOverflow.Models
{
    public class UserContext : DbContext
    {
        public UserContext() : base("DefaultConnection")
        {

        }
        public DbSet<UserDetails> Details { get; set; }
        public DbSet<Questions> Questions { get; set; }
        public DbSet<Answers> Answers { get; set; }
        public DbSet<QuestionVotes> QuestionVotes { get; set; }
        public DbSet<AnswerVotes> AnswerVotes { get; set; }
    }
}