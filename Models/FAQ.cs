using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HumberAreaHospitalProject.Models
{
    public class FAQ
    {
        [Key]
        public int FAQID { get; set; }

        [ForeignKey("FAQCategory")]
        public int FAQCategoryID { get; set; }
        public FAQCategory FAQCategory { get; set; }
        public string FAQQuestion{ get; set; }
        public string FAQAnswer { get; set; }
       
    }
}