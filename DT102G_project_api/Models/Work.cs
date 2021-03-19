using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DT102G_project_API.Models
{
    public class Work
    {
        [Required]
        public int WorkId { get; set; }

        [Required]
        [MaxLength(300)]
        public string Title { get; set; }

        [Required]
        [MaxLength(200)]
        public string Workplace { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        public string Buzzwords { get; set; }

        [Required]
        public string Description { get; set; }

    }

}
