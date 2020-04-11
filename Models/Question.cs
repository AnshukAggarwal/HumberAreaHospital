using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HumberAreaHospitalProject.Models
{
    public class Question
    {
        //This class defines the idea of a question
        /*It will have question id
         * question text*/
        
         [Key]
         public int QuestionID { get; set; }
         public string QuestionText { get; set; }
    }
    
}