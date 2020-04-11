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
using System.Diagnostics;
using System.IO;

namespace HumberAreaHospitalProject.Controllers
{
    public class QuestionController : Controller
    {
        //create db context
        private HospitalContext db = new HospitalContext();
        // GET: Question

        public ActionResult List()
        {
            Debug.WriteLine("Trying to list all the records");
            string query = "Select * from questions";
            List<Question> questions= db.Questions.SqlQuery(query).ToList();
            return View(questions);

        }
        public ActionResult New()
        {
            return View();
        }
        [HttpPost]
        public ActionResult New(string QuestionText)
        {
            /*This method takes in a new speciality name and writes it to the DB*/
            string query = "insert into questions (QuestionText) values(@QuestionText)";
            SqlParameter parameter = new SqlParameter("@QuestionText", QuestionText);

            db.Database.ExecuteSqlCommand(query, parameter);
            return RedirectToAction("List");
        }
        public ActionResult Update(int id)
        {
            /*this method will show the base info of the selected record*/
            Debug.WriteLine("I am trying to update record with id" + id);
            string query = "select * from questions where questionid = @id";
            SqlParameter parameter = new SqlParameter("@id", id);
            Question selectedquestion = db.Questions.SqlQuery(query, parameter).FirstOrDefault();

            return View(selectedquestion);
        }
        //now the post method
        [HttpPost]
        public ActionResult Update(int id,string QuestionText)
        {
            Debug.WriteLine("I am trying to update record with id" + id);
            string query = "Update questions set QuestionText=@QuestionText where questionid=@id";
            SqlParameter[] parameters = new SqlParameter[2];
            parameters[0] = new SqlParameter("@QuestionText", QuestionText);
            parameters[1] = new SqlParameter("@id", id);
            db.Database.ExecuteSqlCommand(query, parameters);
            return RedirectToAction("List");

        }
        public ActionResult View(int id)
        {
            string query = "Select * from questions where questionid=@id";
            SqlParameter parameter = new SqlParameter("@id", id);
            Question selectedquestion = db.Questions.SqlQuery(query, parameter).FirstOrDefault();

            return View(selectedquestion);

        }
        public ActionResult Delete(int id)
        {
            string query = "Select * from questions where questionid=@id";
            SqlParameter parameter = new SqlParameter("@id", id);
            Question selectedquestion = db.Questions.SqlQuery(query, parameter).FirstOrDefault();

            return View(selectedquestion);
        }
        [HttpPost]
        public ActionResult Delete(int? id)
        {
            string query = "Delete from questions where questionid=@id";
            SqlParameter parameter = new SqlParameter("@id", id);
            db.Database.ExecuteSqlCommand(query, parameter);
            return RedirectToAction("List");
        }
    }
}