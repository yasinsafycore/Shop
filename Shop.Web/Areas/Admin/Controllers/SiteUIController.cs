using Microsoft.AspNetCore.Mvc;
using Shop.Application.Generators;
using Shop.Application.Services.Interfaces;
using Shop.Domain.Entities.Admin;
using Shop.Domain.Interfaces;
using Shop.Domain.ViewModels.Admin.SiteUI;
using Shop.Domain.ViewModels.Shop.Site;

namespace Shop.Web.Areas.Admin.Controllers
{
    public class SiteUIController : AdminBaseController
    {
        #region Ctor

        private readonly IAdminService _adminService;
        public SiteUIController(IAdminService adminService)
        {
            _adminService = adminService;
        }

        #endregion

        #region HeaderSlider

        [HttpGet]
        public IActionResult HeaderSlider()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> HeaderSlider(HeaderSliderViewModel viewModel)
        {
            if (viewModel.Img != null)
            {
                var result = await _adminService.HeaderSlider(viewModel);

                if (result)
                {
                    return RedirectToAction("ListHeaderSlider", "SiteUI");
                }
            }

            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> ListHeaderSlider(int sliderId = 1)
        {
            var result = await _adminService.GetAllSliders(sliderId);

            int Count = _adminService.SlidersCount();

            ViewBag.PageID = sliderId;
            ViewBag.PageCount = Count / 4;

            return View(result);
        }

        [HttpGet("EditHeaderSlider/{sliderId}")]
        public async Task<IActionResult> EditHeaderSlider(int sliderId)
        {
            var result = await _adminService.EditSliderView(sliderId);

            if (result == null) return NotFound();

            return View(result);
        }

        [HttpPost("EditHeaderSlider/{sliderId}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditHeaderSlider(EditSliderViewModel viewModel, int sliderId)
        {
            if (ModelState.IsValid)
            {
                var result = await _adminService.EditSlider(viewModel, sliderId);

                if (result)
                {
                    return RedirectToAction("ListHeaderSlider", "SiteUI");
                }
            }

            return View(viewModel);
        }

        [HttpGet("EditImgSlider/{sliderId}")]
        public IActionResult EditImgSlider(int sliderId)
        {
            return View();
        }

        [HttpPost("EditImgSlider/{sliderId}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditImgSlider(EditImgSliderViewModel viewModel, int sliderId)
        {
            var result = await _adminService.EditImgSlider(viewModel, sliderId);

            if (result)
            {
                return RedirectToAction("ListHeaderSlider", "SiteUI");
            }

            return View(viewModel);
        }

        [HttpGet("DeleteSlider")]
        public async Task<IActionResult> DeleteSlider(int sliderId)
        {
            var result = await _adminService.DeleteSlider(sliderId);

            if (result)
            {
                return new JsonResult(new { status = "Success" });
            }

            return new JsonResult(new { status = "Error" });
        }

        [HttpGet("RestoreSlider")]
        public async Task<IActionResult> RestoreSlider(int sliderId)
        {
            var result = await _adminService.RestoreSlider(sliderId);

            if (result)
            {
                return new JsonResult(new { status = "Success" });
            }

            return new JsonResult(new { status = "Error" });
        }

        #endregion

        #region HeaderBanner

        [HttpGet]
        public IActionResult HeaderBanner()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> HeaderBanner(HeaderBannerViewModel viewModel)
        {
            if (viewModel.Img != null)
            {
                var result = await _adminService.HeaderBanner(viewModel);

                if (result)
                {
                    return RedirectToAction("ListHeaderBanner", "SiteUI");
                }
            }

            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> ListHeaderBanner()
        {
            var result = await _adminService.GetAllBanners();

            return View(result);
        }

        [HttpGet("EditHeaderBanner/{bannerId}")]
        public async Task<IActionResult> EditHeaderBanner(int bannerId)
        {
            var result = await _adminService.EditBannerView(bannerId);

            if (result == null) return NotFound();

            return View(result);
        }

        [HttpPost("EditHeaderBanner/{bannerId}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditHeaderBanner(EditBannerViewModel viewModel, int bannerId)
        {
            if (ModelState.IsValid)
            {
                var result = await _adminService.EditBanner(viewModel, bannerId);

                if (result)
                {
                    return RedirectToAction("ListHeaderBanner", "SiteUI");
                }
            }

            return View(viewModel);
        }

        [HttpGet("EditImgBanner/{bannerId}")]
        public IActionResult EditImgBanner(int bannerId)
        {
            return View();
        }

        [HttpPost("EditImgBanner/{bannerId}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditImgBanner(EditImgBannerViewModel viewModel, int bannerId)
        {
            var result = await _adminService.EditImgBanner(viewModel, bannerId);

            if (result)
            {
                return RedirectToAction("ListHeaderBanner", "SiteUI");
            }

            return View(viewModel);
        }

        #endregion

        #region MiddleBanner

        [HttpGet]
        public IActionResult MiddleBanner()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> MiddleBanner(MiddleBannerViewModel viewModel)
        {
            if (viewModel.Img != null)
            {
                var result = await _adminService.MiddleBanner(viewModel);

                if (result)
                {
                    return LocalRedirect("/");
                }
            }

            return View(viewModel);
        }

        [HttpGet("EditMiddleBanner/{bannerId}")]
        public async Task<IActionResult> EditMiddleBanner(int bannerId)
        {
            var result = await _adminService.EditMidBannerView(bannerId);

            if (result == null) return NotFound();

            return View(result);
        }

        [HttpPost("EditMiddleBanner/{bannerId}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditMiddleBanner(EditMidBannerViewModel viewModel, int bannerId)
        {
            if (ModelState.IsValid)
            {
                var result = await _adminService.EditMidBanner(viewModel, bannerId);

                if (result)
                {
                    return LocalRedirect("/Admin");
                }
            }

            return View(viewModel);
        }

        [HttpGet("EditImgMidBanner/{bannerId}")]
        public IActionResult EditImgMidBanner(int bannerId)
        {
            return View();
        }

        [HttpPost("EditImgMidBanner/{bannerId}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditImgMidBanner(EditImgMidBannerViewModel viewModel, int bannerId)
        {
            var result = await _adminService.EditImgMidBanner(viewModel, bannerId);

            if (result)
            {
                return LocalRedirect("/Admin");
            }

            return View(viewModel);
        }

        #endregion
    }
}