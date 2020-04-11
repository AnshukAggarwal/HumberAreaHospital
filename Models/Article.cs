using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HumberAreaHospitalProject.Models
{
    public class Article
    {
        [Key]
        public int ArticleID { get; set; }
        public string ArticleTitle { get; set; }
        public string ArticleBody { get; set; }
        public DateTime Date { get; set; }
        //Featured is YES or NO
        public string Featured { get; set; }

        //Reference of Author Id as a Foreign key
        //Representing the "One" in (Many Articles to One Author)
        public int AuthorID { get; set; }
        [ForeignKey("AuthorID")]
        public virtual Author Authors { get; set; }
    }
}