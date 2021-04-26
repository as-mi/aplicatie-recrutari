using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace aplicatie_recrutari.Models {
    public class Workshop {
        [Key]
        public int WorkshopId { get; set; }
        public DateTime Timestamp { get; set; }

        public int? DepartmentId1 { get; set; }
        [ForeignKey("DepartmentId1")]
        public virtual Department Department1 { get; set; }
        public int? DepartmentId2 { get; set; }
        [ForeignKey("DepartmentId2")]
        public virtual Department Department2 { get; set; }

        [Column("SessionId")]
        public int SessionId { get; set; }
        public virtual Recruitment_Session Session { get; set; }

        public IEnumerable<SelectListItem> AllDepartments { get; set; }
    }
}