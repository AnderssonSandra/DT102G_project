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
        public async Task<IActionResult> Index(string deletemessage)
        
        {
            ViewBag.FormMessage = null;

            //set value of deletemessage if there is any
            if(deletemessage != null)
            {
                ViewBag.Deletemessage = deletemessage;
            }

            //Get project list
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
        public async Task<IActionResult> Index(ProjectViewModel projectViewModel)
        {
            //url
            var url = "Projects";

            //check if form is correct
            if (ModelState.IsValid)
            {
                Project project = new Project()
                {
                    Name = projectViewModel.Project.Name,
                    Techniques = projectViewModel.Project.Techniques,
                    Link = projectViewModel.Project.Link,
                    Repository = projectViewModel.Project.Repository,
                    StartDate = projectViewModel.Project.StartDate,
                    EndDate = projectViewModel.Project.EndDate,
                    Description = projectViewModel.Project.Description

                };
                //skicka till API
                bool response = await ApiHelper.PostObject<Project>(url, project);

                if (response == true)
                {
                    //clear form
                    ModelState.Clear();

                    //set and get session cookie
                    HttpContext.Session.SetString("name", projectViewModel.Project.Name);
                    string projectName = HttpContext.Session.GetString("name");

                    ViewBag.FormMessage = "Du har lagt till utbildningen på " + projectName;

                }  else
            {
                ViewBag.FormMessage = "Du blev något fel med API:et.";
            }
                               
            } else
            {
                ViewBag.FormMessage = "Det gick inte att lägga till projektet eftersom formuläret inte är korrekt ifyllt";
            }

            //Get project list
            List<Project> projects = await ApiHelper.GetAllObjects<Project>(url);

            //set value of list
            var viewModel = new ProjectViewModel
            {
                Projects = projects
            };

            return View(viewModel);
        }

        //GET: delete page
        public async Task<IActionResult> Delete(int? id)
        {
            //check if id is null
            if (id == null)
            {
                return NotFound();
            }

            //Get education object
            var url = "Projects";
            
            Project project = await ApiHelper.GetOneObject<Project>(url, id);

            //check if object is null
            if (project == null)
            {
                return NotFound();
            }

            //set value of object
            var model = new Project
            {
                ProjectId = project.ProjectId,
                Name = project.Name,
                Techniques = project.Techniques,
                Link = project.Link,
                Repository = project.Repository,
                StartDate = project.StartDate,
                EndDate = project.EndDate,
                Description = project.Description
            };

            return View(model);
        }

        // POST: delete page
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            //url
            var url = "Projects";

            //call on delete function
            bool response = await ApiHelper.DeleteObject<Project>(url, id);
            
            var deletemessage = "Det gick inte att radera projektet.";

            if (response == true)
            {
                deletemessage = "Du har nu raderat projektet.";
            }
            
            return RedirectToAction("Index", new { deletemessage = deletemessage });
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
