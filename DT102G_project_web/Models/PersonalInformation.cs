using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DT102G_project_web.Models
{
    public class PersonalInformation
    {
        [Required]
        public int PersonalInformationId { get; set; }

        [Required(ErrorMessage = "Du måste ange ditt namn")]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Du måste ange ditt efternamn")]
        [MaxLength(100, ErrorMessage ="Du kan max skriva 100 tecken")]
        public string Surname { get; set; }

        [MaxLength(200, ErrorMessage ="Du kan max skriva 200 tecken")]
        public string Email { get; set; }

        [MaxLength(20, ErrorMessage ="Du kan max skriva 20 tecken")]
        public string Phone { get; set; }
        public string LinkedIn { get; set; }
        
        [Required(ErrorMessage = "Du måste skriva en introduktionstext")]
        public string IntroductionText { get; set; }

        [Required(ErrorMessage = "Du måste skriva en beskrivning om dig själv")]
        public string Description { get; set; }
    }
}
