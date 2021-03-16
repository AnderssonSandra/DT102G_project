using DT102G_project_web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DT102G_project_web.ViewModels
{
    public class EducationViewModel
    {
        //class Education
        public Education Education { get; set; }

        //list of Education experiences
        public List<Education> Educations { get; set; }
    }
}
