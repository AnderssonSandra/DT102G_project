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
        [Authorize]
        public async Task<IActionResult> Index()
        {
            ViewBag.FormMessage = null;

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
        public async Task<IActionResult> Index(Education education)
        {
            //check if form is correct
            if(ModelState.IsValid)
            {
                /*skicka till API
                var url = "Educations";
                await ApiHelper.PostRequest(url, education);*/

                //updatera sidan 


                //clear form
                ModelState.Clear();

                //set and get session cookie
                HttpContext.Session.SetString("school", education.School);
                string school = HttpContext.Session.GetString("school");
                
                ViewBag.FormMessage = "Du har lagt till utbildningen på " + school;
            } else
            {
                ViewBag.FormMessage = "Det gick inte att lägga till utbildningen";
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
