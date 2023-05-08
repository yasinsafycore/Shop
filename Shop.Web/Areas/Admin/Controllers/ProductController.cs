using Microsoft.AspNetCore.Mvc;
using Shop.Application.Extensions;
using Shop.Application.Services.Implementations;
using Shop.Application.Services.Interfaces;
using Shop.Domain.Entities.Shop.Orders;
using Shop.Domain.ViewModels.Admin.Products;
using Shop.Domain.ViewModels.Shop.Order;
using Shop.Domain.ViewModels.Shop.Site;

namespace Shop.Web.Areas.Admin.Controllers
{
    public class ProductController : AdminBaseController
    {
        #region Ctor

        private readonly IProductAdminService _productService;
        public ProductController(IProductAdminService productService)
        {
            _productService = productService;
        }

        #endregion

        #region Categories

        [HttpGet]
        public IActionResult AddCategories()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddCategories(CategoriesViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var result = await _productService.AddCategories(viewModel);

                if (result)
                {
                    return RedirectToAction("ListCategories", "Product");
                }
            }

            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> ListCategories(int categoryId = 1)
        {
            var result = await _productService.GetAllCategories(categoryId);

            int Count = _productService.CategoriesCount();

            ViewBag.PageID = categoryId;
            ViewBag.PageCount = Count / 4;

            return View(result);
        }

        [HttpGet("EditCategories/{categoryId}")]
        public async Task<IActionResult> EditCategories(int categoryId)
        {
            var result = await _productService.EditCategoriesView(categoryId);

            if (result == null) return NotFound();

            return View(result);
        }

        [HttpPost("EditCategories/{categoryId}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditCategories(EditCategoriesViewModel viewModel, int categoryId)
        {
            if (ModelState.IsValid)
            {
                var result = await _productService.EditCategories(viewModel, categoryId);

                if (result)
                {
                    return RedirectToAction("ListCategories", "Product");
                }
            }

            return View(viewModel);
        }

        [HttpGet("DeleteCategories")]
        public async Task<IActionResult> DeleteCategories(int categoryId)
        {
            var result = await _productService.DeleteCategories(categoryId);

            if (result)
            {
                return new JsonResult(new { status = "Success" });
            }

            return new JsonResult(new { status = "Error" });
        }

        [HttpGet("RestoreCategories")]
        public async Task<IActionResult> RestoreCategories(int categoryId)
        {
            var result = await _productService.RestoreCategories(categoryId);

            if (result)
            {
                return new JsonResult(new { status = "Success" });
            }

            return new JsonResult(new { status = "Error" });
        }

        #endregion

        #region Product

        [HttpGet]
        public async Task<IActionResult> AddProducts()
        {
            ViewData["Categorie"] = await _productService.GetAllCategory();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddProducts(AddProductsViewModel viewModel)
        {
            if (viewModel.Img != null)
            {
                var result = await _productService.AddProducts(viewModel);

                if (result)
                {
                    return RedirectToAction("ListProducts", "Product");
                }
            }

            ViewData["Categorie"] = await _productService.GetAllCategory();
            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> ListProducts(int productId = 1)
        {
            var result = await _productService.GetAllProducts(productId);

            int Count = _productService.ProductsCount();

            ViewBag.PageID = productId;
            ViewBag.PageCount = Count / 7;

            return View(result);
        }

        [HttpGet("EditProducts/{productId}")]
        public async Task<IActionResult> EditProducts(int productId)
        {
            var result = await _productService.EditProductView(productId);

            if (result == null) return NotFound();

            ViewData["Categorie"] = await _productService.GetAllCategory();
            return View(result);
        }

        [HttpPost("EditProducts/{productId}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditProducts(EditProductsViewModel viewModel, int productId)
        {
            if (ModelState.IsValid)
            {
                var result = await _productService.EditProduct(viewModel, productId);

                if (result)
                {
                    return RedirectToAction("ListProducts", "Product");
                }
            }

            ViewData["Categorie"] = await _productService.GetAllCategory();
            return View(viewModel);
        }

        [HttpGet("EditImgProduct/{productId}")]
        public IActionResult EditImgProduct(int productId)
        {
            return View();
        }

        [HttpPost("EditImgProduct/{productId}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditImgProduct(EditImgProductsViewModel viewModel, int productId)
        {
            var result = await _productService.EditImgProduct(viewModel, productId);

            if (result)
            {
                return RedirectToAction("ListProducts", "Product");
            }

            return View(viewModel);
        }

        [HttpGet("DeleteProduct")]
        public async Task<IActionResult> DeleteProduct(int productId)
        {
            var result = await _productService.DeleteProduct(productId);

            if (result)
            {
                return new JsonResult(new { status = "Success" });
            }

            return new JsonResult(new { status = "Error" });
        }

        [HttpGet("RestoreProduct")]
        public async Task<IActionResult> RestoreProducts(int productId)
        {
            var result = await _productService.RestoreProduct(productId);

            if (result)
            {
                return new JsonResult(new { status = "Success" });
            }

            return new JsonResult(new { status = "Error" });

        }

        #endregion

        #region Comment

        [HttpGet]
        public async Task<IActionResult> ListComments(int commentId = 1)
        {
            var result = await _productService.GetALLComments(commentId);

            int Count = _productService.CommentsCount();

            ViewBag.PageID = commentId;
            ViewBag.PageCount = Count / 7;

            return View(result);
        }

        [HttpGet("CommentDetail/{commentId}")]
        public async Task<IActionResult> CommentDetail(int commentId)
        {
            var result = await _productService.GetCommentById(commentId);

            if (result == null)
            {
                return NotFound();
            }

            return View(result);
        }

        [HttpGet("DeleteComment")]
        public async Task<IActionResult> DeleteComment(int commentId)
        {
            var result = await _productService.DeleteComment(commentId);

            if (result)
            {
                return new JsonResult(new { status = "Success" });
            }

            return new JsonResult(new { status = "Error" });
        }

        [HttpGet("RestoreComment")]
        public async Task<IActionResult> RestoreComment(int commentId)
        {
            var result = await _productService.RestoreComment(commentId);

            if (result)
            {
                return new JsonResult(new { status = "Success" });
            }

            return new JsonResult(new { status = "Error" });
        }

        #endregion

        #region Order

        [HttpGet]
        public async Task<IActionResult> ListPostedOrders(int orderId = 1)
        {
            var result = await _productService.GetAllPostedOrders(orderId);

            int Count = _productService.PostedOrdersCount();

            ViewBag.PageID = orderId;
            ViewBag.PageCount = Count / 7;

            return View(result);
        }

        [HttpGet]
        public async Task<IActionResult> ListPendingOrders(int orderId = 1)
        {
            var result = await _productService.GetAllPendingOrders(orderId);

            int Count = _productService.PendingOrdersCount();

            ViewBag.PageID = orderId;
            ViewBag.PageCount = Count / 7;

            return View(result);
        }

        [HttpGet("OrderDetail/{orderId}")]
        public async Task<IActionResult> OrderDetail(int orderId)
        {
            var data = await _productService.GetOrderDetail(orderId);

            if (data == null)
            {
                return NotFound();
            }

            return View(data);
        }

        [HttpGet("ShowAddress/{userId}")]
        public async Task<IActionResult> ShowAddress(int userId)
        {
            var result = await _productService.GetAllAddresses(userId);

            return View(result);
        }

        [HttpGet("PostedOrder/{orderId}")]
        public IActionResult PostedOrder(int orderId)
        {
            return View();
        }

        [HttpPost("PostedOrder/{orderId}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PostedOrder(PostedViewModel viewModel, int orderId)
        {
            if (ModelState.IsValid)
            {
                var result = await _productService.PostedOrder(viewModel, orderId);

                if (result)
                {
                    return RedirectToAction("ListPostedOrders", "Product");
                }
            }

            return View(viewModel);
        }

        [HttpGet("PendingOrder/{orderId}")]
        public IActionResult PendingOrder(int orderId)
        {
            return View();
        }

        [HttpPost("PendingOrder/{orderId}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PendingOrder(PostedViewModel viewModel, int orderId)
        {
            if (ModelState.IsValid)
            {
                var result = await _productService.PendingOrder(viewModel, orderId);

                if (result)
                {
                    return RedirectToAction("ListPendingOrders", "Product");
                }
            }

            return View(viewModel);
        }

        #endregion
    }
}
