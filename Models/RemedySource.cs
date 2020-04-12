using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HumberAreaHospitalProject.Models
{
    public class RemedySource
    {
        [Key]
        public int RemedySource_id { get; set; }

        public string RemedySource_name { get; set; }

        public string RemedySource_url { get; set; }

        public ICollection<HomeRemedies> Remedies { get; set; }

    }
}