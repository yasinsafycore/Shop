using Microsoft.AspNetCore.Components.Routing;
using Microsoft.AspNetCore.Mvc;
using Shop.Application.Services.Implementations;
using Shop.Application.Services.Interfaces;
using Shop.Domain.Entities.Account;
using Shop.Domain.Entities.Admin.Setting.Footer;
using Shop.Domain.Entities.Shop.Site;
using Shop.Domain.ViewModels.Admin.Setting.Footer;
using Shop.Domain.ViewModels.Admin.Setting.General;

namespace Shop.Web.Areas.Admin.Controllers
{
    public class SettingController : AdminBaseController
    {
        #region Ctor

        private readonly ISettingService _settingService;
        public SettingController(ISettingService settingService)
        {
            _settingService = settingService;
        }

        #endregion

        #region Social Networks

        [HttpGet]
        public IActionResult AddSocial()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddSocial(SocialViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var result = await _settingService.AddSocial(viewModel);

                if (result)
                {
                    TempData[SuccessMessage] = "The operation was successful";
                    return LocalRedirect("/admin");
                }
            }

            return View(viewModel);
        }

        [HttpGet("EditSocial/{socialId}")]
        public async Task<IActionResult> EditSocial(int socialId)
        {
            var result = await _settingService.EditSocialView(socialId);

            return View(result);
        }

        [HttpPost("EditSocial/{socialId}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditSocial(EditSocialViewModel viewModel, int socialId)
        {
            if (ModelState.IsValid)
            {
                var result = await _settingService.EditSocial(viewModel, socialId);

                if (result)
                {
                    TempData[SuccessMessage] = "The operation was successful";
                    return LocalRedirect("/admin");
                }
            }

            return View(viewModel);
        }

        #endregion

        #region Footer Label

        [HttpGet]
        public IActionResult AddLabel()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddLabel(FooterLabelViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var result = await _settingService.AddLabel(viewModel);

                if (result)
                {
                    return RedirectToAction("LabelList", "Setting");
                }
            }

            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> LabelList()
        {
            var result = await _settingService.GetListOfLabel();

            return View(result);
        }

        [HttpGet("EditLabel/{labelId}")]
        public async Task<IActionResult> EditLabel(int labelId)
        {
            var result = await _settingService.EditLabelView(labelId);

            return View(result);
        }

        [HttpPost("EditLabel/{labelId}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditLabel(FooterLabelViewModel viewModel, int labelId)
        {
            if (ModelState.IsValid)
            {
                var result = await _settingService.EditLabel(viewModel, labelId);

                if (result)
                {
                    return RedirectToAction("LabelList", "Setting");
                }
            }

            return View(viewModel);
        }

        #endregion

        #region Footer Link

        [HttpGet]
        public async Task<IActionResult> AddLink()
        {
            ViewData["FooterLabel"] = await _settingService.GetListLabel();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddLink(FooterLinkViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var result = await _settingService.AddLink(viewModel);

                if (result)
                {
                    return RedirectToAction("LinkList", "Setting");
                }
            }

            ViewData["FooterLabel"] = await _settingService.GetListLabel();
            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> LinkList(int linkId = 1)
        {
            var result = await _settingService.GetListOfLink(linkId);

            int Count = _settingService.FooterLinkCount();

            ViewBag.PageID = linkId;
            ViewBag.PageCount = Count / 7;

            return View(result);
        }

        [HttpGet("EditLink/{linkId}")]
        public async Task<IActionResult> EditLink(int linkId)
        {
            var result = await _settingService.EditLinkView(linkId);
            ViewData["FooterLabel"] = await _settingService.GetListLabel();
            return View(result);
        }

        [HttpPost("EditLink/{linkId}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditLink(FooterLinkViewModel viewModel, int linkId)
        {
            if (ModelState.IsValid)
            {
                var result = await _settingService.EditLink(viewModel, linkId);

                if (result)
                {
                    return RedirectToAction("LinkList", "Setting");
                }
            }

            ViewData["FooterLabel"] = await _settingService.GetListLabel();
            return View(viewModel);
        }

        [HttpGet("RemoveLink")]
        public async Task<IActionResult> RemoveLink(int linkId)
        {
            var result = await _settingService.DeleteLink(linkId);

            if (result)
            {
                return new JsonResult(new { status = "Success" });
            }

            return new JsonResult(new { status = "Error" });
        }

        #endregion

        #region User

        [HttpGet]
        public async Task<IActionResult> Users(int userId = 1)
        {
            var result = await _settingService.GetUsers(userId);

            int Count = _settingService.GetUserCount();

            ViewBag.PageID = userId;
            ViewBag.PageCount = Count / 7;

            return View(result);
        }

        [HttpGet("BanUser")]
        public async Task<IActionResult> BanUser(int userId)
        {
            var result = await _settingService.BanUser(userId);

            if (result)
            {
                return new JsonResult(new { status = "Success" });
            }

            return new JsonResult(new { status = "Error" });
        }

        [HttpGet("ReBanUser")]
        public async Task<IActionResult> RestoreUser(int userId)
        {
            var result = await _settingService.ReBanUser(userId);

            if (result)
            {
                return new JsonResult(new { status = "Success" });
            }

            return new JsonResult(new { status = "Error" });
        }

        #endregion

        #region Admin Logo

        [HttpGet]
        public IActionResult AdminLogo()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AdminLogo(AdminLogoViewModel viewModel)
        {
            if (viewModel.Img != null)
            {
                var result = await _settingService.AddAdminLogo(viewModel);

                return LocalRedirect("/Admin");
            }

            return View(viewModel);
        }

        [HttpGet("EditAdminLogo/{logoId}")]
        public IActionResult EditAdminLogo(int logoId)
        {
            return View();
        }

        [HttpPost("EditAdminLogo/{logoId}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditAdminLogo(AdminLogoViewModel viewModel, int logoId)
        {
            if (ModelState.IsValid)
            {
                var result = await _settingService.EditLogo(viewModel, logoId);

                if (result)
                {
                    return LocalRedirect("/Admin");
                }
            }

            return View(viewModel);
        }

        #endregion

        #region Site Logo

        [HttpGet]
        public IActionResult SiteLogo()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SiteLogo(SiteLogoViewModel viewModel)
        {
            if (viewModel.Img != null)
            {
                var result = await _settingService.AddSiteLogo(viewModel);

                return LocalRedirect("/");
            }

            return View(viewModel);
        }

        [HttpGet("EditSiteDescription/{logoId}")]
        public async Task<IActionResult> EditSiteDescription(int logoId)
        {
            var result = await _settingService.EditSiteDescriptionView(logoId);

            if (result == null) return NotFound();

            return View(result);
        }

        [HttpPost("EditSiteDescription/{logoId}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditSiteDescription(SiteDescriptionViewModel viewModel, int logoId)
        {
            if (ModelState.IsValid)
            {
                var result = await _settingService.EditSiteDescription(viewModel, logoId);

                return LocalRedirect("/Admin");
            }

            return View(viewModel);
        }

        [HttpGet("EditSiteImg/{logoId}")]
        public IActionResult EditSiteImg(int logoId)
        {
            return View();
        }

        [HttpPost("EditSiteImg/{logoId}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditSiteImg(SiteImgViewModel viewModel, int logoId)
        {
            if (viewModel.Img != null)
            {
                var result = await _settingService.EditSiteImg(viewModel, logoId);

                return LocalRedirect("/");
            }

            return View(viewModel);
        }

        #endregion

        #region Copy Right

        [HttpGet]
        public IActionResult CopyRight()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CopyRight(CopyRightViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var result = await _settingService.AddCopyRight(viewModel);

                return LocalRedirect("/Admin");
            }

            return View(viewModel);
        }

        [HttpGet("EditCopyRight/{Id}")]
        public async Task<IActionResult> EditCopyRight(int Id)
        {
            var result = await _settingService.EditCopyRightView(Id);

            if (result == null) return NotFound();

            return View(result);
        }

        [HttpPost("EditCopyRight/{Id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditCopyRight(CopyRightViewModel viewModel, int Id)
        {
            if (ModelState.IsValid)
            {
                var result = await _settingService.EditCopyRight(viewModel, Id);

                return LocalRedirect("/Admin");
            }

            return View(viewModel);
        }

        #endregion
    }
}
