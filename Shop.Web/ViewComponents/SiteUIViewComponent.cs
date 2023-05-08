using Microsoft.AspNetCore.Mvc;
using Shop.Application.Extensions;
using Shop.Application.Services.Interfaces;

namespace Shop.Web.ViewComponents
{
    public class HeaderSliderViewComponent : ViewComponent
    {
        #region Ctor

        private readonly IShopService _shopService;
        public HeaderSliderViewComponent(IShopService shopService)
        {
            _shopService = shopService;
        }

        #endregion

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var result = await _shopService.GetAllSliders();

            return View("HeaderSlider", result);
        }
    }

    //---------------------------------------------------------------

    public class HeaderBannerViewComponent : ViewComponent
    {
        #region Ctor

        private readonly IShopService _shopService;
        public HeaderBannerViewComponent(IShopService shopService)
        {
            _shopService = shopService;
        }

        #endregion

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var result = await _shopService.GetAllBanners();

            return View("HeaderBanner", result);
        }
    }

    //---------------------------------------------------------------

    public class MidBannerViewComponent : ViewComponent
    {
        #region Ctor

        private readonly IShopService _shopService;
        public MidBannerViewComponent(IShopService shopService)
        {
            _shopService = shopService;
        }

        #endregion

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var result = await _shopService.GetAllMidBanners();

            return View("MidBanner", result);
        }
    }

    //---------------------------------------------------------------

    public class SiteHeaderViewComponent : ViewComponent
    {
        #region Ctor

        private readonly IShopService _shopService;
        public SiteHeaderViewComponent(IShopService shopService)
        {
            _shopService = shopService;
        }

        #endregion

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var result = await _shopService.GetAllOrdersByUserId(HttpContext.User.GetUserId());

            return View("SiteHeader", result);
        }
    }

}
