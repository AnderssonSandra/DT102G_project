using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DT102G_project_web.Models
{
    public class Project
    {
        [Required]
        public int ProjectId { get; set; }

        [Required(ErrorMessage = "Du måste ange namnet på projektet")]
        [MaxLength(300, ErrorMessage ="Du kan max skriva 300 tecken")]
        public string Name { get; set; }

        public string Link { get; set; }

        public string Repository { get; set; }

        [MaxLength(300, ErrorMessage ="Du kan max skriva 300 tecken")]
        public string Techniques { get; set; }

        [Required(ErrorMessage = "Du måste ange startdatum på projektet")]
        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        [Required(ErrorMessage = "Du måste ange en beskrivning av projektet")]
        public string Description { get; set; }
    }
}
