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
    public class FAQCategoryController : Controller
    {
        private HospitalContext db = new HospitalContext();

        // GET: FAQCategory
        public ActionResult List()
        {

            List<FAQCategory> FAQCategories = db.FAQCategories.SqlQuery("Select * from FAQCategories").ToList();
            return View(FAQCategories);

        }
        //Add Category
        public ActionResult New()
        {

            List<FAQCategory> Make = db.FAQCategories.SqlQuery("select * from FAQCategories").ToList();

            return View(Make);
        }
        // [HttpPost] Add
        [HttpPost]
        public ActionResult New(String FAQCategoryName)
        {   //query to add new Make into the make table
            string query = "insert into FAQCategories (FAQCategoryName) values (@Name)";
            SqlParameter parameter = new SqlParameter("@Name", FAQCategoryName);
            db.Database.ExecuteSqlCommand(query, parameter);
            return RedirectToAction("List");
        }
        // [HttpPost] Update
        public ActionResult Update(int id)
        {

            string query = "Select * from FAQCategories where  FAQCategoryID=@id";
            SqlParameter parameter = new SqlParameter("@id", id);
            FAQCategory selectedCategory = db.FAQCategories.SqlQuery(query, parameter).FirstOrDefault();
            return View(selectedCategory);
        }
        [HttpPost]
        public ActionResult Update(int id, String FAQCategoryName)
        {   //query to update the particualar Make based on the id
            string query = "update FAQCategories set FAQCategoryName=@Name where FAQCategoryID=@id";
            //key pair values to store Make details
            SqlParameter[] parameter = new SqlParameter[2];
            parameter[0] = new SqlParameter("@Name", FAQCategoryName);
            parameter[1] = new SqlParameter("@id", id);
            //excecuting the query to update
            db.Database.ExecuteSqlCommand(query, parameter);
            //retunring to list view of the Makes after adding
            return RedirectToAction("List");
        }

        //show Category
        public ActionResult Show(int? id)
        {

            FAQCategory category = db.FAQCategories.SqlQuery("select * from FAQCategories where FAQCategoryID=@categoryID", new SqlParameter("@categoryID", id)).FirstOrDefault();
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        //delete
        public ActionResult Delete(int id)
        {   //Query to delete particualr make from the table based on the make id
            string query = "delete from FAQCategories where FAQCategoryID=@id";
            SqlParameter[] parameter = new SqlParameter[1];
            //storing the id of the make to be deleted 
            parameter[0] = new SqlParameter("@id", id);
            //Excecuting the query
            db.Database.ExecuteSqlCommand(query, parameter);
            // returning to lsit view of the make after deleting 
            return RedirectToAction("List");
        }
    }
}