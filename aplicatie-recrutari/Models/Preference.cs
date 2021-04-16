using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace aplicatie_recrutari.Models
{
    public class Preference
    {
        [Key]
        [Column(Order = 0)]
        public int ProfileId { get; set; }
        [Key]
        [Column(Order = 1)]
        public int DepartmentId { get; set; }

        public int Priority { get; set; }

        public virtual Profile Profile { get; set; }
        public virtual Department Department { get; set; }

    }
}