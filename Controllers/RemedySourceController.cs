using System;
using System.Collections.Generic;
using System.Data;
//sql Parameteres
using System.Data.SqlClient;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HumberAreaHospitalProject.Data;
using HumberAreaHospitalProject.Models;
using System.Diagnostics;

namespace HumberAreaHospitalProject.Controllers
{
    public class RemedySourceController : Controller
    {
        //db context
        private HospitalContext db = new HospitalContext();

        // GET: RemedySource
        public ActionResult List(string remedysrcsearchkey, int pagenum = 0)
        {

            //can we access the search key?
            //Debug.WriteLine("The search key is "+remedysrcsearchkey);

            string query = "Select * from RemedySources";

            List<SqlParameter> sqlparams = new List<SqlParameter>();

            if (remedysrcsearchkey != "")
            {
                //modify the query to include the search key
                query = query + " where RemedySource_name like @searchkey or RemedySource_url like @searchkey";
                sqlparams.Add(new SqlParameter("@searchkey", "%" + remedysrcsearchkey + "%"));
                //Debug.WriteLine("The query is "+ query);
            }

            List<RemedySource> remedysrc = db.RemedySource.SqlQuery(query, sqlparams.ToArray()).ToList();

            //Start of Pagination Algorithm (Raw MSSQL)
            int perpage = 3;
            int remsrccount = remedysrc.Count();
            int maxpage = (int)Math.Ceiling((decimal)remsrccount / perpage) - 1;
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

                if (remedysrcsearchkey != "")
                {
                    newparams.Add(new SqlParameter("@searchkey", "%" + remedysrcsearchkey + "%"));
                    ViewData["remedysrcsearchkey"] = remedysrcsearchkey;
                }
                newparams.Add(new SqlParameter("@start", start));
                newparams.Add(new SqlParameter("@perpage", perpage));
                string pagedquery = query + " order by RemedySource_id offset @start rows fetch first @perpage rows only ";
                Debug.WriteLine(pagedquery);
                Debug.WriteLine("offset " + start);
                Debug.WriteLine("fetch first " + perpage);
                remedysrc = db.RemedySource.SqlQuery(pagedquery, newparams.ToArray()).ToList();
            }
            //End of Pagination Algorithm

            return View(remedysrc);
        }

        public ActionResult New()
        {
            //add new remedy src
            return View();
        }

        [HttpPost]
        public ActionResult New(string RemedySource_name, string RemedySource_url)
        {

            //add remedy src
            string query = "insert into RemedySources (RemedySource_name, RemedySource_url) values(@RemedySource_name,@RemedySource_url)";
            SqlParameter[] sqlparams = new SqlParameter[2];
            sqlparams[0] = new SqlParameter("@RemedySource_name", RemedySource_name);
            sqlparams[1] = new SqlParameter("@RemedySource_url", RemedySource_url);

            db.Database.ExecuteSqlCommand(query, sqlparams);
            return RedirectToAction("List");
        }

        public ActionResult Update(int id)
        {
            //fetch existed details of the remedy srcs
            string query = "select * from RemedySources where RemedySource_id = @id";
            var parameter = new SqlParameter("@id", id);
            RemedySource selectedRemedysrc = db.RemedySource.SqlQuery(query, parameter).FirstOrDefault();

            return View(selectedRemedysrc);
        }

        //update existed details
        [HttpPost]
        public ActionResult Update(int id, string RemedySource_name, string RemedySource_url)
        {
            //Debug.WriteLine("selected remedysrc is" + id);
            string query = "Update RemedySources set RemedySource_name=@RemedySource_name, RemedySource_url=@RemedySource_url where RemedySource_id=@id";
            SqlParameter[] sqlparams = new SqlParameter[3];
            sqlparams[0] = new SqlParameter("@RemedySource_name", RemedySource_name);
            sqlparams[1] = new SqlParameter("@RemedySource_url", RemedySource_url);
            sqlparams[2] = new SqlParameter("@id", id);
            db.Database.ExecuteSqlCommand(query, sqlparams);
            return RedirectToAction("List");

        }

        public ActionResult View(int id)
        {
            string query = "Select * from RemedySources where RemedySource_id=@id";
            var parameter = new SqlParameter("@id", id);
            RemedySource selectedRemedysrc = db.RemedySource.SqlQuery(query, parameter).FirstOrDefault();

            return View(selectedRemedysrc);
        }
        public ActionResult Delete(int id)
        {
            string query = "Select * from RemedySources where RemedySource_id=@id";
            var parameter = new SqlParameter("@id", id);
            RemedySource selectedRemedysrc = db.RemedySource.SqlQuery(query, parameter).FirstOrDefault();

            return View(selectedRemedysrc);
        }

        [HttpPost]
        public ActionResult Delete(int? id)
        {
            string query = "Delete from RemedySources where RemedySource_id=@id";
            var parameter = new SqlParameter("@id", id);
            db.Database.ExecuteSqlCommand(query, parameter);
            return RedirectToAction("List");
        }
    }
}