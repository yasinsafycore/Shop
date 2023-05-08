using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shop.Application.Services.Implementations;
using Shop.Application.Services.Interfaces;
using Shop.Domain.ViewModels.Shop.Site;

namespace Shop.Web.Controllers
{
    public class WeBlogController : BaseController
    {
        #region Ctor

        private readonly IWeBlogService _weBlogService;
        public WeBlogController(IWeBlogService weBlogService)
        {
            _weBlogService = weBlogService;
        }

        #endregion

        #region Blog

        [HttpGet]
        public async Task<IActionResult> BlogList(int blogId = 1)
        {
            var result = await _weBlogService.GetAllBlogs(blogId);

            int Count = _weBlogService.BlogsCount();

            ViewBag.PageID = blogId;
            ViewBag.PageCount = Count / 7;

            return View(result);
        }

        [HttpGet("BlogDetail/{blogId}")]
        public async Task<IActionResult> BlogDetail(int blogId)
        {
            var result = await _weBlogService.GetBlogById(blogId);

            if (result == null)
            {
                return NotFound();
            }

            return View(result);
        }

        #endregion

        #region Comment

        [HttpPost, ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> CommentBlog(CommentBlogViewModel viewModel, int blogId)
        {
            if (ModelState.IsValid)
            {
                var result = await _weBlogService.CommentBlog(viewModel);

                if (result)
                {
                    TempData[SuccessMessage] = "The operation was successful";
                    return RedirectToAction("BlogDetail", "WeBlog", new { blogId = blogId });
                }
            }

            return View(viewModel);
        }

        #endregion
    }
}
