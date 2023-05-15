using Domain.Contracts.V1.CaseFiles;
using Domain.Contracts.V1.CaseFiles.Creation;
using Domain.Entities;
using Domain.ViewModel.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Business.CaseFiles.IAppServices
{
   public interface ICaseFilesRepository
    {
        Task<CaseFilesContract> GetCasesDetails(int casefileId);
        Task<bool> SaveOpenTabCheckListOrder(CaseFileSortList list);
        Task<List<CaseFilesContract>> GetAllCasesList();
        Task<List<CaseFilesContract>> GetAllCasesEstabWiseList(int establishmentId);
        Task<List<CaseFilesContract>> GetRolewiseCaseList(int roleSerial, int userId, int type);
        Task<int> SaveOpenTabFolderCreation(OpenTabFolderCreationContract ord);
        Task<OpenTabFolderCreationContract> GetOpenTabFolderCreation(int establishmentId,int casefileId);
        Task<LastCaseDetailsContract> GetLastDataOpenTabFolderCreation(int establishmentId);
        Task<bool> UpdateOpenTabFolderCreation(OpenTabFolderCreationContract ord);
        Task<OpenTabTownPlanningContract> GetOpenTabTownPlanning(int establishmentId, int casefileId);
        Task<bool> SaveOpenTabTownPlanning(OpenTabTownPlanningContract ord);
        Task<bool> UpdateOpenTabTownPlanning(OpenTabTownPlanningContract ord); 
        Task<VisitPreparingContract> GetOpenTabVisitPreparing(int establishmentId, int casefileId);
        Task<bool> SaveOpenTabVisitPreparing(VisitPreparingContract ord);
        Task<bool> ValidateCaseFile(int casefileId, int EmployeeId);
        Task<SectionFiltersContract> CaseFileCheckSectionsFilter(int casefileId);
        Task<GenericRequestResponse> SaveCaseFileCheckSectionsFilter(CaseFileSectionFilters ord);
        Task<List<CaseFileCheckListDataContract>> GetAllCheckList(int id, int establishmentId, int casefileid);
        Task<List<CaseFileCheckListDataContract>> CaseFileCheckCompleteList();        
        Task<bool> SaveDetailOfObjectChkList(List<OpenTabCheckListDataContract> listData);
        Task<List<CaseFileSelectedCheckList>> GetSelectedCheckList(int id, int casefileid);
        Task<List<CaseFileCheckListDataContract>> GetLastCaseCheckList(int id, int establishmentId);
        Task<bool> CompleteCaseFile(int casefileId);
        Task<GenericRequestResponse> DeleteCaseFile(int casefileId);
        Task<List<SearchCaseFile>> SearchCaseFile(int groupId, int serviceId, int employeeId);
        Task<List<VisitingGroupsListContract>> GetCommiteeMembers(int casefileid, bool flag);
        Task<GenericRequestResponse> SaveCommiteeMembers(VisitingGroupsDataContract listData);
    }
}
