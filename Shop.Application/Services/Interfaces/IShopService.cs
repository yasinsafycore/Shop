using Microsoft.EntityFrameworkCore;
using Shop.Domain.Entities.Admin.About;
using Shop.Domain.Entities.Admin.Blog;
using Shop.Domain.Entities.Admin.Products;
using Shop.Domain.Entities.Admin.Setting.Footer;
using Shop.Domain.Entities.Admin.SiteUI;
using Shop.Domain.Entities.Shop.Account;
using Shop.Domain.Entities.Shop.Orders;
using Shop.Domain.Entities.Shop.Site;
using Shop.Domain.ViewModels.Admin.Products;
using Shop.Domain.ViewModels.Shop.Account;
using Shop.Domain.ViewModels.Shop.Order;
using Shop.Domain.ViewModels.Shop.Site;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Services.Interfaces
{
    public interface IShopService
    {
        #region Site

        Task<List<HeaderSlider>> GetAllSliders();
        Task<List<HeaderBanner>> GetAllBanners();
        Task<MiddleBanner> GetAllMidBanners();
        Task<List<Product>> GetAllProducts();
        Task<List<Product>> GetSliderProducts();
        Task<List<Product>> GetLastProducts();

        #endregion

        #region Product

        Task<List<Product>> GetListProduct(int productId);
        int ProductsCount();
        int ProductsCategoryCount(int categoryId);
        Task<Product> GetProductById(int productId);
        Task<List<Product>> ListRelatedProduct(string title);
        Task<List<Product>> GetProductsByCategoryId(int categoryId, int productId);

        #endregion

        #region Order

        Task<int> AddOrder(int userId, int productId);
        Task UpdatePriceOrder(int orderId);
        Task<Order> GetUserBasket(int orderId, int userId);
        Task<Order> GetUserBasket(int userId);
        Task<bool> RemoveOrderDetailFromOrder(int orderDetailId);
        Task<bool> OrderIsFinalyView(int orderId);
        Task<OrderResult> OrderIsFinaly(OrderViewModel viewModel, int orderId);
        Task<Order> GetOrderById(int orderId);
        Task<List<Order>> GetAllOrdersByUserId(int userId, int orderId);
        Task<List<Order>> GetAllOrdersByUserId(int userId);
        int GetCountOrdersByUserId(int userId);

        #endregion

        #region Address

        Task<bool> AddUserAddress(AddressViewModel viewModel, int userId);
        Task<EditAddressViewModel> EditAddressView(int userId, int addressId);
        Task<bool> EditAddress(EditAddressViewModel viewModel, int userId, int addressId);
        Task<List<Address>> GetAddressUser(int userId);
        Task<Address> GetAllAddresses(int userId);

        #endregion

        #region ContactUs

        Task<bool> AddContact(ContactUsViewModel viewModel);

        #endregion

        #region About

        Task<AboutUs> GetAboutUs();
        Task<List<Team>> GetListOfTeam();

        #endregion

        #region Comment

        Task<bool> CommentShop(CommentViewModel viewModel);
        Task<List<Comment>> GetAllComments(int productId);

        #endregion

        #region Footer

        Task<List<FooterLabel>> GetFooterLabels();
        Task<Social> GetSocials();

        #endregion
    }

    //---------------------------------------------------------------

    public interface IWeBlogService
    {
        #region Blog

        Task<List<WeBloge>> GetAllBlogs(int blogId);
        Task<List<WeBloge>> GetLastBlogs();
        Task<WeBloge> GetBlogById(int blogId);
        int BlogsCount();

        #endregion

        #region Comment

        Task<bool> CommentBlog(CommentBlogViewModel viewModel);
        Task<List<BlogComment>> GetAllBlogsComments(int blogId);

        #endregion
    }
}
