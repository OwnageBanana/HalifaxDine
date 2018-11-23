using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HalifaxDine.Models
{
    public class FeedbackModel
    {
        public int Feedback_Id { get; set; }
        public int Client_Id { get; set; }
        public int Branch_Id { get; set; }
        [StringLength(1000, ErrorMessage = "Looks Like your Message was too long! we are glad you have so much to say though")]
        public string Feedback_Comment { get; set; }
        public int Feedback_Rating { get; set; }

    }
}