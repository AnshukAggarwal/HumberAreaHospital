using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HumberAreaHospitalProject.Data;
using HumberAreaHospitalProject.Models;
using HumberAreaHospitalProject.Models.ViewModels;
using System.Diagnostics;
using System.IO;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace HumberAreaHospitalProject.Controllers
{
    public class StaffController:Controller
    {
        private HospitalContext db = new HospitalContext();
        //Viewing Staff
        //pagination method from Christine Bittle 
        //search method from Christine Bittle
        public ActionResult List(string staffSearchKey, int pagenum = 0)
        {
            TempData["isLogged"] = ((System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated);
            List<Staff> staff = db.Staffs.Where(a => (staffSearchKey != null) ? a.staffLname.Contains(staffSearchKey) : true).ToList();
            int perpage = 5;
            int staffCount = staff.Count();
            int maxpage = (int)Math.Ceiling((decimal)staffCount / perpage) - 1;
            if (maxpage < 0) maxpage = 0;
            if (pagenum < 0) pagenum = 0;
            if (pagenum > maxpage) perpage = maxpage;
            int start = (int)(perpage * pagenum);
            ViewData["pagenum"] = pagenum;
            ViewData["pagesummary"] = "";
            if (maxpage > 0)
            {
                ViewData["pagesummary"] = (pagenum + 1) + " of " + (maxpage + 1);
                staff = db.Staffs
                    .Where(a => (staffSearchKey != null) ? a.staffLname.Contains(staffSearchKey) : true)
                    .OrderBy(a => a.staffId)
                    .Skip(start)
                    .Take(perpage)
                    .ToList();
            }
            return View(staff);
        }

        //Adding staff
        public ActionResult Create()
        {
            if ((System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
            {
                StaffSpecialty staffSpecialty = new StaffSpecialty();
                staffSpecialty.Specialities = db.Specialities.ToList();

                return View(staffSpecialty);
            }
            else
            {
                return RedirectToAction("index");
            }
        }

        [HttpPost]
        public ActionResult Create(string firstName, string lastName, string email, string ext, int specialtyID)
        {
            string query = "insert into staffs (staffFname, staffLname, staffEmail, staffExt, specialtyID)"
                + "values (@staffFname, @staffLname, @staffEmail, @staffExt, @specialtyID)";
            SqlParameter[] sqlParameters = new SqlParameter[5];
            sqlParameters[0] = new SqlParameter("@staffFname", firstName);
            sqlParameters[1] = new SqlParameter("@staffLname", lastName);
            sqlParameters[2] = new SqlParameter("@staffEmail", email);
            sqlParameters[3] = new SqlParameter("@staffExt", ext);
            sqlParameters[4] = new SqlParameter("@specialtyID", specialtyID);

            db.Database.ExecuteSqlCommand(query, sqlParameters);

            return RedirectToAction("List");
        }

        //selects staff to update
        public ActionResult Update(int id)
        {
            if ((System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
            {

                string query = "select * from staffs where staffId = @id";
                var Parameter = new SqlParameter("@id", id);
                Staff staff = db.Staffs.SqlQuery(query, Parameter).FirstOrDefault();
                StaffSpecialty staffSpecialty = new StaffSpecialty();
                staffSpecialty.staff = staff;
                staffSpecialty.Specialities = db.Specialities.ToList();

                return View(staffSpecialty);
            }
            else
            {
                return RedirectToAction("index");
            }
        }

        //loads update
        [HttpPost]
        public ActionResult Update(int id, string firstName, string lastName, string email, string ext, int specialtyID)
        {
            string query = "update staffs set staffFname = @firstName, staffLname = @lastName,  staffEmail = @email, staffExt = @ext, SpecialtyID = @specialtyID where staffId = @id";

            SqlParameter[] sqlParameters = new SqlParameter[6];
            sqlParameters[0] = new SqlParameter("@firstName", firstName);
            sqlParameters[1] = new SqlParameter("@lastName", lastName);
            sqlParameters[2] = new SqlParameter("@ext", ext);
            sqlParameters[3] = new SqlParameter("@email", email);
            sqlParameters[4] = new SqlParameter("@specialtyID", specialtyID);
            sqlParameters[5] = new SqlParameter("@id", id);

            db.Database.ExecuteSqlCommand(query, sqlParameters);
            return RedirectToAction("List");
        }
        //confirms delete
        public ActionResult Delete(int id)
        {
            if ((System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
            {
                string query = "select * from staffs where staffId = @id";
                var Parameter = new SqlParameter("@id", id);
                Staff staff = db.Staffs.SqlQuery(query, Parameter).FirstOrDefault();
                return View(staff);
            }
            else
            {
                return RedirectToAction("index");
            }
        }

        //deletes
        [HttpPost]
        public ActionResult DeleteStaff(int id)
        {
            string query = "delete from staffs where staffId = @id";
            var Parameter = new SqlParameter("@id", id);
            db.Database.ExecuteSqlCommand(query, Parameter);
            return RedirectToAction("List");
        }
    }
}