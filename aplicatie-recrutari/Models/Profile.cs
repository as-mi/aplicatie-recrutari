using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace aplicatie_recrutari.Models
{
    public class Profile
    {
        [Key]
        public int ProfileId { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string InterviewFeedback { get; set; }
        public string WorkshopFeedback { get; set; }
        public string Status { get; set; }
    }
}