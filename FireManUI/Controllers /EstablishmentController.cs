using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using Application.Common.Utilities;
using Domain.Contracts.V1.CaseFiles;
using Domain.Contracts.V1.Establishments;
using FireManUI.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace FireManUI.Controllers
{
    public class EstablishmentController : Controller
    {
        private readonly ILogger<EstablishmentController> _logger;
        private readonly IConfiguration _config;
        public EstablishmentController(ILogger<EstablishmentController> logger, IConfiguration config)
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
        #region API Calling

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            string user = HttpContext.Session.GetObject<string>("Token");

            var stream = user;
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(stream);
            var tokenS = jsonToken as JwtSecurityToken;

            string apiUrl = _config["ServerAddress:Address"] + $"/api/v1/establishment/getEstablishmentList";
            var res = await ApiCalls<EstablishmentListContract>.GetDataWithHeaders(apiUrl, user);
            return Json(new
            {
                data = res
            });
        }
        #endregion
    }
}
