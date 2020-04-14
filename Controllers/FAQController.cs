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
    public class FAQController : Controller
    {
        private HospitalContext db = new HospitalContext();
        //List FAQs
        public ActionResult List( int pagenum = 0)
        {
            string query = "select * from FAQs";//SQL  query to select everything from FAQs table
            List<SqlParameter> sqlparams = new List<SqlParameter>();
            List<FAQ> FAQs = db.FAQs.SqlQuery(query, sqlparams.ToArray()).ToList();
            //Pagination for FAQs
            int perpage = 3;
            int petcount = FAQs.Count();
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
                newparams.Add(new SqlParameter("@start", start));
                newparams.Add(new SqlParameter("@perpage", perpage));
                string pagedquery = query + " order by JobID offset @start rows fetch first @perpage rows only ";
                FAQs = db.FAQs.SqlQuery(pagedquery, newparams.ToArray()).ToList();
            }
            //End of Pagination
            return View(FAQs);
        }
        [HttpPost]
        public ActionResult New(string FAQQuestion, string FAQAnswer, int FAQCategoryID)
        {
            string query = "insert into FAQs (FAQQuestion, FAQAnswer,FAQCategoryID ) values (@FAQQuestion,@FAQAnswer,@FAQCategoryID)";//Sql quey to insert FAQ into database
            SqlParameter[] sqlparams = new SqlParameter[3]; 
            sqlparams[0] = new SqlParameter("@FAQQuestion", FAQQuestion);
            sqlparams[1] = new SqlParameter("@FAQAnswer", FAQAnswer);
            sqlparams[2] = new SqlParameter("@FAQCategoryID", FAQCategoryID);
            db.Database.ExecuteSqlCommand(query, sqlparams);
            //returning list of FAQs After adding 
            return RedirectToAction("List");
        }
        // GET: FAQ
        public ActionResult New()
        {
            List<FAQCategory> categories = db.FAQCategories.SqlQuery("select * from FAQCategories").ToList();
            return View(categories);
        }

        //update FAQ
        public ActionResult Update(int id)
        {
            //need information about a particular FAQ
            FAQ selectedFAQ = db.FAQs.SqlQuery("select * from FAQs where FAQID = @id", new SqlParameter("@id", id)).FirstOrDefault();
            List<FAQCategory> categories = db.FAQCategories.SqlQuery("select * from FAQCategories").ToList();
            UpdateFAQController UpdateFAQViewModel = new UpdateFAQController();
            UpdateFAQViewModel.FAQ = selectedFAQ;
            UpdateFAQViewModel.FAQCategory = categories;
            return View(UpdateFAQViewModel);
        }

        //[HttpPost] Update
        [HttpPost]
        public ActionResult Update(int id, string FAQQuestion, string FAQAnswer, int FAQCategoryID )
        {   //query to update FAQs
            string PostDate = DateTime.Now.ToString("dd/MM/yyyy");
            string query = "update FAQs set FAQQuestion =@FAQQuestion, FAQAnswer=@FAQAnswer, FAQCategoryID=@FAQCategoryID where FAQID=@id";//Sql query to update FAQ
            //key pair values to hold new values 
            SqlParameter[] sqlparams = new SqlParameter[6];
            sqlparams[0] = new SqlParameter("@FAQQuestion", FAQQuestion);
            sqlparams[1] = new SqlParameter("@FAQAnswer", FAQAnswer);
            sqlparams[2] = new SqlParameter("@FAQCategoryID", FAQCategoryID);
            sqlparams[5] = new SqlParameter("@id", id);
            //Exceuting the sql query with new values
            db.Database.ExecuteSqlCommand(query, sqlparams);
            //Return to list view of FAQs
            return RedirectToAction("List");
        }

        //Display Detais about individual FAQ
        public ActionResult Show(int? id)
        {
            //need information about a particular FAQ
            FAQ selectedFAQ = db.FAQs.SqlQuery("select * from FAQs where FAQID = @id", new SqlParameter("@id", id)).FirstOrDefault();
            List<FAQCategory> categories = db.FAQCategories.SqlQuery("select * from FAQCategories").ToList();
            UpdateFAQController UpdateFAQViewModel = new UpdateFAQController();
            UpdateFAQViewModel.FAQ = selectedFAQ;
            UpdateFAQViewModel.FAQCategory = categories;
            return View(UpdateFAQViewModel);

        }
        //delete
        public ActionResult Delete(int id)
        {   //Query to delete particualr FAQ from the table based on the FAQ id
            string query = "delete from FAQs where FAQID=@id";
            SqlParameter[] parameter = new SqlParameter[1];
            //storing the id of the FAQ to be deleted 
            parameter[0] = new SqlParameter("@id", id);
            //Excecuting the query
            db.Database.ExecuteSqlCommand(query, parameter);
            // returning to lsit view of the FAQs after deleting 
            return RedirectToAction("List");
        }
    }
}