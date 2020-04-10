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
    public class ResponseController : Controller
    {
        private HospitalContext db = new HospitalContext();
        // GET: Response
        public ActionResult List()
        { 
            Debug.WriteLine("Trying to list all the records");
            string query = "Select * from questions";
            List<Question> questions = db.Questions.SqlQuery(query).ToList();
            return View(questions);
        }
        [HttpPost]
        public ActionResult List (int? id, string ResponseText) {
                
                string query = "Insert into responses (ResponseText,QuestionID) values(@ResponseText,@id)";
                SqlParameter[] parameters = new SqlParameter[2];
                parameters[0] = new SqlParameter("@ResponseText", ResponseText);
                parameters[1] = new SqlParameter("@id", id);
                db.Database.ExecuteSqlCommand(query, parameters);
            
            
            return RedirectToAction("List");
        }
    }
}