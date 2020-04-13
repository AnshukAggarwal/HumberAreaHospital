using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HumberAreaHospitalProject.Models.ViewModels
{
    public class UpdateHomeRemedy
    {
        public HomeRemedies HomeRemedies { get; set; }
        public List<RemedySource> RemedySources { get; set; }
    }
}