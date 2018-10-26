using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HalifaxDine.Models
{
    public class FeedbackModel
    {
        public int Feedback_Id { get; set; }
        public int Client_Id{ get; set; }
        public string Feedback_Comment { get; set; }
    }
}