using Microsoft.AspNetCore.Mvc;
using Shop.Application.Services.Interfaces;
using Shop.Domain.Entities.Admin.About;
using Shop.Domain.Entities.Shop.Site;
using Shop.Domain.ViewModels.Admin.about;
using Shop.Domain.ViewModels.Admin.Dashboard;

namespace Shop.Web.Areas.Admin.Controllers
{
    public class SiteController : AdminBaseController
    {
        #region Ctor

        private readonly IAdminService _adminService;
        public SiteController(IAdminService adminService)
        {
            _adminService = adminService;
        }

        #endregion

        #region Contact

        [HttpGet]
        public async Task<IActionResult> Contact(int contactId = 1)
        {
            var result = await _adminService.GetAllContact(contactId);

            int Count = _adminService.ContactsCount();

            ViewBag.PageID = contactId;
            ViewBag.PageCount = Count / 6;

            return View(result);
        }

        [HttpGet("ContactDetail/{contactId}")]
        public async Task<IActionResult> ContactDetail(int contactId)
        {
            var result = await _adminService.GetContactUsById(contactId);

            if (result == null)
            {
                return NotFound();
            }

            return View(result);
        }

        [HttpGet("RemoveContact")]
        public async Task<IActionResult> RemoveContact(int contactId)
        {
            var result = await _adminService.DeleteContact(contactId);

            if (result)
            {
                return new JsonResult(new { status = "Success" });
            }

            return new JsonResult(new { status = "Error" });
        }

        #endregion

        #region about

        [HttpGet]
        public IActionResult AboutUs()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AboutUs(AboutUsViewModel viewModel)
        {
            if (viewModel.Img != null)
            {
                var result = await _adminService.AddAboutUs(viewModel);

                if (result)
                {
                    TempData["SuccessMessage"] = "The operation was successful";
                    return LocalRedirect("/Admin");
                }
            }

            return View(viewModel);
        }

        [HttpGet("About/{aboutId}")]
        public async Task<IActionResult> EditAboutUs(int aboutId)
        {
            var result = await _adminService.EditAboutUsView(aboutId);

            return View(result);
        }

        [HttpPost("About/{aboutId}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditAboutUs(EditAboutUsViewModel viewModel, int aboutId)
        {
            if (ModelState.IsValid)
            {
                var result = await _adminService.EditAboutUs(viewModel, aboutId);

                TempData["SuccessMessage"] = "The operation was successful";
                return LocalRedirect("/Admin");
            }

            return View(viewModel);
        }

        [HttpGet("AboutImg/{aboutId}")]
        public IActionResult EditImgAboutUs(int aboutId)
        {
            return View();
        }

        [HttpPost("AboutImg/{aboutId}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditImgAboutUs(EditImgAboutUsViewModel viewModel, int aboutId)
        {
            var result = await _adminService.EditImgAboutUs(viewModel, aboutId);

            if (result)
            {
                TempData["SuccessMessage"] = "The operation was successful";
                return LocalRedirect("/Admin");
            }

            return View(viewModel);
        }

        [HttpGet("AddEmployee")]
        public IActionResult AddTeam()
        {
            return View();
        }

        [HttpPost("AddEmployee")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddTeam(TeamViewModel viewModel)
        {
            if (viewModel.Img != null)
            {
                var result = await _adminService.AddTeam(viewModel);

                if (result)
                {
                    TempData["SuccessMessage"] = "The operation was successful";
                    return RedirectToAction("TeamList", "Site");
                }
            }

            return View(viewModel);
        }

        [HttpGet("ListOfEmployees")]
        public async Task<IActionResult> TeamList(int teamId = 1)
        {
            var result = await _adminService.GetTeamGetList(teamId);

            int Count = _adminService.TeamCount();

            ViewBag.PageID = teamId;
            ViewBag.PageCount = Count / 5;

            return View(result);
        }

        [HttpGet("EditEmployee/{teamId}")]
        public async Task<IActionResult> EditTeam(int teamId)
        {
            var result = await _adminService.EditTeamView(teamId);

            return View(result);
        }

        [HttpPost("EditEmployee/{teamId}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditTeam(EditTeamViewModel viewModel, int teamId)
        {
            if (ModelState.IsValid)
            {
                var result = await _adminService.EditTeam(viewModel, teamId);

                return RedirectToAction("TeamList", "Site");
            }

            return View(viewModel);
        }

        [HttpGet("ChangeImage/{teamId}")]
        public IActionResult EditImgTeam(int teamId)
        {
            return View();
        }

        [HttpPost("ChangeImage/{teamId}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditImgTeam(EditImgTeamViewModel viewModel, int teamId)
        {
            var result = await _adminService.EditImgTeam(viewModel, teamId);

            if (result)
            {
                return RedirectToAction("TeamList", "Site");
            }
            
            return View(viewModel);
        }

        [HttpGet("RemoveTeam")]
        public async Task<IActionResult> RemoveTeam(int teamId)
        {
            var result = await _adminService.DeleteTeam(teamId);

            if (result)
            {
                return new JsonResult(new { status = "Success" });
            }

            return new JsonResult(new { status = "Error" });
        }


        #endregion
    }
}
