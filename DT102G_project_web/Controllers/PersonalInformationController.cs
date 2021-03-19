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
        public async Task<IActionResult> Index(string infomessage)
        {

            //set value of infomessage if there is any
            if (infomessage != null)
            {
                ViewBag.infomessage = infomessage;
            }

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
            var url = "PersonalInformations";

            PersonalInformation personalInformation = await ApiHelper.GetOneObject<PersonalInformation>(url, id);

            //check if object is null
            if (personalInformation == null)
            {
                return NotFound();
            }

            //set value of object
            var model = new PersonalInformation()
            {
                PersonalInformationId = personalInformation.PersonalInformationId,
                Name = personalInformation.Name,
                Surname = personalInformation.Surname,
                LinkedIn = personalInformation.LinkedIn,
                Email = personalInformation.Email,
                Phone = personalInformation.Phone,
                IntroductionText = personalInformation.IntroductionText,
                Description = personalInformation.Description
            };

            return View(model);
        }

        // POST: update page
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int id, PersonalInformation updatedPersonalInformation)
        {
            //url
            var url = "PersonalInformations";

            var infomessage = "Coulden´t update the information because the form is incorrect.";

            //check if form is correct
            if (ModelState.IsValid)
            {
                //create object
                PersonalInformation personalInformation = new PersonalInformation()
                {
                    PersonalInformationId = updatedPersonalInformation.PersonalInformationId,
                    Name = updatedPersonalInformation.Name,
                    Surname = updatedPersonalInformation.Surname,
                    LinkedIn = updatedPersonalInformation.LinkedIn,
                    Email = updatedPersonalInformation.Email,
                    Phone = updatedPersonalInformation.Phone,
                    IntroductionText = updatedPersonalInformation.IntroductionText,
                    Description = updatedPersonalInformation.Description
                };

                //call on update function
                bool response = await ApiHelper.UpdateObject<PersonalInformation>(url, id, personalInformation);

                infomessage = "Coulden´t update the information about you.";

                if (response == true)
                {
                    infomessage = "You succsseded updated the information about you.";

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
