using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FireManAPI.Contract
{
    /*Instead of using hard coded route on each action method, we declare the route here
    and then use on action methods*/
    public static class ApiRoutes
    {
        public const string Root = "api";
        public const string ApiVersion = "v1";
        public const string Base = Root+"/"+ApiVersion;
        public const string LoginAndRegisterBase = Base+"Test";

        public static class Dashboard
        {
            public const string GetTicketsCounts = Base + "/dashboard/GetTicketsCounts";
            public const string GetCurrentYearCaseFileData = Base + "/dashboard/currentyear_casefile_details";
            public const string GetCurrentWeekCaseFileData = Base + "/dashboard/currentweek_casefile_details"; 
            public const string GetLatestFive = Base + "/dashboard/latestfive_establishments";
            public const string GetLastTenMonthlyEmployees = Base + "/dashboard/last_ten_monthly_employees";
            public const string GetLastTenYearlyEmployees = Base + "/dashboard/last_ten_yearly_employees";
            public const string GetAssistanceList = Base + "/dashboard/assistance_list_employees";

            public const string GetLatestCaseFiles = Base + "/dashboard/latest_casefiles_frontend/{id}/{userId}";
            public const string GetCaseFilesCounts = Base + "/dashboard/casefiles_userwise_count_frontend/{userId}"; 

            public const string GetMiscData = Base + "/dashboard/GetMiscData";
            public const string GetWeatherInformation = Base + "/dashboard/GetWeatherInformation"; 

            public const string GetFirstFiveRecords = Base + "/dashboard/GetFirstFiveList";
            public const string GetMonthlyTotRecords = Base + "/dashboard/GetMonthlyTotals/{dat}";
            public const string GetGraphWeekRecords = Base + "/dashboard/GetGraphweeklyList";
            public const string GetNotifications = Base + "/dashboard/GetNotificationList";
            public const string UpdateNotificaiton = Base + "/dashboard/UpdateNotifications/{data}";
        }
        public class ChattingData
        {
            public const string GetRelatedColleagues = Base + "/chatting/all_related_colleagues/{userId}";
            public const string GetEmployeeInfo = Base + "/chatting/get_employeeInfo/{userId}";
            public const string GetChatCollection = Base + "/chatting/get_chatCollection/{sender}/{reciever}";
            public const string InsertMsg = Base + "/chatting/InsertMessage";
            public const string InsertChat = Base + "/chatting/InsertChat";
            public const string GetOnlineEmployees = Base + "/chatting/get_online_employees/{id}";
            public const string GetActiveMessages = Base + "/chatting/get_active_messages/{chatId}/{readMsg}/{Reciever}";
            public const string RecieverSendMessages = Base + "/chatting/reciever_sendmessages/{recieverId}/{readMsg}";
            public const string GetChatById = Base + "/chatting/get_chat_byId/{Id}";
            public const string RecieverUnreadMessages = Base + "/chatting/get_user_unread_messages/{recieverId}"; 
            public const string UpdateUserStatus = Base + "/chatting/update_userstatus";
            public const string BlockChat = Base + "/chatting/block_chat";
            public const string UpdateMsgzReadSeenStatus = Base + "/chatting/update_msgz_read_seen_status";
            public const string UpdateMsgzReadDelieveredStatus = Base + "/chatting/update_msgz_read_delievered_status";
            public const string UpdateMessageStatus = Base + "/chatting/update_msg_status";
            public const string DesktopNotifications = Base + "/chatting/desktop_notifications/{userId}/{roleSerial}";
            public const string AcknowledgeDesktopNotifications = Base + "/chatting/acknowledge_desktop_notifications";

            public const string TestDesktopNotifications = Base + "/chatting/test_desktop_notifications/{userId}";
        }
        public class Chat
        {
            public const string GetMessages = Base + "/chat/GetMessages/{chatId}";
            public const string getorcreatechatroom = Base + "/chat/getorcreatechatroom/{userA}/{userB}";
            public const string GetChatList = Base + "/chat/GetChatList/{id}";
            public const string SendMessage = Base + "/chat/SendMessage";
        }
        public static class SDIS
        {
            public const string GetAll = Base+"/sdis/getall";
            public const string GetActives = Base + "/sdis/getactives";
            public const string GetNonActives = Base + "/sdis/getnonactives";
            public const string Update = Base+ "/sdis/{sdisId}";
            public const string Delete = Base+ "/sdis/{sdisId}";
            public const string Get = Base+ "/sdis/{sdisId}";
            public const string Create = Base+ "/sdis";
            public const string Dropdown = Base + "/sdis/dropdown";
            public const string Counts = Base + "/sdis/count";
            public const string ChkExisting = Base + "/sdis/checkcode/{Code}"; 
        }
        public static class Groups
        {
            public const string GetAll = Base + "/group/getall";
            public const string GetActives = Base + "/group/getactives";
            public const string GetNonActives = Base + "/group/getnonactives";
            public const string Update = Base + "/group/{groupId}";
            public const string Delete = Base + "/group/{groupId}";
            public const string Get = Base + "/group/{groupId}";
            public const string Create = Base + "/group";
            public const string Dropdown = Base + "/group/dropdown/{sdisId}";
            public const string Counts = Base + "/group/count";
            public const string ChkExisting = Base + "/group/checkcode/{Code}";
        }
        public static class Services
        {
            public const string GetAll = Base + "/services/getall";
            public const string GetActives = Base + "/services/getactives";
            public const string GetNonActives = Base + "/services/getnonactives";
            public const string Update = Base + "/services/{servicegroupId}";
            public const string Delete = Base + "/services/{servicegroupId}";
            public const string Get = Base + "/services/{servicegroupId}";
            public const string Create = Base + "/services";
            public const string Dropdown = Base + "/services/dropdown/{groupId}";
            public const string Counts = Base + "/services/count";
            public const string ChkExisting = Base + "/services/checkcode/{Code}";
        }
        public static class Categories
        {
            public const string GetAll = Base + "/categories/getall/{caseFileID}";
            public const string GetActives = Base + "/categories/getactives";
            public const string GetNonActives = Base + "/categories/getnonactives";
            public const string Update = Base + "/categories/{categoryId}";
            public const string Delete = Base + "/categories/{categoryId}";
            public const string Get = Base + "/categories/{categoryId}";
            public const string Create = Base + "/categories";
            public const string Dropdown = Base + "/categories/dropdown";
            public const string Counts = Base + "/categories/count";
            public const string ChkExisting = Base + "/categories/checkcode/{Code}";
            public const string orderCategory = Base + "/categories/sort_category_orderlist";
            public const string orderCategoryTest = Base + "/categories/sort_category_orderlist_test";
            public const string GetFilteredCategories = Base + "/categories/getfilteredcategories/{caseFileID}";
        }
       
        public static class StaffLevelRule
        {
            public const string GetAll = Base + "/stafflevel_rule/getall";
            public const string GetActives = Base + "/stafflevel_rule/getactives";
            public const string GetNonActives = Base + "/stafflevel_rule/getnonactives";
            public const string Update = Base + "/stafflevel_rule/{stafflevelRuleId}";
            public const string Delete = Base + "/stafflevel_rule/{stafflevelRuleId}";
            public const string Get = Base + "/stafflevel_rule/{stafflevelRuleId}";
            public const string Create = Base + "/stafflevel_rule";
            public const string Dropdown = Base + "/stafflevel_rule/dropdown";
            public const string Counts = Base + "/stafflevel_rule/count";
            public const string ChkExisting = Base + "/stafflevel_rule/checkname/{Name}";
        }
        public static class Level
        {
            public const string GetAll = Base + "/level/getall";
            public const string GetActives = Base + "/level/getactives";
            public const string GetNonActives = Base + "/level/getnonactives";
            public const string Update = Base + "/level/{levelId}";
            public const string Delete = Base + "/level/{levelId}";
            public const string Get = Base + "/level/{levelId}";
            public const string Create = Base + "/level";
            public const string Dropdown = Base + "/level/dropdown";
            public const string Counts = Base + "/level/count";
            public const string ChkExisting = Base + "/level/checkname/{Name}";
        }
        public static class CategoryRanking
        {
            public const string GetAll = Base + "/categoryranking/getall";
            public const string GetActives = Base + "/categoryranking/getactives";
            public const string GetNonActives = Base + "/categoryranking/getnonactives";
            public const string Update = Base + "/categoryranking/{categoryrankingId}";
            public const string Delete = Base + "/categoryranking/{categoryrankingId}";
            public const string Get = Base + "/categoryranking/{categoryrankingId}";
            public const string Create = Base + "/categoryranking";
            public const string Dropdown = Base + "/categoryranking/dropdown";
            public const string Counts = Base + "/categoryranking/count";
            public const string ChkExisting = Base + "/categoryranking/checkname/{Name}";
        }
        public static class City
        {
            public const string GetAll = Base + "/city/getall";
            public const string GetActives = Base + "/city/getactives";
            public const string GetNonActives = Base + "/city/getnonactives";
            public const string Update = Base + "/city/{cityId}";
            public const string Delete = Base + "/city/{cityId}";
            public const string Get = Base + "/city/{cityId}";
            public const string Create = Base + "/city";
            public const string Dropdown = Base + "/city/dropdown";
            public const string ExternalDropdown = Base + "/city/external_dropdown";
            public const string Counts = Base + "/city/count";
            public const string ChkExisting = Base + "/city/checkname/{Name}";
        }
        public static class Classification
        {
            public const string GetAll = Base + "/classification/getall";
            public const string GetActives = Base + "/classification/getactives";
            public const string GetNonActives = Base + "/classification/getnonactives";
            public const string Update = Base + "/classification/{classificationId}";
            public const string Delete = Base + "/classification/{classificationId}";
            public const string Get = Base + "/classification/{classificationId}";
            public const string Create = Base + "/classification";
            public const string Dropdown = Base + "/classification/dropdown";
            public const string Counts = Base + "/classification/count";
            public const string ChkExisting = Base + "/classification/checkname/{Name}";
        }
        public static class Positions
        {
            public const string Dropdown = Base + "/positions/dropdown";
        }
        public static class CommitteeQuality
        {
            public const string GetAll = Base + "/committee_quality/getall";
            public const string GetActives = Base + "/committee_quality/getactives";
            public const string GetNonActives = Base + "/committee_quality/getnonactives";
            public const string Update = Base + "/committee_quality/{CommiteeQualityId}";
            public const string Delete = Base + "/committee_quality/{CommiteeQualityId}";
            public const string Get = Base + "/committee_quality/{CommiteeQualityId}";
            public const string Create = Base + "/committee_quality";
            public const string Dropdown = Base + "/committee_quality/dropdown";
            public const string ChkExisting = Base + "/committee_quality/checkname/{Name}";
        }
        //========================== Users =======================================
        public static class UsersInfo
        {
            public const string Login = Base + "/Login";
            public const string AdminLogin = Base + "/Admin_Login";
            public const string Register = Base + "/register";
            public const string SendEmailForVerification = Base + "/sendEmailForVerification";
            public const string EmailVerification = Base + "/emailVerification";
            public const string ForgetPassword = Base + "/forgetPassword";
            public const string Update = Base + "/Users/update";
            public const string UpdateProfile = Base + "/Users/updateprofile";
            public const string Get = Base + "/Users";
            public const string GetAll = Base + "/Users/getAll/{userId}";
            public const string NonValidated = Base + "/Users/NonValidUsers";
            public const string Validate = Base + "/Users/validateUser/{userId}";
            public const string Delete = Base + "/Users/delete_user/{userId}";
        }
        public static class Roles
        {
            public const string GetAll = Base + "/roles/getall";
            public const string GetActives = Base + "/roles/getactives";
            public const string GetNonActives = Base + "/roles/getnonactives";
            public const string Update = Base + "/roles/{roleId}";
            public const string Delete = Base + "/roles/{roleId}";
            public const string Get = Base + "/roles/{roleId}";
            public const string Create = Base + "/roles";
            public const string Dropdown = Base + "/roles/dropdown/{serial}";
            public const string Counts = Base + "/roles/count";
            public const string ChkExisting = Base + "/roles/checkName/{Name}";
        }
        public static class UserRoles
        {
            public const string GetUserRoles = Base + "/RolesOfUser/{UserId}";
            public const string GetUserMenus = Base + "/MenuOfUser/{UserId}";
            public const string GetRolePermissions = Base + "/GetRolePermissions/{RoleId}";
            public const string SetRolePermissions = Base + "/SetRolePermissions/{roleId}/{permissions}";
            public const string SetRolePermissionsAdmin = Base + "/SetRolePermissionsAdmin/{userId}/{permissions}";
        }
        //============================================
        public static class Reports
        {
            public const string CaseFile = Base + "/Reports/{CaseFileId}";
        }
    }
}
