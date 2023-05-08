using Microsoft.EntityFrameworkCore;
using Shop.DataLayer.Context;
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
using Shop.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.DataLayer.Repositories
{
    public class AdminRepository : IAdminRepository
    {
        #region Ctor

        private readonly ShopDBContext _context;

        public AdminRepository(ShopDBContext context)
        {
            _context = context;
        }

        #endregion

        #region HeaderSlider

        public void AddHeaderSlider(HeaderSlider headerSlider)
        {
            _context.HeaderSliders.Add(headerSlider);
        }

        public async Task<List<HeaderSlider>> GetAllSliders(int sliderId)
        {
            int skip = (sliderId - 1) * 3;

            return await _context.HeaderSliders.OrderByDescending(p => p.Id).Skip(skip).Take(3).ToListAsync();
        }

        public int SlidersCount()
        {
            return _context.HeaderSliders.Count();
        }

        public async Task<HeaderSlider> GetSliderById(int sliderId)
        {
            return await _context.HeaderSliders.SingleOrDefaultAsync(p => p.Id == sliderId);
        }

        public void UpdateSlider(HeaderSlider headerSlider)
        {
            _context.HeaderSliders.Update(headerSlider);
        }

        #endregion

        #region HeaderBanner

        public void AddHeaderBanner(HeaderBanner headerBanner)
        {
            _context.HeaderBanners.Add(headerBanner);
        }

        public async Task<List<HeaderBanner>> GetAllBanners()
        {
            return await _context.HeaderBanners.OrderByDescending(p => p.Id).Take(2).ToListAsync();
        }

        public async Task<HeaderBanner> GetBannerById(int bannerId)
        {
            return await _context.HeaderBanners.SingleOrDefaultAsync(p => p.Id == bannerId);
        }

        public void UpdateBanner(HeaderBanner headerBanner)
        {
            _context.HeaderBanners.Update(headerBanner);
        }

        #endregion

        #region MiddleBanner

        public void AddMiddleBanner(MiddleBanner middleBanner)
        {
            _context.MiddleBanners.Add(middleBanner);
        }

        public async Task<MiddleBanner> GetMidBannerById(int bannerId)
        {
            return await _context.MiddleBanners.SingleOrDefaultAsync(p => p.Id == bannerId);
        }

        public void UpdateMidBanner(MiddleBanner middleBanner)
        {
            _context.MiddleBanners.Update(middleBanner);
        }

        public async Task<MiddleBanner> GetAllMidBanners()
        {
            return await _context.MiddleBanners.OrderByDescending(p => p.Id).FirstOrDefaultAsync();
        }

        #endregion

        #region Profile

        public void AddProfile(AdminProfile profile)
        {
            _context.AdminProfiles.Add(profile);
        }

        public void UpdateProfile(AdminProfile profile)
        {
            _context.AdminProfiles.Update(profile);
        }

        public async Task<AdminProfile> GetProfileById(int profileId)
        {
            return await _context.AdminProfiles.SingleOrDefaultAsync(p => p.Id == profileId);
        }

        public async Task<AdminProfile> GetAdminProfile()
        {
            return await _context.AdminProfiles.OrderByDescending(p => p.Id).FirstOrDefaultAsync();
        }

        #endregion

        #region Contact

        public async Task<List<ContactUs>> GetAllContact(int contactId)
        {
            int skip = (contactId - 1) * 5;

            return await _context.ContactUs.OrderByDescending(p => p.Id).Skip(skip).Take(5).ToListAsync();
        }

        public int ContactsCount()
        {
            return _context.ContactUs.Count();
        }

        public async Task<ContactUs> GetContactUsById(int contactId)
        {
            return await _context.ContactUs.SingleOrDefaultAsync(p => p.Id == contactId);
        }

        public void RemoveContactUs(ContactUs contactUs)
        {
            _context.ContactUs.Remove(contactUs);
        }

        public async Task<ContactUs> GetLastContactUs()
        {
            return await _context.ContactUs.OrderByDescending(p => p.Id).Where(p => !p.IsDelete).FirstOrDefaultAsync();
        }

        #endregion

        #region About

        public void AddAboutUs(AboutUs aboutUs)
        {
            _context.AboutUs.Add(aboutUs);
        }

        public void UpdateAboutUs(AboutUs aboutUs)
        {
            _context.AboutUs.Update(aboutUs);
        }

        public async Task<AboutUs> GetAboutUsById(int aboutId)
        {
            return await _context.AboutUs.Where(p => !p.IsDelete).OrderByDescending(p => p.Id).SingleOrDefaultAsync(p => p.Id == aboutId);
        }

        public async Task<AboutUs> GetAboutUs()
        {
            return await _context.AboutUs.Where(p => !p.IsDelete).OrderByDescending(p => p.Id).FirstOrDefaultAsync();
        }

        public void AddTeam(Team team)
        {
            _context.Teams.Add(team);
        }

        public void UpdateTeam(Team team)
        {
            _context.Teams.Update(team);
        }

        public async Task<Team> GetTeamById(int teamId)
        {
            return await _context.Teams.Where(p => !p.IsDelete).OrderByDescending(p => p.Id).SingleOrDefaultAsync(p => p.Id == teamId);
        }

        public async Task<List<Team>> GetTeamGetList(int teamId)
        {
            int skip = (teamId - 1) * 4;

            return await _context.Teams.OrderByDescending(p => p.Id).Skip(skip).Take(4).ToListAsync();
        }

        public int TeamCount()
        {
            return _context.Teams.Count();
        }

        public void RemoveTeam(Team team)
        {
            _context.Teams.Remove(team);
        }

        #endregion

        #region General

        public async Task SaveChange()
        {
            await _context.SaveChangesAsync();
        }

        #endregion
    }

    //---------------------------------------------------------------

    public class ProductAdminRepository : IProductAdminRepository
    {
        #region Ctor

        private readonly ShopDBContext _context;

        public ProductAdminRepository(ShopDBContext context)
        {
            _context = context;
        }

        #endregion

        #region Categories

        public void AddCategories(Categories categories)
        {
            _context.Categories.Add(categories);
        }

        public int CategoriesCount()
        {
            return _context.Categories.Count();
        }

        public async Task<List<Categories>> GetAllCategories(int categoryId)
        {
            int skip = (categoryId - 1) * 3;

            return await _context.Categories.OrderByDescending(p => p.Id).Skip(skip).Take(3).ToListAsync();
        }

        public async Task<Categories> GetCategoriesById(int categoryId)
        {
            return await _context.Categories.SingleOrDefaultAsync(p => p.Id == categoryId);
        }

        public void UpdateCategories(Categories categories)
        {
            _context.Categories.Update(categories);
        }

        #endregion

        #region Product

        public void AddProducts(Product product)
        {
            _context.Products.Add(product);
        }

        public async Task<List<Product>> GetAllProducts(int productId)
        {
            int skip = (productId - 1) * 6;

            return await _context.Products.OrderByDescending(p => p.Id).Skip(skip).Take(6).ToListAsync();
        }

        public async Task<List<Product>> GetProductsForProfile()
        {
            return await _context.Products.OrderByDescending(p => p.Id).Take(4).ToListAsync();
        }

        public async Task<List<Categories>> GetAllCategories()
        {
            return await _context.Categories.OrderByDescending(p => p.Id).Where(p => !p.IsDelete).ToListAsync();
        }

        public int ProductsCount()
        {
            return _context.Products.Count();
        }

        public async Task<Product> GetProductsById(int productId)
        {
            return await _context.Products.SingleOrDefaultAsync(p => p.Id == productId);
        }

        public void UpdateProducts(Product product)
        {
            _context.Products.Update(product);
        }

        #endregion

        #region Order

        public async Task<List<Order>> GetAllPostedOrders(int orderId)
        {
            int skip = (orderId - 1) * 6;

            return await _context.Orders.Include(c => c.OrderDetails).ThenInclude(c => c.Product).Include(c => c.User)
                .OrderByDescending(p => p.Id).Where(p => p.IsFinaly == true && p.Posted == true).Skip(skip).Take(6).ToListAsync();
        }

        public async Task<List<Order>> GetAllPendingOrders(int orderId)
        {
            int skip = (orderId - 1) * 6;

            return await _context.Orders.Include(c => c.OrderDetails).ThenInclude(c => c.Product).Include(c => c.User)
                .OrderByDescending(p => p.Id).Where(p => p.IsFinaly == true && p.Posted == false).Skip(skip).Take(6).ToListAsync();
        }

        public int PostedOrdersCount()
        {
            return _context.Orders.Where(p => p.IsFinaly == true && p.Posted == true).Count();
        }

        public int PendingOrdersCount()
        {
            return _context.Orders.Where(p => p.IsFinaly == true && p.Posted == false).Count();
        }

        public async Task<Order> GetOrderDetail(int orderId)
        {
            return await _context.Orders.Include(c => c.OrderDetails).ThenInclude(c => c.Product).Include(c => c.User)
                .Where(c => c.Id == orderId)
                .FirstOrDefaultAsync();
        }

        public async Task<Order> GetOrderById(int orderId)
        {
            return await _context.Orders.SingleOrDefaultAsync(p => p.Id == orderId);
        }

        public async Task<Address> GetAllAddresses(int userId)
        {
            return await _context.Addresses.Include(p => p.User).SingleOrDefaultAsync(p => p.UserId == userId);
        }

        public async Task<List<Order>> GetOrdersForProfile()
        {
            return await _context.Orders.Include(c => c.User)
                .OrderByDescending(p => p.Id).Where(p => p.IsFinaly == true).Take(4).ToListAsync();
        }

        public void UpdateOrders(Order order)
        {
            _context.Orders.Update(order);
        }

        #endregion

        #region Comment

        public async Task<List<Comment>> GetALLComments(int commentId)
        {
            int skip = (commentId - 1) * 6;

            return await _context.Comments.Include(p => p.Product).OrderByDescending(p => p.Id).Skip(skip).Take(6).ToListAsync();
        }

        public int CommentsCount()
        {
            return _context.Comments.Count();
        }

        public async Task<Comment> GetCommentById(int commentId)
        {
            return await _context.Comments.SingleOrDefaultAsync(p => p.Id == commentId);
        }

        public void UpdateComment(Comment comment)
        {
            _context.Comments.Update(comment);
        }

        #endregion

        #region General

        public async Task SaveChange()
        {
            await _context.SaveChangesAsync();
        }

        #endregion
    }

    //---------------------------------------------------------------

    public class BlogRepository : IBlogRepository
    {
        #region Ctor

        private readonly ShopDBContext _context;

        public BlogRepository(ShopDBContext context)
        {
            _context = context;
        }

        #endregion

        #region WeBloge

        public void AddBlog(WeBloge weBloge)
        {
            _context.WeBloges.Add(weBloge);
        }

        public int BlogsCount()
        {
            return _context.WeBloges.Count();
        }

        public async Task<List<WeBloge>> GetAllBlogs(int blogId)
        {
            int skip = (blogId - 1) * 6;

            return await _context.WeBloges.OrderByDescending(p => p.Id).Skip(skip).Take(6).ToListAsync();
        }

        public async Task<WeBloge> GetBlogById(int blogId)
        {
            return await _context.WeBloges.SingleOrDefaultAsync(p => p.Id == blogId);
        }

        public void UpdateBlog(WeBloge weBloge)
        {
            _context.WeBloges.Update(weBloge);
        }

        #endregion

        #region Comment

        public async Task<List<BlogComment>> GetALLComments(int commentId)
        {
            int skip = (commentId - 1) * 6;

            return await _context.BlogComments.Include(p => p.WeBloge).OrderByDescending(p => p.Id).Skip(skip).Take(6).ToListAsync();
        }

        public int CommentsCount()
        {
            return _context.BlogComments.Count();
        }

        public async Task<BlogComment> GetCommentById(int commentId)
        {
            return await _context.BlogComments.SingleOrDefaultAsync(p => p.Id == commentId);
        }

        public void UpdateComment(BlogComment comment)
        {
            _context.BlogComments.Update(comment);
        }

        #endregion

        #region General

        public async Task SaveChange()
        {
            await _context.SaveChangesAsync();
        }

        #endregion
    }

    //---------------------------------------------------------------

    public class SettingRepository : ISettingRepository
    {
        #region Ctor

        private readonly ShopDBContext _context;

        public SettingRepository(ShopDBContext context)
        {
            _context = context;
        }

        #endregion

        #region Social Networks

        public void AddSocial(Social social)
        {
            _context.Socials.Add(social);
        }

        public async Task<Social> GetAdminSocial()
        {
            return await _context.Socials.OrderByDescending(p => p.Id).FirstOrDefaultAsync();
        }

        public async Task<Social> GetSocialById(int socialId)
        {
            return await _context.Socials.SingleOrDefaultAsync(p => p.Id == socialId);
        }

        public void UpdateSocial(Social social)
        {
            _context.Socials.Update(social);
        }

        #endregion

        #region Footer Label

        public void AddLabel(FooterLabel label)
        {
            _context.FooterLabels.Add(label);
        }

        public void UpdateLabel(FooterLabel label)
        {
            _context.FooterLabels.Update(label);
        }

        public async Task<FooterLabel> GetLabelById(int labelId)
        {
            return await _context.FooterLabels.SingleOrDefaultAsync(p => p.Id == labelId);
        }

        public async Task<List<FooterLabel>> GetListOfLabel()
        {
            return await _context.FooterLabels.OrderByDescending(p => p.Id).Take(4).ToListAsync();
        }

        #endregion

        #region Footer Link

        public void AddLink(FooterLink link)
        {
            _context.FooterLinks.Add(link);
        }

        public void UpdateLink(FooterLink link)
        {
            _context.FooterLinks.Update(link);
        }

        public async Task<FooterLink> GetLinkById(int linkId)
        {
            return await _context.FooterLinks.Include(p => p.FooterLabel).SingleOrDefaultAsync(p => p.Id == linkId);
        }

        public async Task<List<FooterLink>> GetListOfLink(int linkId)
        {
            int skip = (linkId - 1) * 6;

            return await _context.FooterLinks.Include(p => p.FooterLabel).OrderByDescending(p => p.Id).Skip(skip).Take(6).ToListAsync();
        }

        public async Task<List<FooterLabel>> GetListLabel()
        {
            return await _context.FooterLabels.OrderByDescending(p => p.Id).Take(4).ToListAsync();
        }

        public int FooterLinkCount()
        {
            return _context.FooterLinks.Count();
        }

        public void RemoveFooterLink(FooterLink link)
        {
            _context.FooterLinks.Remove(link);
        }

        #endregion

        #region User

        public async Task<List<User>> GetUsers(int userId)
        {
            int skip = (userId - 1) * 6;

            return await _context.Users.OrderByDescending(p => p.Id).Skip(skip).Take(6).ToListAsync();
        }

        public int GetUserCount()
        {
            return _context.Users.Count();
        }

        public async Task<List<User>> GetAllUser()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User> GetUserById(int userId)
        {
            return await _context.Users.SingleOrDefaultAsync(p => p.Id == userId);
        }

        public void UpdateUser(User user)
        {
            _context.Users.Update(user);
        }

        #endregion

        #region Admin Logo

        public void AddAdminLogo(AdminLogo logo)
        {
            _context.AdminLogos.Add(logo);
        }

        public void UpdateAdminLogo(AdminLogo logo)
        {
            _context.AdminLogos.Update(logo);
        }

        public async Task<AdminLogo> GetAdminLogoById(int id)
        {
            return await _context.AdminLogos.SingleOrDefaultAsync(p => p.Id == id);
        }

        public async Task<AdminLogo> GetAdminLogo()
        {
            return await _context.AdminLogos.OrderByDescending(p => p.Id).FirstOrDefaultAsync();
        }

        #endregion

        #region Site Logo

        public void AddSiteLogo(SiteLogo logo)
        {
            _context.SiteLogos.Add(logo);
        }

        public void UpdateSiteLogo(SiteLogo logo)
        {
            _context.SiteLogos.Update(logo);
        }

        public async Task<SiteLogo> GetSiteLogoById(int id)
        {
            return await _context.SiteLogos.SingleOrDefaultAsync(p => p.Id == id);
        }

        public async Task<SiteLogo> GetSiteLogo()
        {
            return await _context.SiteLogos.OrderByDescending(p => p.Id).FirstOrDefaultAsync();
        }

        #endregion

        #region Copy Right

        public void AddCopyRight(CopyRight copyRight)
        {
            _context.CopyRights.Add(copyRight);
        }

        public void UpdateCopyRight(CopyRight copyRight)
        {
            _context.CopyRights.Update(copyRight);
        }

        public async Task<CopyRight> GetCopyRightById(int id)
        {
            return await _context.CopyRights.SingleOrDefaultAsync(p => p.Id == id);
        }

        public async Task<CopyRight> GetCopyRight()
        {
            return await _context.CopyRights.OrderByDescending(p => p.Id).FirstOrDefaultAsync();
        }

        #endregion

        #region General

        public async Task SaveChange()
        {
            await _context.SaveChangesAsync();
        }
        
        #endregion
    }
}
