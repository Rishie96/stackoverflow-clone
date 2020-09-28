using StackOverflow.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Data;
using System.Configuration;

namespace StackOverflow.Services
{
    public class PostsServices
    {
        protected UserContext _dbContext;
        string _connectonString;
        public PostsServices()
        {
            _dbContext = new UserContext();
            _connectonString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        }

        // Get all posts/questions
        public List<Questions> GetAllQuestions()
        {
            return _dbContext.Questions.ToList();
        }
        
        // Vote a question
        public void VoteQuestion(QuestionVotes QV)
        {
            QuestionVotes OldQV = _dbContext.QuestionVotes.Where(q => q.PostID == QV.PostID && q.UserID == QV.UserID).FirstOrDefault();
            if (OldQV != null)
            {
                if (OldQV.Type == QV.Type)
                {
                    return;
                }
                else
                {
                    _dbContext.QuestionVotes.Remove(OldQV);
                    _dbContext.QuestionVotes.Add(QV);
                }
            }
            else
            {
                _dbContext.QuestionVotes.Add(QV);
            }
            _dbContext.SaveChanges();
        }

        // Get a particular question/post
        public QuestionData GetQuestionData(int PostID)
        {
            Questions question = _dbContext.Questions.Where(p => p.PostID == PostID).FirstOrDefault();
            List<QuestionVotes> questionVotes = _dbContext.QuestionVotes.Where(p => p.PostID == PostID).ToList();

            // Question votes
            int questionLikes = questionVotes.Where(q => q.Type == "like").ToList().Count;
            int questionDislikes = questionVotes.Where(q => q.Type == "dislike").ToList().Count;

            // Get user name of post owner
            UserDetails UD = _dbContext.Details.Where(u => u.UserID == question.UserID).FirstOrDefault();
            string UserName = UD.FirstName + " " + UD.LastName;

            QuestionData QD = new QuestionData
            {
                PostID = question.PostID,
                UserID = question.UserID,
                UserName = UserName,
                Date = question.Date,
                Title = question.Title,
                Description = question.Description,
                Category = question.Category,
                Likes = questionLikes,
                Dislikes = questionDislikes
            };
            return QD;
        }

        // Get all answers for a particular question
        public List<AnswerData> GetAnswerData(int PostID)
        {
            List<AnswerData> AD = new List<AnswerData>();
            List<AnswerVotes> answerVotes = null;
            List<Answers> answers = _dbContext.Answers.Where(p => p.PostID == PostID).ToList();

            // For every answer, add its corresponding votes
            foreach (var ans in answers)
            {
                // Get user name of post owner
                UserDetails UD = _dbContext.Details.Where(u => u.UserID == ans.UserID).FirstOrDefault();
                string UserName = UD.FirstName + " " + UD.LastName;

                int likes = 0;
                int dislikes = 0;
                answerVotes = _dbContext.AnswerVotes.Where(a => a.AnswerID == ans.AnswerID).ToList();
                if (answerVotes.Count > 0)
                {
                    likes = answerVotes.Where(a => a.Type == "like").ToList().Count;
                    dislikes = answerVotes.Where(a => a.Type == "dislike").ToList().Count;
                }
                AD.Add(new AnswerData {
                    AnswerID = ans.AnswerID,
                    PostID = ans.PostID,
                    UserID = ans.UserID,
                    UserName = UserName,
                    Date = ans.Date,
                    Answer = ans.Answer,
                    Likes = likes,
                    Dislikes = dislikes
                });
            }
            return AD;
        }

        // Add a question/post
        public void AddQuestion(Questions question)
        {
            _dbContext.Questions.Add(question);
            _dbContext.SaveChanges();
        }

        // Add an answer
        public void AddAnswer(string UserID, Answers answer)
        {
            Answers newAnswer = new Answers()
            {
                UserID = UserID,
                PostID = answer.PostID,
                Date = answer.Date,
                Answer = answer.Answer,
                Likes = 0,
                Dislikes = 0
            };
            _dbContext.Answers.Add(newAnswer);
            _dbContext.SaveChanges();
        }

        // Delete a post/question
        public void DeletePost(int PostID)
        {
            Questions QD = _dbContext.Questions.Where(q => q.PostID == PostID).FirstOrDefault();
            if (QD != null)
            {
                string DeleteProcedure = "Posts_Delete";
                SqlConnection sqlCon = new SqlConnection
                {
                    ConnectionString = _connectonString
                };
                sqlCon.Open();
                SqlCommand sqlCmd = new SqlCommand(DeleteProcedure, sqlCon)
                {
                    CommandType = CommandType.StoredProcedure
                };
                sqlCmd.Parameters.Add("@PostID", SqlDbType.Int).Value = PostID;
                SqlDataReader sqlReader = sqlCmd.ExecuteReader();
                sqlCon.Close();
            }
        }

        // Delete an answer
        public void DeleteAnswer(int AnswerID)
        {
            Answers QD = _dbContext.Answers.Where(q => q.AnswerID == AnswerID).FirstOrDefault();
            if (QD != null)
            {
                string DeleteProcedure = "Answer_Delete";
                SqlConnection sqlCon = new SqlConnection
                {
                    ConnectionString = _connectonString
                };
                sqlCon.Open();
                SqlCommand sqlCmd = new SqlCommand(DeleteProcedure, sqlCon)
                {
                    CommandType = CommandType.StoredProcedure
                };
                sqlCmd.Parameters.Add("@AnswerID", SqlDbType.Int).Value = AnswerID;
                SqlDataReader sqlReader = sqlCmd.ExecuteReader();
                sqlCon.Close();
            }
        }
    }
}