using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HalifaxDine.Models
{
    public class ReservationModel
    {
        public int Res_Id { get; set; }
        public string Res_Name { get; set; }
        public DateTime Res_Datetime { get; set; }
        public string Res_Phone { get; set; }
        public string Res_Status { get; set; }
    }
}