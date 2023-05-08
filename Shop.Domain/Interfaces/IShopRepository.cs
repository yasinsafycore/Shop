using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Shop.Domain.Entities.Account;
using Shop.Domain.Entities.Admin.About;
using Shop.Domain.Entities.Admin.Blog;
using Shop.Domain.Entities.Admin.Products;
using Shop.Domain.Entities.Admin.Setting.Footer;
using Shop.Domain.Entities.Admin.SiteUI;
using Shop.Domain.Entities.Shop.Account;
using Shop.Domain.Entities.Shop.Orders;
using Shop.Domain.Entities.Shop.Site;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Domain.Interfaces
{
    public interface IShopRepository
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
        Task<List<Product>> ListRelatedProduct(string title);
        int ProductsCount();
        int ProductsCategoryCount(int categoryId);
        Task<Product> GetProductById(int productId);
        Task<List<Product>> GetProductsByCategoryId(int categoryId, int productId);

        #endregion

        #region Order

        void AddOrder(Order order);
        Task<Order> CheckUserOrder(int userId);
        Task<Order> GetOrderById(int orderId);
        //Task<Order> GetOrderById(int orderId, int userId);
        Task<OrderDetail> CheckOrderDetail(int orderId, int productId);
        void UpdateOrderDetail(OrderDetail orderDetail);
        Task AddOrderDetail(OrderDetail order);
        Task<double> OrderSum(int orderId);
        void UpdateOrder(Order order);
        Task<Order> GetUserBasket(int orderId, int userId);
        Task<Order> GetUserBasket(int userId);
        Task<OrderDetail> GetOrderDetailById(int id);
        Task<List<Order>> GetAllOrdersByUserId(int userId, int orderId);
        Task<List<Order>> GetAllOrdersByUserId(int userId);
        int GetCountOrdersByUserId(int userId);

        #endregion

        #region Address

        void ADDAddress(Address address);
        void UpdateAddress(Address address);
        Task<Address> GetAddressById(int addressId);
        Task<List<Address>> GetAddressUser(int userId);
        Task<Address> GetAllAddresses(int userId);

        #endregion

        #region Comment

        void AddComment(Comment comment);
        Task<List<Comment>> GetAllComments(int productId);

        #endregion

        #region ContactUs

        void AddContactUs(ContactUs contactUs);

        #endregion

        #region about

        Task<AboutUs> GetAboutUs();
        Task<List<Team>> GetListOfTeam();

        #endregion

        #region Footer

        Task<List<FooterLabel>> GetFooterLabels();
        Task<Social> GetSocials();

        #endregion

        #region General

        Task SaveChange();

        #endregion
    }

    //---------------------------------------------------------------

    public interface IWeBlogRepository
    {
        #region Blog

        Task<List<WeBloge>> GetAllBlogs(int blogId);
        Task<List<WeBloge>> GetLastBlogs();
        Task<WeBloge> GetBlogById(int blogId);
        int BlogsCount();

        #endregion

        #region Comment

        void AddComment(BlogComment comment);
        Task<List<BlogComment>> GetAllBlogsComments(int blogId);

        #endregion

        #region General

        Task SaveChange();

        #endregion
    }
}
