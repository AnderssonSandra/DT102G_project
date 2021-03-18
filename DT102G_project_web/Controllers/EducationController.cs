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
        public async Task<IActionResult> Index(string deletemessage)
        {
            ViewBag.FormMessage = null;

            //set value of deletemessage if there is any
            if (deletemessage != null)
            {
                ViewBag.Deletemessage = deletemessage;
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
                
                    ViewBag.FormMessage = "Du har lagt till utbildningen på " + school;
                } else
                {
                    ViewBag.FormMessage = "Det blev något tokit med API:et";
                }
                //updatera sidan                 
            } else
            {
                ViewBag.FormMessage = "Det gick inte att lägga till utbildningen eftersom formuläret inte är korrekt ifyllt";
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
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            //url
            var url = "Educations";

            //call on delete function
            bool response = await ApiHelper.DeleteObject<Education>(url, id);

            var deletemessage = "Det gick inte att radera utbildningen.";

            if (response == true)
            {
                deletemessage = "Du har nu raderat utbildningen.";
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
