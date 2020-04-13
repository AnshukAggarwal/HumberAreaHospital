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

namespace HumberAreaHospitalProject.Controllers
{
    public class SurveyController : Controller
    {
        private HospitalContext db = new HospitalContext();
        // GET: Response
        public ActionResult Form()
        { 
            Debug.WriteLine("Trying to list all the records");
            string query = "Select * from questions";
            List<Question> questions = db.Questions.SqlQuery(query).ToList();
            return View(questions);
        }
        [HttpPost]
        public ActionResult Form (ICollection <string> ResponseText, ICollection<int> Count) {
            var result = ResponseText.Zip(Count, (r, q) => new { Response=r, Question=q});
            foreach (var rq in result) {
                string query = "Insert into surveys (ResponseText,QuestionID) values(@ResponseText,@id)";
                SqlParameter[] parameters = new SqlParameter[2];
                parameters[0] = new SqlParameter("@ResponseText", rq.Response);
                parameters[1] = new SqlParameter("@id", rq.Question);
                db.Database.ExecuteSqlCommand(query, parameters);
            }
            
            
            return RedirectToAction("Thanks");
        }
        public ActionResult Thanks()
        {
            return View();
        }
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
        //[HttpPost]
        //public ActionResult List (int id)
        //{
        //    string query = "Select * from surveys where questionid=@id";
        //    SqlParameter[] parameters = new SqlParameter[1];
        //    parameters[0] = new SqlParameter("@id", id);
        //    List<Survey> answers = db.Responses.SqlQuery(query, parameters).ToList();
        //    var ListSurvey = new ListSurvey();
        //    ListSurvey.surveys = answers;
        //    return View(answers);
        //}
    }
}