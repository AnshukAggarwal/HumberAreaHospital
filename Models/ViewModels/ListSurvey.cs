using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HumberAreaHospitalProject.Models.ViewModels
{
	public class ListSurvey
	{
        public virtual List<Question> questions { get; set; }
        public virtual List<Survey> answers { get; set; }


	}
}
