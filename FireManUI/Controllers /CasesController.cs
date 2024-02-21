using Application.Common.Utilities;
using Domain.Contracts.V1.CaseFiles;
using Domain.Contracts.V1.CaseFiles.Creation;
using Domain.Contracts.V1.Reports.CaseFiles;
using FireManUI.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;

namespace FireManUI.Controllers
{
    public class CasesController : Controller
    {
        private readonly ILogger<CasesController> _logger;
        private readonly IConfiguration _config;
        public CasesController(ILogger<CasesController> logger, IConfiguration config)
        {
            _logger = logger;
            _config = config;
        }
        public async Task<IActionResult> Index()
        {
            string Token = HttpContext.Session.GetObject<string>("Token");
            if (Token == null)
            {
                return RedirectToAction("Login", "Authentication");
            }
            else
            {
                return await Task.Run(() => View());
            }
        }       
        public async Task<IActionResult> CasefileCheckList()
        {
            string Token = HttpContext.Session.GetObject<string>("Token");
            if (Token == null)
            {
                return RedirectToAction("Login", "Authentication");
            }
            else
            {
                return await Task.Run(() => View());
            }
        }      
        #region API Calling
        [HttpGet]
        public async Task<CaseFilesContract> GetById(int Id)
        {
            string user = HttpContext.Session.GetObject<string>("Token");

            string apiUrl = _config["ServerAddress:Address"] + $"/api/v1/casefiles/{Id}";
            Task<CaseFilesContract> res;
            res = ApiCalls<CaseFilesContract>.GetByIdWithHeaders(apiUrl, user);
            return await res;
        }  
        [HttpGet]
        public async Task<CaseFileReportContract> GetReportData(int casefileId)
        {
            string user = HttpContext.Session.GetObject<string>("Token");
            string apiUrl = _config["ServerAddress:Address"] + $"/api/v1/Reports/{casefileId}";

            Task<CaseFileReportContract> res;
            res = ApiCalls<CaseFileReportContract>.GetByIdWithHeaders(apiUrl, user);
            return await res;
        }      
        #endregion
    }
}
