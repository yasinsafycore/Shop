using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Shop.Application.Extensions;
using Shop.Application.Services.Interfaces;
using Shop.Application.Statics;
using Shop.Domain.Entities.Admin.About;
using Shop.Domain.Entities.Shop.Site;
using Shop.Domain.ViewModels.Admin.about;
using Shop.Domain.ViewModels.Admin.Dashboard;
using Shop.Domain.ViewModels.Admin.SiteUI;
using System.Diagnostics;
using System.Drawing;

namespace Shop.Web.Areas.Admin.Controllers
{
    public class HomeController : AdminBaseController
    {
        #region Ctor

        private readonly IAdminService _adminService;
        public HomeController(IAdminService adminService)
        {
            _adminService = adminService;
        }

        #endregion

        #region Index

        public IActionResult Index()
        {
            return View();
        }

        #endregion

        #region Profile

        [HttpGet]
        public IActionResult AddProfile()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddProfile(AdminProfileViewModel viewModel)
        {
            if (viewModel.Img != null)
            {
                var result = await _adminService.AddProfile(viewModel);

                return LocalRedirect("/Admin");
            }

            return View(viewModel);
        }

        [HttpGet("EditProfile/{profileId}")]
        public async Task<IActionResult> EditProfile(int profileId)
        {
            var result = await _adminService.EditProfileView(profileId);

            if (result == null) return NotFound();

            return View(result);
        }

        [HttpPost("EditProfile/{profileId}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditProfile(EditProfileViewModel viewModel, int profileId)
        {
            if (ModelState.IsValid)
            {
                var result = await _adminService.EditProfile(viewModel, profileId);

                if (result)
                {
                    return LocalRedirect("/Admin");
                }
            }

            return View(viewModel);
        }

        [HttpGet("EditImgProfile/{profileId}")]
        public IActionResult EditImgProfile(int profileId)
        {
            return View();
        }

        [HttpPost("EditImgProfile/{profileId}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditImgProfile(EditImgProfileViewModel viewModel, int profileId)
        {
            var result = await _adminService.EditImgProfile(viewModel, profileId);

            if (result)
            {
                return LocalRedirect("/Admin");
            }

            return View(viewModel);
        }

        #endregion

        #region Editor Upload

        public async Task<IActionResult> UploadEditorImage(IFormFile upload)
        {
            var fileName = Guid.NewGuid() + Path.GetExtension(upload.FileName);

            upload.UploadFile(fileName, PathTools.EditorImageServerPath);

            return Json(new { url = $"{PathTools.EditorImagePath}{fileName}" });
        }

        #endregion
    }
}
