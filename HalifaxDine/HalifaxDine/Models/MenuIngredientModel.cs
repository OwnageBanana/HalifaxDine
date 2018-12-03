using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace HalifaxDine.Models
{
    public class MenuIngredientModel
    {
        [DisplayName("Ingredient Id")]
        public int Ing_Id { get; set; }
        [DisplayName("Menu Id")]
        public int Menu_Id { get; set; }
        [DisplayName("Quantity")]
        public float Quantity { get; set; }
        [DisplayName("Unit")]
        public string Unit { get; set; }
    }
}