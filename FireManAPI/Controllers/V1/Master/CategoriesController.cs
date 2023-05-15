using Application.Interface.UnitOfWork;
using Domain.Contracts.V1.Master;
using Domain.Entities;
using Domain.ViewModel.API;
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

namespace FireManAPI.Controllers.V1.Master
{
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class CategoriesController : ControllerBase
    {
        private readonly IUnitOfWork _IUnitOfWork;
        private readonly ILogger<CategoriesController> _logger;
        public CategoriesController(ILogger<CategoriesController> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _IUnitOfWork = unitOfWork;
        }
        [HttpGet(ApiRoutes.Categories.GetAll)]
        public async Task<IActionResult> GetAll(int caseFileID = 0)
        {
            //if (caseFileID>0)
            //{
                await _IUnitOfWork.excelDateTime.GetRangeDateTimefromCaseFile(caseFileID);
            //}
            //else
            //{
            //    await _IUnitOfWork.excelDateTime.RoecordsDateTime();
            //}
            var sdisInfo = await _IUnitOfWork.category.GetAllCategories(caseFileID);
            return Ok(sdisInfo);
        }
        [HttpGet(ApiRoutes.Categories.GetActives)]
        public async Task<IActionResult> GetActives()
        {
            var sdisInfo = await _IUnitOfWork.category.GetAll(filter: x => x.IsActive == true, orderBy: x => x.OrderBy(p => p.orderNum));
            return Ok(sdisInfo);
        }
        [HttpGet(ApiRoutes.Categories.GetNonActives)]
        public async Task<IActionResult> GetNonActives()
        {
            var sdisInfo = await _IUnitOfWork.category.GetAll(filter: x => x.IsActive == false, orderBy: x => x.OrderByDescending(p => p.SectionCategoryId));
            return Ok(sdisInfo);
        }
        [HttpGet(ApiRoutes.Categories.Get)]
        public async Task<IActionResult> Get(int categoryId)
        {
            if (categoryId >= 0)
            {
                var sdisInfo = await _IUnitOfWork.category.GetCategoryInfo(categoryId);
                return Ok(sdisInfo);
            }
            else
            {
                return BadRequest(new GenericRequestResponse
                {
                    ErrorMessage = new[] { "Please provide categoryId Id" }
                });
            }
        }

        [HttpDelete(ApiRoutes.Categories.Delete)]
        public async Task<IActionResult> Delete(int categoryId)
        {
            try
            {
                var delSDISItem = await _IUnitOfWork.category.SetRecordAsDeleted(categoryId);
                return Ok(delSDISItem);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }
        [HttpPut(ApiRoutes.Categories.Update)]
        public async Task<IActionResult> Update([FromBody] SectionCategory cat)
        {
            try
            {
                var updateSDISItem = await _IUnitOfWork.category.Update(cat);
                return Ok(updateSDISItem);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }
        [HttpPost(ApiRoutes.Categories.Create)]
        public async Task<IActionResult> Create([FromBody] SectionCategory cat)
        {
            try
            {
                var addSDISItem = await _IUnitOfWork.category.CreateCustom(cat);
                return Ok(addSDISItem);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }
        [HttpGet(ApiRoutes.Categories.ChkExisting)]
        public async Task<IActionResult> GetExistingCode(string Code)
        {
            var sdisInfo = await _IUnitOfWork.category.GetExixtingCode(Code);
            return Ok(sdisInfo);
        }
        [HttpGet(ApiRoutes.Categories.Dropdown)]
        public async Task<IActionResult> Dropdown()
        {
            var companyInfo = await _IUnitOfWork.category.GetListCategoryForDropDown();
            return Ok(companyInfo);
        }
        [HttpPost(ApiRoutes.Categories.orderCategory)]
        public async Task<IActionResult> Ordering([FromBody] List<CategoriesSortListContract> cat)
        {
            try
            {
                var addSDISItem = await _IUnitOfWork.category.CreateCategoryOrder(cat);
                return Ok(addSDISItem);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }


        [HttpPost(ApiRoutes.Categories.orderCategoryTest)]
        public async Task<IActionResult> OrderingTest([FromBody] List<CategoriesSortListTestContract> cat)
        {
            try
            {
                var addSDISItem = await _IUnitOfWork.category.CreateCategoryOrderTest(cat);
                return Ok(addSDISItem);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        [HttpGet(ApiRoutes.Categories.GetFilteredCategories)]
        public async Task<IActionResult> GetFilteredCategories(int caseFileID = 0)
        {
            await _IUnitOfWork.excelDateTime.GetRangeDateTimefromCaseFile(caseFileID);
            var filteredCategory = await _IUnitOfWork.category.GetFilteredCategories(caseFileID);
            return Ok(filteredCategory);
        }
    }
}
