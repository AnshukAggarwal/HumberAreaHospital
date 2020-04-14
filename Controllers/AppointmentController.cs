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
using Microsoft.AspNet.Identity.Owin;

namespace HumberAreaHospitalProject.Controllers
{
    public class AppointmentController : Controller
    {
        private HospitalContext db = new HospitalContext();
        //Viewing appointments
        //pagination method from Christine Bittle 
        //search method from Christine Bittle
        public ActionResult List(string appointmentSearchKey, int pagenum = 0)
        {
            //checking if the users logged in
            if((System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
            { 
            List<Appointment> appointments = db.Appointments.Where(a => (appointmentSearchKey != null) ? a.appointmentLname.Contains(appointmentSearchKey) : true).ToList();
            int perpage = 8;
            int appointmentCount = appointments.Count();
            int maxpage = (int)Math.Ceiling((decimal)appointmentCount / perpage) - 1;
            if (maxpage < 0) maxpage = 0;
            if (pagenum < 0) pagenum = 0;
            if (pagenum > maxpage) perpage = maxpage;
            int start = (int)(perpage * pagenum);
            ViewData["pagenum"] = pagenum;
            ViewData["pagesummary"] = "";
            if (maxpage > 0)
                {
                    ViewData["pagesummary"] = (pagenum + 1) + " of " + (maxpage + 1);
                    appointments = db.Appointments
                    .Where(a => (appointmentSearchKey != null) ? a.appointmentLname.Contains(appointmentSearchKey) : true)
                    .OrderBy(a => a.appointmentId)
                    .Skip(start)
                    .Take(perpage)
                    .ToList();
                }
             
            return View(appointments);
            } else
            //if not sends to login page   
            {
                return RedirectToAction("Login","Account");
            }
        }
        //Adding an appointment
        public ActionResult Create()
        {   //checks if user is logged in
            if ((System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
            {
                AppointmentViewModel appointmentViewModel = new AppointmentViewModel();
                appointmentViewModel.doctors = db.Doctors.ToList();
                //using a view model to also show doctors
                return View(appointmentViewModel);
            }
            else
            {
                return RedirectToAction("index");
            }
        }
        //sends added appointment
        [HttpPost]
        public ActionResult Create(string firstName, string lastName, string phoneNum, string email, DateTime dateTime, int doctorID)
        {
            string query = "insert into appointments (appointmentFname, appointmentLname, appointmentPhone, appointmentEmail, appointmentDate, DoctorID)"
                + "values (@appointmentFname, @appointmentLname, @appointmentPhone, @appointmentEmail, @appointmentDate, @DoctorID)";
            SqlParameter[] sqlParameters = new SqlParameter[6];
            sqlParameters[0] = new SqlParameter("@appointmentFname", firstName);
            sqlParameters[1] = new SqlParameter("@appointmentLname", lastName);
            sqlParameters[2] = new SqlParameter("@appointmentPhone", phoneNum);
            sqlParameters[3] = new SqlParameter("@appointmentEmail", email);
            sqlParameters[4] = new SqlParameter("@appointmentDate", dateTime);
            sqlParameters[5] = new SqlParameter("@DoctorID", doctorID);

            db.Database.ExecuteSqlCommand(query, sqlParameters);

            return RedirectToAction("List");
        }

        //selects appointment to update
        public ActionResult Update(int id)
        {
            //checks if logged in
            if((System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
            {
                string query = "select * from appointments where appointmentID = @id";
                var Parameter = new SqlParameter("@id", id);
                Appointment appointment = db.Appointments.SqlQuery(query, Parameter).FirstOrDefault();
                AppointmentViewModel appointmentViewModel = new AppointmentViewModel();
                appointmentViewModel.appointment = appointment;
                appointmentViewModel.doctors = db.Doctors.ToList();
                //using a view model to also show doctors
                return View(appointmentViewModel);
            }
            else
            {
                return RedirectToAction("index");
            }
           
        }

        //sends update
        [HttpPost]
        public ActionResult Update(int id, string firstName, string lastName, string phoneNum, string email, DateTime dateTime, int doctorID)
        {
            string query = "update appointments set appointmentFname = @firstName, appointmentLname = @lastName, appointmentPhone = @phoneNum, appointmentEmail = @email, appointmentDate = @dateTime, DoctorID = @doctorID where appointmentID = @id";

            SqlParameter[] sqlParameters = new SqlParameter[7];
            sqlParameters[0] = new SqlParameter("@firstName", firstName);
            sqlParameters[1] = new SqlParameter("@lastName", lastName);
            sqlParameters[2] = new SqlParameter("@phoneNum", phoneNum);
            sqlParameters[3] = new SqlParameter("@email", email);
            sqlParameters[4] = new SqlParameter("@dateTime", dateTime);
            sqlParameters[5] = new SqlParameter("@doctorID", doctorID);
            sqlParameters[6] = new SqlParameter("@id", id);

            db.Database.ExecuteSqlCommand(query, sqlParameters);
            return RedirectToAction("List");
        }

        //confirms delete
        public ActionResult Delete(int id)
        {
            if ((System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
            {
                string query = "select * from appointments where appointmentID = @id";
                var Parameter = new SqlParameter("@id", id);
                Appointment appointment = db.Appointments.SqlQuery(query, Parameter).FirstOrDefault();
                return View(appointment);
            }
            else
            {
                return RedirectToAction("index");
            }
        }

        //deletes an appointment
        [HttpPost]
        public ActionResult DeleteApp(int id)
        {
            string query = "delete from appointments where appointmentID = @id";
            var Parameter = new SqlParameter("@id", id);
            db.Database.ExecuteSqlCommand(query, Parameter);
            return RedirectToAction("List");
        }
    }
}
