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

        /*
        foreigh key of remedy resources 
        one source has list of remedies
        One source - many remedies
        
            here I have taken primary key of RemedySources table as a
            foreign key for HomeRemedies table
             */

        public int RemedySource_id { get; set; }
        [ForeignKey("RemedySource_id")]
        public virtual RemedySource RemedySource { get; set; }
    }
}