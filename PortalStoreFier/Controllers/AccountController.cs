using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PortalStoreFier.ViewModels;
//using ServiceReference1;
//using Microsoft.AspNetCore.Identity.UI.Services;
//using System;
//using System.Collections.Generic;
//using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;
using PortalStoreFier.Data;
using System.Net.Mail;
using System.Net;

namespace PortalStoreFier.Controllers
{
    
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser>   _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        //private readonly IEmailSender _sender;
        private readonly ILogger<AccountController> _logger;

        public AccountController(UserManager<ApplicationUser> userManager,
                                      SignInManager<ApplicationUser> signInManager, ILogger<AccountController> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            //_sender = sender;
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }

        
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    UserName           = model.Email,
                    Email              = model.Email,
                    Employee_id        = model.Employee_id,
                    FULL_Name          = model.FULL_Name,
                    Id_number          = model.Id_number,
                    Gender             = model.Gender,
                    EducationSpecialty = model.EducationSpecialty,
                    Phone              = model.Phone,
                    Position           = model.Position,
                    Salary             = model.Salary,
                };
                //model.Password = "Qwer@2255";
                //model.ConfirmPassword = "Qwer@2255";
                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    // Send email notification
                    using (var client = new SmtpClient("smtp.gmail.com", 465))
                    {
                        client.EnableSsl = true; // Enable SSL if required
                        client.UseDefaultCredentials = false;
                        client.Credentials = new NetworkCredential("younes.khdeir@storefire.net", "store456@@@");

                        var message = new MailMessage();
                        message.From = new MailAddress("younes.khdeir@storefire.net");
                        message.To.Add("younes.khdeir@gmail.com");
                        message.Subject = "New Item Added";
                        message.Body = "An item has been added to the database.";

                        client.Send(message);
                    }

                    await _signInManager.SignInAsync(user, isPersistent: false);

                    return RedirectToAction("index", "Home");
                }
                //if (result.Succeeded)
                //{
                //    var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                //    var confirmationLink = Url.Action(nameof(ConfirmEmail), "Account", new { Email = user.Email, token }, Request.Scheme);

                //    //ServiceSoapClient p = new ServiceSoapClient(new ServiceSoapClient.EndpointConfiguration());




                //    //var data = await p.SendEmailAsync(
                //    //    "Notification@reach.ps",
                //    //    "Abcd@1234",
                //    //    user.Email,
                //    //    "Confirm your email",
                //    //    "test email to ok \n" + confirmationLink,
                //    //    "reachex.reach.ps");
                //    //_logger.LogInformation($"AccountController :send confirmationLink Email to {user.Email} by Action Register");

                //    if (@User.Identity.Name != null) return RedirectToAction("ListUsers", "Administration");
                //    else return RedirectToAction("login", "Account");
                //}

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

                ModelState.AddModelError(string.Empty, "Invalid Login Attempt");
                _logger.LogInformation("AccountController :" + user.Email+ " Invalid Login Attempt, and not sent confirm eamil by Action "+ "Register");
            }
            return View(model);
        }
         

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> ConfirmEmail(string token, string Email)
        {
            if (Email == null || token == null)
            {
                _logger.LogInformation($"AccountController : not exit email: {Email} by Action ConfirmEmail");
                return RedirectToAction("index", "home");
            }

            var user = await _userManager.FindByEmailAsync(Email);
            if (user == null)
            {
                ViewBag.ErrorMessage = $"The User ID {Email } is invalid";
                _logger.LogInformation($"AccountController : not found Email: {Email} by Action ConfirmEmail");

                return View("Error");
            }

            var result = await _userManager.ConfirmEmailAsync(user, token);
            if (result.Succeeded)
            {
                _logger.LogInformation($"AccountController : confirmed Email: {Email} by Action ConfirmEmail");
                ResetPasswordViewModel model = new ResetPasswordViewModel();
                model.Email = Email;
                model.Token = token;
                return RedirectToAction("ResetPassword", "Account", model);
                //return View(model);
            }

            ViewBag.ErrorTitle = "Email cannot be confirmed";
            _logger.LogInformation("AccountController : " + Email + " this Email cannot be confirmed by Action " + "ConfirmEmail");

            return View("Error");
        }


        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel user)
        {
            if (ModelState.IsValid)
            {

                var data = await _userManager.FindByEmailAsync(user.Email);
                if (data != null)
                {
                    var result = await _signInManager.PasswordSignInAsync(data.UserName, user.Password, user.RememberMe, false); ;
                    if (result.Succeeded)
                    {
                        _logger.LogInformation($"AccountController : login Email: {user.Email} by Action Login");
                        return RedirectToAction("Index", "Customers");
                    }

                }
                //if (result.RequiresTwoFactor)
                //{
                //    var user2 = await _userManager.FindByNameAsync(data.UserName);
                //    return RedirectToAction(nameof(LoginTwoStep), new { user.Email, userModel, userModel.RememberMe, returnUrl });
                //}

                ModelState.AddModelError(string.Empty, "Invalid Login Attempt");
                _logger.LogInformation(user.Email + "AccountController :  Invalid Login Attempt by Action " + "Login");

            }
            return View(user);
        }


        [HttpGet]
        [AllowAnonymous]
        public IActionResult ForgetPassword()
        {
            return View();
        }


        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> ForgetPassword(ForgetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Find the user by email
                var user = await _userManager.FindByEmailAsync(model.Email);
                // If the user is found AND Email is confirmed
                if (user != null && await _userManager.IsEmailConfirmedAsync(user))
                {
                    // Generate the reset password token
                    var token = await _userManager.GeneratePasswordResetTokenAsync(user);

                    // Build the password reset link
                    var passwordResetLink = Url.Action("ResetPassword", "Account",
                            new { Email = user.Email, token = token }, Request.Scheme);


                    //ServiceSoapClient p = new ServiceSoapClient(new ServiceSoapClient.EndpointConfiguration());

                    //var data = await p.SendEmailAsync(
                    //    "Notification@reach.ps",
                    //    "Abcd@1234",
                    //    user.Email,
                    //    "Forgot Password in your email",
                    //    "test email to ok \n" + passwordResetLink,
                    //    "reachex.reach.ps");
                    // Log the password reset link
                    //logger.Log(LogLevel.Warning, passwordResetLink);

                    // Send the user to Forgot Password Confirmation view
                    _logger.LogInformation($"AccountController : send token to  Email: {user.Email} by Action ForgetPassword");
                    return View("ForgotPasswordConfirmation");
                }

                // To avoid account enumeration and brute force attacks, don't
                // reveal that the user does not exist or is not confirmed
                _logger.LogInformation("AccountController : " + user.Email + " can not sent email by Action " + "ForgetPassword");
                return View("ForgotPasswordConfirmation");

            }

            return View(model);
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            _logger.LogInformation($"logout  Email by Action Logout");

            return RedirectToAction("Login");
        }



        [HttpGet]
        [AllowAnonymous]
        public IActionResult ResetPassword(ResetPasswordViewModel model, string thanks ="oky")
        {
            // If password reset token or email is null, most likely the
            // user tried to tamper the password reset link
            if (model.Token == null || model.Email == null)
            {
                ModelState.AddModelError("", "Invalid password reset token");
            }
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Find the user by email
                var user = await _userManager.FindByEmailAsync(model.Email);

                if (user != null)
                {
                    // reset the user password
                    var result = await _userManager.ResetPasswordAsync(user, model.Token, model.Password);
                    if (result.Succeeded)
                    {
                        _logger.LogInformation($"AccountController : Reset Password to  Email: {user.Email} by Action ResetPassword");
                        return View("ResetPasswordConfirmation");
                    }
                    // Display validation errors. For example, password reset token already
                    // used to change the password or password complexity rules not met
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                    return View(model);
                }

                // To avoid account enumeration and brute force attacks, don't
                // reveal that the user does not exist
                _logger.LogInformation("AccountController : " + user.Email + " Invalid password reset token " + "ResetPassword");

                return View("ResetPasswordConfirmation");
            }
            // Display validation errors if model state is not valid
            return View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult AccessDenied()
        {
            return View();
        }


    }
}


