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
using Microsoft.AspNet.Identity;
using ApplicationUser = HumberAreaHospitalProject.Data.ApplicationUser;

namespace HumberAreaHospitalProject.Controllers
{
    public class SpecialityController : Controller
    {
        //create db context
        private HospitalContext db = new HospitalContext();
        // GET: Speciality
        [Authorize]
        public ActionResult List()
        {
            Debug.WriteLine("Trying to list all the records");
            string query = "Select * from specialities";
            List<Speciality> specialities = db.Specialities.SqlQuery(query).ToList();
            return View(specialities);

        }
        [Authorize]
        public ActionResult New()
        {
            
            string currentUserId = User.Identity.GetUserId();
            ApplicationUser currentUser = db.Users.FirstOrDefault(x => x.Id == currentUserId);
            Debug.WriteLine("The current userid is " + currentUserId);
            var a = User.Identity.GetUserName();
            Debug.WriteLine("The current userid is " + a);
            //This method only needs the add page, Nothing from the db
            return View();
        }
        [HttpPost]
        public ActionResult New(string SpecialityName)
        {
            /*This method takes in a new speciality name and writes it to the DB*/
            string query = "insert into specialities (Name) values(@SpecialityName)";
            SqlParameter parameter = new SqlParameter("@SpecialityName", SpecialityName);

            db.Database.ExecuteSqlCommand(query, parameter);
            return RedirectToAction("List");
        }
        [Authorize]
        public ActionResult Update(int id)
        {
            /*this method will show the base info of the selected record*/
            Debug.WriteLine("I am trying to update record with id" + id);
            string query = "select * from specialities where specialityid = @id";
            SqlParameter parameter = new SqlParameter("@id", id);
            Speciality selectedspeciality = db.Specialities.SqlQuery(query, parameter).FirstOrDefault();

            return View(selectedspeciality);
        }
        //now the post method when the user clicks on update
        [HttpPost]
        public ActionResult Update(int id, string SpecialityName)
        {
            Debug.WriteLine("I am trying to update record with id" + id);
            string query = "Update specialities set Name=@SpecialityName where specialityid=@id";
            SqlParameter[] parameters = new SqlParameter[2];
            parameters[0] = new SqlParameter("@SpecialityName", SpecialityName);
            parameters[1] = new SqlParameter("@id", id);
            db.Database.ExecuteSqlCommand(query, parameters);
            return RedirectToAction("List");

        }
        [Authorize]
        public ActionResult View(int id)
        {
            string query = "Select * from specialities where specialityid=@id";
            SqlParameter parameter = new SqlParameter("@id", id);
            Speciality selectedspeciality = db.Specialities.SqlQuery(query, parameter).FirstOrDefault();

            return View(selectedspeciality);

        }
        [Authorize]
        public ActionResult Delete(int id)
        {
            string query = "Select * from specialities where specialityid=@id";
            SqlParameter parameter = new SqlParameter("@id", id);
            Speciality selectedspeciality = db.Specialities.SqlQuery(query, parameter).FirstOrDefault();

            return View(selectedspeciality);
        }
        [HttpPost]
        public ActionResult Delete(int? id)
        {
            string query = "Delete from specialities where specialityid=@id";
            SqlParameter parameter = new SqlParameter("@id", id);
            db.Database.ExecuteSqlCommand(query, parameter);
            return RedirectToAction("List");
        }
    }
}