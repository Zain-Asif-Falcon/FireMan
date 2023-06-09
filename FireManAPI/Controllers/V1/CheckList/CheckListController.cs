using Application.Interface.UnitOfWork;
using Domain.Contracts.V1.CaseFiles;
using Domain.Contracts.V1.CaseFiles.Creation;
using Domain.Contracts.V1.CheckList;
using Domain.Contracts.V1.CheckList.ClientTabs.Creation;
using Domain.DTO;
using Domain.ViewModel.CheckList;
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

namespace FireManAPI.Controllers.V1.CheckList
{
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class CheckListController : ControllerBase
    {
        private readonly IUnitOfWork _IUnitOfWork;
        private readonly ILogger<CheckListController> _logger;
        public CheckListController(ILogger<CheckListController> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _IUnitOfWork = unitOfWork;
        }
        [HttpGet(ApiRoutes.CheckList.AllCheckList)]
        public async Task<IActionResult> AllCheckList()
        {
            var ServGrpInfo = await _IUnitOfWork.chkList.GetAllCheckList();
            return Ok(ServGrpInfo);
        }
        [HttpGet(ApiRoutes.CheckList.AllSeries)]
        public async Task<IActionResult> Series()
        {
            var caseFile = 0;//Case file function may add in fun
            await _IUnitOfWork.excelDateTime.GetRangeDateTimefromCaseFile(caseFile);
            var ServGrpInfo = await _IUnitOfWork.chkList.GetAllSeries();
            return Ok(ServGrpInfo);
        }
        [HttpGet(ApiRoutes.CheckList.GetSectionChecks)]
        public async Task<IActionResult> SectionCheckListData(int sectionId)
        {
            var ServGrpInfo = await _IUnitOfWork.chkList.GetSectionCheckList(sectionId);
            return Ok(ServGrpInfo);
        }
        [HttpPost(ApiRoutes.CheckList.Create)]
        public async Task<IActionResult> PlaceOrder([FromBody] CheckListViewModel ord)
        {
            try
            {
                var placeOrder = await _IUnitOfWork.chkList.SaveCheckList(ord);
                return Ok(placeOrder);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }
        [HttpDelete(ApiRoutes.CheckList.Delete)]
        public async Task<IActionResult> Delete(int checkListId)
        {
            try
            {
                var delItem = await _IUnitOfWork.chkList.SetRecordAsDeleted(checkListId);
                return Ok(delItem);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }
       [HttpGet(ApiRoutes.CheckList.GetSectionComments)]
        public async Task<IActionResult> SectionComments(int sectionId)
        {
            var ServGrpInfo = await _IUnitOfWork.chkList.GetSectionComments(sectionId);
            return Ok(ServGrpInfo);
        }
        [HttpPost(ApiRoutes.CheckList.SectionAssistance)]
        public async Task<IActionResult> SectionAssistance([FromForm] AssistanceEmail assistance)
        {
            var ServGrpInfo = await _IUnitOfWork.chkList.GetSectionAssitance(assistance);
            return Ok(ServGrpInfo);
        }
        [HttpGet(ApiRoutes.CheckList.GetSelectCheckedList)]
        public async Task<IActionResult> ShowSelectedCheckList(int casefileId , int sectionId)
        {
            var ServGrpInfo = await _IUnitOfWork.chkList.GetSectionSelectedCheckList(casefileId,sectionId);
            return Ok(ServGrpInfo);
        }
        [HttpGet(ApiRoutes.CheckList.GetSectionNotes)]
        public async Task<IActionResult> SectionNotes(int casefileId)
        {
            var ServGrpInfo = await _IUnitOfWork.chkList.GetSectionNotes(casefileId);
            return Ok(ServGrpInfo);
        }
        [HttpPost(ApiRoutes.CheckList.SaveSectionNotes)]
        public async Task<IActionResult> SaveSectionComment([FromBody] CaseFileNotesContract tabData)
        {
            var ServGrpInfo = await _IUnitOfWork.chkList.SaveSectionNotes(tabData);
            return Ok(ServGrpInfo);
        }
        [HttpDelete(ApiRoutes.CheckList.DeleteSectionComment)]
        public async Task<IActionResult> DeleteSectionComments(int caseFileCommentId)
        {
            try
            {
                var delSDISItem = await _IUnitOfWork.chkList.SetSectionNotes(caseFileCommentId);
                return Ok(delSDISItem);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }
        [HttpGet(ApiRoutes.CheckList.GetChapterSelectedSectionList)]
        public async Task<IActionResult> GetChapterSectionsList(int casefileId, int categoryId)
        {
            var ServGrpInfo = await _IUnitOfWork.chkList.GetchapterSelectSections(casefileId, categoryId);
            return Ok(ServGrpInfo);
        }
        //===========================================================
        [HttpPost(ApiRoutes.CheckList.UploadExcelFile)]
        public async Task<IActionResult> UploadChecklistExcelSheet([FromForm] CheckListExcelFile file)//([FromBody] UserInfo userInfo)
        {
            await _IUnitOfWork.excelDateTime.GetRangeDateTimefromCaseFile();
            var response = await _IUnitOfWork.chkList.UploadCheckListExcelFile(file);
            if (response.ErrorMessage != null)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
        [HttpPost(ApiRoutes.CheckList.CheckOrderListExistOrNot)]
        public async Task<IActionResult> CheckOrderListExist([FromBody] SectionSentenceDTO sectionSentenceDTO)
        {
            
            var ServGrpInfo = await _IUnitOfWork.chkList.GetCheckListOrder( sectionSentenceDTO);
             
                return Ok(ServGrpInfo);
        }
        [HttpPost(ApiRoutes.CheckList.SubCheckOrderListExistOrNot)]
        public async Task<IActionResult> SubCheckOrderListExist([FromBody] SectionSentenceDTO sectionSentenceDTO)
        {

            var ServGrpInfo = await _IUnitOfWork.chkList.GetSubCheckListOrder(sectionSentenceDTO);

            return Ok(ServGrpInfo);
        }

    }
}
