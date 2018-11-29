using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace HalifaxDine.Models
{
    public class BranchModel
    {
        [DisplayName("Branch Id")]
        public int Branch_Id { get; set; }
        [DisplayName("City")]
        public string Branch_City { get; set; }
        [DisplayName("Province")]
        public string Branch_Province { get; set; }
        [DisplayName("Best Resturant")]
        public bool Branch_Is_Best_Resturant { get; set; }
        [DisplayName("Description")]
        public string Branch_Description { get; set; }
        [DisplayName("Tax")]
        public float Branch_Tax { get; set; }
    }
}