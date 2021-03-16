using DT102G_project_web.Models;
using DT102G_project_web.Services;
using DT102G_project_web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace DT102G_project_web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            //Get education list
            var workUrl = "Works";
            List<Work> works = await ApiHelper.GetAllObjects<Work>(workUrl);

            //Get education list
            var educationUrl = "Educations";
            List<Education> educations = await ApiHelper.GetAllObjects<Education>(educationUrl);

            //Get project list
            var projectUrl = "Projects";
            List<Project> projects = await ApiHelper.GetAllObjects<Project>(projectUrl);

            //Get personal information list
            var personalInformationUrl = "PersonalInformations";
            List<PersonalInformation> personalInformation = await ApiHelper.GetAllObjects<PersonalInformation>(personalInformationUrl);

            //set value of list
            var viewModel = new CvViewModel
            {
                PersonalInformations = personalInformation,
                Educations = educations,
                Projects = projects,
                Works = works,
                
            };
            return View(viewModel);
            //list of personal infomation
        
    }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
