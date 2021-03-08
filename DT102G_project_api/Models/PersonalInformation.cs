using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DT102G_project_API.Models
{
    public class PersonalInformation
    {
        [Required]
        public int PersonalInformationId { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        [MaxLength(100)]
        public string Surname { get; set; }

        [MaxLength(200)]
        public string Email { get; set; }

        [MaxLength(20)]
        public string Phone { get; set; }
        public string LinkedIn { get; set; }
        public string IntroductionText { get; set; }
        public string Description { get; set; }
    }
}
