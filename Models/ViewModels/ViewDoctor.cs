using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HumberAreaHospitalProject.Models.ViewModels
{
    public class ViewDoctor
    {
        public virtual Doctor doctor { get; set; }
        public List<Doctor> similardoctors { get; set; }
    }
}