using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HumberAreaHospitalProject.Models.ViewModels
{
    public class UpdateDoctor
    {
        /*While updating a doctor we need to update details of doctor and
         also the speciality*/

        public Doctor doctor { get; set; }
        public List<Speciality> specialities { get; set; }
    }
}