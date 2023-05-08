using Microsoft.EntityFrameworkCore;
using Shop.DataLayer.Context;
using Shop.Domain.Entities.Account;
using Shop.Domain.Entities.Admin.About;
using Shop.Domain.Entities.Admin.Blog;
using Shop.Domain.Entities.Admin.Products;
using Shop.Domain.Entities.Admin.Setting.Footer;
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
    public class ShopRepository : IShopRepository
    {
        #region Ctor

        private readonly ShopDBContext _context;

        public ShopRepository(ShopDBContext context)
        {
            _context = context;
        }

        #endregion

        #region Site

        public async Task<List<HeaderSlider>> GetAllSliders()
        {
            return await _context.HeaderSliders.OrderByDescending(p => p.Id).Where(p => !p.IsDelete).ToListAsync();
        }

        public async Task<List<HeaderBanner>> GetAllBanners()
        {
            return await _context.HeaderBanners.OrderByDescending(p => p.Id).Where(p => !p.IsDelete).Take(2).ToListAsync();
        }

        public async Task<MiddleBanner> GetAllMidBanners()
        {
            return await _context.MiddleBanners.OrderByDescending(p => p.Id).Where(p => !p.IsDelete).FirstOrDefaultAsync();
        }

        public async Task<List<Product>> GetAllProducts()
        {
            return await _context.Products.Include(p => p.Categories).OrderBy(p => Guid.NewGuid()).Where(p => !p.IsDelete).Take(8).ToListAsync();
        }

        public async Task<List<Product>> GetSliderProducts()
        {
            return await _context.Products.Include(p => p.Categories).OrderBy(p => Guid.NewGuid()).Where(p => !p.IsDelete).Take(8).ToListAsync();
        }

        public async Task<List<Product>> GetLastProducts()
        {
            return await _context.Products.Include(p => p.Categories).OrderBy(p => Guid.NewGuid()).Where(p => !p.IsDelete).Take(6).ToListAsync();
        }

        #endregion

        #region Product

        public async Task<List<Product>> GetListProduct(int productId)
        {
            int skip = (productId - 1) * 8;

            return await _context.Products.Include(p => p.Categories).OrderByDescending(p => p.Id).Where(p => !p.IsDelete).Skip(skip).Take(8).ToListAsync();
        }

        public int ProductsCount()
        {
            return _context.Products.Where(p => !p.IsDelete).Count();
        }

        public int ProductsCategoryCount(int categoryId)
        {
            return _context.Products.Where(p => p.CategoriesId == categoryId && !p.IsDelete).Count();
        }

        public async Task<Product> GetProductById(int productId)
        {
            return await _context.Products.Include(p => p.Categories).Where(p => !p.IsDelete).FirstOrDefaultAsync(p => p.Id == productId);
        }

        public async Task<List<Product>> GetProductsByCategoryId(int categoryId, int productId)
        {
            int skip = (productId - 1) * 8;

            return await _context.Products.Include(p => p.Categories).OrderByDescending(p => p.Id).Where(p => p.CategoriesId == categoryId && !p.IsDelete).Skip(skip).Take(8).ToListAsync();
        }

        public async Task<List<Product>> ListRelatedProduct(string title)
        {
            return await _context.Products.Include(p => p.Categories)
                .Where(p => p.Categories.Title == title).ToListAsync();
        }

        #endregion

        #region Order

        public void AddOrder(Order order)
        {
            _context.Orders.Add(order);
        }

        public async Task<Order> CheckUserOrder(int userId)
        {
            return await _context.Orders
                .FirstOrDefaultAsync(c => c.UserId == userId && !c.IsFinaly);
        }

        public async Task<Order> GetOrderById(int orderId)
        {
            return await _context.Orders.Include(p => p.User)
                .SingleOrDefaultAsync(c => c.Id == orderId);
        }

        //public async Task<Order> GetOrderById(int orderId, int userId)
        //{
        //    return await _context.Orders
        //       .SingleOrDefaultAsync(c => c.Id == orderId && c.UserId == userId);
        //}

        public async Task<OrderDetail> CheckOrderDetail(int orderId, int productId)
        {
            return await _context.OrderDetails
                .Where(c => c.OrderId == orderId && c.ProductId == productId && !c.IsDelete)
                .FirstOrDefaultAsync();
        }

        public void UpdateOrderDetail(OrderDetail orderDetail)
        {
            _context.OrderDetails.Update(orderDetail);
        }

        public async Task AddOrderDetail(OrderDetail order)
        {
            await _context.OrderDetails.AddAsync(order);
        }

        public async Task<double> OrderSum(int orderId)
        {
            return await _context.OrderDetails
                .Where(c => c.OrderId == orderId && !c.IsDelete).SumAsync(c => c.Price * c.Count);
        }

        public void UpdateOrder(Order order)
        {
            _context.Orders.Update(order);
        }

        public async Task<Order> GetUserBasket(int orderId, int userId)
        {
            return await _context.Orders
                .Include(c => c.User)
                .Where(c => c.UserId == userId && c.Id == orderId)
                .Select(c => new Order
                {
                    UserId = c.UserId,
                    CreateDate = c.CreateDate,
                    Id = c.Id,
                    IsDelete = c.IsDelete,
                    IsFinaly = c.IsFinaly,
                    OrderSum = c.OrderSum,
                    OrderDetails = _context.OrderDetails.Where(c => !c.IsDelete && !c.Order.IsFinaly && c.Order.UserId == userId).Include(c => c.Product).ToList()
                })
                .FirstOrDefaultAsync();
        }

        public async Task<Order> GetUserBasket(int userId)
        {
            return await _context.Orders.Include(c => c.OrderDetails).ThenInclude(c => c.Product)
                .Where(c => !c.IsDelete && c.UserId == userId && !c.IsFinaly)
                .FirstOrDefaultAsync();
        }

        public async Task<OrderDetail> GetOrderDetailById(int id)
        {
            return await _context.OrderDetails
                 .SingleOrDefaultAsync(c => c.Id == id);
        }

        public async Task<List<Order>> GetAllOrdersByUserId(int userId, int orderId)
        {
            int skip = (orderId - 1) * 3;

            return await _context.Orders.Include(p => p.User).Where(p => p.IsFinaly == true && p.UserId == userId && !p.IsDelete).OrderByDescending(p => p.CreateDate).Skip(skip).Take(3).ToListAsync();
        }

        public async Task<List<Order>> GetAllOrdersByUserId(int userId)
        {
            return await _context.Orders.Include(p => p.User).Where(p => p.IsFinaly == true && p.UserId == userId && !p.IsDelete).OrderByDescending(p => p.CreateDate).ToListAsync();
        }

        public int GetCountOrdersByUserId(int userId)
        {
            return _context.Orders.Include(p => p.User).Where(p => p.IsFinaly == true && p.UserId == userId && !p.IsDelete).Count();
        }

        #endregion

        #region Address

        public void ADDAddress(Address address)
        {
            _context.Addresses.Add(address);
        }

        public void UpdateAddress(Address address)
        {
            _context.Addresses.Update(address);
        }

        public async Task<Address> GetAddressById(int addressId)
        {
            return await _context.Addresses.SingleOrDefaultAsync(p => p.Id == addressId);
        }

        public async Task<List<Address>> GetAddressUser(int userId)
        {
            return await _context.Addresses.Where(p => p.UserId == userId).ToListAsync();
        }

        public async Task<Address> GetAllAddresses(int userId)
        {
            return await _context.Addresses.OrderByDescending(p => p.Id).Where(p => !p.IsDelete && p.UserId == userId).FirstOrDefaultAsync();
        }

        #endregion

        #region Comment

        public void AddComment(Comment comment)
        {
            _context.Comments.Add(comment);
        }

        public async Task<List<Comment>> GetAllComments(int productId)
        {
            return await _context.Comments.Include(p => p.Product).Where(p => !p.IsDelete && p.ProductId == productId).OrderByDescending(p => p.CreateDate).ToListAsync();
        }

        #endregion

        #region ContactUs

        public void AddContactUs(ContactUs contactUs)
        {
            _context.ContactUs.Add(contactUs);
        }

        #endregion

        #region About

        public async Task<AboutUs> GetAboutUs()
        {
            return await _context.AboutUs.Where(p => !p.IsDelete).OrderByDescending(p => p.Id).FirstOrDefaultAsync();
        }

        public async Task<List<Team>> GetListOfTeam()
        {
            return await _context.Teams.OrderByDescending(p => p.Id).Where(p => !p.IsDelete).ToListAsync();
        }

        #endregion

        #region Footer

        public async Task<List<FooterLabel>> GetFooterLabels()
        {
            return await _context.FooterLabels.Include(p => p.FooterLinks).OrderByDescending(p => p.Id).Take(4).ToListAsync();
        }

        public async Task<Social> GetSocials()
        {
            return await _context.Socials.OrderByDescending(p => p.Id).FirstOrDefaultAsync();
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

    public class WeBlogRepository : IWeBlogRepository
    {
        #region Ctor

        private readonly ShopDBContext _context;

        public WeBlogRepository(ShopDBContext context)
        {
            _context = context;
        }

        #endregion

        #region Blog

        public int BlogsCount()
        {
            return _context.WeBloges.Where(p => !p.IsDelete).Count();
        }

        public async Task<List<WeBloge>> GetAllBlogs(int blogId)
        {
            int skip = (blogId - 1) * 6;

            return await _context.WeBloges.OrderByDescending(p => p.Id).Where(p => !p.IsDelete).Skip(skip).Take(6).ToListAsync();
        }

        public async Task<WeBloge> GetBlogById(int blogId)
        {
            return await _context.WeBloges.SingleOrDefaultAsync(p => p.Id == blogId);
        }

        public async Task<List<WeBloge>> GetLastBlogs()
        {
            return await _context.WeBloges.OrderByDescending(p => p.Id).Where(p => !p.IsDelete).Take(3).ToListAsync();
        }

        #endregion

        #region Comment

        public void AddComment(BlogComment comment)
        {
            _context.BlogComments.Add(comment);
        }

        public async Task<List<BlogComment>> GetAllBlogsComments(int blogId)
        {
            return await _context.BlogComments.Include(p => p.WeBloge).Where(p => !p.IsDelete && p.WeBlogeId == blogId).OrderByDescending(p => p.CreateDate).ToListAsync();
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
