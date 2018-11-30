using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace HalifaxDine.Models
{
    public class PopularityModel
    {
        [DisplayName("Month")]
        public int Month { get; set; }
        [DisplayName("Branch Id")]
        public int Branch_id { get; set; }
        [DisplayName("Province")]
        public string Branch_Province { get; set; }
        [DisplayName("Description")]
        public string Branch_Description { get; set; }
        [DisplayName("Count")]
        public int Item_Count { get; set; }
        [DisplayName("Description")]
        public string Menu_Desc { get; set; }
        [DisplayName("Rating")]
        public float Avg_Rating { get; set; }

    }
}