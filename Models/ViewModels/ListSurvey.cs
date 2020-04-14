    using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HumberAreaHospitalProject.Models.ViewModels
{
    public class ListSurvey
    {
        /*This class requires list of question and list of answers*/
        public List<Question> questions { get; set; }
        public List<Survey> surveys { get; set; }
    }
}