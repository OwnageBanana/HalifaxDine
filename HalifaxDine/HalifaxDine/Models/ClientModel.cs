using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HalifaxDine.Models
{
    public class ClientModel
    {
        public int Client_Id { get; set; }
        public string Client_FName{ get; set; }
        public string Client_LName { get; set; }
        public string Client_Contact { get; set; }
    }
}