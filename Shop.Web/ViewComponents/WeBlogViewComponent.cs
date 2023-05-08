using Microsoft.AspNetCore.Mvc;
using Shop.Application.Services.Implementations;
using Shop.Application.Services.Interfaces;

namespace Shop.Web.ViewComponents
{
    public class LastBlogsViewComponent : ViewComponent
    {
        #region Ctor

        private readonly IWeBlogService _weBlogService;
        public LastBlogsViewComponent(IWeBlogService weBlogService)
        {
            _weBlogService = weBlogService;
        }

        #endregion

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var result = await _weBlogService.GetLastBlogs();

            return View("LastBlogs", result);
        }
    }

    //---------------------------------------------------------------

    public class ShowBlogCommentViewComponent : ViewComponent
    {
        #region Ctor

        private readonly IWeBlogService _weBlogService;
        public ShowBlogCommentViewComponent(IWeBlogService weBlogService)
        {
            _weBlogService = weBlogService;
        }

        #endregion

        public async Task<IViewComponentResult> InvokeAsync(int blogId)
        {
            var result = await _weBlogService.GetAllBlogsComments(blogId);

            return View("ShowBlogComment", result);
        }
    }
}
