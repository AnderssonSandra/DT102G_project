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
    public class WorkController : Controller
    {
        [Authorize]
        public async Task<IActionResult> Index()
        {
            ViewBag.FormMessage = null;

            //Get education list
            var url = "Works";
            List<Work> works = await ApiHelper.GetAllObjects<Work>(url);

            //set value of list
            var viewModel = new WorkViewModel
            {
                Works = works
            };
            return View(viewModel);
        }

        //POST 
        [HttpPost]
        public IActionResult Index(Work work)
        {
            //check if form is correct
            if (ModelState.IsValid)
            {
                //skicka till API

                //updatera sidan 


                //clear form
                ModelState.Clear();

                //set and get session cookie
                HttpContext.Session.SetString("workplace", work.Workplace);
                string workplace = HttpContext.Session.GetString("workplace");

                ViewBag.FormMessage = "Du har lagt till arbetet på " + workplace;
            }
            else
            {
                ViewBag.FormMessage = "Det gick inte att lägga till arbetet";
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
