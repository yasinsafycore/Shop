using Microsoft.AspNetCore.Mvc;
using Shop.Application.Services.Interfaces;

namespace Shop.Web.Areas.Admin.ViewComponents
{
    public class MiddleBannerViewComponent : ViewComponent
    {
        #region Ctor

        private readonly IAdminService _adminService;

        public MiddleBannerViewComponent(IAdminService adminService)
        {
            _adminService = adminService;
        }

        #endregion

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var result = await _adminService.GetAllMidBanners();

            return View("MiddleBanner", result);
        }
    }

    //---------------------------------------------------------------

    public class AdminSideBarViewComponent : ViewComponent
    {
        #region Ctor

        private readonly IAdminService _adminService;

        public AdminSideBarViewComponent(IAdminService adminService)
        {
            _adminService = adminService;
        }

        #endregion

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var result = await _adminService.GetAdminProfile();

            return View("AdminSideBar", result);
        }
    }

    //---------------------------------------------------------------

    public class AdminHeaderBarViewComponent : ViewComponent
    {
        #region Ctor

        private readonly IAdminService _adminService;

        public AdminHeaderBarViewComponent(IAdminService adminService)
        {
            _adminService = adminService;
        }

        #endregion

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var result = await _adminService.GetAdminProfile();

            return View("AdminHeaderBar", result);
        }
    }

    //---------------------------------------------------------------

    public class LastOrdersViewComponent : ViewComponent
    {
        #region Ctor

        private readonly IProductAdminService _productService;

        public LastOrdersViewComponent(IProductAdminService productService)
        {
            _productService = productService;
        }

        #endregion

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var result = await _productService.GetOrdersForProfile();

            return View("LastOrders", result);
        }

    }

    //---------------------------------------------------------------

    public class LastProductsViewComponent : ViewComponent
    {
        #region Ctor

        private readonly IProductAdminService _productService;

        public LastProductsViewComponent(IProductAdminService productService)
        {
            _productService = productService;
        }

        #endregion

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var result = await _productService.GetProductsForProfile();

            return View("LastProducts", result);
        }

    }

    //---------------------------------------------------------------

    public class AboutUsViewComponent : ViewComponent
    {
        #region Ctor

        private readonly IAdminService _adminService;

        public AboutUsViewComponent(IAdminService adminService)
        {
            _adminService = adminService;
        }

        #endregion

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var result = await _adminService.GetAboutUs();

            return View("AboutUs", result);
        }
    }

    //---------------------------------------------------------------

    public class SocialViewComponent : ViewComponent
    {
        #region Ctor

        private readonly ISettingService _settingService;

        public SocialViewComponent(ISettingService settingService)
        {
            _settingService = settingService;
        }

        #endregion

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var result = await _settingService.GetAdminSocial();

            return View("Social", result);
        }
    }

    //---------------------------------------------------------------

    public class AdminLogoViewComponent : ViewComponent
    {
        #region Ctor

        private readonly ISettingService _settingService;

        public AdminLogoViewComponent(ISettingService settingService)
        {
            _settingService = settingService;
        }

        #endregion

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var result = await _settingService.GetAdminLogo();

            return View("AdminLogo", result);
        }
    }

    //---------------------------------------------------------------

    public class SiteLogoViewComponent : ViewComponent
    {
        #region Ctor

        private readonly ISettingService _settingService;

        public SiteLogoViewComponent(ISettingService settingService)
        {
            _settingService = settingService;
        }

        #endregion

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var result = await _settingService.GetSiteLogo();

            return View("SiteLogo", result);
        }
    }

    //---------------------------------------------------------------

    public class CopyRightViewComponent : ViewComponent
    {
        #region Ctor

        private readonly ISettingService _settingService;

        public CopyRightViewComponent(ISettingService settingService)
        {
            _settingService = settingService;
        }

        #endregion

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var result = await _settingService.GetCopyRight();

            return View("CopyRight", result);
        }
    }

    //---------------------------------------------------------------

    public class ShowCopyRightViewComponent : ViewComponent
    {
        #region Ctor

        private readonly ISettingService _settingService;

        public ShowCopyRightViewComponent(ISettingService settingService)
        {
            _settingService = settingService;
        }

        #endregion

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var result = await _settingService.GetCopyRight();

            return View("ShowCopyRight", result);
        }
    }

    //---------------------------------------------------------------

    public class LastCallViewComponent : ViewComponent
    {
        #region Ctor

        private readonly IAdminService _adminService;

        public LastCallViewComponent(IAdminService adminService)
        {
            _adminService = adminService;
        }

        #endregion

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var result = await _adminService.GetLastContactUs();

            return View("LastCall", result);
        }
    }

    //---------------------------------------------------------------

    public class UserCountViewComponent : ViewComponent
    {
        #region Ctor

        private readonly ISettingService _settingService;

        public UserCountViewComponent(ISettingService settingService)
        {
            _settingService = settingService;
        }

        #endregion

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var result = await _settingService.GetAllUser();

            return View("UserCount", result);
        }
    }
}
