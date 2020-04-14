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

        /*
         * Application model consists of applicant details who applies for the job
         * 
         * ApplicationId            - Identity oof the application
         * JObID                    -  Job identity which is foreign key refereing to jobs table
         * ApplicantFirstName       - First name of the applicant
         * ApplicantLastName        - Last name of the applicant
         * ApplicantEmail           - Email id of the applicant
         * Applicantphone           - Phone number of the applicant
         * ApplicantAddress         - Address of the applicant
         * ApplicantCity            - City of the applicant
         * ApplicantProvince        - Province of the applicant
         * ApplicantZipCode         - Postal code or Zipcode of the applicant
         * ApplicantResume          - Resume of the applicant
         * ApplicationDate          - Date on which the applicant has applied for job
         */
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
        public DateTime ApplicationDate { get; set; }
    }
}