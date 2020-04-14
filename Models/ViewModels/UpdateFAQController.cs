using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HumberAreaHospitalProject.Models.ViewModels
{
    public class UpdateFAQController
    {
        public FAQ FAQ { get; set; }

        public List<FAQCategory> FAQCategory { get; set; }
    }
}