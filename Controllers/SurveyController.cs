using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HumberAreaHospitalProject.Data;
using HumberAreaHospitalProject.Models;
using HumberAreaHospitalProject.Models.ViewModels;
using System.Diagnostics;
using System.IO;
using Microsoft.AspNet.Identity;
using ApplicationUser = HumberAreaHospitalProject.Data.ApplicationUser;

namespace HumberAreaHospitalProject.Controllers
{
    public class SurveyController : Controller
    {
        private HospitalContext db = new HospitalContext();
        // GET: Response
        [Authorize]
        public ActionResult Form()
        { 
            Debug.WriteLine("Trying to list all the records");
            string query = "Select * from questions";
            List<Question> questions = db.Questions.SqlQuery(query).ToList();
            return View(questions);
        }
        [HttpPost]
        public ActionResult Form (ICollection <string> ResponseText, ICollection<int> Count) {
            //string currentUserId = User.Identity.GetUserId();
            //ApplicationUser currentUser = db.Users.FirstOrDefault(x => x.Id == currentUserId);
            //Debug.WriteLine("The current userid is " + currentUserId);
            string username = User.Identity.GetUserName();
            Debug.WriteLine("The username is " + username);
            var result = ResponseText.Zip(Count, (r, q) => new { Response=r, Question=q});
            foreach (var rq in result) {
                string query = "Insert into surveys (ResponseText,QuestionID,UserName) values(@ResponseText,@id,@UserName)";
                SqlParameter[] parameters = new SqlParameter[3];
                parameters[0] = new SqlParameter("@ResponseText", rq.Response);
                parameters[1] = new SqlParameter("@id", rq.Question);
                parameters[2] = new SqlParameter("@UserName", username);
                db.Database.ExecuteSqlCommand(query, parameters);
            }
            
            
            return RedirectToAction("Thanks");
        }
        [Authorize]
        public ActionResult Thanks()
        {
            return View();
        }
        [Authorize]
        public ActionResult List()
        {
            ListSurvey question = new ListSurvey();
            question.questions = db.Questions.ToList();
            question.answers = db.Surveys.ToList();
            return View(question);
            //return View();
        }
       [HttpPost]
        public ActionResult List(string id)
        {
            string basequery = "Select * from questions";
            List<Question> questions = db.Questions.SqlQuery(basequery).ToList();

            string query = "Select * from surveys where questionid=@id";
            SqlParameter parameter = new SqlParameter("@id", id);
            List<Survey> answers = db.Surveys.SqlQuery(query, parameter).ToList();
            var ListSurvey = new ListSurvey();
            ListSurvey.answers = answers;
            ListSurvey.questions = questions;

            return View(ListSurvey);
            
            

        }
       
    }
}