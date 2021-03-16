using DT102G_project_web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DT102G_project_web.ViewModels
{
    public class CvViewModel
    {
        //list of personal infomation
        public List<PersonalInformation> PersonalInformations { get; set; }

        //list of education experiences
        public List<Education> Educations { get; set; }

        //list of projects
        public List<Project> Projects { get; set; }

        //list of work experiences
        public List<Work> Works { get; set; }
    }
}
