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
    public class VolunteerController : Controller
    {
        private HospitalContext db = new HospitalContext();

        //Accessible only for user that is login
        [Authorize]
        public ActionResult List()
        {
            //Query to get All the VOlunteers
            string query = "Select * from Volunteers";
            //Debug.WriteLine(query);
            //Run the Sql command
            List<Volunteer> volunteers = db.Volunteers.SqlQuery(query).ToList();
            return View(volunteers);
        }

        //Accessible only for user that is login
        [Authorize]
        //Get View for the specific volunteer
        public ActionResult View(int id)
        {
            //Execute the sql with the Query on it
            Volunteer selectedvolunteer = db.Volunteers.SqlQuery("Select * from Volunteers where VolunteerID = @id", new SqlParameter("@id", id)).First();
            return View(selectedvolunteer);
        }

        //Use to apply also on the other view
        [ActionName("Apply_today")]
        //This function is needed for the User to Show as a different URL
        public ActionResult Create()
        {
            return View();
        }

        //Use to apply also on the other view 
        [HttpPost]
        public ActionResult Create(string volunteerFname, string volunteerLname, string address, string email, string homePhone, string workPhone, string skills, ICollection<string> day, string time, string preference, string notes)
        {
            
            String seperator = ",";
            string listdays = "";
            //All value on the days will be concatenated here
            //Joining "days" and the "separator" and storing them in a variable "listdays"
            listdays += String.Join(seperator, day);

            try
            {
                //Try if the SQL query will work
                string query = "INSERT into Volunteers (VolunteerFname, VolunteerLname, Address, Email, HomePhone, WorkPhone, Skills, Day, Time, Preference, Notes)" +
                                " values (@fname, @lname, @address, @email, @homephone, @workphone, @skills, @day, @time, @preference, @notes)";
                //Debug.WriteLine(query);
                SqlParameter[] parameters = new SqlParameter[11];
                parameters[0] = new SqlParameter("@fname", volunteerFname);
                parameters[1] = new SqlParameter("@lname", volunteerLname);
                parameters[2] = new SqlParameter("@address", address);
                parameters[3] = new SqlParameter("@email", email);
                parameters[4] = new SqlParameter("@homephone", homePhone);
                parameters[5] = new SqlParameter("@workphone", workPhone);
                parameters[6] = new SqlParameter("@skills", skills);
                parameters[7] = new SqlParameter("@day", listdays);
                parameters[8] = new SqlParameter("@time", time);
                parameters[9] = new SqlParameter("@preference", preference);
                parameters[10] = new SqlParameter("@notes", notes);

                db.Database.ExecuteSqlCommand(query, parameters);
                // Debug.WriteLine("The new record being added in Volunteers");
                
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
            
            string query = "Select * from Volunteers where VolunteerID=@id";
            //Debug.WriteLine(query);
            SqlParameter parameter = new SqlParameter("@id", id);
            Volunteer selectedvolunteer = db.Volunteers.SqlQuery(query, parameter).First();
            if (selectedvolunteer == null)
            {
                //If value is null on selectedvolunteer, no value on the database
                return HttpNotFound();
            }

            return View(selectedvolunteer);
        }

        
        [HttpPost]
        public ActionResult Update(int id, string volunteerFname, string volunteerLname, string address, string email, string homePhone, string workPhone, string skills, ICollection<string> day, string time, string preference, string notes)
        {
            try
            {
                String seperator = ",";
                string listdays = "";
                //All value on the days will be concatenated here
                //Joining "days" and the "separator" and storing them in a variable "listdays"
                listdays += String.Join(seperator, day);

                //ArticleTitle, ArticleBody, Date,Featured, AuthorID
                string query = "UPDATE Volunteers set VolunteerFname = @fname, VolunteerLname = @lname, Email = @email, HomePhone = @homephone, " +
                                    "WorkPhone = @workphone, Skills = @skills, Day = @day, Time = @time, Preference = @preference, Notes = @notes  where VolunteerID = @id ";
                //Debug.WriteLine(query);
                SqlParameter[] parameters = new SqlParameter[12];
                parameters[0] = new SqlParameter("@fname", volunteerFname);
                parameters[1] = new SqlParameter("@lname", volunteerLname);
                parameters[2] = new SqlParameter("@address", address);
                parameters[3] = new SqlParameter("@email", email);
                parameters[4] = new SqlParameter("@homephone", homePhone);
                parameters[5] = new SqlParameter("@workphone", workPhone);
                parameters[6] = new SqlParameter("@skills", skills);
                parameters[7] = new SqlParameter("@day", listdays);
                parameters[8] = new SqlParameter("@time", time);
                parameters[9] = new SqlParameter("@preference", preference);
                parameters[10] = new SqlParameter("@notes", notes);
                parameters[11] = new SqlParameter("@id", id);

                //Debug.WriteLine(query);
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
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                //If no id value has been presented, will return a BadRequest
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Volunteer selectedvolunteer = db.Volunteers.SqlQuery("Select * from Volunteers where VolunteerID = @id", new SqlParameter("@id", id)).First();

            if (selectedvolunteer == null)
            {
                //If value is null on selectedvolunteer, no value on the database
                return HttpNotFound();
            }
            return View(selectedvolunteer);
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            try
            {
                //query for deleting Volunteer
                string query = "DELETE from Volunteers where VolunteerID= @id";

                //Debug.WriteLine(query);
                SqlParameter parameters = new SqlParameter("@id", id);
                //Run the sql command
                db.Database.ExecuteSqlCommand(query, parameters);

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