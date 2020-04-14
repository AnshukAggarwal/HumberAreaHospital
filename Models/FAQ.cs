using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HumberAreaHospitalProject.Models
{
    public class FAQ

        /*
         * FAQ Model Describes
         * FAQID            - Identity  of FAQ
         * FAQCategoryID    - Identity of FAQCategory in the FAQCAtegories Table also, A foreign Key
         * FAQQuestion      - The Actual question 
         * FAQAnswer        - The Actual answer for the question
         * 
        */
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