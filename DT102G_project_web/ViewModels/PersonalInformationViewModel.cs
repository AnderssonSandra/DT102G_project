using DT102G_project_web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DT102G_project_web.ViewModels
{
    public class PersonalInformationViewModel
    {
        //class Education
        public PersonalInformation PersonalInformation { get; set; }

        //list of Education experiences
        public List<PersonalInformation> PersonalInformations { get; set; }
    }
}
