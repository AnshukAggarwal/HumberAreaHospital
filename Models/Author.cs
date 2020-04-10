using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HumberAreaHospitalProject.Models
{
    public class Author
    {
        [Key]
        public int AuthorID { get; set; }
        public string AuthorFname { get; set; }
        public string AuthorLname { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }

        //Representing the "Many" in (One Author to many Articles)
        public ICollection<Article> Articles { get; set; }
    }
}