using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HalifaxDine.Models
{
    public class SalesModel
    {
        public int Branch_Id { get; set; }
        public float Sales { get; set; }
        public int Month { get; set; }
        public string Branch_Province { get; set; }
        public string Menu_Name { get; set; }
    }
}