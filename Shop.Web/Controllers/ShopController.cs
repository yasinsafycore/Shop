using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using NuGet.Versioning;
using Shop.Application.Extensions;
using Shop.Application.Services.Interfaces;
using Shop.Domain.Entities.Shop.Orders;
using Shop.Domain.ViewModels.Admin.Products;
using Shop.Domain.ViewModels.Shop.Account;
using Shop.Domain.ViewModels.Shop.Order;
using Shop.Domain.ViewModels.Shop.Site;

namespace Shop.Web.Controllers
{
    public class ShopController : BaseController
    {
        #region Ctor

        private readonly IShopService _shopService;
        private readonly IProductAdminService _productService;
        public ShopController(IShopService shopService, IProductAdminService productService)
        {
            _shopService = shopService;
            _productService = productService;
        }

        #endregion

        #region Product

        [HttpGet]
        public async Task<IActionResult> ProductList(int productId = 1)
        {
            var result = await _shopService.GetListProduct(productId);

            int Count = _shopService.ProductsCount();

            ViewBag.PageID = productId;
            ViewBag.PageCount = Count / 9;

            return View(result);
        }

        [HttpGet("ProductsByCategory/{categoryId}")]
        public async Task<IActionResult> ProductsByCategory(int categoryId, int productId = 1)
        {
            var result = await _shopService.GetProductsByCategoryId(categoryId, productId);

            int Count = _shopService.ProductsCategoryCount(categoryId);

            ViewBag.PageID = productId;
            ViewBag.PageCount = Count / 9;

            return View(result);
        }

        [HttpGet("ProductDetail/{productId}")]
        public async Task<IActionResult> ProductDetail(int productId)
        {
            var result = await _shopService.GetProductById(productId);

            if (result == null)
            {
                return NotFound();
            }

            return View(result);
        }

        #endregion

        #region buy-product

        [Authorize]
        public async Task<IActionResult> BuyProduct(int productId)
        {
            int orderId = await _shopService.AddOrder(User.GetUserId(), productId);
            return LocalRedirect("/Basket/" + orderId);
        }

        #endregion

        #region Order

        [HttpGet("basket/{orderId}")]
        [Authorize]
        public async Task<IActionResult> UserBasket(int orderId)
        {
            var order = await _shopService.GetUserBasket(orderId, User.GetUserId());

            ViewData["Address"] = await _shopService.GetAddressUser(HttpContext.User.GetUserId());

            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        [HttpGet("Checkout/{orderId}")]
        public async Task<IActionResult> Checkout(int orderId)
        {
            var result = await _shopService.OrderIsFinalyView(orderId);

            return View(result);
        }

        [HttpPost("Checkout/{orderId}")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Checkout(OrderViewModel viewModel, int orderId)
        {
            if (ModelState.IsValid)
            {
                var result = await _shopService.OrderIsFinaly(viewModel, orderId);

                switch (result)
                {
                    case OrderResult.Success:
                        TempData[SuccessMessage] = "The operation was successful";
                        return LocalRedirect("/");
                    case OrderResult.IsBan:
                        TempData[WarningMessage] = "Unfortunately, your account has been blocked by the admin and you no longer have access";
                        break;
                    case OrderResult.NotFound:
                        TempData[ErrorMessage] = "The desired user was not found";
                        break;
                }
            }

            return View(viewModel);
        }

        [HttpGet("delete-orderDetail/{orderDetailId}")]
        public async Task<IActionResult> DeleteOrderDetail(int orderDetailId)
        {
            var result = await _shopService.RemoveOrderDetailFromOrder(orderDetailId);
            ViewBag.Order = await _shopService.GetUserBasket(User.GetUserId());

            return View(result);
        }

        [HttpGet]
        public async Task<IActionResult> ListOrders(int orderId = 1)
        {
            var result = await _shopService.GetAllOrdersByUserId(HttpContext.User.GetUserId(), orderId);

            int Count = _shopService.GetCountOrdersByUserId(HttpContext.User.GetUserId());

            ViewBag.PageID = orderId;
            ViewBag.PageCount = Count / 4;

            return View(result);
        }

        [HttpGet("OrderStatusDetail/{orderId}")]
        public async Task<IActionResult> OrderDetail(int orderId)
        {
            var data = await _productService.GetOrderDetail(orderId);

            if (data == null)
            {
                return NotFound();
            }

            return View(data);
        }

        #endregion

        #region Address

        [HttpGet]
        [Authorize]
        public IActionResult Address()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Address(AddressViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var result = await _shopService.AddUserAddress(viewModel, HttpContext.User.GetUserId());

                return LocalRedirect("/");
            }

            return View(viewModel);
        }

        [HttpGet("EditAddress/{addressId}")]
        [Authorize]
        public async Task<IActionResult> EditAddress(int addressId)
        {
            var result = await _shopService.EditAddressView(HttpContext.User.GetUserId(), addressId);

            return View(result);
        }

        [HttpPost("EditAddress/{addressId}")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> EditAddress(EditAddressViewModel viewModel, int addressId)
        {
            if (ModelState.IsValid)
            {
                var result = await _shopService.EditAddress(viewModel, HttpContext.User.GetUserId(), addressId);

                return LocalRedirect("/");
            }

            return View(viewModel);
        }

        #endregion

        #region ContactUs

        [HttpGet]
        public IActionResult ContactUs()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ContactUs(ContactUsViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var result = await _shopService.AddContact(viewModel);

                if (result)
                {
                    TempData["SuccessMessage"] = "The operation was successful";
                    return RedirectToAction("ContactUs", "Shop");
                }
            }

            return View(viewModel);
        }

        #endregion

        #region About

        [HttpGet]
        public async Task<IActionResult> AboutUs()
        {
            var result = await _shopService.GetAboutUs();

            ViewData["Team"] = await _shopService.GetListOfTeam();

            return View(result);
        }

        #endregion

        #region Comment

        [HttpPost, ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> CommentShops(CommentViewModel viewModel, int productId)
        {
            if (ModelState.IsValid)
            {
                var result = await _shopService.CommentShop(viewModel);

                if (result)
                {
                    TempData[SuccessMessage] = "The operation was successful";
                    return RedirectToAction("ProductDetail", "Shop", new { productId = productId });
                }
            }

            return View(viewModel);
        }

        #endregion
    }
}
