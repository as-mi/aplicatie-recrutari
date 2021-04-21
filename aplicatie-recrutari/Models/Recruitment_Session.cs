using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace aplicatie_recrutari.Models
{
    public class Recruitment_Session
    {
        [Key]
        public int SessionId { get; set; }
        public string Period { get; set; }
        public int Year { get; set; }
        public virtual ICollection<Workshop> Workshops { get; set; }
        public virtual ICollection<Interview> Interviews { get; set; }
    }
}