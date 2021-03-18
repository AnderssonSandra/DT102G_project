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
        //GET index page
        [Authorize]
        public async Task<IActionResult> Index(string deletemessage)
        {
            ViewBag.FormMessage = null;

            //set value of deletemessage if there is any
            if (deletemessage != null)
            {
                ViewBag.Deletemessage = deletemessage;
            }

            //Get work list
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
        public async Task<IActionResult> Index(WorkViewModel workViewModel)
        {
            //url
            var url = "Works";

            //check if form is correct
            if (ModelState.IsValid)
            {
                Work work = new Work()
                {
                    Title = workViewModel.Work.Title,
                    Workplace = workViewModel.Work.Workplace,
                    Buzzwords = workViewModel.Work.Buzzwords,
                    StartDate = workViewModel.Work.StartDate,
                    EndDate = workViewModel.Work.EndDate,
                    Description = workViewModel.Work.Description

                };
                //skicka till API
                bool response = await ApiHelper.PostObject<Work>(url, work);

                if (response == true)
                {
                    //clear form
                    ModelState.Clear();

                    //set and get session cookie
                    HttpContext.Session.SetString("workplace", workViewModel.Work.Workplace);
                    string workplace = HttpContext.Session.GetString("workplace");

                    ViewBag.FormMessage = "Du har lagt till arbetet på: " + workplace;
                }
                else
                {
                    ViewBag.FormMessage = "Det blev något fel med API:et";
                }
            }
            else
            {
                ViewBag.FormMessage = "Det gick inte att lägga till arbetet eftersom formuläret inte är korrekt ifyllt";
            }

            //Get work list
            List<Work> works = await ApiHelper.GetAllObjects<Work>(url);

            //set value of list
            var viewModel = new WorkViewModel
            {
                Works = works
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
            var url = "Works";
            Work work = await ApiHelper.GetOneObject<Work>(url, id);

            //check if object is null
            if (work == null)
            {
                return NotFound();
            }

            //set value of object
            var model = new Work
            {
                WorkId = work.WorkId,
                Title = work.Title,
                Workplace = work.Workplace,
                Buzzwords = work.Buzzwords,
                StartDate = work.StartDate,
                EndDate = work.EndDate,
                Description = work.Description
            };

            return View(model);
        }

        // POST: delete page
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            //url
            var url = "Works";

            //call on delete function
            bool response = await ApiHelper.DeleteObject<Work>(url, id);

            var deletemessage = "Det gick inte att radera arbetet.";

            if (response == true)
            {
                deletemessage = "Du har nu raderat arbetet.";
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
