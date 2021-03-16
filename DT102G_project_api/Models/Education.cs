using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DT102G_project_API.Models
{
    public class Education
    {
        [Required]
        public int EducationId { get; set; }

        [Required]
        [MaxLength(200)]
        public string  Name { get; set; }

        [Required]
        [MaxLength(200)]
        public string School { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        [Required]
        public string Description { get; set; }
    }
}
