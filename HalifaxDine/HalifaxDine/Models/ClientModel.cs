using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace HalifaxDine.Models
{
    public class ClientModel
    {
        [DisplayName("Client Id")]
        public int Client_Id { get; set; }
        [DisplayName("First Name")]
        public string Client_FName { get; set; }
        [DisplayName("Last Name")]
        public string Client_LName { get; set; }
        [DisplayName("Contact")]
        public string Client_Contact { get; set; }
        [DisplayName("Account Id")]
        public string Account_Id { get; set; }
    }
}