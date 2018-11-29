using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HalifaxDine.Models
{
    public class EmployeeModel
    {
        [Key]
        [DisplayName("Employee Id")]
        public int Emp_Id { get; set; }
        [DisplayName("Privilege Id")]
        public int Privilege_Id { get; set; }
        [DisplayName("Branch Id")]
        public int Branch_Id { get; set; }
        [DisplayName("First Name")]
        public string Emp_FName { get; set; }
        [DisplayName("Last Name")]
        public string Emp_LName { get; set; }
        [DisplayName("Hourly Rate")]
        public decimal Emp_Hourly_Rate { get; set; }
        [DisplayName("Hire Date")]
        public DateTime Emp_Hire_Date { get; set; }
        [DisplayName("Email")]
        public string Emp_Email { get; set; }
        [DisplayName("Phone")]
        public string Emp_Phone { get; set; }
        [DisplayName("Account Id")]
        public string Account_Id { get; set; }
    }
}