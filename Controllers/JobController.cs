using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;
using HumberAreaHospitalProject.Models;
using System.Data.Entity;
using HumberAreaHospitalProject.Data;
using System.Diagnostics;

namespace HumberAreaHospitalProject.Controllers
{
    public class JobController : Controller
    {
        private HospitalContext db = new HospitalContext();
        // GET: Job
        public ActionResult List(String jobsearchkey, int pagenum=0)
        {

            string query = "select * from Jobs";
            List<SqlParameter> sqlparams = new List<SqlParameter>();
            if (jobsearchkey!= "") //Checkign if the search key is empty or null
            {
                query = query + " where JobTitle like @searchkey";
                sqlparams.Add(new SqlParameter("@searchkey", "%" + jobsearchkey + "%"));                
            }
             List<Job> jobs = db.Jobs.SqlQuery(query, sqlparams.ToArray()).ToList();
            //Pagination for jobs 
            int perpage = 5;
            int petcount = jobs.Count();
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

                if (jobsearchkey != "")
                {
                    newparams.Add(new SqlParameter("@searchkey", "%" + jobsearchkey + "%"));
                    ViewData["jobsearchkey"] = jobsearchkey;
                }
                newparams.Add(new SqlParameter("@start", start));
                newparams.Add(new SqlParameter("@perpage", perpage));
                string pagedquery = query + " order by JobID offset @start rows fetch first @perpage rows only ";
                jobs = db.Jobs.SqlQuery(pagedquery, newparams.ToArray()).ToList();
            }
            //End of Pagination


            return View(jobs);
        }

        //Add new job
        [HttpPost]
        public ActionResult New(string JobTitle, string JobCategory, string JobType, string Description, string Requirements)
        {
            string PostDate = DateTime.Now.ToString("dd/MM/yyyy");
            string query = "insert into Jobs (JobTitle, JobCategory, JobType, Description, Requirements, PostDate) values (@JobTitle, @JobCategory, @JobType, @Description, @Requirements, @PostDate)";
            SqlParameter[] sqlparams = new SqlParameter[6]; 
            sqlparams[0] = new SqlParameter("@JobTitle", JobTitle);
            sqlparams[1] = new SqlParameter("@JobCategory",JobCategory);
            sqlparams[2] = new SqlParameter("@JobType", JobType);
            sqlparams[3] = new SqlParameter("@Description", Description);
            sqlparams[4] = new SqlParameter("@Requirements", Requirements);
            sqlparams[5] = new SqlParameter("@PostDate", PostDate);
            db.Database.ExecuteSqlCommand(query, sqlparams);
            return RedirectToAction("List");
        }
        public ActionResult New()
        {
            return View();
            
        }
        //Display individual job details 
        public ActionResult Show(int id)
        {
            string query = "select * from Jobs where JobID = @JobID"; //sql query to slect all fromJobs table based on JobID
            var parameter = new SqlParameter("@JobID", id);
            Job jobs = db.Jobs.SqlQuery(query, parameter).FirstOrDefault();
            return View(jobs);
        }
        //Update
        public ActionResult Update(int id)
        {
            //need information about a particular Bike
            Job selectedjob = db.Jobs.SqlQuery("select * from Jobs where JobID = @id", new SqlParameter("@id", id)).FirstOrDefault();
            string query = "select * from Bikes";
            return View(selectedjob);
        }
        //[HttpPost] Update
        [HttpPost]
        public ActionResult Update(int id, string JobTitle, string JobCategory, string JobType, string Description, string Requirements)
        {   //query to update jobs
            string PostDate = DateTime.Now.ToString("dd/MM/yyyy");
            string query = "update Jobs set JobTitle =@JobTitle, JobCategory=@JobCategory, JobType=@JobType, Description=@Description, Requirements=@Requirements where JobID=@id";
            //key pair values to hold new values 
            SqlParameter[] sqlparams = new SqlParameter[6];
            sqlparams[0] = new SqlParameter("@JobTitle", JobTitle);
            sqlparams[1] = new SqlParameter("@JobCategory", JobCategory);
            sqlparams[2] = new SqlParameter("@JobType", JobType);
            sqlparams[3] = new SqlParameter("@Description", Description);
            sqlparams[4] = new SqlParameter("@Requirements", Requirements);
            sqlparams[5] = new SqlParameter("@id", id);
            //Exceuting the sql query with new values
            db.Database.ExecuteSqlCommand(query, sqlparams);
            //Return to list view of Jobd
            return RedirectToAction("List");
        }
        //Deletion of job 
        public ActionResult Delete(int id)
        {   //Query to delete particualr Bikes from the table based on the BikeID
            string query = "delete from Jobs where JobID=@id";
            SqlParameter[] parameter = new SqlParameter[1];
            //storing the id of the Bike to be deleted 
            parameter[0] = new SqlParameter("@id", id);
            //Excecuting the query
            db.Database.ExecuteSqlCommand(query, parameter);
            // returning to lsit view of the Bikes after deleting 
            return RedirectToAction("List");
        }

    }
 
}