using Shop.Application.Generators;
using Shop.Application.Security;
using Shop.Application.Services.Interfaces;
using Shop.Application.Statics;
using Shop.Domain.Entities.Account;
using Shop.Domain.Interfaces;
using Shop.Domain.ViewModels.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Services.Implementations
{
    public class UserService : IUserService
    {
        #region Ctor

        private readonly IUserRepository _userRepository;
        private IEmailService _emailService;

        public UserService(IUserRepository userRepository, IEmailService emailService)
        {
            _userRepository = userRepository;
            _emailService = emailService;
        }

        #endregion

        #region Register

        public async Task<RegisterUserResult> RegisterUser(RegisterUserViewModel viewModel)
        {
            if (await _userRepository.IsExistEmail(viewModel.Email.Trim().SanitizeText()))
            {
                return RegisterUserResult.EmailExist;
            }

            var user = new User
            {
                Email = viewModel.Email.Trim().SanitizeText(),
                Password = PasswordHelper.EncodePasswordMd5(viewModel.Password.SanitizeText()),
                EmailActivationCode = CodeGenerators.CreateActivationCode()
            };

            _userRepository.AddUser(user);
            _userRepository.SaveChange();

            #region Send Activation Email

            var body = $@"
                <div> To forget your password, click on the link below. </div>
                <a href='{PathTools.SiteAddress}/Reset-Password/{user.EmailActivationCode}'>Forgot password</a>
                ";

            await _emailService.SendEmail(user.Email, "Forgot password", body);

            #endregion

            return RegisterUserResult.Success;
        }

        #endregion

        #region Login

        public async Task<LoginUserResult> LoginUser(LoginUserViewModel viewModel)
        {
            var user = await _userRepository.GetUserByEmail(viewModel.Email.Trim().SanitizeText());
            var password = PasswordHelper.EncodePasswordMd5(viewModel.Password.SanitizeText());

            if (user == null) return LoginUserResult.NotFound;
            if (user.Password != password) return LoginUserResult.NotFound;
            if (user.IsDelete) return LoginUserResult.NotFound;
            if (user.IsBan) return LoginUserResult.IsBan;
            if (!user.IsEmailActive) return LoginUserResult.NotActive;

            return LoginUserResult.Success;
        }

        public async Task<User> GetUserByEmail(string email)
        {
            return await _userRepository.GetUserByEmail(email);
        }

        #endregion

        #region Activation Email

        public async Task<bool> ActivationEmail(string activationCode)
        {
            var user = await _userRepository.GetUserActivationCode(activationCode);

            if (user == null) return false;
            if (user.IsDelete) return false;
            if (user.IsBan) return false;

            user.IsEmailActive = true;
            user.EmailActivationCode = CodeGenerators.CreateActivationCode();

            _userRepository.UpdateUser(user);
            _userRepository.SaveChange();

            return true;
        }

        #endregion

        #region Forgot Password

        public async Task<ForgotPasswordResult> ForgotPassword(ForgotPasswordViewModel viewModel)
        {
            var user = await _userRepository.GetUserByEmail(viewModel.Email.Trim().SanitizeText());

            if (user == null) return ForgotPasswordResult.NotFound;
            if (user.IsDelete) return ForgotPasswordResult.NotFound;
            if (user.IsBan) return ForgotPasswordResult.IsBan;

            #region Send Activation Email

            var body = $@"
                <div> برای فراموشی کلمه عبور روی لینک زیر کلیک کنید . </div>
                <a href='{PathTools.SiteAddress}/Reset-Password/{user.EmailActivationCode}'>فراموشی کلمه عبور</a>
                ";

            await _emailService.SendEmail(user.Email, "فراموشی کلمه عبور", body);

            #endregion

            return ForgotPasswordResult.Success;
        }

        #endregion

        #region Reset Password

        public async Task<ResetPasswordResult> ResetPassword(ResetPasswordViewModel viewModel)
        {
            var user = await _userRepository.GetUserActivationCode(viewModel.ActivationCode);
            var password = PasswordHelper.EncodePasswordMd5(viewModel.Password.SanitizeText());

            if (user == null) return ResetPasswordResult.NotFound;
            if (user.IsDelete) return ResetPasswordResult.NotFound;
            if (user.IsBan) return ResetPasswordResult.IsBan;

            user.Password = password;
            user.IsEmailActive = true;
            user.EmailActivationCode = CodeGenerators.CreateActivationCode();
            
            _userRepository.UpdateUser(user);
            _userRepository.SaveChange();

            return ResetPasswordResult.Success;
        }

        public async Task<User> GetUserActivationCode(string activationCode)
        {
            return await _userRepository.GetUserActivationCode(activationCode);
        }

        #endregion

        #region User Panel

        public async Task<EditPanelViewModel> ShowEditPanel(int userId)
        {
            var user = await _userRepository.GetUserById(userId);

            var result = new EditPanelViewModel
            {
                LastName = user.LastName,
                FirstName = user.FirstName,
            };

            return result;
        }

        public async Task<EditPanelResult> EditPanel(EditPanelViewModel viewModel, int userId)
        {
            var user = await _userRepository.GetUserById(userId);

            if (user.IsBan) return EditPanelResult.IsBan;

            user.FirstName = viewModel.FirstName.SanitizeText();
            user.LastName = viewModel.LastName.SanitizeText();

            _userRepository.UpdateUser(user);
            _userRepository.SaveChange();

            return EditPanelResult.Success;
        }

        public async Task<User> GetUserById(int userId)
        {
            return await _userRepository.GetUserById(userId);
        }

        #endregion

        #region Change Password

        public async Task<bool> ChangePassword(ChangePasswordViewModel viewModel, int userId)
        {
            var user = await _userRepository.GetUserById(userId);

            if (user == null || user.IsDelete || user.IsBan) return false;

            var currentPassword = PasswordHelper.EncodePasswordMd5(viewModel.CurrentPassword.SanitizeText());

            if (user.Password != currentPassword) return false;

            var newPassword = PasswordHelper.EncodePasswordMd5(viewModel.NewPassword.SanitizeText());

            user.Password = newPassword;

            _userRepository.UpdateUser(user);
            _userRepository.SaveChange();

            return true;
        }

        #endregion

        #region Admin

        public async Task<bool> CheckUserPermission(int permissionId, int userId)
        {
            var user = await _userRepository.GetUserById(userId);

            if (user == null) return false;

            if (user.IsAdmin) return true;

            return await _userRepository.CheckUserHasPermission(user.Id, permissionId);
        }

        #endregion
    }
}
