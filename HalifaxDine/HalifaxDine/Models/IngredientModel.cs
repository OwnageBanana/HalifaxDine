using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace HalifaxDine.Models
{
    public class IngredientModel
    {
        [DisplayName("Ingredient Id")]
        public int  Ing_Id { get; set; }
        [DisplayName("Supplier Id")]
        public int Sup_Id { get; set; }
        [DisplayName("Name")]
        public string Ing_Name { get; set; }
        [DisplayName("Price")]
        public decimal Ing_Price { get; set; }
    }
}