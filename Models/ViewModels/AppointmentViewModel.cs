using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HumberAreaHospitalProject.Models.ViewModels
{
    public class AppointmentViewModel
    {
        public virtual Appointment appointment { get; set; }
        public virtual List<Doctor> doctors { get; set; }
    }
}