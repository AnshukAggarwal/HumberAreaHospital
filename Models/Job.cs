using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HumberAreaHospitalProject.Models
{
    public class Job
    {
        /* Description about Job 
             * 
             * JobID           -    Job Identity
             * JobTitle        -    Job Title 
             * JobCategory     -    Category of the job 
             * JobType         -    job type such as partimr or full time   
             * Description     -    Job Description 
             * Requirements    -    Job Requirements
             * PostDate        -    Job post date
         
        */
        [Key]
        public int JobID { get; set; }
        public string JobTitle { get; set; }
        public string JobCategory { get; set; }
        public string JobType { get; set; }
        public string Description { get; set; }
        public string Requirements { get; set; }
        public DateTime PostDate { get; set; }
    }
}