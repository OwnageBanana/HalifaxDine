using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HalifaxDine.Models
{
    public class TransactionModel
    {
        public int Trans_Id { get; set; }
        public int Client_Id { get; set; }
        public DateTime Trans_Date { get; set; }
        public string Trans_Status { get; set; }
    }
}