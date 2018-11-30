using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HalifaxDine.Models
{
    public class PopularityViewModel
    {

        /// <summary>
        /// Branch Province
        /// </summary>
        public string[] x { get; set; }

        /// <summary>
        /// vals
        /// </summary>
        public float[] y { get; set; }
        /// <summary>
        /// Month
        /// </summary>
        public string name { get; set; }

        public string type { get; set; } = "bar";
    }

}