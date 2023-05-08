using Microsoft.AspNetCore.Mvc;
using Shop.Application.Extensions;
using Shop.Application.Services.Interfaces;

namespace Shop.Web.ViewComponents
{
    public class AddressViewComponent : ViewComponent
    {
        #region Ctor

        private readonly IShopService _shopService;
        public AddressViewComponent(IShopService shopService)
        {
            _shopService = shopService;
        }

        #endregion

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var result = await _shopService.GetAllAddresses(HttpContext.User.GetUserId());

            return View("Address", result);
        }

    }

    //---------------------------------------------------------------

    public class HeaderSiteLogoViewComponent : ViewComponent
    {
        #region Ctor

        private readonly ISettingService _settingService;

        public HeaderSiteLogoViewComponent(ISettingService settingService)
        {
            _settingService = settingService;
        }

        #endregion

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var result = await _settingService.GetSiteLogo();

            return View("HeaderSiteLogo", result);
        }
    }

    //---------------------------------------------------------------

    public class CategoriesViewComponent : ViewComponent
    {
        #region Ctor

        private readonly IProductAdminService _shopService;
        public CategoriesViewComponent(IProductAdminService shopService)
        {
            _shopService = shopService;
        }

        #endregion

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var result = await _shopService.GetAllCategory();

            return View("Categories", result);
        }

    }
}
