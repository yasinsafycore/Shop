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
using Shop.Domain.Entities.SiteSetting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.DataLayer.Context
{
    public class ShopDBContext : DbContext
    {
        #region Ctor

        public ShopDBContext(DbContextOptions<ShopDBContext> options) : base(options)
        {

        }

        #endregion

        #region DBSet

        public DbSet<User> Users { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<UserPermission> UserPermissions { get; set; }
        public DbSet<EmailSetting> EmailSettings { get; set; }
        public DbSet<HeaderSlider> HeaderSliders { get; set; }
        public DbSet<HeaderBanner> HeaderBanners { get; set; }
        public DbSet<MiddleBanner> MiddleBanners { get; set; }
        public DbSet<AdminProfile> AdminProfiles { get; set; }
        public DbSet<Categories> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<WeBloge> WeBloges { get; set; }
        public DbSet<BlogComment> BlogComments { get; set; }
        public DbSet<ContactUs> ContactUs { get; set; }
        public DbSet<AboutUs> AboutUs { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<Social> Socials { get; set; }
        public DbSet<FooterLabel> FooterLabels { get; set; }
        public DbSet<FooterLink> FooterLinks { get; set; }
        public DbSet<AdminLogo> AdminLogos { get; set; }
        public DbSet<SiteLogo> SiteLogos { get; set; }
        public DbSet<CopyRight> CopyRights { get; set; }

        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var relation in modelBuilder.Model.GetEntityTypes().SelectMany(s => s.GetForeignKeys()))
            {
                relation.DeleteBehavior = DeleteBehavior.Restrict;
            }

            #region Seed Data

            var date = DateTime.MinValue;

            modelBuilder.Entity<EmailSetting>().HasData(new EmailSetting()
            {
                CreateDate = date,
                DisplayName = "Shop",
                EnableSSL = true,

                //Email
                From = "",
                Id = 1,
                IsDefault = true,
                IsDelete = false,

                //emailPassword
                Password = "",
                Port = 587,
                SMTP = "smtp.gmail.com"
            });

            #endregion

            base.OnModelCreating(modelBuilder);
        }

    }
}
