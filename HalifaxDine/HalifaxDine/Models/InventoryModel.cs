using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HalifaxDine.Models
{
    public class InventoryModel
    {
        public int Inv_Id { get; set; }
        public int Ing_Id { get; set; }
        public int Branch_Id { get; set; }
        public int Inv_Amount { get; set; }
        public int Inv_Unit { get; set; }
    }
}