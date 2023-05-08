using Microsoft.AspNetCore.Mvc;
using Shop.Application.Services.Interfaces;
using Shop.Domain.ViewModels.Admin.Blog;
using Shop.Domain.ViewModels.Shop.Site;

namespace Shop.Web.Areas.Admin.Controllers
{
    public class BlogController : AdminBaseController
    {
        #region Ctor

        private readonly IBlogService _blogService;
        public BlogController(IBlogService blogService)
        {
            _blogService = blogService;
        }

        #endregion

        #region WeBloge

        [HttpGet]
        public IActionResult AddBlog()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddBlog(BlogeViewModel viewModel)
        {
            if (viewModel.Img != null)
            {
                var result = await _blogService.AddWeBloge(viewModel);

                if (result)
                {
                    return RedirectToAction("ListBlogs", "Blog");
                }
            }

            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> ListBlogs(int blogId = 1)
        {
            var result = await _blogService.GetAllBlogs(blogId);

            int Count = _blogService.BlogsCount();

            ViewBag.PageID = blogId;
            ViewBag.PageCount = Count / 7;

            return View(result);
        }

        [HttpGet("EditBlog/{blogId}")]
        public async Task<IActionResult> EditBlog(int blogId)
        {
            var result = await _blogService.EditBlogView(blogId);

            if (result == null) return NotFound();

            return View(result);
        }

        [HttpPost("EditBlog/{blogId}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditBlog(EditBlogeViewModel viewModel, int blogId)
        {
            if (ModelState.IsValid)
            {
                var result = await _blogService.EditBlog(viewModel, blogId);

                if (result)
                {
                    return RedirectToAction("ListBlogs", "Blog");
                }
            }

            return View(viewModel);
        }

        [HttpGet("EditImgBlog/{blogId}")]
        public IActionResult EditImgBlog(int blogId)
        {
            return View();
        }

        [HttpPost("EditImgBlog/{blogId}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditImgBlog(EditImgBlogeViewModel viewModel, int blogId)
        {
            var result = await _blogService.EditImgBlog(viewModel, blogId);

            if (result)
            {
                return RedirectToAction("ListBlogs", "Blog");
            }

            return View(viewModel);
        }

        [HttpGet("DeleteBlog")]
        public async Task<IActionResult> DeleteBlog(int blogId)
        {
            var result = await _blogService.DeleteBlog(blogId);

            if (result)
            {
                return new JsonResult(new { status = "Success" });
            }

            return new JsonResult(new { status = "Error" });
        }

        [HttpGet("RestoreBlog")]
        public async Task<IActionResult> RestoreBlog(int blogId)
        {
            var result = await _blogService.RestoreBlog(blogId);

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
            var result = await _blogService.GetALLComments(commentId);

            int Count = _blogService.CommentsCount();

            ViewBag.PageID = commentId;
            ViewBag.PageCount = Count / 7;

            return View(result);
        }

        [HttpGet("CommentBlogDetail/{commentId}")]
        public async Task<IActionResult> CommentDetail(int commentId)
        {
            var result = await _blogService.GetCommentById(commentId);

            if (result == null)
            {
                return NotFound();
            }

            return View(result);
        }

        [HttpGet("DeleteBlogComment")]
        public async Task<IActionResult> DeleteComment(int commentId)
        {
            var result = await _blogService.DeleteComment(commentId);

            if (result)
            {
                return new JsonResult(new { status = "Success" });
            }

            return new JsonResult(new { status = "Error" });
        }

        [HttpGet("RestoreBlogComment")]
        public async Task<IActionResult> RestoreComment(int commentId)
        {
            var result = await _blogService.RestoreComment(commentId);

            if (result)
            {
                return new JsonResult(new { status = "Success" });
            }

            return new JsonResult(new { status = "Error" });
        }

        #endregion
    }
}
