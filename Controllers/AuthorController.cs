using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HumberAreaHospitalProject.Data;
using HumberAreaHospitalProject.Models;
using System.Diagnostics;
using System.Data.SqlClient;
using System.Data.Entity;
using System.Net;

namespace HumberAreaHospitalProject.Controllers
{
    public class AuthorController : Controller
    {
        private HospitalContext db = new HospitalContext();

        //Accessible only for user that is login
        //[Authorize]
        public ActionResult List(string searchkey, int pagenum = 1)
        {
            //Christine Bittle In-Class Example
            //Query to get All the Authors    
            string query = "Select * from Authors";
            //Debug.WriteLine(query);
            //Debug.WriteLine(query);
            //Run the Sql command
            List<SqlParameter> parameters = new List<SqlParameter>();
            //Searchkey is not empty
            if (searchkey != "")
            {
                //Add to the query for Search
                query = query + " where AuthorFname like @searchkey";
                parameters.Add(new SqlParameter("@searchkey", "%" + searchkey + "%"));
            }
            //Execute the sql query
            List<Author> authors = db.Authors.SqlQuery(query, parameters.ToArray()).ToList();
            //Add Variable for how many per page
            int perpage = 3;
            //Debug.WriteLine("Authors Count is " + authors.Count());
            //Get the total count values 
            int volunteercount = authors.Count();
            //Max page will be the Count Divided by per page. Rounding this to the upper limit so that
            //rows which are not divisible to the perpage will still be shown
            int maxpage = (int)Math.Ceiling((decimal)volunteercount / perpage);
            //Maxpage is greater than 0, maxpage will be zero
            if (maxpage < 0) maxpage = 0;
            //Page number less than 1 will always be 1
            if (pagenum < 1) pagenum = 1;
            //If page number is greater than maxpage, pagenum will be equal to maxpag
            if (pagenum > maxpage) pagenum = maxpage;
            //Start value should be 0, but should be ascending depending on the perpage value
            //Start with index 0, and then next will be 3, since perpage is 3. So the next page will start with index 3.
            //The first page will show 0,1,2 index row and the next page will be 3,4,5 indexes and so on. 
            int start = (int)(perpage * pagenum) - perpage;
            Debug.WriteLine("start number is: " + start);
            ViewData["pagenum"] = pagenum;
            ViewData["pagesummary"] = "";
            ViewData["maxpage"] = maxpage;
            if (maxpage > 0)
            {
                ViewData["pagesummary"] = (pagenum) + " of " + (maxpage);
                List<SqlParameter> newparams = new List<SqlParameter>();

                if (searchkey != "")
                {
                    newparams.Add(new SqlParameter("@searchkey", "%" + searchkey + "%"));
                    ViewData["searchkey"] = searchkey;
                }
                newparams.Add(new SqlParameter("@start", start));
                newparams.Add(new SqlParameter("@perpage", perpage));
                string pagedquery = query + " order by AuthorID offset @start rows fetch first @perpage rows only ";
                //Debug.WriteLine(pagedquery);
                //Debug.WriteLine("offset " + start);
                //Debug.WriteLine("fetch first " + perpage);
                //Re-write the execution of sql query with page listing
                authors = db.Authors.SqlQuery(pagedquery, newparams.ToArray()).ToList();
            }

            return View(authors);
        }

        //Accessible only for user that is login
        //[Authorize]
        //Get View for the specific Author
        public ActionResult View(int id)
        {
            //Execute the sql with the Query on it
            Author selectedauthor = db.Authors.SqlQuery("Select * from Authors where AuthorID = @id", new SqlParameter("@id", id)).First();
            return View(selectedauthor);
        }

        //Accessible only for user that is login
        //[Authorize]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Article/Create
        [HttpPost]
        public ActionResult Create(string authorFname, string authorLname, string email, string phone)
        {
            try
            {
                //Try if the SQL query will work
                string query = "INSERT into Authors (AuthorFname, AuthorLname, Email, Phone) values(@fname, @lname, @email,@phone)";
                //Debug.WriteLine(query);
                SqlParameter[] parameters = new SqlParameter[4];
                parameters[0] = new SqlParameter("@fname", authorFname);
                parameters[1] = new SqlParameter("@lname", authorLname);
                parameters[2] = new SqlParameter("@email", email);
                parameters[3] = new SqlParameter("@phone", phone);

                db.Database.ExecuteSqlCommand(query, parameters);
                // Debug.WriteLine("The new record being added in Author");
                return RedirectToAction("List");
            }
            catch
            {
                //Get Bad Request if Sql got error
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
        }

        //Accessible only for user that is login
        //[Authorize]
        public ActionResult Update(int? id)
        {
            if (id == null)
            {
                //If no id value has been presented, will return a BadRequest
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Debug.WriteLine("Author id: " + id);
            string query = "Select * from Authors where AuthorID=@id";
            SqlParameter parameter = new SqlParameter("@id", id);
            Author selectedauthor = db.Authors.SqlQuery(query, parameter).First();
            if (selectedauthor == null)
            {
                //If value is null on selectedauthor, no value on the database
                return HttpNotFound();
            }

            return View(selectedauthor);
        }

        // POST: Article/Edit/5
        [HttpPost]
        public ActionResult Update(int id, string authorFname, string authorLname, string email, string phone)
        {
            try
            {
                //Sql for Authors
                string query = "UPDATE Authors set AuthorFname = @fname, AuthorLname = @lname, Email = @email, Phone = @phone where AuthorID = @id ";
                //Debug.WriteLine(query);
                SqlParameter[] parameters = new SqlParameter[5];
                parameters[0] = new SqlParameter("@fname", authorFname);
                parameters[1] = new SqlParameter("@lname", authorLname);
                parameters[2] = new SqlParameter("@email", email);
                parameters[3] = new SqlParameter("@phone", phone);
                parameters[4] = new SqlParameter("@id", id);

                //Debug.WriteLine("Author Id = " +id);
                db.Database.ExecuteSqlCommand(query, parameters);

                return RedirectToAction("List");
            }
            catch
            {
                //Get Bad Request if Sql got error
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
        }

        //Use to apply also on the other view
        //[Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                //If no id value has been presented, will return a BadRequest
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Author selectedauthor = db.Authors.SqlQuery("Select * from Authors where AuthorID = @id", new SqlParameter("@id", id)).First();

            if (selectedauthor == null)
            {
                //If value is null on selectedauthor, no value on the database
                return HttpNotFound();
            }

            return View(selectedauthor);
        }


        [HttpPost]
        public ActionResult Delete(int id)
        {
            try
            {
                //query for deleting Author
                string deleteauthor = "DELETE from Authors where AuthorID= @id";

                //Debug.WriteLine(query);
                SqlParameter parameters = new SqlParameter("@id", id);
                //Run the sql command
                db.Database.ExecuteSqlCommand(deleteauthor, parameters);

                return RedirectToAction("List");
            }
            catch
            {
                //Get Bad Request if Sql got error
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
        }
    }
}