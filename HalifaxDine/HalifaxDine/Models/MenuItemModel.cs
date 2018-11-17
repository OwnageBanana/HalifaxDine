using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HalifaxDine.Models
{
    public class MenuItemModel
    {
        public int Menu_Id { get; set; }
        public string Menu_Ref { get; set; }
        public string Menu_Ing { get; set; }
        public string Menu_Meal { get; set; }
    }
}