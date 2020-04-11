using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HumberAreaHospitalProject.Models
{
    public class Application
    {
        [Key]
        public int ApplicationID { get; set; }
        public int JobID { get; set; }
        [ForeignKey("JobID")]
        public virtual Job Job { get; set; }
        public string ApplicantFirstName { get; set; }
        public string ApplicantLastName { get; set; }
        public string ApplicantEmail { get; set; }
        public string ApplicantPhone { get; set; }
        public string ApplicantAddress { get; set; }
        public string ApplicantCity { get; set; }
        public string ApplicantProvince { get; set; }
        public string ApplicantZipCode { get; set; }
        public string ApplicantResume { get; set; }
        public string ApplicationDate { get; set; }
    }
}