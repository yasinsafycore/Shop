using Microsoft.Extensions.DependencyInjection;
using Shop.Application.Services.Implementations;
using Shop.Application.Services.Interfaces;
using Shop.DataLayer.Repositories;
using Shop.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.IoC
{
    public class DependencyContainer
    {
        public static void RegisterDependencies(IServiceCollection services)
        {
            #region Services

            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<IAdminService, AdminService>();
            services.AddScoped<IShopService, ShopService>();
            services.AddScoped<IProductAdminService, ProductAdminService>();
            services.AddScoped<IBlogService, BlogService>();
            services.AddScoped<IWeBlogService, WeBlogService>();
            services.AddScoped<ISettingService, SettingService>();

            #endregion

            #region Repositories

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ISiteSettingRepository, SiteSettingRepository>();
            services.AddScoped<IAdminRepository, AdminRepository>();
            services.AddScoped<IShopRepository, ShopRepository>();
            services.AddScoped<IProductAdminRepository, ProductAdminRepository>();
            services.AddScoped<IBlogRepository, BlogRepository>();
            services.AddScoped<IWeBlogRepository, WeBlogRepository>();
            services.AddScoped<ISettingRepository, SettingRepository>();

            #endregion
        }
    }
}
