using Application.Common.Utilities;
using Domain.Contracts.V1.Master;
using Domain.Entities;
using Domain.ViewModel.API;
using Domain.ViewModel.Master;
using FireManUI.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;

namespace FireManUI.Controllers
{
    public class ServicesController : Controller
    {
        [BindProperty]
        public ServicesViewModel ServicesVM { get; set; }
        private readonly ILogger<ServicesController> _logger;
        private readonly IConfiguration _config;

        public ServicesController(ILogger<ServicesController> logger, IConfiguration config)
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
                ServicesVM = new ServicesViewModel();
                ViewBag.SDISList = await DropDownSDISData();
                if (id != null)
                {
                    var service = await GetById((int)id);
                    ServicesVM.service.ServiceId = service.service.ServiceId;
                    ServicesVM.service.ServiceName = service.service.ServiceName;
                    ServicesVM.service.GroupId = service.service.GroupId;
                    ServicesVM.sdisId = (int)service.sdisId;
                    ViewBag.GroupsList = await DropDownGroupsData((int)service.sdisId);
                    //ServicesVM.service.UpdatedAt = service.service.UpdatedAt;
                }
                return View(ServicesVM);
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert()
        {
            Service sdi = new Service();
            sdi.ServiceId = ServicesVM.service.ServiceId;
            sdi.GroupId = ServicesVM.service.GroupId;
            sdi.ServiceName = ServicesVM.service.ServiceName;
            if (ServicesVM.service.ServiceId == 0)
            {
                sdi.IsActive = true;
                sdi.CreatedAt = DateTime.Now;
                var res = await Create(sdi);
                return Json(res);
            }
            else
            {
                sdi.UpdatedAt = DateTime.Now;
                var res = await Update(sdi);
                return Json(res);
            }
            return RedirectToAction(nameof(Index));

            //if (ModelState.IsValid)
            //{
                
            //}
            //else
            //{
            //    return View(ServicesVM);
            //}
        }
        #region API Calling
        [HttpPost]
        public async Task<IActionResult> Create(Service itm)
        {
            string user = HttpContext.Session.GetObject<string>("Token");

            string apiUrl = _config["ServerAddress:Address"] + $"/api/v1/services/";
            var res = await ApiCalls<Service>.AddWithHeadersResponse(itm, apiUrl, user);
            return Json(res);
            //return Json(new
            //{
            //    data = res
            //});
        }       

        [HttpGet]
        public async Task<ServicesViewModel> GetById(int Id)
        {
            string user = HttpContext.Session.GetObject<string>("Token");

            string apiUrl = _config["ServerAddress:Address"] + $"/api/v1/services/{Id}";
            Task<ServicesViewModel> res;
            res = ApiCalls<ServicesViewModel>.GetByIdWithHeaders(apiUrl, user);
            return await res;
        }       
        #endregion
    }
}
