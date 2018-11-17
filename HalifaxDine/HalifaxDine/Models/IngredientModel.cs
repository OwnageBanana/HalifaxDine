using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HalifaxDine.Models
{
    public class IngredientModel
    {
        public int  Ing_Id { get; set; }
        public int Sup_Id { get; set; }
        public string Ing_Name { get; set; }
        public decimal Ing_Price { get; set; }
    }
}