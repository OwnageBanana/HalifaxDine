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
    }
}