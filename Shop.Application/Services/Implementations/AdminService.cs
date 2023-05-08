using Microsoft.AspNetCore.Http;
using Shop.Application.Generators;
using Shop.Application.Security;
using Shop.Application.Services.Interfaces;
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
using Shop.Domain.ViewModels.Admin.about;
using Shop.Domain.ViewModels.Admin.Blog;
using Shop.Domain.ViewModels.Admin.Dashboard;
using Shop.Domain.ViewModels.Admin.Products;
using Shop.Domain.ViewModels.Admin.Setting.Footer;
using Shop.Domain.ViewModels.Admin.Setting.General;
using Shop.Domain.ViewModels.Admin.SiteUI;
using Shop.Domain.ViewModels.Shop.Order;
using Shop.Domain.ViewModels.Shop.Site;

namespace Shop.Application.Services.Implementations
{
    public class AdminService : IAdminService
    {
        #region Ctor

        private readonly IAdminRepository _adminRepository;
        private readonly IUserRepository _userRepository;

        public AdminService(IAdminRepository adminRepository, IUserRepository userRepository)
        {
            _adminRepository = adminRepository;
            _userRepository = userRepository;
        }

        #endregion

        #region HeaderSlider

        public async Task<bool> HeaderSlider(HeaderSliderViewModel viewModel)
        {
            string filePath = "";
            viewModel.ImgName = RandomCodeGenerators.CreateFileCode() + Path.GetExtension(viewModel.Img.FileName);
            filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/content/gallery", viewModel.ImgName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                viewModel.Img.CopyTo(stream);
            }

            HeaderSlider slider = new HeaderSlider()
            {
                Img = viewModel.ImgName.SanitizeText(),
                ButtonLink = viewModel.ButtonLink.SanitizeText(),
                ButtonTitle = viewModel.ButtonTitle.SanitizeText(),
                SmallDescription = viewModel.SmallDescription.SanitizeText(),
                Title = viewModel.Title.SanitizeText(),
            };

            _adminRepository.AddHeaderSlider(slider);
            await _adminRepository.SaveChange();

            return true;
        }

        public async Task<List<HeaderSlider>> GetAllSliders(int sliderId)
        {
            return await _adminRepository.GetAllSliders(sliderId);
        }

        public int SlidersCount()
        {
            return _adminRepository.SlidersCount();
        }

        public async Task<HeaderSlider> GetSliderById(int sliderId)
        {
            return await _adminRepository.GetSliderById(sliderId);
        }

        public async Task<EditSliderViewModel> EditSliderView(int sliderId)
        {
            var slider = await _adminRepository.GetSliderById(sliderId);

            var result = new EditSliderViewModel
            {
                ButtonTitle = slider.ButtonTitle,
                SmallDescription = slider.SmallDescription,
                ButtonLink = slider.ButtonLink,
                Title = slider.Title,
            };

            return result;
        }

        public async Task<bool> EditSlider(EditSliderViewModel viewModel, int sliderId)
        {
            var slider = await _adminRepository.GetSliderById(sliderId);

            if (slider == null) return false;

            slider.Title = viewModel.Title.SanitizeText();
            slider.SmallDescription = viewModel.SmallDescription.SanitizeText();
            slider.ButtonLink = viewModel.ButtonLink.SanitizeText();
            slider.ButtonTitle = viewModel.ButtonTitle.SanitizeText();

            _adminRepository.UpdateSlider(slider);
            await _adminRepository.SaveChange();

            return true;
        }

        public async Task<bool> EditImgSlider(EditImgSliderViewModel viewModel, int sliderId)
        {
            if (viewModel.Img != null)
            {
                var slider = await _adminRepository.GetSliderById(sliderId);

                string filePath = "";
                viewModel.ImgName = RandomCodeGenerators.CreateFileCode() + Path.GetExtension(viewModel.Img.FileName);
                filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/content/gallery", viewModel.ImgName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    viewModel.Img.CopyTo(stream);
                }

                slider.Img = viewModel.ImgName.SanitizeText();

                _adminRepository.UpdateSlider(slider);
                await _adminRepository.SaveChange();
            }

            return true;
        }

        public async Task<bool> DeleteSlider(int sliderId)
        {
            var slider = await _adminRepository.GetSliderById(sliderId);

            if (slider == null) return false;

            slider.IsDelete = true;

            _adminRepository.UpdateSlider(slider);
            await _adminRepository.SaveChange();

            return true;
        }

        public async Task<bool> RestoreSlider(int sliderId)
        {
            var slider = await _adminRepository.GetSliderById(sliderId);

            if (slider == null) return false;

            slider.IsDelete = false;

            _adminRepository.UpdateSlider(slider);
            await _adminRepository.SaveChange();

            return true;
        }

        #endregion

        #region HeaderBanner

        public async Task<bool> HeaderBanner(HeaderBannerViewModel viewModel)
        {
            string filePath = "";
            viewModel.ImgName = RandomCodeGenerators.CreateFileCode() + Path.GetExtension(viewModel.Img.FileName);
            filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/content/gallery", viewModel.ImgName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                viewModel.Img.CopyTo(stream);
            }

            HeaderBanner banner = new HeaderBanner()
            {
                Img = viewModel.ImgName.SanitizeText(),
                Title = viewModel.Title.SanitizeText(),
                Link = viewModel.Link.SanitizeText()
            };

            _adminRepository.AddHeaderBanner(banner);
            await _adminRepository.SaveChange();

            return true;
        }

        public async Task<List<HeaderBanner>> GetAllBanners()
        {
            return await _adminRepository.GetAllBanners();
        }

        public async Task<HeaderBanner> GetBannerById(int bannerId)
        {
            return await _adminRepository.GetBannerById(bannerId);
        }

        public async Task<EditBannerViewModel> EditBannerView(int bannerId)
        {
            var banner = await _adminRepository.GetBannerById(bannerId);

            var result = new EditBannerViewModel
            {
                Link = banner.Link,
                Title = banner.Title
            };

            return result;
        }

        public async Task<bool> EditBanner(EditBannerViewModel viewModel, int bannerId)
        {
            var banner = await _adminRepository.GetBannerById(bannerId);

            if (banner == null) return false;

            banner.Title = viewModel.Title.SanitizeText();
            banner.Link = viewModel.Link.SanitizeText();

            _adminRepository.UpdateBanner(banner);
            await _adminRepository.SaveChange();

            return true;
        }

        public async Task<bool> EditImgBanner(EditImgBannerViewModel viewModel, int bannerId)
        {
            if (viewModel.Img != null)
            {
                var banner = await _adminRepository.GetBannerById(bannerId);

                string filePath = "";
                viewModel.ImgName = RandomCodeGenerators.CreateFileCode() + Path.GetExtension(viewModel.Img.FileName);
                filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/content/gallery", viewModel.ImgName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    viewModel.Img.CopyTo(stream);
                }

                banner.Img = viewModel.ImgName.SanitizeText();

                _adminRepository.UpdateBanner(banner);
                await _adminRepository.SaveChange();
            }

            return true;
        }

        #endregion

        #region MiddleBanner

        public async Task<bool> MiddleBanner(MiddleBannerViewModel viewModel)
        {
            string filePath = "";
            viewModel.ImgName = RandomCodeGenerators.CreateFileCode() + Path.GetExtension(viewModel.Img.FileName);
            filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/content/gallery", viewModel.ImgName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                viewModel.Img.CopyTo(stream);
            }

            MiddleBanner banner = new MiddleBanner()
            {
                Img = viewModel.ImgName.SanitizeText(),
                ButtonLink = viewModel.ButtonLink.SanitizeText(),
                ButtonTitle = viewModel.ButtonTitle.SanitizeText(),
                SmallDescription = viewModel.SmallDescription.SanitizeText(),
                Title = viewModel.Title.SanitizeText()
            };

            _adminRepository.AddMiddleBanner(banner);
            await _adminRepository.SaveChange();

            return true;
        }

        public async Task<MiddleBanner> GetMidBannerById(int bannerId)
        {
            return await _adminRepository.GetMidBannerById(bannerId);
        }

        public async Task<EditMidBannerViewModel> EditMidBannerView(int bannerId)
        {
            var banner = await _adminRepository.GetMidBannerById(bannerId);

            var result = new EditMidBannerViewModel
            {
                ButtonTitle = banner.ButtonTitle,
                SmallDescription = banner.SmallDescription,
                ButtonLink = banner.ButtonLink,
                Title = banner.Title,
            };

            return result;
        }

        public async Task<bool> EditMidBanner(EditMidBannerViewModel viewModel, int bannerId)
        {
            var banner = await _adminRepository.GetMidBannerById(bannerId);

            if (banner == null) return false;

            banner.Title = viewModel.Title.SanitizeText();
            banner.SmallDescription = viewModel.SmallDescription.SanitizeText();
            banner.ButtonLink = viewModel.ButtonLink.SanitizeText();
            banner.ButtonTitle = viewModel.ButtonTitle.SanitizeText();

            _adminRepository.UpdateMidBanner(banner);
            await _adminRepository.SaveChange();

            return true;
        }

        public async Task<bool> EditImgMidBanner(EditImgMidBannerViewModel viewModel, int bannerId)
        {
            if (viewModel.Img != null)
            {
                var banner = await _adminRepository.GetMidBannerById(bannerId);

                string filePath = "";
                viewModel.ImgName = RandomCodeGenerators.CreateFileCode() + Path.GetExtension(viewModel.Img.FileName);
                filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/content/gallery", viewModel.ImgName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    viewModel.Img.CopyTo(stream);
                }

                banner.Img = viewModel.ImgName.SanitizeText();

                _adminRepository.UpdateMidBanner(banner);
                await _adminRepository.SaveChange();
            }

            return true;
        }

        public async Task<MiddleBanner> GetAllMidBanners()
        {
            return await _adminRepository.GetAllMidBanners();
        }

        #endregion

        #region Profile

        public async Task<bool> AddProfile(AdminProfileViewModel viewModel)
        {
            string filePath = "";
            viewModel.ImgName = RandomCodeGenerators.CreateFileCode() + Path.GetExtension(viewModel.Img.FileName);
            filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/content/gallery", viewModel.ImgName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                viewModel.Img.CopyTo(stream);
            }

            var result = new AdminProfile
            {
                FirstName = viewModel.FirstName.SanitizeText(),
                LastName = viewModel.LastName.SanitizeText(),
                Img = viewModel.ImgName.SanitizeText()
            };

            _adminRepository.AddProfile(result);
            await _adminRepository.SaveChange();

            return true;
        }

        public async Task<EditProfileViewModel> EditProfileView(int profileId)
        {
            var profile = await _adminRepository.GetProfileById(profileId);

            var result = new EditProfileViewModel
            {
                FirstName = profile.FirstName,
                LastName = profile.LastName
            };

            return result;
        }

        public async Task<bool> EditProfile(EditProfileViewModel viewModel, int profileId)
        {
            var profile = await _adminRepository.GetProfileById(profileId);

            if (profile == null) return false;

            profile.FirstName = viewModel.FirstName.SanitizeText();
            profile.LastName = viewModel.LastName.SanitizeText();

            _adminRepository.UpdateProfile(profile);    
            await _adminRepository.SaveChange();

            return true;
        }

        public async Task<bool> EditImgProfile(EditImgProfileViewModel viewModel, int profileId)
        {
            if (viewModel.Img != null)
            {
                var profile = await _adminRepository.GetProfileById(profileId);

                string filePath = "";
                viewModel.ImgName = RandomCodeGenerators.CreateFileCode() + Path.GetExtension(viewModel.Img.FileName);
                filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/content/gallery", viewModel.ImgName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    viewModel.Img.CopyTo(stream);
                }

                profile.Img = viewModel.ImgName.SanitizeText();

                _adminRepository.UpdateProfile(profile);
                await _adminRepository.SaveChange();
            }

            return true;
        }

        public async Task<AdminProfile> GetAdminProfile()
        {
            return await _adminRepository.GetAdminProfile(); 
        }

        #endregion

        #region Contact

        public async Task<List<ContactUs>> GetAllContact(int contactId)
        {
            return await _adminRepository.GetAllContact(contactId);
        }

        public int ContactsCount()
        {
            return _adminRepository.ContactsCount();
        }

        public async Task<ContactUs> GetContactUsById(int contactId)
        {
            return await _adminRepository.GetContactUsById(contactId);
        }

        public async Task<bool> DeleteContact(int contactId)
        {
            var contact = await _adminRepository.GetContactUsById(contactId);

            if (contact == null) return false;

            _adminRepository.RemoveContactUs(contact);
            await _adminRepository.SaveChange();

            return true;
        }

        public async Task<ContactUs> GetLastContactUs()
        {
            return await _adminRepository.GetLastContactUs();
        }

        #endregion

        #region About

        public async Task<bool> AddAboutUs(AboutUsViewModel viewModel)
        {
            string filePath = "";
            viewModel.ImgName = RandomCodeGenerators.CreateFileCode() + Path.GetExtension(viewModel.Img.FileName);
            filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/content/gallery", viewModel.ImgName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                viewModel.Img.CopyTo(stream);
            }

            var aboutUs = new AboutUs
            {
                Img = viewModel.ImgName.SanitizeText(),
                ButtonLink = viewModel.ButtonLink.SanitizeText(),
                ButtonTitle = viewModel.ButtonTitle.SanitizeText(),
                Description = viewModel.Description.SanitizeText(),
                SubTitle = viewModel.SubTitle.SanitizeText(),
                Title = viewModel.Title.SanitizeText(),
            };

            _adminRepository.AddAboutUs(aboutUs);
            await _adminRepository.SaveChange();

            return true;
        }

        public async Task<AboutUs> GetAboutUsById(int aboutId)
        {
            return await _adminRepository.GetAboutUsById(aboutId);
        }

        public async Task<EditAboutUsViewModel> EditAboutUsView(int aboutId)
        {
            var about = await _adminRepository.GetAboutUsById(aboutId);

            var aboutUs = new EditAboutUsViewModel
            {
                Title = about.Title,
                SubTitle = about.SubTitle,
                Description = about.Description,
                ButtonLink = about.ButtonLink,
                ButtonTitle = about.ButtonTitle,
            };

            return aboutUs;
        }

        public async Task<bool> EditAboutUs(EditAboutUsViewModel viewModel, int aboutId)
        {
            var about = await _adminRepository.GetAboutUsById(aboutId);

            if (about == null) return false;

            about.Title = viewModel.Title.SanitizeText();
            about.SubTitle = viewModel.SubTitle.SanitizeText();
            about.ButtonLink = viewModel.ButtonLink.SanitizeText();
            about.ButtonTitle = viewModel.ButtonTitle.SanitizeText();
            about.Title = viewModel.Title.SanitizeText();

            _adminRepository.UpdateAboutUs(about);
            await _adminRepository.SaveChange();

            return true;
        }

        public async Task<bool> EditImgAboutUs(EditImgAboutUsViewModel viewModel, int aboutId)
        {
            if (viewModel.Img != null)
            {
                string filePath = "";
                viewModel.ImgName = RandomCodeGenerators.CreateFileCode() + Path.GetExtension(viewModel.Img.FileName);
                filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/content/gallery", viewModel.ImgName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    viewModel.Img.CopyTo(stream);
                }

                var about = await _adminRepository.GetAboutUsById(aboutId);

                about.Img = viewModel.ImgName.SanitizeText();

                _adminRepository.UpdateAboutUs(about);
                await _adminRepository.SaveChange();
            }

            return true;
        }

        public async Task<AboutUs> GetAboutUs()
        {
            return await _adminRepository.GetAboutUs();
        }


        public async Task<bool> AddTeam(TeamViewModel viewModel)
        {
            string filePath = "";
            viewModel.ImgName = RandomCodeGenerators.CreateFileCode() + Path.GetExtension(viewModel.Img.FileName);
            filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/content/gallery", viewModel.ImgName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                viewModel.Img.CopyTo(stream);
            }

            var team = new Team
            {
                Img = viewModel.ImgName.SanitizeText(),
                FirstName = viewModel.FirstName.SanitizeText(),
                LastName = viewModel.LastName.SanitizeText(),
                Job = viewModel.Job.SanitizeText(),
            };

            _adminRepository.AddTeam(team);
            await _adminRepository.SaveChange();

            return true;
        }

        public async Task<List<Team>> GetTeamGetList(int teamId)
        {
            return await _adminRepository.GetTeamGetList(teamId);
        }

        public async Task<EditTeamViewModel> EditTeamView(int teamId)
        {
            var team = await _adminRepository.GetTeamById(teamId);

            var Team = new EditTeamViewModel
            {
                FirstName = team.FirstName,
                LastName = team.LastName,
                Job = team.Job,
            };

            return Team;
        }

        public async Task<bool> EditTeam(EditTeamViewModel viewModel, int teamId)
        {
            var team = await _adminRepository.GetTeamById(teamId);

            if (team == null) return false;

            team.FirstName = viewModel.FirstName;
            team.LastName = viewModel.LastName;
            team.Job = viewModel.Job;

            _adminRepository.UpdateTeam(team);
            await _adminRepository.SaveChange();

            return true;
        }

        public async Task<bool> EditImgTeam(EditImgTeamViewModel viewModel, int teamId)
        {
            if (viewModel.Img != null)
            {
                string filePath = "";
                viewModel.ImgName = RandomCodeGenerators.CreateFileCode() + Path.GetExtension(viewModel.Img.FileName);
                filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/content/gallery", viewModel.ImgName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    viewModel.Img.CopyTo(stream);
                }

                var team = await _adminRepository.GetTeamById(teamId);

                team.Img = viewModel.ImgName.SanitizeText();

                _adminRepository.UpdateTeam(team);
                await _adminRepository.SaveChange();
            }

            return true;
        }

        public int TeamCount()
        {
            return _adminRepository.TeamCount();
        }

        public async Task<bool> DeleteTeam(int teamId)
        {
            var Team = await _adminRepository.GetTeamById(teamId);

            if (Team == null) return false;

            _adminRepository.RemoveTeam(Team);
            await _adminRepository.SaveChange();

            return true;
        }

        #endregion
    }

    //---------------------------------------------------------------

    public class ProductAdminService : IProductAdminService
    {
        #region Ctor

        private readonly IProductAdminRepository _productRepository;

        public ProductAdminService(IProductAdminRepository productRepository)
        {
            _productRepository = productRepository;
        }

        #endregion

        #region Categories

        public async Task<bool> AddCategories(CategoriesViewModel viewModel)
        {
            var result = new Categories
            {
                Title = viewModel.Title
            };

            if (result == null) return false;

            _productRepository.AddCategories(result);
            await _productRepository.SaveChange();

            return true;
        }

        public async Task<EditCategoriesViewModel> EditCategoriesView(int categoryId)
        {
            var category = await _productRepository.GetCategoriesById(categoryId);

            var result = new EditCategoriesViewModel
            {
                Title = category.Title
            };

            return result;
        }

        public async Task<bool> EditCategories(EditCategoriesViewModel viewModel, int categoryId)
        {
            var category = await _productRepository.GetCategoriesById(categoryId);

            if (category == null) return false;

            category.Title = viewModel.Title;

            _productRepository.UpdateCategories(category);
            await _productRepository.SaveChange();

            return true;
        }

        public async Task<List<Categories>> GetAllCategories(int categoryId)
        {
            return await _productRepository.GetAllCategories(categoryId);
        }

        public async Task<Categories> GetCategoriesById(int categoryId)
        {
            return await _productRepository.GetCategoriesById(categoryId);
        }

        public async Task<bool> DeleteCategories(int categoryId)
        {
            var category = await _productRepository.GetCategoriesById(categoryId);

            if (category == null) return false;

            category.IsDelete = true;

            _productRepository.UpdateCategories(category);
            await _productRepository.SaveChange();

            return true;
        }

        public async Task<bool> RestoreCategories(int categoryId)
        {
            var category = await _productRepository.GetCategoriesById(categoryId);

            if (category == null) return false;

            category.IsDelete = false;

            _productRepository.UpdateCategories(category);
            await _productRepository.SaveChange();

            return true;
        }

        public int CategoriesCount()
        {
            return _productRepository.CategoriesCount();
        }

        #endregion

        #region Product

        public async Task<bool> AddProducts(AddProductsViewModel viewModel)
        {
            string filePath = "";
            viewModel.ImgName = RandomCodeGenerators.CreateFileCode() + Path.GetExtension(viewModel.Img.FileName);
            filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/content/gallery", viewModel.ImgName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                viewModel.Img.CopyTo(stream);
            }

            Product product = new Product()
            {
                Img = viewModel.ImgName.SanitizeText(),
                NumberOfProducts = viewModel.NumberOfProducts,
                ProductPrice = viewModel.ProductPrice,
                Title = viewModel.Title.SanitizeText(),
                SubTitle = viewModel.SubTitle.SanitizeText(),
                Description = viewModel.Description.SanitizeText(),
                CategoriesId = viewModel.CategoriesId,
            };

            _productRepository.AddProducts(product);
            await _productRepository.SaveChange();

            return true;
        }

        public async Task<List<Product>> GetAllProducts(int productId)
        {
            return await _productRepository.GetAllProducts(productId);
        }

        public async Task<List<Product>> GetProductsForProfile()
        {
            return await _productRepository.GetProductsForProfile();
        }

        public int ProductsCount()
        {
            return _productRepository.ProductsCount();
        }

        public async Task<Product> GetProductById(int productId)
        {
            return await _productRepository.GetProductsById(productId);
        }

        public async Task<EditProductsViewModel> EditProductView(int productId)
        {
            var product = await _productRepository.GetProductsById(productId);

            var result = new EditProductsViewModel
            {
                CategoriesId = product.CategoriesId,
                ProductPrice = product.ProductPrice,
                NumberOfProducts = product.NumberOfProducts,
                Description = product.Description,
                Title = product.Title,
                SubTitle = product.SubTitle,
            };

            return result;
        }

        public async Task<bool> EditProduct(EditProductsViewModel viewModel, int productId)
        {
            var product = await _productRepository.GetProductsById(productId);

            if (product == null) return false;

            product.ProductPrice = viewModel.ProductPrice;
            product.NumberOfProducts = viewModel.NumberOfProducts;
            product.Description = viewModel.Description.SanitizeText();
            product.Title = viewModel.Title.SanitizeText();
            product.SubTitle = viewModel.SubTitle.SanitizeText();
            product.CategoriesId = viewModel.CategoriesId;

            _productRepository.UpdateProducts(product);
            await _productRepository.SaveChange();

            return true;
        }

        public async Task<bool> EditImgProduct(EditImgProductsViewModel viewModel, int productId)
        {
            if (viewModel.Img != null)
            {
                var product = await _productRepository.GetProductsById(productId);

                string filePath = "";
                viewModel.ImgName = RandomCodeGenerators.CreateFileCode() + Path.GetExtension(viewModel.Img.FileName);
                filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/content/gallery", viewModel.ImgName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    viewModel.Img.CopyTo(stream);
                }

                product.Img = viewModel.ImgName;

                _productRepository.UpdateProducts(product);
                await _productRepository.SaveChange();
            }

            return true;
        }

        public async Task<bool> DeleteProduct(int productId)
        {
            var product = await _productRepository.GetProductsById(productId);

            if (product == null) return false;

            product.IsDelete = true;

            _productRepository.UpdateProducts(product);
            await _productRepository.SaveChange();

            return true;
        }

        public async Task<bool> RestoreProduct(int productId)
        {
            var product = await _productRepository.GetProductsById(productId);

            if (product == null) return false;

            product.IsDelete = false;

            _productRepository.UpdateProducts(product);
            await _productRepository.SaveChange();

            return true;
        }

        public async Task<List<Categories>> GetAllCategory()
        {
            return await _productRepository.GetAllCategories(); 
        }

        #endregion

        #region Order

        public async Task<List<Order>> GetAllPostedOrders(int orderId)
        {
            return await _productRepository.GetAllPostedOrders(orderId);
        }

        public async Task<List<Order>> GetAllPendingOrders(int orderId)
        {
            return await _productRepository.GetAllPendingOrders(orderId);
        }

        public int PostedOrdersCount()
        {
            return _productRepository.PostedOrdersCount();
        }

        public int PendingOrdersCount()
        {
            return _productRepository.PendingOrdersCount();
        }

        public async Task<Order> GetOrderDetail(int orderId)
        {
            return await _productRepository.GetOrderDetail(orderId);
        }

        public async Task<Address> GetAllAddresses(int userId)
        {
            return await _productRepository.GetAllAddresses(userId);
        }

        public async Task<List<Order>> GetOrdersForProfile()
        {
            return await _productRepository.GetOrdersForProfile();
        }

        public async Task<bool> PostedOrder(PostedViewModel viewModel, int orderId)
        {
            var order = await _productRepository.GetOrderById(orderId);

            if (order == null) return false;

            order.Posted = true;

            _productRepository.UpdateOrders(order);
            await _productRepository.SaveChange();

            return true;
        }

        public async Task<bool> PendingOrder(PostedViewModel viewModel, int orderId)
        {
            var order = await _productRepository.GetOrderById(orderId);

            if (order == null) return false;

            order.Posted = false;

            _productRepository.UpdateOrders(order);
            await _productRepository.SaveChange();

            return true;
        }

        #endregion

        #region Comment

        public async Task<List<Comment>> GetALLComments(int commentId)
        {
            return await _productRepository.GetALLComments(commentId);
        }

        public async Task<Comment> GetCommentById(int commentId)
        {
            return await _productRepository.GetCommentById(commentId);
        }

        public int CommentsCount()
        {
            return _productRepository.CommentsCount();
        }

        public async Task<bool> DeleteComment(int commentId)
        {
            var comment = await _productRepository.GetCommentById(commentId);

            if (comment == null) return false;

            comment.IsDelete = true;

            _productRepository.UpdateComment(comment);
            await _productRepository.SaveChange();

            return true;
        }

        public async Task<bool> RestoreComment(int commentId)
        {
            var comment = await _productRepository.GetCommentById(commentId);

            if (comment == null) return false;

            comment.IsDelete = false;

            _productRepository.UpdateComment(comment);
            await _productRepository.SaveChange();

            return true;
        }

        #endregion
    }

    //---------------------------------------------------------------

    public class BlogService : IBlogService
    {
        #region Ctor

        private readonly IBlogRepository _blogRepository;

        public BlogService(IBlogRepository blogRepository)
        {
            _blogRepository = blogRepository;
        }

        #endregion

        #region WeBloge

        public async Task<bool> AddWeBloge(BlogeViewModel viewModel)
        {
            string filePath = "";
            viewModel.ImgName = RandomCodeGenerators.CreateFileCode() + Path.GetExtension(viewModel.Img.FileName);
            filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/content/gallery", viewModel.ImgName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                viewModel.Img.CopyTo(stream);
            }

            WeBloge weBloge = new WeBloge()
            {
                Img = viewModel.ImgName.SanitizeText(),
                Title = viewModel.Title.SanitizeText(),
                Content = viewModel.Content.SanitizeText(),
                ShortDescription = viewModel.ShortDescription.SanitizeText(),
            };

            _blogRepository.AddBlog(weBloge);
            await _blogRepository.SaveChange();

            return true;
        }

        public int BlogsCount()
        {
            return _blogRepository.BlogsCount();
        }

        public async Task<EditBlogeViewModel> EditBlogView(int blogId)
        {
            var blog = await _blogRepository.GetBlogById(blogId);

            var result = new EditBlogeViewModel
            {
                Content = blog.Content,
                ShortDescription = blog.ShortDescription,
                Title = blog.Title
            };

            return result;
        }

        public async Task<bool> EditBlog(EditBlogeViewModel viewModel, int blogId)
        {
            var blog = await _blogRepository.GetBlogById(blogId);

            if (blog == null) return false;

            blog.Title = viewModel.Title.SanitizeText();
            blog.Content = viewModel.Content.SanitizeText();
            blog.ShortDescription = viewModel.ShortDescription.SanitizeText();

            _blogRepository.UpdateBlog(blog);
            await _blogRepository.SaveChange();

            return true;
        }

        public async Task<bool> EditImgBlog(EditImgBlogeViewModel viewModel, int blogId)
        {
            if (viewModel.Img != null)
            {
                var blog = await _blogRepository.GetBlogById(blogId);

                string filePath = "";
                viewModel.ImgName = RandomCodeGenerators.CreateFileCode() + Path.GetExtension(viewModel.Img.FileName);
                filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/content/gallery", viewModel.ImgName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    viewModel.Img.CopyTo(stream);
                }

                if (blog == null) return false;

                blog.Img = viewModel.ImgName.SanitizeText();

                _blogRepository.UpdateBlog(blog);
                await _blogRepository.SaveChange();
            }

            return true;
        }

        public async Task<List<WeBloge>> GetAllBlogs(int blogId)
        {
            return await _blogRepository.GetAllBlogs(blogId);
        }

        public async Task<WeBloge> GetBlogById(int blogId)
        {
            return await _blogRepository.GetBlogById(blogId);
        }

        public async Task<bool> DeleteBlog(int blogId)
        {
            var blog = await _blogRepository.GetBlogById(blogId);

            if (blog == null) return false;

            blog.IsDelete = true;

            _blogRepository.UpdateBlog(blog);
            await _blogRepository.SaveChange();

            return true;
        }

        public async Task<bool> RestoreBlog(int blogId)
        {
            var blog = await _blogRepository.GetBlogById(blogId);

            if (blog == null) return false;

            blog.IsDelete = false;

            _blogRepository.UpdateBlog(blog);
            await _blogRepository.SaveChange();

            return true;
        }

        #endregion

        #region Comment

        public async Task<List<BlogComment>> GetALLComments(int commentId)
        {
            return await _blogRepository.GetALLComments(commentId);
        }

        public int CommentsCount()
        {
            return _blogRepository.CommentsCount();
        }

        public async Task<BlogComment> GetCommentById(int commentId)
        {
            return await _blogRepository.GetCommentById(commentId);
        }

        public async Task<bool> DeleteComment(int commentId)
        {
            var comment = await _blogRepository.GetCommentById(commentId);

            if (comment == null) return false;

            comment.IsDelete = true;

            _blogRepository.UpdateComment(comment);
            await _blogRepository.SaveChange();

            return true;
        }

        public async Task<bool> RestoreComment(int commentId)
        {
            var comment = await _blogRepository.GetCommentById(commentId);

            if (comment == null) return false;

            comment.IsDelete = false;

            _blogRepository.UpdateComment(comment);
            await _blogRepository.SaveChange();

            return true;
        }

        #endregion
    }

    //---------------------------------------------------------------

    public class SettingService : ISettingService
    {
        #region Ctor

        private readonly ISettingRepository _settingRepository;

        public SettingService(ISettingRepository settingRepository)
        {
            _settingRepository = settingRepository;
        }

        #endregion

        #region Social Networks

        public async Task<bool> AddSocial(SocialViewModel viewModel)
        {
            var result = new Social
            {
                Instagram = viewModel.Instagram.SanitizeText(),
                Facebook = viewModel.Facebook.SanitizeText(),
                Twitter = viewModel.Twitter.SanitizeText(),
                YouTube = viewModel.YouTube.SanitizeText(),
            };

            _settingRepository.AddSocial(result);
            await _settingRepository.SaveChange();

            return true;
        }

        public async Task<bool> EditSocial(EditSocialViewModel viewModel, int socialId)
        {
            var social = await _settingRepository.GetSocialById(socialId);

            if (social == null) return false;

            social.YouTube = viewModel.YouTube.SanitizeText();
            social.Facebook = viewModel.Facebook.SanitizeText();
            social.Twitter = viewModel.Twitter.SanitizeText();
            social.Instagram = viewModel.Instagram.SanitizeText();

            _settingRepository.UpdateSocial(social);
            await _settingRepository.SaveChange();

            return true;
        }

        public async Task<EditSocialViewModel> EditSocialView(int socialId)
        {
            var social = await _settingRepository.GetSocialById(socialId);

            var result = new EditSocialViewModel
            {
                Instagram = social.Instagram,
                Facebook = social.Facebook,
                YouTube = social.YouTube,
                Twitter = social.Twitter,
            };

            return result;
        }

        public async Task<Social> GetAdminSocial()
        {
            return await _settingRepository.GetAdminSocial();
        }

        #endregion

        #region Footer Label

        public async Task<bool> AddLabel(FooterLabelViewModel viewModel)
        {
            var result = new FooterLabel()
            {
                Title = viewModel.Title.SanitizeText(),
            };

            _settingRepository.AddLabel(result);
            await _settingRepository.SaveChange();

            return true;
        }

        public async Task<FooterLabelViewModel> EditLabelView(int labelId)
        {
            var label = await _settingRepository.GetLabelById(labelId);

            var result = new FooterLabelViewModel
            {
                Title = label.Title,
            };

            return result;
        }

        public async Task<bool> EditLabel(FooterLabelViewModel viewModel, int labelId)
        {
            var label = await _settingRepository.GetLabelById(labelId);

            label.Title = viewModel.Title.SanitizeText();

            _settingRepository.UpdateLabel(label);
            await _settingRepository.SaveChange();

            return true;
        }

        public async Task<List<FooterLabel>> GetListOfLabel()
        {
            return await _settingRepository.GetListOfLabel();
        }

        #endregion

        #region Footer Link

        public async Task<bool> AddLink(FooterLinkViewModel viewModel)
        {
            var result = new FooterLink()
            {
                Title = viewModel.Title.SanitizeText(),
                Url = viewModel.Url.SanitizeText(),
                FooterLabelId = viewModel.FooterLabelId,
            };

            _settingRepository.AddLink(result);
            await _settingRepository.SaveChange();

            return true;
        }

        public async Task<FooterLinkViewModel> EditLinkView(int linkId)
        {
            var link = await _settingRepository.GetLinkById(linkId);

            var result = new FooterLinkViewModel
            {
                Title = link.Title,
                Url = link.Url,
                FooterLabelId = link.FooterLabelId,
            };

            return result;
        }

        public async Task<bool> EditLink(FooterLinkViewModel viewModel, int linkId)
        {
            var link = await _settingRepository.GetLinkById(linkId);

            if (link == null) return false;

            link.Title = viewModel.Title.SanitizeText();
            link.Url = viewModel.Url.SanitizeText();
            link.FooterLabelId = viewModel.FooterLabelId;

            _settingRepository.UpdateLink(link);
            await _settingRepository.SaveChange();

            return true;
        }

        public async Task<List<FooterLink>> GetListOfLink(int linkId)
        {
            return await _settingRepository.GetListOfLink(linkId);
        }

        public async Task<List<FooterLabel>> GetListLabel()
        {
            return await _settingRepository.GetListLabel();
        }

        public int FooterLinkCount()
        {
            return _settingRepository.FooterLinkCount();
        }

        public async Task<bool> DeleteLink(int linkId)
        {
            var link = await _settingRepository.GetLinkById(linkId);

            if (link == null) return false;

            _settingRepository.RemoveFooterLink(link);
            await _settingRepository.SaveChange();

            return true;
        }

        #endregion

        #region User

        public async Task<List<User>> GetUsers(int userId)
        {
            return await _settingRepository.GetUsers(userId);
        }

        public int GetUserCount()
        {
            return _settingRepository.GetUserCount();
        }

        public async Task<List<User>> GetAllUser()
        {
            return await _settingRepository.GetAllUser();
        }

        public async Task<bool> BanUser(int userId)
        {
            var user = await _settingRepository.GetUserById(userId);

            if (user == null) return false;

            user.IsBan = true;

            _settingRepository.UpdateUser(user);
            await _settingRepository.SaveChange();

            return true;
        }

        public async Task<bool> ReBanUser(int userId)
        {
            var user = await _settingRepository.GetUserById(userId);

            if (user == null) return false;

            user.IsBan = false;

            _settingRepository.UpdateUser(user);
            await _settingRepository.SaveChange();

            return true; 
        }

        #endregion

        #region Admin Logo

        public async Task<bool> AddAdminLogo(AdminLogoViewModel viewModel)
        {
            string filePath = "";
            viewModel.ImgName = RandomCodeGenerators.CreateFileCode() + Path.GetExtension(viewModel.Img.FileName);
            filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/content/gallery", viewModel.ImgName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                viewModel.Img.CopyTo(stream);
            }

            var result = new AdminLogo
            {
                Img = viewModel.ImgName.SanitizeText()
            };

            _settingRepository.AddAdminLogo(result);
            await _settingRepository.SaveChange();

            return true;
        }

        public async Task<bool> EditLogo(AdminLogoViewModel viewModel, int logoId)
        {
            if (viewModel.Img != null)
            {
                var adminLogo = await _settingRepository.GetAdminLogoById(logoId);

                if (adminLogo == null) return false;

                string filePath = "";
                viewModel.ImgName = RandomCodeGenerators.CreateFileCode() + Path.GetExtension(viewModel.Img.FileName);
                filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/content/gallery", viewModel.ImgName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    viewModel.Img.CopyTo(stream);
                }

                adminLogo.Img = viewModel.ImgName.SanitizeText();

                _settingRepository.UpdateAdminLogo(adminLogo);
                await _settingRepository.SaveChange();
            }

            return true;
        }

        public async Task<AdminLogo> GetAdminLogo()
        {
            return await _settingRepository.GetAdminLogo();
        }

        #endregion

        #region Site Logo

        public async Task<bool> AddSiteLogo(SiteLogoViewModel viewModel)
        {
            string filePath = "";
            viewModel.ImgName = RandomCodeGenerators.CreateFileCode() + Path.GetExtension(viewModel.Img.FileName);
            filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/content/gallery", viewModel.ImgName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                viewModel.Img.CopyTo(stream);
            }

            var result = new SiteLogo
            {
                Img = viewModel.ImgName.SanitizeText(),
                SiteDescription = viewModel.SiteDescription.SanitizeText(),
            };

            _settingRepository.AddSiteLogo(result);
            await _settingRepository.SaveChange();

            return true;
        }

        public async Task<SiteDescriptionViewModel> EditSiteDescriptionView(int logoId)
        {
            var logo = await _settingRepository.GetSiteLogoById(logoId);

            var result = new SiteDescriptionViewModel
            {
                SiteDescription = logo.SiteDescription,
            };

            return result;
        }

        public async Task<bool> EditSiteDescription(SiteDescriptionViewModel viewModel, int logoId)
        {
            var logo = await _settingRepository.GetSiteLogoById(logoId);

            if (logo == null) return false;

            logo.SiteDescription = viewModel.SiteDescription.SanitizeText();

            _settingRepository.UpdateSiteLogo(logo);
            await _settingRepository.SaveChange();

            return true;
        }

        public async Task<bool> EditSiteImg(SiteImgViewModel viewModel, int logoId)
        {
            if (viewModel.Img != null)
            {
                var logo = await _settingRepository.GetSiteLogoById(logoId);

                if (logo == null) return false;

                string filePath = "";
                viewModel.ImgName = RandomCodeGenerators.CreateFileCode() + Path.GetExtension(viewModel.Img.FileName);
                filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/content/gallery", viewModel.ImgName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    viewModel.Img.CopyTo(stream);
                }

                logo.Img = viewModel.ImgName.SanitizeText();

                _settingRepository.UpdateSiteLogo(logo);
                await _settingRepository.SaveChange();
            }

            return true;
        }

        public async Task<SiteLogo> GetSiteLogo()
        {
            return await _settingRepository.GetSiteLogo();
        }

        #endregion

        #region Copy Right

        public async Task<bool> AddCopyRight(CopyRightViewModel viewModel)
        {
            var result = new CopyRight
            {
                AdminText = viewModel.AdminText.SanitizeText(),
                SiteText = viewModel.SiteText.SanitizeText(),
            };

            _settingRepository.AddCopyRight(result);
            await _settingRepository.SaveChange();

            return true;
        }

        public async Task<CopyRightViewModel> EditCopyRightView(int Id)
        {
            var copyRight = await _settingRepository.GetCopyRightById(Id);

            var result = new CopyRightViewModel
            {
                AdminText = copyRight.AdminText,
                SiteText = copyRight.SiteText,
            };

            return result;
        }

        public async Task<bool> EditCopyRight(CopyRightViewModel viewModel, int Id)
        {
            var copyRight = await _settingRepository.GetCopyRightById(Id);

            if (copyRight == null) return false;

            copyRight.AdminText = viewModel.AdminText.SanitizeText();
            copyRight.SiteText = viewModel.SiteText.SanitizeText();

            _settingRepository.UpdateCopyRight(copyRight);
            await _settingRepository.SaveChange();

            return true;
        }

        public async Task<CopyRight> GetCopyRight()
        {
            return await _settingRepository.GetCopyRight();
        }

        #endregion
    }
}
