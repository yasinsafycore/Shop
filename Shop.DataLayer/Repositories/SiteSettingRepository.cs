using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Shop.DataLayer.Context;
using Shop.Domain.Entities.SiteSetting;
using Shop.Domain.Interfaces;

namespace Shop.DataLayer.Repositories
{
    public class SiteSettingRepository : ISiteSettingRepository
    {
        #region Ctor

        private ShopDBContext _context;

        public SiteSettingRepository(ShopDBContext context)
        {
            _context = context;
        }

        #endregion

        public async Task<EmailSetting> GetDefaultEmail()
        {
            return await _context.EmailSettings.FirstOrDefaultAsync(s => !s.IsDelete && s.IsDefault);
        }
    }
}
