using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HalifaxDine.Models
{
    public class MenuIngredientModel
    {
        public int  Ing_Id { get; set; }
        public int Menu_Id { get; set; }
        public float Quantity { get; set; }
        public char Unit { get; set; }
    }
}