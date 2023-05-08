using GoogleReCaptcha.V3.Interface;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Win32;
using NuGet.Protocol.Plugins;
using Shop.Application.Services.Interfaces;
using Shop.Domain.ViewModels.Account;
using System.Drawing;
using System.Security.Claims;
using Shop.Application.Extensions;
using Microsoft.AspNetCore.Authorization;
using Shop.Web.ActionFilters;

namespace Shop.Web.Controllers
{
    public class AccountController : BaseController
    {
        #region Ctor

        private readonly IUserService _userService;
        private ICaptchaValidator _captchaValidator;

        public AccountController(IUserService userService, ICaptchaValidator captchaValidator)
        {
            _userService = userService;
            _captchaValidator = captchaValidator;
        }

        #endregion

        #region Register

        [HttpGet]
        [RedirectActionFilters]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        [RedirectActionFilters]
        public async Task<IActionResult> Register(RegisterUserViewModel viewModel)
        {
            #region Captcha

            if (!await _captchaValidator.IsCaptchaPassedAsync(viewModel.Captcha))
            {
                TempData[ErrorMessage] = "Captcha validation failed. Please try again";
                return View(viewModel);
            }

            #endregion

            if (ModelState.IsValid)
            {
                var result = await _userService.RegisterUser(viewModel);
                switch (result)
                {
                    case RegisterUserResult.Success:
                        TempData[SuccessMessage] = "The operation was successful";
                        return RedirectToAction("Login", "Account");
                    case RegisterUserResult.EmailExist:
                        TempData[ErrorMessage] = "The email entered already exists";
                        break;
                }
            }

            return View(viewModel);
        }

        #endregion

        #region Login

        [HttpGet("Login")]
        [RedirectActionFilters]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost("Login"), ValidateAntiForgeryToken]
        [RedirectActionFilters]
        public async Task<IActionResult> Login(LoginUserViewModel viewModel)
        {
            #region Captcha

            if (!await _captchaValidator.IsCaptchaPassedAsync(viewModel.Captcha))
            {
                TempData[ErrorMessage] = "Captcha validation failed. Please try again";
                return View(viewModel);
            }

            #endregion

            if (ModelState.IsValid)
            {
                var result = await _userService.LoginUser(viewModel);
                switch (result)
                {
                    case LoginUserResult.Success:
                        var user = await _userService.GetUserByEmail(viewModel.Email);

                        #region Login User

                        var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    };

                        var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                        var principal = new ClaimsPrincipal(identity);
                        var properties = new AuthenticationProperties { IsPersistent = viewModel.RememberMe };

                        await HttpContext.SignInAsync(principal, properties);

                        #endregion

                        return LocalRedirect("/");

                    case LoginUserResult.NotActive:
                        TempData[WarningMessage] = "To login, first activate your email";
                        break;
                    case LoginUserResult.NotFound:
                        TempData[ErrorMessage] = "The desired user was not found";
                        break;
                    case LoginUserResult.IsBan:
                        TempData[WarningMessage] = "Unfortunately, your account has been blocked by the admin and you no longer have access";
                        break;
                }

            }

            return View(viewModel);

        }

        #endregion
        
        #region LogOut

        [HttpGet("Logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return LocalRedirect("/");
        }

        #endregion

        #region Activation Email

        [HttpGet("Activate-Email/{activationCode}")]
        [RedirectActionFilters]
        public async Task<IActionResult> ActivationEmail(string activationCode)
        {
            var result = await _userService.ActivationEmail(activationCode);

            if (result)
            {
                TempData[SuccessMessage] = "Your account has been successfully activated";
            }
            else
            {
                TempData[ErrorMessage] = "Account activation failed";
            }
            return RedirectToAction("Login", "Account");
        }

        #endregion

        #region Forgot Password

        [HttpGet]
        [RedirectActionFilters]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost,ValidateAntiForgeryToken]
        [RedirectActionFilters]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel viewModel)
        {
            #region Captcha

            if (!await _captchaValidator.IsCaptchaPassedAsync(viewModel.Captcha))
            {
                TempData[ErrorMessage] = "Captcha validation failed. Please try again";
                return View(viewModel);
            }

            #endregion

            if (ModelState.IsValid)
            {
                var result = await _userService.ForgotPassword(viewModel);
                switch (result)
                {
                    case ForgotPasswordResult.Success:

                        TempData[InfoMessage] = "Password reset link has been sent to your email";
                        return RedirectToAction("Login", "Account");
                    case ForgotPasswordResult.NotFound:
                        TempData[ErrorMessage] = "The user with the entered email address was not found";
                        break;
                    case ForgotPasswordResult.IsBan:
                        TempData[WarningMessage] = "Unfortunately, your account has been blocked by the admin and you no longer have access";
                        break;
                }
            }

            return View(viewModel);
        }

        #endregion

        #region Reset Password

        [HttpGet("Reset-Password/{activationCode}")]
        [RedirectActionFilters]
        public async Task<IActionResult> ResetPassword(string activationCode)
        {
            var user = await _userService.GetUserActivationCode(activationCode);

            if (user == null || user.IsDelete) return NotFound();

            return View(new ResetPasswordViewModel
            {
                ActivationCode = user.EmailActivationCode,
            });
        }

        [HttpPost("Reset-Password/{activationCode}"),ValidateAntiForgeryToken]
        [RedirectActionFilters]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel viewModel, string activationCode)
        {
            if (ModelState.IsValid)
            {
                var result = await _userService.ResetPassword(viewModel);
                switch (result)
                {
                    case ResetPasswordResult.Success:
                        TempData[SuccessMessage] = "The operation was successful";
                        return RedirectToAction("Login", "Account");
                    case ResetPasswordResult.NotFound:
                        TempData[ErrorMessage] = "The desired user was not found";
                        break;
                    case ResetPasswordResult.IsBan:
                        TempData[WarningMessage] = "Unfortunately, your account has been blocked by the admin and you no longer have access";
                        break;
                }
            }

            return View(viewModel);
        }

        #endregion

        #region User Panel

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> EditPanel()
        {
            var result = await _userService.ShowEditPanel(HttpContext.User.GetUserId());

            return View(result);
        }

        [Authorize]
        [HttpPost,ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPanel(EditPanelViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var result = await _userService.EditPanel(viewModel, HttpContext.User.GetUserId());

                switch (result)
                {
                    case EditPanelResult.Success:
                        TempData[SuccessMessage] = "The operation was successful";
                        return View(viewModel);
                    case EditPanelResult.IsBan:
                        TempData[WarningMessage] = "Unfortunately, your account has been blocked by the admin and you no longer have access";
                        break;
                }
            }

            return View(viewModel);
        }

        #endregion

        #region Change Password

        [HttpGet]
        public IActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost,ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var result = await _userService.ChangePassword(viewModel, HttpContext.User.GetUserId());

                if (result)
                {
                    await HttpContext.SignOutAsync();
                    return RedirectToAction("Login", "Account");
                }
            }

            return View(viewModel);
        }

        #endregion
    }
}