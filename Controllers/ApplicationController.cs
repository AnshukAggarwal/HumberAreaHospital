using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;
using HumberAreaHospitalProject.Models;
using System.Data.Entity;
using HumberAreaHospitalProject.Data;

namespace HumberAreaHospitalProject.Controllers
{
    public class ApplicationController : Controller
    {
        private HospitalContext db = new HospitalContext();
        // GET: Application
        public ActionResult List(String applicationsearchkey, int pagenum = 0)
        {
            string query = "select * from Applications";
            List<SqlParameter> sqlparams = new List<SqlParameter>();
            List<Application> applications = db.Applications.SqlQuery(query, sqlparams.ToArray()).ToList();
            return View(applications);
        }
    }
}