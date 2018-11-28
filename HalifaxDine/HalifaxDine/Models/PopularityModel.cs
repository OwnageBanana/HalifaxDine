using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HalifaxDine.Models
{
    public class PopularityModel
    {
        public int? Month { get; set; }
        public int Branch_id { get; set; }
        public string Branch_Province { get; set; }
        public string Branch_Description { get; set; }
        public int Item_Count { get; set; }
        public string Menu_Desc { get; set; }
        public float Avg_Rating { get; set; }

    }
}