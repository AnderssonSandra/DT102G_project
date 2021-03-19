using DT102G_project_web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DT102G_project_web.ViewModels
{
    public class ProjectViewModel
    {
        //class project
        public Project Project { get; set; }

        //list of projects
        public List<Project> Projects { get; set; }
    }
}
