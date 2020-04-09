using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HumberAreaHospitalProject.Models
{
    public class Doctor
    {
        /*This class defines a doctor.
            Let's see what defines a doctor in general or what is the idea of a doctor
            ID
            Title
            First Name
            Last Name
            It also needs to reference the speciality
         */
        [Key]
        public int DoctorID { get; set; }
        public string Title { get; set; }
        public string DoctorFname { get; set; }
        public string DoctorLname { get; set; }

        /*Now I need to refernce the speciality*/

        public int SpecialityID { get; set; }
        [ForeignKey("SpecialityID")]
        public virtual Speciality Specialities { get; set; }
    }
}