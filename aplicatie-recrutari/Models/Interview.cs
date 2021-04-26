using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace aplicatie_recrutari.Models {
    public class Interview {
        [Key]
        public int InterviewId { get; set; }
        public DateTime Timestamp { get; set; }

        [Column("SessionId")]
        public int SessionId { get; set; }
        public virtual Recruitment_Session Session { get; set; }

        [Column("DepartmentId")]
        public int DepartmentId { get; set; }
        public virtual Department Department { get; set; }

        [Column("ProfileId")]
        public int ProfileId { get; set; }
        public virtual Profile Profile { get; set; }

        [NotMapped]
        public IEnumerable<SelectListItem> AllDepartments { get; set; }
    }
}