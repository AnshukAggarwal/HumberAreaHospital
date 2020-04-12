using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace HumberAreaHospitalProject.Models
{
    public class HomeRemedies
    {
        [Key]
        public int HomeRemedies_id { get; set; }
        public string HomeRemedies_title { get; set; }

        public string HomeRemedies_desc { get; set; }


    }
}