using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HumberAreaHospitalProject.Models
{
    public class SocialServiceClubs
    {
        [Key]
        public int SocialServiceClubs_id { get; set; }
        public string SocialServiceClubs_title { get; set; }

        public string SocialServiceClubs_details { get; set; }

        public string SocialServiceClubs_address { get; set; }

        public string SocialServiceClubs_map { get; set; }

        public string SocialServiceClubs_website { get; set; }

    }
}