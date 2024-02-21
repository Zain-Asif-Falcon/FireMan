using Application.Common.Utilities;
using Domain.Contracts.V1;
using Domain.Contracts.V1.Users;
using Domain.Entities;
using Domain.ViewModel.Users;
using FireManUI.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace FireManUI.Controllers
{
    public class UsersController : Controller
    {
        [BindProperty]
        public UserViewModel UsersVM { get; set; }
        private readonly ILogger<UsersController> _logger;
        private readonly IConfiguration _config;

        public UsersController(ILogger<UsersController> logger, IConfiguration config)
        {
            _logger = logger;
            _config = config;
        }
        public IActionResult Index()
        {
            string Token = HttpContext.Session.GetObject<string>("Token");
            if (Token == null)
            {
                return RedirectToAction("Login", "Authentication");
            }
            else
            {
                return View();
            }
        }
        public async Task<IActionResult> Upsert(int? id)
        {
            string Token = HttpContext.Session.GetObject<string>("Token");
            if (Token == null)
            {
                return RedirectToAction("Login", "Authentication");
            }
            else
            {
                UsersVM = new UserViewModel();
                ViewBag.SDISList = await DropDownSDISData();
                ViewBag.EmployeeRolesList = await DropDownEmployeeRolesData();
                if (id != null)
                {
                    ViewBag.GroupsList = await DropDownGroupsData(0);
                    ViewBag.ServicesList = await DropDownServicesData(0);

                    var employee = await GetById((int)id);
                    UsersVM.employee.EmployeeId = employee.EmployeeId;
                    UsersVM.employee.EmpFirstName = employee.EmpFirstName;
                    UsersVM.employee.EmpLastName = employee.EmpLastName;
                    UsersVM.employee.Email = employee.Email;
                    UsersVM.employee.CellNo = employee.CellNo;
                    UsersVM.employee.ServiceId = employee.ServiceId;
                    UsersVM.employee.GroupId = employee.GroupId;
                    UsersVM.employee.RoleId = employee.RoleId;
                    UsersVM.SDISId = employee.SDISId;
                    UsersVM.employee.Password = employee.Password;
                    UsersVM.SDISId = employee.SDISId;
                    UsersVM.employee.PostalAddress = employee.PostalAddress;
                }
                return View(UsersVM);
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert()
        {
            UserRegistration sdi = new UserRegistration();
            sdi.EmployeeId = UsersVM.employee.EmployeeId;
            sdi.RoleId = UsersVM.employee.RoleId;
            sdi.ServiceId = (UsersVM.employee.ServiceId == null) ? null : UsersVM.employee.ServiceId;
            sdi.GroupId = UsersVM.employee.GroupId;
            sdi.EmpFirstName = UsersVM.employee.EmpFirstName;
            sdi.EmpLastName = UsersVM.employee.EmpLastName;
            sdi.Email = UsersVM.employee.Email;
            sdi.CellNo = UsersVM.employee.CellNo;
            sdi.Code = UsersVM.employee.Code;
            sdi.PostalAddress = UsersVM.employee.PostalAddress;
            sdi.IsAdmin = true;

            if (UsersVM.employee.EmployeeId == 0)
            {
                sdi.Password = UsersVM.employee.Password;
                sdi.EmailConfirmed = true;
                sdi.IsActive = true;
                sdi.IsValidated = true;
                var res = await Create(sdi);
                return Json(res);
                //await Create(sdi);
            }
            else
            {
                sdi.UpdatedAt = DateTime.Now;
                //await Update(sdi);
                var res = await Update(sdi);
                return Json(res);
            }
            return RedirectToAction(nameof(Index));
            //if (ModelState.IsValid)
            //{

            //}
            //else
            //{
            //    return View(UsersVM);
            //}
        }       

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UserProfile(UserProfile profile)
        {
            //profile.PostalAddress = profile.PostalAddress;
            await UpdateProfileTest(profile);

            UserProfile prof = await GetProfileData(profile.UserId);
            string conDt = prof.ProfilePicture.Split("\\UserProfileImages\\")[1];
            prof.ProfilePicture = _config["ServerAddress:Address"] + $"/Images/UserProfileImages/" + conDt;
            HttpContext.Session.SetObject("UserInfo", JsonConvert.SerializeObject(prof));

            return RedirectToAction("Index", "Home");
        }

        #region API Calling        
        [HttpPost]
        public async Task<IActionResult> Create(UserRegistration itm)
        {
            string token = HttpContext.Session.GetObject<string>("Token");
            
            var stream = token;
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(stream);
            var tokenS = jsonToken as JwtSecurityToken;
            var tokDet = tokenS.Claims.ToList();
            foreach(var item in tokDet)
            {
                if(item.Type == "Id")
                {
                    itm.CurrentUserId = int.Parse(item.Value);
                }
            }

            string apiUrl = _config["ServerAddress:Address"] + $"/api/v1/register/";
            var res = await ApiCalls<UserRegistration>.SaveWithHeaders(itm, apiUrl, token);
            //return Json(new
            //{
            //    data = res
            //});
            return Json(res);
        }      
        [HttpGet]
        public async Task<IActionResult> CheckExistingCode(string Code)
        {
            string token = HttpContext.Session.GetObject<string>("Token");

            string apiUrl = _config["ServerAddress:Address"] + $"/api/v1/items/CheckCode/{Code}";
            var res = await ApiCalls<Employee>.CheckExistingWithHeaders(apiUrl, token);
            //await _serviceCaller.GetData(apiUrl);
            return Json(new
            {
                data = res
            });
        }
        [HttpGet]
        public async Task<IEnumerable<SelectListItem>> DropDownSDISData()
        {
            try
            {
                string user = HttpContext.Session.GetObject<string>("Token");

                string apiUrl = _config["ServerAddress:Address"] + $"/api/v1/sdis/dropdown/";
                string res = await ApiCalls<string>.GetDropDownListWithHeaders(apiUrl, user);
                List<SelectListItem> record = Newtonsoft.Json.JsonConvert.DeserializeObject<List<SelectListItem>>(res);
                return record;
            }
            catch (Exception ex)
            {
                throw;
            }
        }       
        [HttpGet]
        public async Task<IEnumerable<SelectListItem>> DropDownEmployeeRolesData()
        {
            try
            {
                string user = HttpContext.Session.GetObject<string>("Token");

                string apiUrl = _config["ServerAddress:Address"] + $"/api/v1/roles/dropdown/1";
                string res = await ApiCalls<string>.GetDropDownListWithHeaders(apiUrl, user);
                List<SelectListItem> record = Newtonsoft.Json.JsonConvert.DeserializeObject<List<SelectListItem>>(res);
                return record;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetValidatedUsers()
        {
            string token = HttpContext.Session.GetObject<string>("Token");

            var stream = token;
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(stream);
            var tokenS = jsonToken as JwtSecurityToken;

            string apiUrl = _config["ServerAddress:Address"] + $"/api/v1/Users/NonValidUsers";
            var res = await ApiCalls<EmployeeListContract>.GetDataWithHeaders(apiUrl, token);
            return Json(new
            {
                data = res
            });
        }
 
        #endregion
    }
}
