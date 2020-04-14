using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HumberAreaHospitalProject.Models.ViewModels
{
    public class ShowJob
    {
        public virtual Job Job { get; set; }
        public List<Application> Applications { get; set; }
    }
}