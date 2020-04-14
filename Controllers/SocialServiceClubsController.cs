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
using System.Diagnostics;
using HumberAreaHospitalProject.Data;
using HumberAreaHospitalProject.Models;

namespace HumberAreaHospitalProject.Controllers
{
    public class SocialServiceClubsController : Controller
    {
        // GET: SocialServiceClubs
        //db context
        private HospitalContext db = new HospitalContext();

        // GET: SocialServiceClubs
        public ActionResult List(string sscsearchkey, int pagenum = 0)
        {
            //can we access the search key?
            //Debug.WriteLine("The search key is "+sscsearchkey);

            string query = "Select * from SocialServiceClubs"; //order by is needed for offset
            //easier in a list.. we don't know how many more we'll add yet
            List<SqlParameter> sqlparams = new List<SqlParameter>();

            if (sscsearchkey != "")
            {
                //modify the query to include the search key
                query = query + " where SocialServiceClubs_title like @searchkey or SocialServiceClubs_details like @searchkey";
                sqlparams.Add(new SqlParameter("@searchkey", "%" + sscsearchkey + "%"));
                //Debug.WriteLine("The query is "+ query);
            }

            List<SocialServiceClubs> ssc = db.SocialServiceClubs.SqlQuery(query, sqlparams.ToArray()).ToList();

            //Start of Pagination Algorithm (Raw MSSQL)
            int perpage = 3;
            int remcount = ssc.Count();
            int maxpage = (int)Math.Ceiling((decimal)remcount / perpage) - 1;
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

                if (sscsearchkey != "")
                {
                    newparams.Add(new SqlParameter("@searchkey", "%" + sscsearchkey + "%"));
                    ViewData["sscsearchkey"] = sscsearchkey;
                }
                newparams.Add(new SqlParameter("@start", start));
                newparams.Add(new SqlParameter("@perpage", perpage));
                string pagedquery = query + " order by SocialServiceClubs_id offset @start rows fetch first @perpage rows only ";
                Debug.WriteLine(pagedquery);
                Debug.WriteLine("offset " + start);
                Debug.WriteLine("fetch first " + perpage);
                ssc = db.SocialServiceClubs.SqlQuery(pagedquery, newparams.ToArray()).ToList();
            }
            //End of Pagination Algorithm

            return View(ssc);
        }

        public ActionResult User_list(string sscsearchkey, int pagenum = 0)
        {
            //can we access the search key?
            //Debug.WriteLine("The search key is "+sscsearchkey);

            string query = "Select * from SocialServiceClubs"; //order by is needed for offset
            //easier in a list.. we don't know how many more we'll add yet
            List<SqlParameter> sqlparams = new List<SqlParameter>();

            if (sscsearchkey != "")
            {
                //modify the query to include the search key
                query = query + " where SocialServiceClubs_title like @searchkey or SocialServiceClubs_details like @searchkey";
                sqlparams.Add(new SqlParameter("@searchkey", "%" + sscsearchkey + "%"));
                //Debug.WriteLine("The query is "+ query);
            }

            List<SocialServiceClubs> ssc = db.SocialServiceClubs.SqlQuery(query, sqlparams.ToArray()).ToList();

            //Start of Pagination Algorithm (Raw MSSQL)
            int perpage = 3;
            int remcount = ssc.Count();
            int maxpage = (int)Math.Ceiling((decimal)remcount / perpage) - 1;
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

                if (sscsearchkey != "")
                {
                    newparams.Add(new SqlParameter("@searchkey", "%" + sscsearchkey + "%"));
                    ViewData["sscsearchkey"] = sscsearchkey;
                }
                newparams.Add(new SqlParameter("@start", start));
                newparams.Add(new SqlParameter("@perpage", perpage));
                string pagedquery = query + " order by SocialServiceClubs_id offset @start rows fetch first @perpage rows only ";
                Debug.WriteLine(pagedquery);
                Debug.WriteLine("offset " + start);
                Debug.WriteLine("fetch first " + perpage);
                ssc = db.SocialServiceClubs.SqlQuery(pagedquery, newparams.ToArray()).ToList();
            }
            //End of Pagination Algorithm

            return View(ssc);
        }





        /*public ActionResult List()
        {
            string query = "Select * from SocialServiceClubs";
            List<SocialServiceClubs> SocialServiceClubs = db.SocialServiceClubs.SqlQuery(query).ToList();
            return View(SocialServiceClubs);
        }*/

        public ActionResult New()
        {
            //add new remedy
            return View();
        }

        [HttpPost]
        public ActionResult New(string SocialServiceClubs_title, string SocialServiceClubs_details, string SocialServiceClubs_address, string SocialServiceClubs_map, string SocialServiceClubs_website)
        {
            //add social service clubs
            string query = "insert into SocialServiceClubs (SocialServiceClubs_title, SocialServiceClubs_details, SocialServiceClubs_address, SocialServiceClubs_map, SocialServiceClubs_website) values(@SocialServiceClubs_title,@SocialServiceClubs_details,@SocialServiceClubs_address, @SocialServiceClubs_map,@SocialServiceClubs_website)";
            SqlParameter[] sqlparams = new SqlParameter[5];
            sqlparams[0] = new SqlParameter("@SocialServiceClubs_title", SocialServiceClubs_title);
            sqlparams[1] = new SqlParameter("@SocialServiceClubs_details", SocialServiceClubs_details);
            sqlparams[2] = new SqlParameter("@SocialServiceClubs_address", SocialServiceClubs_address);
            sqlparams[3] = new SqlParameter("@SocialServiceClubs_map", SocialServiceClubs_address);
            sqlparams[4] = new SqlParameter("@SocialServiceClubs_website", SocialServiceClubs_website);
            db.Database.ExecuteSqlCommand(query, sqlparams);
            return RedirectToAction("List");
        }

        public ActionResult Update(int id)
        {
            //fetch existed details of the remedy
            string query = "select * from SocialServiceClubs where SocialServiceClubs_id = @id";
            var parameter = new SqlParameter("@id", id);
            SocialServiceClubs selectedSocialServiceClub = db.SocialServiceClubs.SqlQuery(query, parameter).FirstOrDefault();

            return View(selectedSocialServiceClub);
        }

        //update existed details
        [HttpPost]
        public ActionResult Update(int id, string SocialServiceClubs_title, string SocialServiceClubs_details, string SocialServiceClubs_address, string SocialServiceClubs_map, string SocialServiceClubs_website)
        {
            //Debug.WriteLine("selected club is" + id);
            string query = "Update SocialServiceClubs set SocialServiceClubs_title=@SocialServiceClubs_title, SocialServiceClubs_details=@SocialServiceClubs_details, SocialServiceClubs_address=@SocialServiceClubs_address,SocialServiceClubs_map=@SocialServiceClubs_map,SocialServiceClubs_website=@SocialServiceClubs_website  where SocialServiceClubs_id=@id";
            SqlParameter[] sqlparams = new SqlParameter[6];
            sqlparams[0] = new SqlParameter("@SocialServiceClubs_title", SocialServiceClubs_title);
            sqlparams[1] = new SqlParameter("@SocialServiceClubs_details", SocialServiceClubs_details);
            sqlparams[2] = new SqlParameter("@SocialServiceClubs_address", SocialServiceClubs_address);
            sqlparams[3] = new SqlParameter("@SocialServiceClubs_map", SocialServiceClubs_map);
            sqlparams[4] = new SqlParameter("@SocialServiceClubs_website", SocialServiceClubs_website);
            sqlparams[5] = new SqlParameter("@id", id);
            db.Database.ExecuteSqlCommand(query, sqlparams);
            return RedirectToAction("List");

        }
        public ActionResult View(int id)
        {
            string query = "Select * from SocialServiceClubs where SocialServiceClubs_id=@id";
            var parameter = new SqlParameter("@id", id);
            SocialServiceClubs selectedSocialServiceClub = db.SocialServiceClubs.SqlQuery(query, parameter).FirstOrDefault();

            return View(selectedSocialServiceClub);

        }
        public ActionResult User_view(int id)
        {
            string query = "Select * from SocialServiceClubs where SocialServiceClubs_id=@id";
            var parameter = new SqlParameter("@id", id);
            SocialServiceClubs selectedSocialServiceClub = db.SocialServiceClubs.SqlQuery(query, parameter).FirstOrDefault();

            return View(selectedSocialServiceClub);

        }

        public ActionResult Delete(int id)
        {
            string query = "Select * from SocialServiceClubs where SocialServiceClubs_id=@id";
            var parameter = new SqlParameter("@id", id);
            SocialServiceClubs selectedSocialServiceClub = db.SocialServiceClubs.SqlQuery(query, parameter).FirstOrDefault();

            return View(selectedSocialServiceClub);
        }
        [HttpPost]
        public ActionResult Delete(int? id)
        {
            string query = "Delete from SocialServiceClubs where SocialServiceClubs_id=@id";
            var parameter = new SqlParameter("@id", id);
            db.Database.ExecuteSqlCommand(query, parameter);
            return RedirectToAction("List");
        }
    }
}