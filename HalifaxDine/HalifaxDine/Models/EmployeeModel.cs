using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HalifaxDine.Models
{
    public class EmployeeModel
    {
        [Key]
        public int Emp_id { get; set; }
        public int Privilege_id { get; set; }
        public int Branch_id { get; set; }
        public string Emp_FName { get; set; }
        public string Emp_LName { get; set; }
        public decimal Emp_Hourly_Rate { get; set; }
        public DateTime Emp_Hire_Date { get; set; }
        public string Emp_Email { get; set; }
        public string Emp_Phone { get; set; }
    }
}