using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DT102G_project_web.Models
{
    public class Work
    {
        [Required]
        public int WorkId { get; set; }

        [Required(ErrorMessage = "Du måste ange en titel")]
        [MaxLength(300, ErrorMessage = "Du kan max skriva 300 tecken")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Du måste ange arbetsplats")]
        [MaxLength(200, ErrorMessage ="Du kan max skriva 200 tecken")]
        public string Workplace { get; set; }

        [Required(ErrorMessage = "Du måste ange startdatum för arbetet")]
        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string Buzzwords { get; set; }

        [Required(ErrorMessage = "Du måste ange en beskrivning av arbetet")]
        public string Description { get; set; }

    }

}
