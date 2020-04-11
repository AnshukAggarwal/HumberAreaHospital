using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HumberAreaHospitalProject.Data;
using HumberAreaHospitalProject.Models;
using System.Diagnostics;
using System.Data.SqlClient;
using HumberAreaHospitalProject.Models.ViewModel;
using System.Data.Entity;
using System.Net;
namespace HumberAreaHospitalProject.Controllers
{
    public class ArticleController : Controller
    {
        private HospitalContext db = new HospitalContext();

        //Accessible only for user that is login
        [Authorize]
        public ActionResult List()
        {
            //Query to get All the Articles
            string query = "Select * from Articles INNER JOIN Authors ON Articles.AuthorID = Authors.AuthorID";
            //Debug.WriteLine(query);
            //Run the Sql command
            List<Article> articles = db.Articles.SqlQuery(query).ToList();
            return View(articles);
        }

        //Accessible only for user that is login
        //Use to apply also on the other view
        [ActionName("NewsPage")]
        public ActionResult View(int id)
        {
            Article selectedarticle = db.Articles.SqlQuery("Select * from Articles INNER JOIN Authors ON Articles.AuthorID = Authors.AuthorID where Articles.ArticleID = @id", new SqlParameter("@id", id)).FirstOrDefault();
            return View(selectedarticle);
        }

        //Accessible only for user that is login
        [Authorize]
        //This function is needed for the User to Show as a different URL
        public ActionResult Create()
        {
            string query = "Select * from Authors";
            List<Author> authors = db.Authors.SqlQuery(query).ToList();
            return View(authors);
        }

        [HttpPost]
        public ActionResult Create(string articleTitle, string articleBody, DateTime date, string featured, int authorId)
        {
            try
            {
                //Try if the SQL query will work
                string query = "insert into Articles (ArticleTitle, ArticleBody, Date,Featured, AuthorID) values(@articletitle, @articlebody, @date,@featured,@authorId)";
                //Debug.WriteLine(query);
                SqlParameter[] parameters = new SqlParameter[5];
                parameters[0] = new SqlParameter("@articletitle", articleTitle);
                parameters[1] = new SqlParameter("@articlebody", articleBody);
                parameters[2] = new SqlParameter("@date", date);
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
        [Authorize]
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
        public ActionResult Update(int id, string articleTitle, string articleBody, DateTime date, string featured, int authorId)
        {
            try
            {
                //ArticleTitle, ArticleBody, Date,Featured, AuthorID
                // TODO: Add update logic here
                string query = "UPDATE Articles set ArticleTitle = @articletitle, ArticleBody = @articlebody, Date = @date, Featured = @featured, AuthorID = @authorId where ArticleID = @id ";
                SqlParameter[] parameters = new SqlParameter[6];
                parameters[0] = new SqlParameter("@articletitle", articleTitle);
                parameters[1] = new SqlParameter("@articlebody", articleBody);
                parameters[2] = new SqlParameter("@date", date);
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
        [Authorize]
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