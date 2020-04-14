using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HumberAreaHospitalProject.Models
{
    public class Staff
    {
        [Key]
        public int staffId { get; set; }
        public string staffFname { get; set; }
        public string staffLname { get; set; }
        public string staffEmail { get; set; }
        public string staffExt { get; set; }

        public int SpecialtyID { get; set; }
        [ForeignKey("SpecialtyID")]
        public virtual Speciality Speciality { get; set; }
    }

}