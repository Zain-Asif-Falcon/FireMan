using Application.Common.Utilities;
using Domain.Contracts.V1;
using Domain.ViewModel.API;
using FireManUI.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace FireManUI.Controllers
{
    public class AuthenticationController : Controller
    {
        private readonly IConfiguration _config;
        public AuthenticationController(IConfiguration config)
        {
            _config = config;
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(UserLogin login)
        {
            if(ModelState.IsValid)
            {
               var dt = LoginMethod(login);
               AuthenticationResponse res = (AuthenticationResponse)dt.Result.Value;
               if(res.Success == true)
                {
                    HttpContext.Session.SetObject("Token", res.Token.ToString());

                    UserProfile profile = await GetProfileData(res.UserId);
                    string conDt = (profile.ProfilePicture != null)? profile.ProfilePicture.Split("\\UserProfileImages\\")[1]:"";
                    profile.ProfilePicture = _config["ServerAddress:Address"] + $"/Images/UserProfileImages/" + conDt;
                    HttpContext.Session.SetObject("UserInfo", JsonConvert.SerializeObject(profile));

                    return RedirectToAction("Index", "Home");
                }
               else
                {
                    return Json(res);
                }
            }
            return View();
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Register(UserRegistration registration)
        {
            if (ModelState.IsValid)
            {
                var dt = RegisterMethod(registration);
                AuthenticationResponse res = (AuthenticationResponse)dt.Result.Value;
                if (res.Success == true)
                {
                   
                }
            }
            return View();
        }
       
        public IActionResult EmailSent()
        {
            return View();
        }
        [HttpGet]
        public IActionResult EmailVerified([FromQuery] string id)
        {
            VerifyEmail ver = new VerifyEmail();
            ver.UserId = id;
            var dt = VerifyEmailMethod(ver);
            return View();
        }
        public IActionResult ChangePassword()
        {
            return View();
        }
        [HttpGet]
        public IActionResult ChangePassword([FromQuery] string id)
        {
            ChangePasswordContract pass = new ChangePasswordContract();
            pass.UserId = id;
            return View(pass);
        }
        [HttpPost]
        public IActionResult ChangePassword(ChangePasswordContract pass)
        {
            if (ModelState.IsValid)
            {
                var dt = ChangePasswordMethod(pass);
                GenericRequestResponse res = (GenericRequestResponse)dt.Result.Value;
                if (res.Success == true)
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            return View();
        }
        #region API Calling

        [HttpPost]
        public async Task<JsonResult> LoginMethod(UserLogin loginDt)
        {
            string apiUrl = _config["ServerAddress:Address"] + $"/api/v1/Admin_Login/";
            var res = await LoginCall(loginDt, apiUrl);
            return Json(res);
            //return Json(new
            //{
            //    data = res
            //});
        }      
        [HttpPost]
        public async Task<JsonResult> RegisterMethod(UserRegistration regDt)
        {
            string apiUrl = _config["ServerAddress:Address"] + $"/api/v1/Register/";
            var res = await RegisterCall(regDt, apiUrl);
            return Json(new
            {
                data = res
            });
        }   

        [HttpGet]
        public async Task<GenericRequestResponse> ForgotPasswordCall(string Email)
        {
            string apiUrl = _config["ServerAddress:Address"] + $"/api/v1/forgetPassword/{Email}";
            Task<GenericRequestResponse> res;
            res = ApiCalls<GenericRequestResponse>.GetById(apiUrl);
            return await res;
        }
        [HttpPost]
        public async Task<JsonResult> ChangePasswordMethod(ChangePasswordContract loginDt)
        {
            string apiUrl = _config["ServerAddress:Address"] + $"/api/v1/Users/update/ChangePassword";
            var res = await ChangePasswordCall(loginDt, apiUrl);
            return Json(res);
            //return Json(new
            //{
            //    data = res
            //});
        }      
        #endregion
    }
}
