using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HalifaxDine.Models
{
    public class FeedbackModel
    {
        [DisplayName("FeedBack Id")]
        public int Feedback_Id { get; set; }
        [DisplayName("Client Id")]
        public int Client_Id { get; set; }
        [DisplayName("Branch Id")]
        public int Branch_Id { get; set; }
        [StringLength(1000, ErrorMessage = "Looks Like your Message was too long! we are glad you have so much to say though")]
        [DisplayName("Comment")]
        public string Feedback_Comment { get; set; }
        [DisplayName("Rating")]
        public int Feedback_Rating { get; set; }
        [DisplayName("Date")]
        public DateTime DateTime { get; set; }
    }
}