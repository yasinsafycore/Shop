using Microsoft.EntityFrameworkCore;
using Shop.DataLayer.Context;
using Shop.Domain.Entities.Account;
using Shop.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.DataLayer.Repositories
{
    public class UserRepository : IUserRepository
    {
        #region Ctor

        private readonly ShopDBContext _context;

        public UserRepository(ShopDBContext context)
        {
            _context = context;
        }

        #endregion

        #region Register

        public void AddUser(User user)
        {
            _context.Users.Add(user);
        }

        public async Task<bool> IsExistEmail(string email)
        {
            return await _context.Users.AnyAsync(p => p.Email == email);
        }

        #endregion

        #region Login

        public async Task<User> GetUserByEmail(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(p => p.Email == email);
        }

        public async Task<User> GetUserById(int userId)
        {
            return await _context.Users.SingleOrDefaultAsync(p => p.Id == userId);
        }

        #endregion

        #region Activation Email

        public async Task<User> GetUserActivationCode(string activationCode)
        {
            return await _context.Users.FirstOrDefaultAsync(p => p.EmailActivationCode == activationCode);
        }

        #endregion

        #region General

        public void SaveChange()
        {
            _context.SaveChanges();
        }
        
        public void UpdateUser(User user)
        {
            _context.Users.Update(user);
        }

        #endregion

        #region Admin

        public async Task<bool> CheckUserHasPermission(int userId, int permissionId)
        {
            return await _context.UserPermissions
                .AnyAsync(s => s.UserId == userId && s.PermissionId == permissionId);
        }

        #endregion
    }
}
