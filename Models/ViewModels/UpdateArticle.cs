using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HumberAreaHospitalProject.Models.ViewModels
{
    public class UpdateArticle
    {
        //Updatin the Article should also see the list of authors available 
        public Article article { get; set; }
        public List<Author> authors { get; set; }
    }
}