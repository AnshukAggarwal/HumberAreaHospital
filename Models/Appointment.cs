using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace HumberAreaHospitalProject.Models
{
    public class Appointment
    {
        /*Defines appointment*/
        [Key]
        public int appointmentId { get; set; }
        public string appointmentFname { get; set; }
        public string appointmentLname { get; set; }
        public string appointmentPhone { get; set; }
        public string appointmentEmail { get; set; }
        public  DateTime appointmentDate { get; set; }

        public int DoctorID { get; set; }
        [ForeignKey("DoctorID")]
        public virtual Doctor Doctor { get; set; }
    }
}