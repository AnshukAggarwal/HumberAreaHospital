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
using HumberAreaHospitalProject.Models.ViewModels;

namespace HumberAreaHospitalProject.Controllers
{
    public class JobController : Controller
    {
        private HospitalContext db = new HospitalContext();
        // GET: Job Admin perspective where admin gets to see the edit update and delete buttons
        public ActionResult List(String jobsearchkey, int pagenum=0)
        {
            string query = "select * from Jobs";//SQL  query to select everything from Jobs table
            List<SqlParameter> sqlparams = new List<SqlParameter>();
            if (jobsearchkey!= "") //Checkign if the search key is empty or null
            {
                query = query + " where JobTitle like @searchkey";//Appending sql query to existing query 
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

        //Add new job to the table
        [HttpPost]
        public ActionResult New(string JobTitle, string JobCategory, string JobType, string Description, string Requirements)
        {
            DateTime now = DateTime.Now;
            DateTime PostDate = now;
            string query = "insert into Jobs (JobTitle, JobCategory, JobType, Description, Requirements, PostDate) values (@JobTitle, @JobCategory, @JobType, @Description, @Requirements, @PostDate)";
            SqlParameter[] sqlparams = new SqlParameter[6];
            sqlparams[0] = new SqlParameter("@JobTitle", JobTitle);
            sqlparams[1] = new SqlParameter("@JobCategory", JobCategory);
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
        public ActionResult Show(int? id)
        {
            Job job = db.Jobs.SqlQuery("Select * from Jobs Where JobID=@JobID", new SqlParameter("@JobID", id)).FirstOrDefault();
            string query = "select * from Applications where JobID = @id";
            SqlParameter parameter = new SqlParameter("@id", id);
            List<Application> applications = db.Applications.SqlQuery(query, parameter).ToList();

            ShowJob ViewModel = new ShowJob();
            ViewModel.Job = job;
            ViewModel.Applications = applications;

            return View(ViewModel);
        }
        //Update
        public ActionResult Update(int id)
        {
            //need information about a particular job
            Job selectedjob = db.Jobs.SqlQuery("select * from Jobs where JobID = @id", new SqlParameter("@id", id)).FirstOrDefault();
            //string query = "select * from Jobs";
            return View(selectedjob);
        }
        //[HttpPost] Update
        [HttpPost]
        public ActionResult Update(int id, string JobTitle, string JobCategory, string JobType, string Description, string Requirements)
        {   //query to update jobs
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
        {   //Query to delete particualr job from the table based on the jobID
            string query = "delete from Jobs where JobID=@id";
            SqlParameter[] parameter = new SqlParameter[1];
            //storing the id of the job to be deleted 
            parameter[0] = new SqlParameter("@id", id);
            //Excecuting the query
            db.Database.ExecuteSqlCommand(query, parameter);
            // returning to lsit view of the jobs after deleting 
            return RedirectToAction("List");
        }
        // GET: Job and display in users persepective where all the buttons will be removed so the user is restriceted to only see aand apply for the job
        public ActionResult User_Perspective_List(String jobsearchkey, int pagenum = 0)
        {
            string query = "select * from Jobs";//SQL  query to select everything from Jobs table
            List<SqlParameter> sqlparams = new List<SqlParameter>();
            if (jobsearchkey != "") //Checkign if the search key is empty or null
            {
                query = query + " where JobTitle like @searchkey";//Appending sql query to existing query 
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
        //Function to Display individual  user perspective of  job details
        public ActionResult User_Show(int id)
        {
            string query = "select * from Jobs where JobID = @JobID"; //sql query to slect all fromJobs table based on JobID
            var parameter = new SqlParameter("@JobID", id);
            Job jobs = db.Jobs.SqlQuery(query, parameter).FirstOrDefault();
            return View(jobs);
        }




        

    }
 
}