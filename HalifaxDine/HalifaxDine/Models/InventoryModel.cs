using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace HalifaxDine.Models
{
    public class InventoryModel
    {
        [DisplayName("Inventory Id")]
        public int Inv_Id { get; set; }
        [DisplayName("Ingredient Id")]
        public int Ing_Id { get; set; }
        [DisplayName("Branch Id")]
        public int Branch_Id { get; set; }
        [DisplayName("Ammount")]
        public int Inv_Amount { get; set; }
        [DisplayName("Unit")]
        public int Inv_Unit { get; set; }
    }
}