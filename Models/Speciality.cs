using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HumberAreaHospitalProject.Models
{
    public class Speciality
    {
        //This class has id & name. I also need to represent the doctors table to depict the "Many" in One speciality to many doctors.

        [Key]
        public int SpecialityID { get; set; }
        public string Name { get; set; }

        //now the "Many" in one speciality to many doctors.
        public ICollection<Doctor> Doctors { get; set; }
    }
}