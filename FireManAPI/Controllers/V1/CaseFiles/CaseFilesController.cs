using Application.Interface.UnitOfWork;
using Domain.Contracts.V1.CaseFiles;
using Domain.Contracts.V1.CaseFiles.Creation;
using Domain.Entities;
using FireManAPI.Contract;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FireManAPI.Controllers.V1.CaseFiles
{
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class CaseFilesController : ControllerBase
    {
        private readonly IUnitOfWork _IUnitOfWork;
        private readonly ILogger<CaseFilesController> _logger;
        public CaseFilesController(ILogger<CaseFilesController> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _IUnitOfWork = unitOfWork;
        }
        [HttpGet(ApiRoutes.CaseFiles.GetAll)]
        public async Task<IActionResult> GetAll()
        {
            var ServGrpInfo = await _IUnitOfWork.cases.GetAllCasesList();
            return Ok(ServGrpInfo);
        }
        [HttpGet(ApiRoutes.CaseFiles.GetAllEstabWise)]
        public async Task<IActionResult> GetAllEstabWise(int establishmentId)
        {
            var ServGrpInfo = await _IUnitOfWork.cases.GetAllCasesEstabWiseList(establishmentId);
            return Ok(ServGrpInfo);
        }
        [HttpGet(ApiRoutes.CaseFiles.Get)]
        public async Task<IActionResult> Get(int casefileId)
        {
            var ServGrpInfo = await _IUnitOfWork.cases.GetCasesDetails(casefileId);
            return Ok(ServGrpInfo);
        }
        [HttpPut(ApiRoutes.CaseFiles.ValidateCaseFile)]
        public async Task<IActionResult> ValidateCaseFile(int casefileId, int EmployeeId)
        {
            var ServGrpInfo = await _IUnitOfWork.cases.ValidateCaseFile(casefileId, EmployeeId);
            return Ok(ServGrpInfo);
        }
        [HttpGet(ApiRoutes.CaseFiles.CaseFileCheckListAll)]
        public async Task<IActionResult> CaseFileCheckListComplete()
        {
            var ServGrpInfo = await _IUnitOfWork.cases.CaseFileCheckCompleteList();
            return Ok(ServGrpInfo);
        }
       
        [HttpGet(ApiRoutes.CaseFiles.GetLastCaseOpenFolderCreation)]
        public async Task<IActionResult> GetLastCaseOpenFolderCreation(int establishmentId)
        {
            var ServGrpInfo = await _IUnitOfWork.cases.GetLastDataOpenTabFolderCreation(establishmentId);
            return Ok(ServGrpInfo);
        }
        
        [HttpPost(ApiRoutes.CaseFiles.OpenFolderCreation)]
        public async Task<IActionResult> OpenTabFolderCreation([FromBody] OpenTabFolderCreationContract ord)
        {
            try
            {
                var placeOrder = await _IUnitOfWork.cases.SaveOpenTabFolderCreation(ord);
                return Ok(placeOrder);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }
        [HttpPut(ApiRoutes.CaseFiles.UpdateOpenFolderCreation)]
        public async Task<IActionResult> UpdateOpenTabFolderCreation([FromBody] OpenTabFolderCreationContract ord)
        {
            try
            {
                var placeOrder = await _IUnitOfWork.cases.UpdateOpenTabFolderCreation(ord);
                return Ok(placeOrder);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }
        [HttpGet(ApiRoutes.CaseFiles.GetOpenTownPlanning)]
        public async Task<IActionResult> GetOpenTownPlanning(int establishmentId, int casefileId)
        {
            var ServGrpInfo = await _IUnitOfWork.cases.GetOpenTabTownPlanning(establishmentId,casefileId);
            return Ok(ServGrpInfo);
        }
        [HttpPost(ApiRoutes.CaseFiles.OpenTownPlanning)]
        public async Task<IActionResult> OpenTabTownPlanning([FromBody] OpenTabTownPlanningContract ord)
        {
            try
            {
                var placeOrder = await _IUnitOfWork.cases.SaveOpenTabTownPlanning(ord);
                return Ok(placeOrder);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }
        [HttpPut(ApiRoutes.CaseFiles.UpdateTownPlanning)]
        public async Task<IActionResult> UpdateOpenTabTownPlanning([FromBody] OpenTabTownPlanningContract ord)
        {
            try
            {
                var placeOrder = await _IUnitOfWork.cases.UpdateOpenTabTownPlanning(ord);
                return Ok(placeOrder);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }
        [HttpPost(ApiRoutes.CaseFiles.GetPreparingVisit)]
        public async Task<IActionResult> OpenTabPreparingVisit(int establishmentId, int casefileId)
        {
            try
            {
                var placeOrder = await _IUnitOfWork.cases.GetOpenTabVisitPreparing(establishmentId, casefileId);
                return Ok(placeOrder);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }
        [HttpPut(ApiRoutes.CaseFiles.SavePreparingVisit)]
        public async Task<IActionResult> SaveOpenTabPreparingVisit([FromBody] VisitPreparingContract ord)
        {
            try
            {
                var placeOrder = await _IUnitOfWork.cases.SaveOpenTabVisitPreparing(ord);
                return Ok(placeOrder);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }
        [HttpGet(ApiRoutes.CaseFiles.GetCommitteeMembers)]
        public async Task<IActionResult> GetCommiteeMembers(int casefileId, bool flag)
        {
            var ServGrpInfo = await _IUnitOfWork.cases.GetCommiteeMembers(casefileId, flag);
            return Ok(ServGrpInfo);
        }
        [HttpPost(ApiRoutes.CaseFiles.SaveCommitteeMembers)]
        public async Task<IActionResult> SaveCommitteeMembers([FromBody] VisitingGroupsDataContract ord)
        {
            try
            {
                var placeOrder = await _IUnitOfWork.cases.SaveCommiteeMembers(ord);
                return Ok(placeOrder);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }
        [HttpGet(ApiRoutes.CaseFiles.CaseFileCheckList)]
        public async Task<IActionResult> GetCaseFileCheckList(int id, int establishmentId, int casefileid)
        {
            var ServGrpInfo = await _IUnitOfWork.cases.GetAllCheckList(id, establishmentId, casefileid);
            return Ok(ServGrpInfo);
        }
        [HttpPost(ApiRoutes.CaseFiles.SaveFileCheckList)]
        public async Task<IActionResult> SaveDetailOfObjectChkList([FromBody] List<OpenTabCheckListDataContract> ord)
        {
            try
            {
                var placeOrder = await _IUnitOfWork.cases.SaveDetailOfObjectChkList(ord);
                return Ok(placeOrder);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }
        [HttpGet(ApiRoutes.CaseFiles.GetSelectedCheckList)]
        public async Task<IActionResult> GetSelectCheckList(int id,int casefileid)
        {
            try
            {
                var placeOrder = await _IUnitOfWork.cases.GetSelectedCheckList(id, casefileid);
                return Ok(placeOrder);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }
        [HttpGet(ApiRoutes.CaseFiles.LastCaseFileCheckList)]
        public async Task<IActionResult> GetLastCaseFileCheckList(int id, int establishmentId)
        {
            var ServGrpInfo = await _IUnitOfWork.cases.GetLastCaseCheckList(id, establishmentId);
            return Ok(ServGrpInfo);
        }

        //=================================================================
        [HttpPut(ApiRoutes.CaseFiles.CompleteCaseFile)]
        public async Task<IActionResult> CompletedCaseFile(int casefileId)
        {
            var ServGrpInfo = await _IUnitOfWork.cases.CompleteCaseFile(casefileId);
            return Ok(ServGrpInfo);
        }
        [HttpGet(ApiRoutes.CaseFiles.SearchCaseFile)]
        public async Task<IActionResult> SearcCaseFile(int groupId , int serviceId , int employeeId)
        {
            var ServGrpInfo = await _IUnitOfWork.cases.SearchCaseFile(groupId, serviceId, employeeId);
            return Ok(ServGrpInfo);
        }
        
        //====================================================================
        [HttpDelete(ApiRoutes.CaseFiles.DeleteCaseFile)]
        public async Task<IActionResult> DeleteCaseFile(int casefileId)
        {
            var ServGrpInfo = await _IUnitOfWork.cases.DeleteCaseFile(casefileId);
            return Ok(ServGrpInfo);
        }
    }
}
