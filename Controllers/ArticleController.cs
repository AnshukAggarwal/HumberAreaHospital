using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HumberAreaHospitalProject.Data;
using HumberAreaHospitalProject.Models;
using System.Diagnostics;
using System.Data.SqlClient;
using HumberAreaHospitalProject.Models.ViewModels;
using System.Data.Entity;
using System.Net;

namespace HumberAreaHospitalProject.Controllers
{
    public class ArticleController : Controller
    {
        private HospitalContext db = new HospitalContext();

        //Accessible only for user that is login
        //[Authorize]
        public ActionResult NewsPage()
        {
            string query = "SELECT * from Articles";
            List<Article> articles = db.Articles.SqlQuery(query).ToList();
            return View(articles);

        }
        public ActionResult List(string searchkey, int pagenum = 1)
        {
            //Christine Bittle In-Class Example
            //Query to get All the Articles
            string query = "Select * from Articles INNER JOIN Authors ON Articles.AuthorID = Authors.AuthorID";
            //Debug.WriteLine(query);
            //Start creating a Class for sql parameter
            List<SqlParameter> parameters = new List<SqlParameter>();
            //Searchkey is not empty
            if (searchkey != "")
            {
                //Add to the query for Search
                query = query + " where ArticleTitle like @searchkey";
                parameters.Add(new SqlParameter("@searchkey", "%" + searchkey + "%"));
            }
            //Execute the sql query for articles
            List<Article> articles = db.Articles.SqlQuery(query, parameters.ToArray()).ToList();
            //Add Variable for how many per page 
            int perpage = 3;
            //Get the total count values of articles
            //Debug.WriteLine("Article Count is " + articles.Count());
            int volunteercount = articles.Count();
            //Max page will be the Count Divided by per page. Rounding this to the upper limit so that
            //rows which are not divisible to the perpage will still be shown
            int maxpage = (int)Math.Ceiling((decimal)volunteercount / perpage);
            //Maxpage is greater than 0, maxpage will be zero
            if (maxpage < 0) maxpage = 0;
            //Page number less than 1 will always be 1
            if (pagenum < 1) pagenum = 1;
            //If page number is greater than maxpage, pagenum will be equal to maxpage
            if (pagenum > maxpage) pagenum = maxpage;
            //Start value should be 0, but should be ascending depending on the perpage value
            //Start with index 0, and then next will be 3, since perpage is 3. So the next page will start with index 3.
            //The first page will show 0,1,2 index row and the next page will be 3,4,5 indexes and so on. 
            int start = (int)(perpage * pagenum) - perpage;
            //Debug.WriteLine("start number is: "+start);
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
                string pagedquery = query + " order by ArticleID offset @start rows fetch first @perpage rows only ";
                //Debug.WriteLine(pagedquery);
                //Debug.WriteLine("offset " + start);
                //Debug.WriteLine("fetch first " + perpage);
                //Re-write the execution of sql query with page listing
                articles = db.Articles.SqlQuery(pagedquery, newparams.ToArray()).ToList();
            }

            return View(articles);
        }

        //Accessible only for user that is login
        //Use to apply also on the other view
        //[ActionName("NewsPage")]
        public ActionResult View(int id)
        {
            Article selectedarticle = db.Articles.SqlQuery("Select * from Articles INNER JOIN Authors ON Articles.AuthorID = Authors.AuthorID where Articles.ArticleID = @id", new SqlParameter("@id", id)).FirstOrDefault();
            return View(selectedarticle);
        }

        //Accessible only for user that is login
        //[Authorize]
        //This function is needed for the User to Show as a different URL
        public ActionResult Create()
        {
            string query = "Select * from Authors";
            List<Author> authors = db.Authors.SqlQuery(query).ToList();
            return View(authors);
        }

        [HttpPost]
        public ActionResult Create(string articleTitle, string articleBody, DateTime published, string featured, int authorId)
        {
            try
            {
                //Try if the SQL query will work
                string query = "insert into Articles (ArticleTitle, ArticleBody, Published,Featured, AuthorID) values(@articletitle, @articlebody, @published,@featured,@authorId)";
                //Debug.WriteLine(query);
                SqlParameter[] parameters = new SqlParameter[5];
                parameters[0] = new SqlParameter("@articletitle", articleTitle);
                parameters[1] = new SqlParameter("@articlebody", articleBody);
                parameters[2] = new SqlParameter("@published", published);
                parameters[3] = new SqlParameter("@featured", featured);
                parameters[4] = new SqlParameter("@authorId", authorId);

                db.Database.ExecuteSqlCommand(query, parameters);
                //Debug.WriteLine("The new record being added in Articles");
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

            string query = "Select * from Articles where ArticleID=@id";
            //Debug.WriteLine(query);
            //Debug.WriteLine("Article ID of: " + id);
            SqlParameter parameter = new SqlParameter("@id", id);
            Article selectedarticle = db.Articles.SqlQuery(query, parameter).First();

            //Query to get all the Authors
            string query2 = "Select * from Authors";
            //Sql Execute for 2nd Query
            List<Author> authors = db.Authors.SqlQuery(query2).ToList();
            var UpdateArticle = new UpdateArticle();
            //Add list of authors and an article in a model view 
            UpdateArticle.authors = authors;
            UpdateArticle.article = selectedarticle;

            if (selectedarticle == null)
            {
                //if article is not found in the database
                return HttpNotFound();
            }

            return View(UpdateArticle);
        }


        [HttpPost]
        public ActionResult Update(int id, string articleTitle, string articleBody, DateTime published, string featured, int authorId)
        {
            try
            {
                //ArticleTitle, ArticleBody, Date,Featured, AuthorID
                // TODO: Add update logic here
                string query = "UPDATE Articles set ArticleTitle = @articletitle, ArticleBody = @articlebody, Published = @published, Featured = @featured, AuthorID = @authorId where ArticleID = @id ";
                SqlParameter[] parameters = new SqlParameter[6];
                parameters[0] = new SqlParameter("@articletitle", articleTitle);
                parameters[1] = new SqlParameter("@articlebody", articleBody);
                parameters[2] = new SqlParameter("@published", published);
                parameters[3] = new SqlParameter("@featured", featured);
                parameters[4] = new SqlParameter("@authorId", authorId);
                parameters[5] = new SqlParameter("@id", id);

                //Debug.WriteLine(query);
                //Debu.WriteLine("The Article ID is :" + id);
                db.Database.ExecuteSqlCommand(query, parameters);

                return RedirectToAction("List");
            }
            catch
            {
                //Get Bad Request if Sql got error
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
        }

        //Use to accessible only for login user
        //[Authorize]
        public ActionResult Delete(int? id)
        {

            if (id == null)
            {
                //If no id value has been presented, will return a BadRequest
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Article selectedarticle = db.Articles.SqlQuery("Select * from Articles INNER JOIN Authors ON Articles.AuthorID = Authors.AuthorID where Articles.ArticleID = @id", new SqlParameter("@id", id)).FirstOrDefault();

            if (selectedarticle == null)
            {
                //If article has no value in the database
                return HttpNotFound();
            }
            return View(selectedarticle);
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            try
            {
                ////query to delete the Book in the books table
                string deletearticle = "DELETE from Articles where ArticleID= @id";
                //query to delete the relationship on the bridging table

                //Debug.WriteLine(delbooks);
                //Debug.WriteLine(delrelationship);
                SqlParameter parameters = new SqlParameter("@id", id);
                //Run the sql command
                db.Database.ExecuteSqlCommand(deletearticle, parameters);

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