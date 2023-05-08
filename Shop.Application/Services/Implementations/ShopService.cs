using Microsoft.EntityFrameworkCore;
using Shop.Application.Security;
using Shop.Application.Services.Interfaces;
using Shop.Domain.Entities.Admin.About;
using Shop.Domain.Entities.Admin.Blog;
using Shop.Domain.Entities.Admin.Products;
using Shop.Domain.Entities.Admin.Setting.Footer;
using Shop.Domain.Entities.Admin.SiteUI;
using Shop.Domain.Entities.Shop.Account;
using Shop.Domain.Entities.Shop.Orders;
using Shop.Domain.Entities.Shop.Site;
using Shop.Domain.Interfaces;
using Shop.Domain.ViewModels.Admin.Products;
using Shop.Domain.ViewModels.Shop.Account;
using Shop.Domain.ViewModels.Shop.Order;
using Shop.Domain.ViewModels.Shop.Site;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Services.Implementations
{
    public class ShopService : IShopService
    {
        #region Ctor

        private readonly IShopRepository _shopRepository;
        private readonly IUserService _userService;

        public ShopService(IShopRepository shopRepository, IUserService userService)
        {
            _shopRepository = shopRepository;
            _userService = userService;
        }

        #endregion

        #region Site

        public async Task<List<HeaderSlider>> GetAllSliders()
        {
            return await _shopRepository.GetAllSliders();
        }

        public async Task<List<HeaderBanner>> GetAllBanners()
        {
            return await _shopRepository.GetAllBanners();
        }

        public async Task<MiddleBanner> GetAllMidBanners()
        {
            return await _shopRepository.GetAllMidBanners();
        }

        public async Task<List<Product>> GetSliderProducts()
        {
            return await _shopRepository.GetSliderProducts();
        }

        public async Task<List<Product>> GetAllProducts()
        {
            return await _shopRepository.GetAllProducts();
        }

        public async Task<List<Product>> GetLastProducts()
        {
            return await _shopRepository.GetLastProducts();
        }

        #endregion

        #region Product

        public async Task<List<Product>> GetListProduct(int productId)
        {
            return await _shopRepository.GetListProduct(productId);
        }

        public int ProductsCount()
        {
            return _shopRepository.ProductsCount();
        }

        public int ProductsCategoryCount(int categoryId)
        {
            return _shopRepository.ProductsCategoryCount(categoryId);
        }

        public async Task<Product> GetProductById(int productId)
        {
            return await _shopRepository.GetProductById(productId);
        }

        public async Task<List<Product>> ListRelatedProduct(string title)
        {
            return await _shopRepository.ListRelatedProduct(title);
        }

        public async Task<List<Product>> GetProductsByCategoryId(int categoryId, int productId)
        {
            return await _shopRepository.GetProductsByCategoryId(categoryId , productId);
        }

        #endregion

        #region Order

        public async Task<int> AddOrder(int userId, int productId)
        {
            var product = await _shopRepository.GetProductById(productId);

            var order = await _shopRepository.CheckUserOrder(userId);

            if (order == null)
            {
                // Add Order

                order = new Order
                {
                    UserId = userId,
                    IsFinaly = false,
                    OrderSum = product.ProductPrice,
                    OrderDetails = new List<OrderDetail>
                    {
                        new OrderDetail()
                        {
                            ProductId =productId,
                            Price =product.ProductPrice,
                            Count =1
                        }
                    }
                };

                 _shopRepository.AddOrder(order);
                 await _shopRepository.SaveChange();
            }
            else
            {
                var detail = await _shopRepository.CheckOrderDetail(order.Id, product.Id);
                if (detail != null)
                {
                    detail.Count += 1;
                    _shopRepository.UpdateOrderDetail(detail);
                    //await _orderRepository.SaveChanges();
                }
                else
                {
                    detail = new OrderDetail()
                    {
                        OrderId = order.Id,
                        Count = 1,
                        ProductId = productId,
                        Price = product.ProductPrice
                    };
                    await _shopRepository.AddOrderDetail(detail);
                    //await _orderRepository.SaveChanges();
                }

                 await _shopRepository.SaveChange();
            }

            await UpdatePriceOrder(order.Id);
            return order.Id;
        }

        public async Task UpdatePriceOrder(int orderId)
        {
            var order = await _shopRepository.GetOrderById(orderId);

            order.OrderSum = await _shopRepository.OrderSum(orderId);

            _shopRepository.UpdateOrder(order);
            await _shopRepository.SaveChange();

        }

        public async Task<Order> GetUserBasket(int orderId, int userId)
        {
            return await _shopRepository.GetUserBasket(orderId, userId);
        }

        public async Task<Order> GetUserBasket(int userId)
        {
            return await _shopRepository.GetUserBasket(userId);
        }

        public async Task<bool> OrderIsFinalyView(int orderId)
        {
            var order = await _shopRepository.GetOrderById(orderId);

            if (order == null) return false;

            return true;
        }

        public async Task<OrderResult> OrderIsFinaly(OrderViewModel viewModel, int orderId)
        {
            var order = await _shopRepository.GetOrderById(orderId);

            if (order == null) return OrderResult.NotFound;
            if (order.User.IsBan) return OrderResult.IsBan;

            order.IsFinaly = true;

            _shopRepository.UpdateOrder(order);
            await _shopRepository.SaveChange();

            return OrderResult.Success;
        }

        public async Task<Order> GetOrderById(int orderId)
        {
            return await _shopRepository.GetOrderById(orderId);
        }

        public async Task<bool> RemoveOrderDetailFromOrder(int orderDetailId)
        {
            var orderDetail = await _shopRepository.GetOrderDetailById(orderDetailId);
            var order = await _shopRepository.GetOrderById(orderDetail.OrderId);

            if (orderDetail != null)
            {
                orderDetail.IsDelete = true;
                _shopRepository.UpdateOrderDetail(orderDetail);

                order.OrderSum = (await _shopRepository.OrderSum(order.Id) - orderDetail.Price);
                _shopRepository.UpdateOrder(order);

                await _shopRepository.SaveChange();
                return true;
            }

            return false;
        }

        public async Task<List<Order>> GetAllOrdersByUserId(int userId, int orderId)
        {
            return await _shopRepository.GetAllOrdersByUserId(userId,orderId);
        }

        public async Task<List<Order>> GetAllOrdersByUserId(int userId)
        {
            return await _shopRepository.GetAllOrdersByUserId(userId);
        }

        public int GetCountOrdersByUserId(int userId)
        {
            return _shopRepository.GetCountOrdersByUserId(userId);
        }

        #endregion

        #region Address

        public async Task<bool> AddUserAddress(AddressViewModel viewModel, int userId)
        {
            var user = await _userService.GetUserById(userId);

            if (user == null) return false;

            var address = new Address
            {
                City = viewModel.City.SanitizeText(),
                Country = viewModel.Country.SanitizeText(),
                FirstName = viewModel.FirstName.SanitizeText(),
                LastName = viewModel.LastName.SanitizeText(),
                Phone = viewModel.Phone.SanitizeText(),
                OrderNotes = viewModel.OrderNotes?.SanitizeText(),
                Postcode = viewModel.Postcode.SanitizeText(),
                StreetAddress = viewModel.StreetAddress.SanitizeText(),
                UserId = user.Id
            };

            _shopRepository.ADDAddress(address);
            await _shopRepository.SaveChange();

            return true;
        }

        public async Task<EditAddressViewModel> EditAddressView(int userId, int addressId)
        {
            var user = await _userService.GetUserById(userId);

            var address = await _shopRepository.GetAddressById(addressId);

            var result = new EditAddressViewModel()
            {
                FirstName = address.FirstName,
                LastName = address.LastName,
                City = address.City,
                Postcode = address.Postcode,
                Country = address.Country,
                OrderNotes = address.OrderNotes,
                Phone = address.Phone,
                StreetAddress = address.StreetAddress,
                UserId = user.Id,
            };

            return result;
        }

        public async Task<bool> EditAddress(EditAddressViewModel viewModel, int userId, int addressId)
        {
            var user = await _userService.GetUserById(userId);

            var address = await _shopRepository.GetAddressById(addressId);

            if (user == null || address == null) return false;

            address.FirstName = viewModel.FirstName.SanitizeText();
            address.LastName = viewModel.LastName.SanitizeText();
            address.City = viewModel.City.SanitizeText();
            address.Postcode = viewModel.Postcode.SanitizeText();
            address.Country = viewModel.Country.SanitizeText();
            address.OrderNotes = viewModel.OrderNotes?.SanitizeText();
            address.Phone = viewModel.Phone.SanitizeText();
            address.StreetAddress = viewModel.StreetAddress.SanitizeText();
            address.UserId = user.Id;

            _shopRepository.UpdateAddress(address);
            await _shopRepository.SaveChange();

            return true;
        }

        public async Task<List<Address>> GetAddressUser(int userId)
        {
            return await _shopRepository.GetAddressUser(userId);
        }

        public async Task<Address> GetAllAddresses(int userId)
        {
            return await _shopRepository.GetAllAddresses(userId);
        }

        #endregion

        #region ContactUs

        public async Task<bool> AddContact(ContactUsViewModel viewModel)
        {
            var contact = new ContactUs
            {
                Email = viewModel.Email.SanitizeText(),
                Message = viewModel.Message.SanitizeText(),
                Name = viewModel.Name.SanitizeText(),
                Subject = viewModel.Subject.SanitizeText(),
            };

            _shopRepository.AddContactUs(contact);
            await _shopRepository.SaveChange();

            return true;
        }

        #endregion

        #region Comment

        public async Task<bool> CommentShop(CommentViewModel viewModel)
        {
            var product = await _shopRepository.GetProductById(viewModel.ProductId);

            if (product == null) return false;

            var comment = new Comment
            {
                Content = viewModel.Comment.SanitizeText().SanitizeText(),
                ProductId = viewModel.ProductId
            };

            _shopRepository.AddComment(comment);
            await _shopRepository.SaveChange();

            return true;
        }

        public async Task<List<Comment>> GetAllComments(int productId)
        {
            return await _shopRepository.GetAllComments(productId);
        }

        #endregion

        #region About

        public async Task<AboutUs> GetAboutUs()
        {
            return await _shopRepository.GetAboutUs();
        }

        public async Task<List<Team>> GetListOfTeam()
        {
            return await _shopRepository.GetListOfTeam();
        }

        #endregion

        #region Footer

        public async Task<List<FooterLabel>> GetFooterLabels()
        {
            return await _shopRepository.GetFooterLabels();
        }

        public async Task<Social> GetSocials()
        {
            return await _shopRepository.GetSocials();
        }

        #endregion
    }

    //---------------------------------------------------------------

    public class WeBlogService : IWeBlogService
    {
        #region Ctor

        private readonly IWeBlogRepository _weBlogRepository;
        private readonly IUserService _userService;

        public WeBlogService(IWeBlogRepository weBlogRepository, IUserService userService)
        {
            _weBlogRepository = weBlogRepository;
            _userService = userService;
        }

        #endregion

        #region Blog

        public int BlogsCount()
        {
            return _weBlogRepository.BlogsCount();
        }

        public async Task<List<WeBloge>> GetAllBlogs(int blogId)
        {
            return await _weBlogRepository.GetAllBlogs(blogId);
        }

        public async Task<WeBloge> GetBlogById(int blogId)
        {
            return await _weBlogRepository.GetBlogById(blogId);
        }

        public async Task<List<WeBloge>> GetLastBlogs()
        {
            return await _weBlogRepository.GetLastBlogs();
        }

        #endregion

        #region Comment

        public async Task<bool> CommentBlog(CommentBlogViewModel viewModel)
        {
            var blog = await _weBlogRepository.GetBlogById(viewModel.blogId);

            if (blog == null) return false;

            var comment = new BlogComment
            {
                Content = viewModel.Comment.SanitizeText().SanitizeText(),
                WeBlogeId = viewModel.blogId,
                IsDelete = false
            };

            _weBlogRepository.AddComment(comment);
            await _weBlogRepository.SaveChange();

            return true;
        }

        public async Task<List<BlogComment>> GetAllBlogsComments(int blogId)
        {
            return await _weBlogRepository.GetAllBlogsComments(blogId);
        }

        #endregion
    }

}
