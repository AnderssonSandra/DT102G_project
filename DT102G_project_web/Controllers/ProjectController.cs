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
        //GET index
        [Authorize]
        public async Task<IActionResult> Index(string infomessage)
        
        {
            ViewBag.FormMessage = null;

            //set value of infomessage if there is any
            if(infomessage != null)
            {
                ViewBag.infomessage = infomessage;
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
        [Authorize]
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

                    ViewBag.FormMessage = "You added the project with the name of: " + projectName;

                }  else
            {
                ViewBag.FormMessage = "Something went wrong with the API.";
            }
                               
            } else
            {
                ViewBag.FormMessage = "Coulden´t add the project because the form is incorrect";
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
        [Authorize]
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
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            //url
            var url = "Projects";

            //call on delete function
            bool response = await ApiHelper.DeleteObject<Project>(url, id);
            
            var infomessage = "Coulden´t delete the project.";

            if (response == true)
            {
                infomessage = "You succeed to delete the project.";
            }
            
            return RedirectToAction("Index", new { infomessage = infomessage });
        }

        //GET: update page
        [Authorize]
        public async Task<IActionResult> Update(int? id)
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

        // POST: update page
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int id, Project updatedProject)
        {
            //url
            var url = "Projects";

            var infomessage = "Coulden´t update the project because the form is incorrect.";

            //check if form is correct
            if (ModelState.IsValid)
            {
                //create object
                Project project = new Project()
                {
                    ProjectId = updatedProject.ProjectId,
                    Name = updatedProject.Name,
                    Techniques = updatedProject.Techniques,
                    Link = updatedProject.Link,
                    Repository = updatedProject.Repository,
                    StartDate = updatedProject.StartDate,
                    EndDate = updatedProject.EndDate,
                    Description = updatedProject.Description
                };

                //call on update function
                bool response = await ApiHelper.UpdateObject<Project>(url, id, project);

                infomessage = "Coulden´t update the project.";

                if (response == true)
                {
                    infomessage = "You succsseded to update the project.";

                }

                return RedirectToAction("Index", new { infomessage = infomessage });
            }

            ViewBag.FormMessage = infomessage;

            return View();

        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
