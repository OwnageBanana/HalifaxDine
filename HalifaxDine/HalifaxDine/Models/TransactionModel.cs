using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace HalifaxDine.Models
{
    public class TransactionModel
    {
        [DisplayName("Transaction Id")]
        public int Trans_Id { get; set; }
        [DisplayName("Branch Id")]
        public int Branch_Id { get; set; }
        [DisplayName("Client Id")]
        public int Client_Id { get; set; }
        [DisplayName("Date")]
        public DateTime Trans_Date { get; set; }
        [DisplayName("Status")]
        public TransactionStatus Trans_Status { get; set; }
    }

    public enum TransactionStatus
    {
        PAID,
        UNPAID
    }
}