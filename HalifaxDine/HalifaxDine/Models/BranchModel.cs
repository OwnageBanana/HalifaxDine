using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HalifaxDine.Models
{
    public class BranchModel
    {
        [DisplayName("Branch Id")]
        public int Branch_Id { get; set; }
        [DisplayName("City")]
        [Required]
        public string Branch_City { get; set; }
        [DisplayName("Province")]
        [Required]
        public string Branch_Province { get; set; }
        [DisplayName("Best Resturant")]
        [Required]
        public bool Branch_Is_Best_Resturant { get; set; }
        [DisplayName("Description")]
        [Required]
        public string Branch_Description { get; set; }
        [DisplayName("Tax")]
        [Required]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public float Branch_Tax { get; set; }
    }
}