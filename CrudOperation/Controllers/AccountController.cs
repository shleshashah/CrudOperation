using CrudOperation.Interface;
using CrudOperation.Models;
using CrudOperation.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CrudOperation.Controllers
{
    public class AccountController : Controller
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ICustomer _Customer;
        private readonly EmailServices _emailServices;
        private IConfiguration _config;

        public AccountController(IHttpContextAccessor httpContextAccessor, ICustomer customers, EmailServices emailServices, IConfiguration config)
        {
            _Customer = customers;
            _emailServices = emailServices;
            _config = config;
            _httpContextAccessor = httpContextAccessor;
        }
        public IActionResult Index(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login(LoginPayload model, string returnUrl = null)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    IActionResult response = Unauthorized();
                    //var result = await _signInManager.PasswordSignInAsync(model.LoginUser, model.Password, true, lockoutOnFailure: false);
                    //if (result.Succeeded)
                    //{
                    Customer result = await _Customer.ValidateUser(model.LoginUser, model.Password);
                    if (result != null)
                    {
                        string strToken = GenerateJSONWebToken(model);
                        await _Customer.AddToken(result.Id, strToken);
                       
                        // Add the token to the request headers
                        //_httpContextAccessor.HttpContext.Request.Headers.Add("Authorization", strToken);

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

        private string GenerateJSONWebToken(LoginPayload userInfo)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[] {
        new Claim(JwtRegisteredClaimNames.UniqueName, userInfo.LoginUser),
        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
    };

            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
                _config["Jwt:Issuer"],
                claims,
                expires: DateTime.Now.AddMinutes(120),
                signingCredentials: credentials);

            return new System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
