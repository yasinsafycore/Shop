using Microsoft.EntityFrameworkCore;
using Shop.Domain.Entities.Account;
using Shop.Domain.Entities.Admin.About;
using Shop.Domain.Entities.Admin.Blog;
using Shop.Domain.Entities.Admin.Dashboard;
using Shop.Domain.Entities.Admin.Products;
using Shop.Domain.Entities.Admin.Setting.Footer;
using Shop.Domain.Entities.Admin.Setting.General;
using Shop.Domain.Entities.Admin.SiteUI;
using Shop.Domain.Entities.Shop.Account;
using Shop.Domain.Entities.Shop.Orders;
using Shop.Domain.Entities.Shop.Site;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Domain.Interfaces
{
    public interface IAdminRepository
    {
        #region HeaderSlider

        void AddHeaderSlider(HeaderSlider headerSlider);
        Task<List<HeaderSlider>> GetAllSliders(int sliderId);
        int SlidersCount();
        Task<HeaderSlider> GetSliderById(int sliderId);
        void UpdateSlider(HeaderSlider headerSlider);

        #endregion

        #region HeaderBanner

        void AddHeaderBanner(HeaderBanner headerBanner);
        Task<List<HeaderBanner>> GetAllBanners();
        Task<HeaderBanner> GetBannerById(int bannerId);
        void UpdateBanner(HeaderBanner headerBanner);

        #endregion

        #region MiddleBanner

        void AddMiddleBanner(MiddleBanner middleBanner);
        Task<MiddleBanner> GetMidBannerById(int bannerId);
        void UpdateMidBanner(MiddleBanner middleBanner);
        Task<MiddleBanner> GetAllMidBanners();

        #endregion

        #region Profile

        void AddProfile(AdminProfile profile);
        void UpdateProfile(AdminProfile profile);
        Task<AdminProfile> GetProfileById(int profileId);
        Task<AdminProfile> GetAdminProfile();

        #endregion

        #region Contact

        Task<List<ContactUs>> GetAllContact(int contactId);
        int ContactsCount();
        Task<ContactUs> GetContactUsById(int contactId);
        void RemoveContactUs(ContactUs contactUs);
        Task<ContactUs> GetLastContactUs();

        #endregion

        #region About

        void AddAboutUs(AboutUs aboutUs);
        void UpdateAboutUs(AboutUs aboutUs);
        Task<AboutUs> GetAboutUsById(int aboutId);
        Task<AboutUs> GetAboutUs();

        void AddTeam(Team team);
        void UpdateTeam(Team team);
        Task<Team> GetTeamById(int teamId);
        Task<List<Team>> GetTeamGetList(int teamId);
        int TeamCount();
        void RemoveTeam(Team team);

        #endregion

        #region General


        Task SaveChange();

        #endregion
    }

    //---------------------------------------------------------------

    public interface IProductAdminRepository
    {
        #region Categories

        void AddCategories(Categories categories);
        Task<List<Categories>> GetAllCategories(int categoryId);
        int CategoriesCount();
        Task<Categories> GetCategoriesById(int categoryId);
        void UpdateCategories(Categories categories);

        #endregion

        #region Product

        void AddProducts(Product product);
        Task<List<Product>> GetAllProducts(int productId);
        Task<List<Product>> GetProductsForProfile();
        int ProductsCount();
        Task<List<Categories>> GetAllCategories();
        Task<Product> GetProductsById(int productId);
        void UpdateProducts(Product product);

        #endregion

        #region Order

        Task<List<Order>> GetAllPostedOrders(int orderId);
        Task<List<Order>> GetAllPendingOrders(int orderId);
        int PostedOrdersCount();
        int PendingOrdersCount();
        Task<Order> GetOrderDetail(int orderId);
        Task<Order> GetOrderById(int orderId);
        Task<Address> GetAllAddresses(int userId);
        Task<List<Order>> GetOrdersForProfile();
        void UpdateOrders(Order order);

        #endregion

        #region Comment

        Task<List<Comment>> GetALLComments(int commentId);
        int CommentsCount();
        Task<Comment> GetCommentById(int commentId);
        void UpdateComment(Comment comment);

        #endregion

        #region General

        Task SaveChange();

        #endregion
    }

    //---------------------------------------------------------------

    public interface IBlogRepository
    {
        #region WeBloge

        void AddBlog(WeBloge weBloge);
        Task<List<WeBloge>> GetAllBlogs(int blogId);
        int BlogsCount();
        Task<WeBloge> GetBlogById(int blogId);
        void UpdateBlog(WeBloge weBloge);

        #endregion

        #region Comment

        Task<List<BlogComment>> GetALLComments(int commentId);
        int CommentsCount();
        Task<BlogComment> GetCommentById(int commentId);
        void UpdateComment(BlogComment comment);

        #endregion

        #region General

        Task SaveChange();

        #endregion
    }

    //---------------------------------------------------------------

    public interface ISettingRepository
    {
        #region Social Networks

        void AddSocial(Social social);
        void UpdateSocial(Social social);
        Task<Social> GetSocialById(int socialId);
        Task<Social> GetAdminSocial();

        #endregion

        #region Footer Label

        void AddLabel(FooterLabel label);
        void UpdateLabel(FooterLabel label);
        Task<FooterLabel> GetLabelById(int labelId);
        Task<List<FooterLabel>> GetListOfLabel();

        #endregion

        #region Footer Link

        void AddLink(FooterLink link);
        void UpdateLink(FooterLink link);
        Task<FooterLink> GetLinkById(int linkId);
        Task<List<FooterLink>> GetListOfLink(int linkId);
        Task<List<FooterLabel>> GetListLabel();
        int FooterLinkCount();
        void RemoveFooterLink(FooterLink link);

        #endregion

        #region User

        Task<List<User>> GetUsers(int userId);
        Task<User> GetUserById(int userId);
        int GetUserCount();
        Task<List<User>> GetAllUser();
        void UpdateUser(User user);

        #endregion

        #region Admin Logo

        void AddAdminLogo(AdminLogo logo);

        void UpdateAdminLogo(AdminLogo logo);

        Task<AdminLogo> GetAdminLogoById(int id);

        Task<AdminLogo> GetAdminLogo();

        #endregion

        #region Site Logo

        void AddSiteLogo(SiteLogo logo);

        void UpdateSiteLogo(SiteLogo logo);

        Task<SiteLogo> GetSiteLogoById(int id);

        Task<SiteLogo> GetSiteLogo();

        #endregion

        #region Copy Right

        void AddCopyRight(CopyRight copyRight);

        void UpdateCopyRight(CopyRight copyRight);

        Task<CopyRight> GetCopyRightById(int id);

        Task<CopyRight> GetCopyRight();

        #endregion

        #region General

        Task SaveChange();

        #endregion
    }

}
