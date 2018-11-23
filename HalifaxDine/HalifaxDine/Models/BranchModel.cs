using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HalifaxDine.Models
{
    public class BranchModel
    {
        public int Branch_Id { get; set; }
        public string Branch_City { get; set; }
        public string Branch_Province { get; set; }
        public bool Branch_Is_Best_Resturant{ get; set; }
        public string Branch_Description { get; set; }
    }
}