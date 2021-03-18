using DT102G_project_web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using DT102G_project_web.Services;
using DT102G_project_web.ViewModels;

namespace DT102G_project_web.Controllers
{
    public class EducationController : Controller
    {
        //GET Index
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Index(string infomessage)
        {
            ViewBag.FormMessage = null;

            //set value of infomessage if there is any
            if (infomessage != null)
            {
                ViewBag.infomessage = infomessage;
            }

            //Get education list
            var url = "Educations";
            List<Education> educations = await ApiHelper.GetAllObjects<Education>(url);

            //set value of list
            var viewModel = new EducationViewModel{
                Educations = educations 
            }; 
            return View(viewModel);
        }

        //POST 
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Index(EducationViewModel educationViewModel)
        {
            //url
            var url = "Educations";

            //check if form is correct
            if (ModelState.IsValid)
            {
                Education education = new Education()
                {
                    Name = educationViewModel.Education.Name,
                    School = educationViewModel.Education.School,
                    StartDate = educationViewModel.Education.StartDate,
                    EndDate = educationViewModel.Education.EndDate,
                    Description = educationViewModel.Education.Description

                };
                //skicka till API
                bool response = await ApiHelper.PostObject<Education>(url, education);

                if(response == true)
                {
                    //clear form
                    ModelState.Clear();

                    //set and get session cookie
                    HttpContext.Session.SetString("school", educationViewModel.Education.School);
                    string school = HttpContext.Session.GetString("school");
                
                    ViewBag.FormMessage = "You added the education at: " + school;
                } else
                {
                    ViewBag.FormMessage = "Something went wrong with the API.";
                }
                //updatera sidan                 
            } else
            {
                ViewBag.FormMessage = "Coulden´t add the education because the form is incorrect";
            }

            //Get education list
            List<Education> educations = await ApiHelper.GetAllObjects<Education>(url);

            //set value of list
            var viewModel = new EducationViewModel
            {
                Educations = educations
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

            //Get work object
            var url = "Educations";
            Education education = await ApiHelper.GetOneObject<Education>(url, id);

            //check if object is null
            if (education == null)
            {
                return NotFound();
            }

            //set value of object
            var model = new Education
            {
                EducationId = education.EducationId,
                Name = education.Name,
                School = education.School,
                StartDate = education.StartDate,
                EndDate = education.EndDate,
                Description = education.Description
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
            var url = "Educations";

            //call on delete function
            bool response = await ApiHelper.DeleteObject<Education>(url, id);

            var infomessage = "Coulden´t delete the education.";

            if (response == true)
            {
                infomessage = "You succeed to delete the education.";
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
            var url = "Educations";

            Education education = await ApiHelper.GetOneObject<Education>(url, id);

            //check if object is null
            if (education == null)
            {
                return NotFound();
            }

            //set value of object
            var model = new Education
            {
                EducationId = education.EducationId,
                Name = education.Name,
                School = education.School,
                StartDate = education.StartDate,
                EndDate = education.EndDate,
                Description = education.Description
            };

            return View(model);
        }

        // POST: update page
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int id, Education updatedEducation)
        {
            //url
            var url = "Educations";

            var infomessage = "Coulden´t update the education because the form is incorrect.";

            //check if form is correct
            if (ModelState.IsValid)
            {
                //create object
                Education education = new Education()
                {
                    EducationId = updatedEducation.EducationId,
                    Name = updatedEducation.Name,
                    School = updatedEducation.School,
                    StartDate = updatedEducation.StartDate,
                    EndDate = updatedEducation.EndDate,
                    Description = updatedEducation.Description
                };

                //call on update function
                bool response = await ApiHelper.UpdateObject<Education>(url, id, education);

                infomessage = "Coulden´t update the education.";

                if (response == true)
                {
                    infomessage = "You succsseded to update the education.";

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
