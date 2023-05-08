using Microsoft.AspNetCore.Mvc;
using Shop.Application.Services.Implementations;
using Shop.Application.Services.Interfaces;

namespace Shop.Web.ViewComponents
{
    public class FooterViewComponent : ViewComponent
    {
        #region Ctor

        private readonly IShopService _shopService;
        public FooterViewComponent(IShopService shopService)
        {
            _shopService = shopService;
        }

        #endregion

        public async Task<IViewComponentResult> InvokeAsync()
        {
            ViewData["Social"] = await _shopService.GetSocials();

            var result = await _shopService.GetFooterLabels();

            return View("Footer", result);
        }
    }

    //---------------------------------------------------------------

    public class FooterSiteLogoViewComponent : ViewComponent
    {
        #region Ctor

        private readonly ISettingService _settingService;

        public FooterSiteLogoViewComponent(ISettingService settingService)
        {
            _settingService = settingService;
        }

        #endregion

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var result = await _settingService.GetSiteLogo();

            return View("FooterSiteLogo", result);
        }
    }

    //---------------------------------------------------------------

    public class SiteCopyRightViewComponent : ViewComponent
    {
        #region Ctor

        private readonly ISettingService _settingService;

        public SiteCopyRightViewComponent(ISettingService settingService)
        {
            _settingService = settingService;
        }

        #endregion

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var result = await _settingService.GetCopyRight();

            return View("SiteCopyRight", result);
        }
    }
}
