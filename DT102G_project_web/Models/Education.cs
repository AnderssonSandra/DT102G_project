using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DT102G_project_web.Models
{
    public class Education
    {
        [Required]
        public int EducationId { get; set; }

        [Required(ErrorMessage = "Du måste ange namnet på utbildningen")]
        [MaxLength(200, ErrorMessage ="Du kan max skriva 200 tecken")]
        public string  Name { get; set; }

        [Required(ErrorMessage = "Du måste ange institutet")]
        [MaxLength(200, ErrorMessage ="Du kan max skriva 200 tecken")]
        public string School { get; set; }

        [Required(ErrorMessage = "Du måste ange vilket datum utbildningen startade")]
        [DataType(DataType.Date, ErrorMessage = "Ange datum i korrekt format")]

        public DateTime StartDate { get; set; }

        [DataType(DataType.Date, ErrorMessage = "Ange datum i korrekt format")]
        public DateTime EndDate { get; set; }

        [Required(ErrorMessage = "Du måste ange en beskrivning")]
        public string Description { get; set; }

    }
}
