using Microsoft.Win32;
using Shop.Domain.Entities.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Domain.Interfaces
{
    public interface IUserRepository
    {
        #region Register

        void AddUser(User user);

        Task<bool> IsExistEmail(string email);

        #endregion

        #region Login

        Task<User> GetUserByEmail(string email);    

        Task<User> GetUserById(int userId);

        #endregion

        #region Activation Email

        Task<User> GetUserActivationCode(string activationCode);

        #endregion

        #region Forgot Password

        #endregion

        #region Reset Password

        #endregion

        #region General

        void UpdateUser(User user);
        void SaveChange();

        #endregion

        #region Admin

        Task<bool> CheckUserHasPermission(int userId, int permissionId);

        #endregion
    }
}
