using Shop.Domain.Entities.Account;
using Shop.Domain.ViewModels.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Services.Interfaces
{
    public interface IUserService
    {
        #region Register

        Task<RegisterUserResult> RegisterUser(RegisterUserViewModel viewModel);

        #endregion

        #region Login

        Task<LoginUserResult> LoginUser(LoginUserViewModel viewModel);
        Task<User> GetUserByEmail(string email);

        #endregion

        #region Activation Email

        Task<bool> ActivationEmail(string activationCode);

        #endregion

        #region Forgot Password

        Task<ForgotPasswordResult> ForgotPassword(ForgotPasswordViewModel viewModel);

        #endregion

        #region Reset Password
        
        Task<ResetPasswordResult> ResetPassword(ResetPasswordViewModel viewModel);
        Task<User> GetUserActivationCode(string activationCode);

        #endregion

        #region User Panel

        Task<EditPanelViewModel> ShowEditPanel(int userId);
        Task<EditPanelResult> EditPanel(EditPanelViewModel viewModel, int userId);
        Task<User> GetUserById(int userId);

        #endregion

        #region Change Password

        Task<bool> ChangePassword(ChangePasswordViewModel viewModel, int userId);

        #endregion

        #region Admin

        Task<bool> CheckUserPermission(int permissionId, int userId);

        #endregion
    }
}
