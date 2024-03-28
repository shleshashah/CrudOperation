using CrudOperation.Interface;
using CrudOperation.Models;
using CrudOperation.Utility;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrudOperation.Controllers
{
    public class AccountController : Controller
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly ICustomer _Customer;
        private readonly EmailServices _emailServices;

        public AccountController(SignInManager<IdentityUser> signInManager, ICustomer customers, EmailServices emailServices)
        {
            _signInManager = signInManager;
            _Customer = customers;
            _emailServices = emailServices;
        }
        public IActionResult Index(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginPayload model, string returnUrl = null)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    //var result = await _signInManager.PasswordSignInAsync(model.LoginUser, model.Password, true, lockoutOnFailure: false);
                    //if (result.Succeeded)
                    //{
                    Customer result = await _Customer.ValidateUser(model.LoginUser, model.Password);
                    if (result != null)
                    {
                        if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                        {
                            return Redirect(returnUrl);
                        }
                        else
                        {
                            return RedirectToAction("Index", "Customers");
                        }
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                        return View("Index", model);
                    }
                }
            }
            catch (Exception ex)
            {

            }

            // If we got this far, something failed, redisplay form
            return View("Index", model);
        }

        public IActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordPayload changePasswordPayload)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string strEmail = await _Customer.UpdatePassword(changePasswordPayload);
                    _emailServices.SendEmail(strEmail, "Changed Password Successfully", "You Password has been changed Successfully");
                }

            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                return View();
            }
            TempData["SuccessMessage"] = ErrrorMessageEnum.SavedSuccessfully;
            return RedirectToAction("Index");
        }
    }
}
