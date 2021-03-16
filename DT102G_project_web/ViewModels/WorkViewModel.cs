using DT102G_project_web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DT102G_project_web.ViewModels
{
    public class WorkViewModel
    {
        //work project
        public Work Work { get; set; }

        //list of work experiences
        public List<Work> Works { get; set; }
    }
}
