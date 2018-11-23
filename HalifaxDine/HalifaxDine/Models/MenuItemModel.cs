using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HalifaxDine.Models
{
    public class MenuItemModel
    {
        public int Menu_Id { get; set; }
        public string Menu_Name { get; set; }
        public string Menu_Desc { get; set; }
        public string Menu_Price { get; set; }
    }
}