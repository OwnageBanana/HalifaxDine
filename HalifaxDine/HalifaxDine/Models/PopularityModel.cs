using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HalifaxDine.Models
{
    public class PopularityModel
    {
        public int? Month { get; set; }
        public int Branch_id{ get; set; }
        public string Branch_Description { get; set; }
        public float avg_rating { get; set; }
    }
}