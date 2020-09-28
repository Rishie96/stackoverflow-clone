using StackOverflow.Models;
using StackOverflow.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StackOverflow.Controllers
{
    public class PostsController : Controller
    {
        protected PostsServices _postsServices;
        public PostsController()
        {
            _postsServices = new PostsServices();
        }
        
        // Get all posts
        [Route("posts")]
        public ActionResult Index(string filter = null)
        {
            ViewBag.Questions = _postsServices.GetAllQuestions();
            return View(_postsServices.GetAllQuestions());
        }

        // Add a question/post
        [Authorize]
        [Route("askquestion")]
        [HttpGet]
        public ActionResult AskQuestion()
        {
            ViewBag.UserID = Session["UserID"];
            return View();
        }

        [Authorize]
        [Route("askquestion")]
        [HttpPost]
        public ActionResult AskQuestion(Questions question)
        {
            _postsServices.AddQuestion(question);
            return RedirectToAction("Index", "Posts");
        }

        // View a particular question/post
        [Route("viewpost/{id}")]
        [HttpGet]
        public ActionResult ViewPost(int id)
        {
            QuestionData QD = _postsServices.GetQuestionData(id);
            List<AnswerData> AD = _postsServices.GetAnswerData(id);
            if (QD == null)
            {
                return RedirectToAction("Index", "Posts");
            }
            ViewBag.QuestionData = QD;
            ViewBag.AnswersData = AD;
            return View();
        }

        // Vote a question
        [Authorize]
        [Route("votequestion/{id}/{type}")]
        public ActionResult VoteQuestion(int id, string type)
        {
            if (Session["UserID"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            string UserID = Session["UserID"].ToString();
            QuestionVotes vote = new QuestionVotes
            {
                UserID = UserID,
                PostID = id,
                Type = type
            };
            _postsServices.VoteQuestion(vote);
            return RedirectToAction("ViewPost", "Posts", new { id });
        }

        // Add an answer
        [Authorize]
        [Route("addanswer")]
        [HttpPost]
        public ActionResult AddAnswer(Answers newAnswer)
        {
            string UserID = Session["UserID"].ToString();
            _postsServices.AddAnswer(UserID, newAnswer);
            return RedirectToAction("Index", "Posts");
        }

        // Delete a post/question
        [Authorize]
        [HttpGet]
        [Route("delete")]
        public ActionResult Delete(int PostID = 0)
        {
            if (PostID != 0)
            {
                _postsServices.DeletePost(PostID);
            }
            return RedirectToAction("Index", "Posts");
        }

        // Delete an answer
        [Authorize]
        [HttpGet]
        [Route("deleteanswer")]
        public ActionResult DeleteAnswer(int AnswerID = 0)
        {
            if (AnswerID != 0)
            {
                _postsServices.DeleteAnswer(AnswerID);
            }
            return RedirectToAction("Index", "Posts");
        }
    }
}