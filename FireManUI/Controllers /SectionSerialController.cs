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
    public class SectionSerialController : Controller
    {
        [BindProperty]
        public SectionSeriesViewModel SectionSeriesVM { get; set; }
        private readonly ILogger<SectionSerialController> _logger;
        private readonly IConfiguration _config;

        public SectionSerialController(ILogger<SectionSerialController> logger, IConfiguration config)
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

                SectionSeriesVM = new SectionSeriesViewModel();
                ViewBag.CategoryList = await DropDownCategoryData();
                if (id != null)
                {

                    var service = await GetById((int)id);
                    SectionSeriesVM.series.SectionSerialId = service.SectionSerialId;
                    SectionSeriesVM.series.SeriesName = service.SeriesName;
                    SectionSeriesVM.series.ReferenceCode = service.ReferenceCode;
                    SectionSeriesVM.series.SectionId = service.SectionId;
                    SectionSeriesVM.chapterId = service.SectionHeadId;
                    SectionSeriesVM.categoryId = service.SectionCategoryId;
                    SectionSeriesVM.series.orderNum = service.orderNum;

                    ViewBag.ChaptersList = await DropDownChaptersData((int)service.SectionCategoryId);
                    ViewBag.SectionsList = await DropDownSectionsData(service.SectionHeadId);

                }
                else
                {
                    var service = await GetById(0);
                    SectionSeriesVM.series.SectionSerialId = 0;
                    SectionSeriesVM.series.SeriesName = null;
                    SectionSeriesVM.series.ReferenceCode = null;
                    SectionSeriesVM.series.SectionId = 0;
                    SectionSeriesVM.chapterId = 0;
                    SectionSeriesVM.categoryId = 0;
                    SectionSeriesVM.series.orderNum = ++service.orderNum;
                }
                return View(SectionSeriesVM);
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert()
        {
            SectionSerial sdi = new SectionSerial();
            sdi.SectionSerialId = SectionSeriesVM.series.SectionSerialId;
            sdi.SectionId = SectionSeriesVM.series.SectionId;
            sdi.SeriesName = SectionSeriesVM.series.SeriesName;
            sdi.ReferenceCode = SectionSeriesVM.series.ReferenceCode;
            sdi.orderNum = SectionSeriesVM.series.orderNum;
            if (SectionSeriesVM.series.SectionSerialId == 0)
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
        }
        #region API Calling
        [HttpPost]
        public async Task<IActionResult> Create(SectionSerial itm)
        {
            string user = HttpContext.Session.GetObject<string>("Token");

            string apiUrl = _config["ServerAddress:Address"] + $"/api/v1/sectionseries/";
            var res = await ApiCalls<SectionSerial>.AddWithHeadersResponse(itm, apiUrl, user);
            return Json(res);
            //await _serviceCaller.GetData(apiUrl);
            //return Json(new
            //{
            //    data = res
            //});
        }          
         
        [HttpGet]
        public async Task<IEnumerable<SelectListItem>> DropDownChaptersData(int categoryId)
        {
            try
            {
                string user = HttpContext.Session.GetObject<string>("Token");

                string apiUrl = _config["ServerAddress:Address"] + $"/api/v1/sectionhead/categorywise_sectionheads_dropdown/{categoryId}";
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
        public async Task<IEnumerable<SelectListItem>> DropDownAllSectionsData()
        {
            try
            {
                string user = HttpContext.Session.GetObject<string>("Token");

                string apiUrl = _config["ServerAddress:Address"] + $"/api/v1/sections/all_dropdown";
                string res = await ApiCalls<string>.GetDropDownListWithHeaders(apiUrl, user);

                List<SelectListItem> record = Newtonsoft.Json.JsonConvert.DeserializeObject<List<SelectListItem>>(res);
                return record;
            }
            catch (Exception ex)
            {
                throw;
            }
        }        
        #endregion
    }
}
