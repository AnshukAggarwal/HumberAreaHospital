using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HumberAreaHospitalProject.Models.ViewModels
{
    public class StaffSpecialty
    {
        public virtual Staff staff { get; set; }
        public virtual List<Speciality> Specialities { get; set; }
    }
}