using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DT102G_project_API.Models
{
    public class Project
    {
        [Required]
        public int ProjectId { get; set; }

        [Required]
        [MaxLength(300)]
        public string Name { get; set; }

        public string Link { get; set; }

        public string Repository { get; set; }

        [MaxLength(300)]
        public string Techniques { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        [Required]
        public string Description { get; set; }
    }
}
