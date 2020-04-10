using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HumberAreaHospitalProject.Models
{
    public class Job
    {
        [Key]
        public int JobID { get; set; }
        public string JobTitle { get; set; }
        public string JobType { get; set; }
        public string Description { get; set; }
        public string Requirements { get; set; }
        public DateTime PostDate { get; set; }
    }
}