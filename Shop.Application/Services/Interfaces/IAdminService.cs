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
using Shop.Domain.ViewModels.Admin.about;
using Shop.Domain.ViewModels.Admin.Blog;
using Shop.Domain.ViewModels.Admin.Dashboard;
using Shop.Domain.ViewModels.Admin.Products;
using Shop.Domain.ViewModels.Admin.Setting.Footer;
using Shop.Domain.ViewModels.Admin.Setting.General;
using Shop.Domain.ViewModels.Admin.SiteUI;
using Shop.Domain.ViewModels.Shop.Order;
using Shop.Domain.ViewModels.Shop.Site;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Services.Interfaces
{
    public interface IAdminService
    {
        #region HeaderSlider

        Task<bool> HeaderSlider(HeaderSliderViewModel viewModel);
        Task<List<HeaderSlider>> GetAllSliders(int sliderId);
        int SlidersCount();
        Task<HeaderSlider> GetSliderById(int sliderId);
        Task<EditSliderViewModel> EditSliderView(int sliderId);
        Task<bool> EditSlider(EditSliderViewModel viewModel, int sliderId);
        Task<bool> EditImgSlider(EditImgSliderViewModel viewModel, int sliderId);
        Task<bool> DeleteSlider(int sliderId);
        Task<bool> RestoreSlider(int sliderId);

        #endregion

        #region HeaderBanner

        Task<bool> HeaderBanner(HeaderBannerViewModel viewModel);
        Task<List<HeaderBanner>> GetAllBanners();
        Task<HeaderBanner> GetBannerById(int bannerId);
        Task<EditBannerViewModel> EditBannerView(int bannerId);
        Task<bool> EditBanner(EditBannerViewModel viewModel, int bannerId);
        Task<bool> EditImgBanner(EditImgBannerViewModel viewModel, int bannerId);

        #endregion

        #region MiddleBanner

        Task<bool> MiddleBanner(MiddleBannerViewModel viewModel);
        Task<MiddleBanner> GetAllMidBanners();
        Task<MiddleBanner> GetMidBannerById(int bannerId);
        Task<EditMidBannerViewModel> EditMidBannerView(int bannerId);
        Task<bool> EditMidBanner(EditMidBannerViewModel viewModel, int bannerId);
        Task<bool> EditImgMidBanner(EditImgMidBannerViewModel viewModel, int bannerId);

        #endregion

        #region Profile

        Task<bool> AddProfile(AdminProfileViewModel viewModel);
        Task<EditProfileViewModel> EditProfileView(int profileId);
        Task<bool> EditProfile(EditProfileViewModel viewModel, int profileId);
        Task<bool> EditImgProfile(EditImgProfileViewModel viewModel, int profileId);
        Task<AdminProfile> GetAdminProfile();

        #endregion

        #region Contact

        Task<List<ContactUs>> GetAllContact(int contactId);
        int ContactsCount();
        Task<ContactUs> GetContactUsById(int contactId);
        Task<bool> DeleteContact(int contactId);
        Task<ContactUs> GetLastContactUs();

        #endregion

        #region About

        Task<bool> AddAboutUs(AboutUsViewModel viewModel);
        Task<AboutUs> GetAboutUsById(int aboutId);
        Task<AboutUs> GetAboutUs();
        Task<EditAboutUsViewModel> EditAboutUsView(int aboutId);
        Task<bool> EditAboutUs(EditAboutUsViewModel viewModel, int aboutId);
        Task<bool> EditImgAboutUs(EditImgAboutUsViewModel viewModel, int aboutId);
        
        Task<bool> AddTeam(TeamViewModel viewModel);
        Task<List<Team>> GetTeamGetList(int teamId);
        int TeamCount();
        Task<EditTeamViewModel> EditTeamView(int teamId);
        Task<bool> EditTeam(EditTeamViewModel viewModel, int teamId);
        Task<bool> EditImgTeam(EditImgTeamViewModel viewModel, int teamId);
        Task<bool> DeleteTeam(int teamId);

        #endregion
    }

    //---------------------------------------------------------------

    public interface IProductAdminService
    {
        #region Categories

        Task<bool> AddCategories(CategoriesViewModel viewModel);
        Task<List<Categories>> GetAllCategories(int categoryId);
        int CategoriesCount();
        Task<Categories> GetCategoriesById(int categoryId);
        Task<EditCategoriesViewModel> EditCategoriesView(int categoryId);
        Task<bool> EditCategories(EditCategoriesViewModel viewModel, int categoryId);
        Task<bool> DeleteCategories(int categoryId);
        Task<bool> RestoreCategories(int categoryId);

        #endregion

        #region Product

        Task<bool> AddProducts(AddProductsViewModel viewModel);
        Task<List<Product>> GetAllProducts(int productId);
        Task<List<Product>> GetProductsForProfile();
        int ProductsCount();
        Task<Product> GetProductById(int productId);
        Task<EditProductsViewModel> EditProductView(int productId);
        Task<bool> EditProduct(EditProductsViewModel viewModel, int productId);
        Task<bool> EditImgProduct(EditImgProductsViewModel viewModel, int productId);
        Task<bool> DeleteProduct(int productId);
        Task<bool> RestoreProduct(int productId);
        Task<List<Categories>> GetAllCategory();

        #endregion

        #region Order

        Task<List<Order>> GetAllPostedOrders(int orderId);
        Task<List<Order>> GetAllPendingOrders(int orderId);
        int PostedOrdersCount();
        int PendingOrdersCount();
        Task<Order> GetOrderDetail(int orderId);
        Task<Address> GetAllAddresses(int userId);
        Task<List<Order>> GetOrdersForProfile();
        Task<bool> PostedOrder(PostedViewModel viewModel, int orderId);
        Task<bool> PendingOrder(PostedViewModel viewModel, int orderId);

        #endregion

        #region Comment

        Task<List<Comment>> GetALLComments(int commentId);
        Task<Comment> GetCommentById(int commentId);
        int CommentsCount();
        Task<bool> DeleteComment(int commentId);
        Task<bool> RestoreComment(int commentId);

        #endregion
    }

    //---------------------------------------------------------------

    public interface IBlogService
    {
        #region WeBloge

        Task<bool> AddWeBloge(BlogeViewModel viewModel);
        Task<List<WeBloge>> GetAllBlogs(int blogId);
        int BlogsCount();
        Task<WeBloge> GetBlogById(int blogId);
        Task<EditBlogeViewModel> EditBlogView(int blogId);
        Task<bool> EditBlog(EditBlogeViewModel viewModel, int blogId);
        Task<bool> EditImgBlog(EditImgBlogeViewModel viewModel, int blogId);
        Task<bool> DeleteBlog(int blogId);
        Task<bool> RestoreBlog(int blogId);

        #endregion

        #region Comment

        Task<List<BlogComment>> GetALLComments(int commentId);
        int CommentsCount();
        Task<BlogComment> GetCommentById(int commentId);
        Task<bool> DeleteComment(int commentId);
        Task<bool> RestoreComment(int commentId);

        #endregion
    }

    //---------------------------------------------------------------

    public interface ISettingService
    {
        #region Social Networks

        Task<bool> AddSocial(SocialViewModel viewModel);
        Task<EditSocialViewModel> EditSocialView(int socialId);
        Task<bool> EditSocial(EditSocialViewModel viewModel, int socialId);
        Task<Social> GetAdminSocial();

        #endregion

        #region Footer Label

        Task<bool> AddLabel(FooterLabelViewModel viewModel);
        Task<FooterLabelViewModel> EditLabelView(int labelId);
        Task<bool> EditLabel(FooterLabelViewModel viewModel, int labelId);
        Task<List<FooterLabel>> GetListOfLabel();

        #endregion

        #region Footer Link

        Task<bool> AddLink(FooterLinkViewModel viewModel);
        Task<FooterLinkViewModel> EditLinkView(int linkId);
        Task<bool> EditLink(FooterLinkViewModel viewModel, int linkId);
        Task<List<FooterLink>> GetListOfLink(int linkId);
        Task<List<FooterLabel>> GetListLabel();
        int FooterLinkCount();
        Task<bool> DeleteLink(int linkId);

        #endregion

        #region User

        Task<List<User>> GetUsers(int userId);
        int GetUserCount();
        Task<List<User>> GetAllUser();
        Task<bool> BanUser(int userId);
        Task<bool> ReBanUser(int userId);

        #endregion

        #region Admin Logo

        Task<bool> AddAdminLogo(AdminLogoViewModel viewModel);
        Task<bool> EditLogo(AdminLogoViewModel viewModel, int logoId);
        Task<AdminLogo> GetAdminLogo();

        #endregion

        #region Site Logo

        Task<bool> AddSiteLogo(SiteLogoViewModel viewModel);
        Task<SiteDescriptionViewModel> EditSiteDescriptionView(int logoId);
        Task<bool> EditSiteDescription(SiteDescriptionViewModel viewModel, int logoId);
        Task<bool> EditSiteImg(SiteImgViewModel viewModel, int logoId);
        Task<SiteLogo> GetSiteLogo();

        #endregion

        #region Copy Right

        Task<bool> AddCopyRight(CopyRightViewModel viewModel);
        Task<CopyRightViewModel> EditCopyRightView(int Id);
        Task<bool> EditCopyRight(CopyRightViewModel viewModel, int Id);
        Task<CopyRight> GetCopyRight();

        #endregion
    }
}
