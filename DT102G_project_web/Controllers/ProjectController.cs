using DT102G_project_web.Models;
using DT102G_project_web.Services;
using DT102G_project_web.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace DT102G_project_web.Controllers
{
    public class ProjectController : Controller
    {
        [Authorize]
        public async Task<IActionResult> Index()
        {
            ViewBag.FormMessage = null;

            //Get education list
            var url = "Projects";
            List<Project> projects = await ApiHelper.GetAllObjects<Project>(url);

            //set value of list
            var viewModel = new ProjectViewModel
            {
                Projects = projects
            };
            return View(viewModel);
        }

        //POST 
        [HttpPost]
        public IActionResult Index(Project project)
        {
            //check if form is correct
            if (ModelState.IsValid)
            {
                //skicka till API

                //updatera sidan 


                //clear form
                ModelState.Clear();

                //set and get session cookie
                HttpContext.Session.SetString("projectName", project.Name);
                string projectName = HttpContext.Session.GetString("projectName");

                ViewBag.FormMessage = "Du har lagt till projektet med namn: " + projectName;
            }
            else
            {
                ViewBag.FormMessage = "Det gick inte att lägga till projektet";
            }
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
