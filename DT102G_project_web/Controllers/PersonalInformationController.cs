using DT102G_project_web.Models;
using DT102G_project_web.Services;
using DT102G_project_web.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace DT102G_project_web.Controllers
{
    public class PersonalInformationController : Controller
    {
        [Authorize]
        public async Task<IActionResult> Index()
        {

            //Get education list
            var url = "PersonalInformations";
            List<PersonalInformation> personalInformation = await ApiHelper.GetAllObjects<PersonalInformation>(url);

            //set value of list
            var viewModel = new PersonalInformationViewModel
            {
                PersonalInformations = personalInformation
            };
            return View(viewModel);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
