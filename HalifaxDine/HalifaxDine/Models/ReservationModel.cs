using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace HalifaxDine.Models
{
    public class ReservationModel
    {
        [DisplayName("Reservation Id")]
        public int Res_Id { get; set; }
        [DisplayName("Name")]
        public string Res_Name { get; set; }
        [DisplayName("Date")]
        public DateTime Res_Datetime { get; set; }
        [DisplayName("Phone")]
        public string Res_Phone { get; set; }
        [DisplayName("Status")]
        public string Res_Status { get; set; }
    }
}