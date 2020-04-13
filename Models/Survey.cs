using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HumberAreaHospitalProject.Models
{
    public class Survey
    {
        /*this class will have
         * response_id
         * response_text
         * question_id*/

        [Key]
        public int ResponseID { get; set; }
        public string ResponseText { get; set; }
        public string UserName { get; set; }

        //Now I need to reference the question id
        public int QuestionID { get; set; }
        [ForeignKey("QuestionID")]
        public virtual Question Questions { get; set; }
    }
}