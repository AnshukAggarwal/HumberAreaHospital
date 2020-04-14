using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HumberAreaHospitalProject.Models
{
    public class FAQCategory
    {

       [Key]
       public int FAQCategoryID { get; set; }
       public string FAQCategoryName { get; set; } //Name of the category

    }
}