﻿using Shop.Domain.Entities.SiteSetting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Domain.Interfaces
{
    public interface ISiteSettingRepository
    {
        Task<EmailSetting> GetDefaultEmail();
    }
}
