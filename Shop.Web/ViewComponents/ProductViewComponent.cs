using Microsoft.AspNetCore.Mvc;
using Shop.Application.Extensions;
using Shop.Application.Services.Interfaces;
using Shop.Domain.Interfaces;

namespace Shop.Web.ViewComponents
{
    public class AllProductViewComponent : ViewComponent
    {
        #region Ctor

        private readonly IShopService _shopService;
        public AllProductViewComponent(IShopService shopService)
        {
            _shopService = shopService;
        }

        #endregion

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var result = await _shopService.GetAllProducts();

            return View("AllProduct", result);
        }
    }

    //---------------------------------------------------------------

    public class LastProductViewComponent : ViewComponent
    {
        #region Ctor

        private readonly IShopService _shopService;
        public LastProductViewComponent(IShopService shopService)
        {
            _shopService = shopService;
        }

        #endregion

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var result = await _shopService.GetSliderProducts();

            return View("LastProduct", result);
        }
    }

    //---------------------------------------------------------------

    public class RelatedProductViewComponent : ViewComponent
    {
        #region Ctor

        private readonly IShopService _shopService;
        public RelatedProductViewComponent(IShopService shopService)
        {
            _shopService = shopService;
        }

        #endregion

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var result = await _shopService.GetLastProducts();

            return View("RelatedProduct", result);
        }
    }

    //---------------------------------------------------------------

    public class ShowCommentViewComponent : ViewComponent
    {
        #region Ctor

        private readonly IShopService _shopService;
        public ShowCommentViewComponent(IShopService shopService)
        {
            _shopService = shopService;
        }

        #endregion

        public async Task<IViewComponentResult> InvokeAsync(int productId)
        {
            var result = await _shopService.GetAllComments(productId);

            return View("ShowComment", result);
        }
    }

}
