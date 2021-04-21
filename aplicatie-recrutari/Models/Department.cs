using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace aplicatie_recrutari.Models
{
    public class Department
    {
        [Key]
        public int DepartmentId { get; set; }
        public string Name { get; set; }
        [ForeignKey("DepartmentId1")]
        public virtual ICollection<Workshop> Workshops1 { get; set; }
        [ForeignKey("DepartmentId2")]
        public virtual ICollection<Workshop> Workshops2 { get; set; }
        public virtual ICollection<Interview> Interviews { get; set; }
    }
}