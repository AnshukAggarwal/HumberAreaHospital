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
        public ActionResult List(String appsearchkey, int pagenum = 0)
        {
            string query = "select * from Applications";//SQL  query to select everything from Applications table
            List<SqlParameter> sqlparams = new List<SqlParameter>();
            if (appsearchkey != "") //Checkign if the search key is empty or null
            {
                query = query + " where ApplicantFirstName like @searchkey";//Appending sql query to existing query 
                sqlparams.Add(new SqlParameter("@searchkey", "%" + appsearchkey + "%"));
            }
            List<Application> Applications = db.Applications.SqlQuery(query, sqlparams.ToArray()).ToList();
            //Pagination for jobs 
            int perpage = 5;
            int petcount = Applications.Count();
            int maxpage = (int)Math.Ceiling((decimal)petcount / perpage) - 1;
            if (maxpage < 0) maxpage = 0;
            if (pagenum < 0) pagenum = 0;
            if (pagenum > maxpage) pagenum = maxpage;
            int start = (int)(perpage * pagenum);
            ViewData["pagenum"] = pagenum;
            ViewData["pagesummary"] = "";
            if (maxpage > 0)
            {
                ViewData["pagesummary"] = (pagenum + 1) + " of " + (maxpage + 1);
                List<SqlParameter> newparams = new List<SqlParameter>();

                if (appsearchkey != "")
                {
                    newparams.Add(new SqlParameter("@searchkey", "%" + appsearchkey + "%"));
                    ViewData["appsearchkey"] = appsearchkey;
                }
                newparams.Add(new SqlParameter("@start", start));
                newparams.Add(new SqlParameter("@perpage", perpage));
                string pagedquery = query + " order by ApplicationDate offset @start rows fetch first @perpage rows only ";
                Applications = db.Applications.SqlQuery(pagedquery, newparams.ToArray()).ToList();
            }
            //End of Pagination


            return View(Applications);
        }
        //Add new Application
        [HttpPost]
        public ActionResult New(string ApplicantFirstName, string ApplicantLastName, string ApplicantEmail, string ApplicantPhone, string ApplicantAddress, string ApplicantCity,string ApplicantProvince,string ApplicantZipCode)
        {
            string ApplicationDate = DateTime.Now.ToString("dd/MM/yyyy");
            string query = "insert into Applications (ApplicantFirstName, ApplicantLastName, ApplicantEmail, ApplicantPhone, ApplicantAddress, ApplicantCity, ApplicantProvince, ApplicantZipCode, ApplicantionDate) values (@JobTitle, @JobCategory, @JobType, @Description, @Requirements, @ApplicationDate)";
            SqlParameter[] sqlparams = new SqlParameter[9];
            sqlparams[0] = new SqlParameter("ApplicantFirstName", ApplicantFirstName);
            sqlparams[1] = new SqlParameter("@ApplicantLastName", ApplicantLastName);
            sqlparams[2] = new SqlParameter("@ApplicantEmail", ApplicantEmail);
            sqlparams[3] = new SqlParameter("@ApplicantPhone", ApplicantPhone);
            sqlparams[4] = new SqlParameter("@ApplicantAddress", ApplicantAddress);
            sqlparams[5] = new SqlParameter("@ApplicantCity", ApplicantCity);
            sqlparams[6] = new SqlParameter("@ApplicantProvince", ApplicantProvince);
            sqlparams[7] = new SqlParameter("@ApplicantZipCode", ApplicantZipCode);
            sqlparams[8] = new SqlParameter("@ApplicationDate", ApplicationDate);
            db.Database.ExecuteSqlCommand(query, sqlparams);
            return RedirectToAction("List");
        }
        public ActionResult New()
        {
            return View();

        }
    }
}