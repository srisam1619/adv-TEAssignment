using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Script.Services;
using System.Web.Script.Serialization;
using System.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Reflection;
using System.Text;
using System.IO;


namespace TE_WebService_V001
{
    /// <summary>
    /// Summary description for TEAssignment
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class TEAssignment : System.Web.Services.WebService
    {

        #region Objects

        clsLog oLog = new clsLog();
        clsDataAccess oDataAccess = new clsDataAccess();
        public string sErrDesc = string.Empty;
        List<result> lstResult = new List<result>();
        JavaScriptSerializer js = new JavaScriptSerializer();
        SAPbobsCOM.Company oDICompany;

        public static string EngineerAvailabilitySetup = System.Configuration.ConfigurationManager.AppSettings["EngineerAvailabilitySetup"].ToString();

        #endregion

        #region Web Methods

        #region Login

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = true)]
        public void GetCompanyList()
        {
            string sFuncName = string.Empty;
            try
            {
                sFuncName = "GetCompanyList";
                oLog.WriteToDebugLogFile("Starting Function ", sFuncName);

                oLog.WriteToDebugLogFile("Before calling the Get_CompanyList() ", sFuncName);
                DataSet ds = oDataAccess.Get_CompanyList();
                oLog.WriteToDebugLogFile("After calling the Get_CompanyList() ", sFuncName);
                if (ds != null && ds.Tables.Count > 0)
                {
                    List<Company> lstCompany = new List<Company>();
                    foreach (DataRow r in ds.Tables[0].Rows)
                    {
                        Company _company = new Company();
                        _company.U_DBName = r["U_DBName"].ToString();
                        _company.U_CompName = r["U_CompName"].ToString();
                        _company.U_SAPUserName = r["U_SAPUserName"].ToString();
                        _company.U_SAPPassword = r["U_SAPPassword"].ToString();
                        _company.U_DBUserName = r["U_DBUserName"].ToString();
                        _company.U_DBPassword = r["U_DBPassword"].ToString();
                        _company.U_ConnString = r["U_ConnString"].ToString();
                        _company.U_Server = r["U_Server"].ToString();
                        _company.U_LicenseServer = r["U_LicenseServer"].ToString();
                        lstCompany.Add(_company);
                    }
                    oLog.WriteToDebugLogFile("Before Serializing the Company List ", sFuncName);
                    Context.Response.Output.Write(js.Serialize(lstCompany));
                    oLog.WriteToDebugLogFile("After Serializing the Company List , the Serialized data is ' " + js.Serialize(lstCompany) + " '", sFuncName);
                }
                else
                {
                    List<Company> lstCompany = new List<Company>();
                    Context.Response.Output.Write(js.Serialize(lstCompany));
                }
            }
            catch (Exception ex)
            {
                sErrDesc = ex.Message.ToString();
                oLog.WriteToErrorLogFile(sErrDesc, sFuncName);
                oLog.WriteToDebugLogFile("Completed With ERROR  ", sFuncName);
                result objResult = new result();
                objResult.Result = "Error";
                objResult.DisplayMessage = sErrDesc;
                lstResult.Add(objResult);
                Context.Response.Output.Write(js.Serialize(lstResult));
            }
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = true)]
        public void LoginValidation(string sJsonInput)
        {
            string sFuncName = string.Empty;
            try
            {
                sFuncName = "LoginValidation()";
                oLog.WriteToDebugLogFile("Starting Function ", sFuncName);

                string sUserName = string.Empty;
                string sPassword = string.Empty;
                string sCompany = string.Empty;

                sJsonInput = "[" + sJsonInput + "]";
                //Split JSON to Individual String
                oLog.WriteToDebugLogFile("Getting the Json Input from Mobile  '" + sJsonInput + "'", sFuncName);
                oLog.WriteToDebugLogFile("Before Deserialize the Json Input ", sFuncName);
                List<Json_UserInfo> lstDeserialize = js.Deserialize<List<Json_UserInfo>>(sJsonInput);
                oLog.WriteToDebugLogFile("After Deserialize the Json Input ", sFuncName);
                if (lstDeserialize.Count > 0)
                {
                    Json_UserInfo objUserInfo = lstDeserialize[0];
                    sUserName = objUserInfo.sUserName;
                    sPassword = objUserInfo.sPassword;
                    sCompany = objUserInfo.sCompany;
                }

                DataSet oDTCompanyList = new DataSet();
                oLog.WriteToDebugLogFile("Before calling the Get_CompanyList() ", sFuncName);
                oDTCompanyList = oDataAccess.Get_CompanyList();
                oLog.WriteToDebugLogFile("After calling the Get_CompanyList() ", sFuncName);
                Session["ODTCompanyList"] = oDTCompanyList;
                oLog.WriteToDebugLogFile("Before calling the Method LoginValidation() ", sFuncName);
                DataSet ds = oDataAccess.LoginValidation(oDTCompanyList, sUserName, sPassword, sCompany);
                oLog.WriteToDebugLogFile("After calling the Method LoginValidation() ", sFuncName);
                if (ds != null && ds.Tables.Count > 0)
                {
                    List<UserInfo> lstUserInfo = new List<UserInfo>();
                    foreach (DataRow r in ds.Tables[0].Rows)
                    {
                        UserInfo _userInfo = new UserInfo();
                        _userInfo.UserId = r["UserId"].ToString();
                        _userInfo.UserName = r["UserName"].ToString();
                        _userInfo.CompanyCode = r["CompanyCode"].ToString();
                        _userInfo.Message = r["Message"].ToString();
                        lstUserInfo.Add(_userInfo);
                    }
                    oLog.WriteToDebugLogFile("Before Serializing the UserInformation ", sFuncName);
                    Context.Response.Output.Write(js.Serialize(lstUserInfo));
                    oLog.WriteToDebugLogFile("After Serializing the UserInformation , the Serialized data is ' " + js.Serialize(lstUserInfo) + " '", sFuncName);
                }
                else
                {
                    List<UserInfo> lstUserInfo = new List<UserInfo>();
                    UserInfo objUserInfo = new UserInfo();
                    objUserInfo.UserId = string.Empty;
                    objUserInfo.UserName = string.Empty;
                    objUserInfo.CompanyCode = sCompany;
                    objUserInfo.Message = "UserName/ Password is Incorrect";
                    lstUserInfo.Add(objUserInfo);

                    Context.Response.Output.Write(js.Serialize(lstUserInfo));
                }
            }
            catch (Exception ex)
            {
                sErrDesc = ex.Message.ToString();
                oLog.WriteToErrorLogFile(sErrDesc, sFuncName);
                oLog.WriteToDebugLogFile("Completed With ERROR  ", sFuncName);
                result objResult = new result();
                objResult.Result = "Error";
                objResult.DisplayMessage = sErrDesc;
                lstResult.Add(objResult);
                Context.Response.Output.Write(js.Serialize(lstResult));
            }
        }

        #endregion

        #region TE Case Assignment

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = true)]
        public void GetCallType(string sJsonInput)
        {
            string sFuncName = string.Empty;
            try
            {
                sFuncName = "GetCallType()";
                oLog.WriteToDebugLogFile("Starting Function ", sFuncName);

                string sCompany = string.Empty;

                sJsonInput = "[" + sJsonInput + "]";
                //Split JSON to Individual String
                oLog.WriteToDebugLogFile("Getting the Json Input from Mobile  '" + sJsonInput + "'", sFuncName);
                oLog.WriteToDebugLogFile("Before Deserialize the Json Input ", sFuncName);
                List<Json_CallType> lstDeserialize = js.Deserialize<List<Json_CallType>>(sJsonInput);
                oLog.WriteToDebugLogFile("After Deserialize the Json Input ", sFuncName);
                if (lstDeserialize.Count > 0)
                {
                    Json_CallType objCallType = lstDeserialize[0];
                    sCompany = objCallType.sCompany;
                }

                oLog.WriteToDebugLogFile("Before Retrieving the Company list from session ", sFuncName);

                DataSet dsCompanyList = oDataAccess.Get_CompanyList();
                oLog.WriteToDebugLogFile("After Retrieving the Company list from session ", sFuncName);
                oLog.WriteToDebugLogFile("Before Calling the method Get_CallType() ", sFuncName);
                DataSet ds = oDataAccess.Get_CallType(dsCompanyList, sCompany);
                oLog.WriteToDebugLogFile("After Calling the method Get_CallType() ", sFuncName);
                if (ds != null && ds.Tables.Count > 0)
                {
                    List<CallType> lstCallType = new List<CallType>();
                    foreach (DataRow r in ds.Tables[0].Rows)
                    {
                        CallType _CallType = new CallType();
                        _CallType.Id = r["Id"].ToString();
                        _CallType.Name = r["Name"].ToString();
                        lstCallType.Add(_CallType);
                    }
                    oLog.WriteToDebugLogFile("Before Serializing the Call Type", sFuncName);
                    Context.Response.Output.Write(js.Serialize(lstCallType));
                    oLog.WriteToDebugLogFile("After Serializing the Call Type, the serialized data is ' " + js.Serialize(lstCallType) + " '", sFuncName);

                }
                else
                {
                    List<CallType> lstCallType = new List<CallType>();
                    Context.Response.Output.Write(js.Serialize(lstCallType));
                }
            }
            catch (Exception ex)
            {
                sErrDesc = ex.Message.ToString();
                oLog.WriteToErrorLogFile(sErrDesc, sFuncName);
                oLog.WriteToDebugLogFile("Completed With ERROR  ", sFuncName);
                result objResult = new result();
                objResult.Result = "Error";
                objResult.DisplayMessage = sErrDesc;
                lstResult.Add(objResult);
                Context.Response.Output.Write(js.Serialize(lstResult));
            }
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = true)]
        public void GetServiceCall(string sJsonInput)
        {
            string sFuncName = string.Empty;
            try
            {
                sFuncName = "GetServiceCall()";
                oLog.WriteToDebugLogFile("Starting Function ", sFuncName);

                string sCompany = string.Empty;

                sJsonInput = "[" + sJsonInput + "]";
                //Split JSON to Individual String
                oLog.WriteToDebugLogFile("Getting the Json Input from Mobile  '" + sJsonInput + "'", sFuncName);
                oLog.WriteToDebugLogFile("Before Deserialize the Json Input ", sFuncName);
                List<Json_CallType> lstDeserialize = js.Deserialize<List<Json_CallType>>(sJsonInput);
                oLog.WriteToDebugLogFile("After Deserialize the Json Input ", sFuncName);
                if (lstDeserialize.Count > 0)
                {
                    Json_CallType objCallType = lstDeserialize[0];
                    sCompany = objCallType.sCompany;
                }

                oLog.WriteToDebugLogFile("Before Retrieving the Company list from session ", sFuncName);

                DataSet dsCompanyList = oDataAccess.Get_CompanyList();
                oLog.WriteToDebugLogFile("After Retrieving the Company list from session ", sFuncName);
                oLog.WriteToDebugLogFile("Before Calling the method Get_ServiceCall() ", sFuncName);
                DataSet ds = oDataAccess.Get_ServiceCall(dsCompanyList, sCompany);
                oLog.WriteToDebugLogFile("After Calling the method Get_ServiceCall() ", sFuncName);
                if (ds != null && ds.Tables.Count > 0)
                {
                    List<ServiceCall> lstServiceCall = new List<ServiceCall>();
                    foreach (DataRow r in ds.Tables[0].Rows)
                    {
                        ServiceCall _ServiceCall = new ServiceCall();
                        _ServiceCall.RowNumber = r["RowNumber"].ToString();
                        _ServiceCall.ServiceCallId = r["ServiceCallId"].ToString();
                        _ServiceCall.CustomerName = r["CustomerName"].ToString();
                        _ServiceCall.StartDate = r["StartDate"].ToString();
                        _ServiceCall.StartTime = r["StartTime"].ToString();
                        _ServiceCall.Issue = r["Issue"].ToString();
                        _ServiceCall.SerialNo = r["SerialNo"].ToString();
                        _ServiceCall.Model = r["Model"].ToString();
                        _ServiceCall.CallType = r["CallType"].ToString();
                        _ServiceCall.ActivityNo = r["ActivityNo"].ToString();
                        _ServiceCall.LineNo = r["LineNo"].ToString();
                        _ServiceCall.ClgCode = r["ClgCode"].ToString();
                        lstServiceCall.Add(_ServiceCall);
                    }
                    oLog.WriteToDebugLogFile("Before Serializing the Service Call", sFuncName);
                    Context.Response.Output.Write(js.Serialize(lstServiceCall));
                    oLog.WriteToDebugLogFile("After Serializing the Service Call, the serialized data is ' " + js.Serialize(lstServiceCall) + " '", sFuncName);

                }
                else
                {
                    List<ServiceCall> lstServiceCall = new List<ServiceCall>();
                    Context.Response.Output.Write(js.Serialize(lstServiceCall));
                }
            }
            catch (Exception ex)
            {
                sErrDesc = ex.Message.ToString();
                oLog.WriteToErrorLogFile(sErrDesc, sFuncName);
                oLog.WriteToDebugLogFile("Completed With ERROR  ", sFuncName);
                result objResult = new result();
                objResult.Result = "Error";
                objResult.DisplayMessage = sErrDesc;
                lstResult.Add(objResult);
                Context.Response.Output.Write(js.Serialize(lstResult));
            }
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = true)]
        public void GetEngineerList(string sJsonInput)
        {
            string sFuncName = string.Empty;
            try
            {
                sFuncName = "GetEngineerList()";
                oLog.WriteToDebugLogFile("Starting Function ", sFuncName);

                string sCompany = string.Empty;
                string sDate = string.Empty;

                sJsonInput = "[" + sJsonInput + "]";
                //Split JSON to Individual String
                oLog.WriteToDebugLogFile("Getting the Json Input from Mobile  '" + sJsonInput + "'", sFuncName);
                oLog.WriteToDebugLogFile("Before Deserialize the Json Input ", sFuncName);
                List<Json_EngineerList> lstDeserialize = js.Deserialize<List<Json_EngineerList>>(sJsonInput);
                oLog.WriteToDebugLogFile("After Deserialize the Json Input ", sFuncName);
                if (lstDeserialize.Count > 0)
                {
                    Json_EngineerList objEngineerList = lstDeserialize[0];
                    sCompany = objEngineerList.sCompany;
                    sDate = objEngineerList.sDate;
                }

                oLog.WriteToDebugLogFile("Before Retrieving the Company list from session ", sFuncName);

                DataSet dsCompanyList = oDataAccess.Get_CompanyList();
                oLog.WriteToDebugLogFile("After Retrieving the Company list from session ", sFuncName);
                oLog.WriteToDebugLogFile("Before Calling the method Get_EngineerList() ", sFuncName);
                DataSet ds = oDataAccess.Get_EngineerList(dsCompanyList, sCompany, sDate);
                oLog.WriteToDebugLogFile("After Calling the method Get_EngineerList() ", sFuncName);
                if (ds != null && ds.Tables.Count > 0)
                {
                    List<EngineerList> lstEngineerList = new List<EngineerList>();
                    foreach (DataRow r in ds.Tables[0].Rows)
                    {
                        EngineerList _EngineerList = new EngineerList();
                        _EngineerList.Id = r["Id"].ToString();
                        _EngineerList.Date = r["Date"].ToString();
                        _EngineerList.EngineerId = r["EngineerId"].ToString();
                        _EngineerList.EngineerName = r["EngineerName"].ToString();
                        _EngineerList.Availability = r["Availability"].ToString();
                        _EngineerList.DisplayCalender = r["DisplayCalender"].ToString();
                        lstEngineerList.Add(_EngineerList);
                    }
                    oLog.WriteToDebugLogFile("Before Serializing the Engineer List", sFuncName);
                    Context.Response.Output.Write(js.Serialize(lstEngineerList));
                    oLog.WriteToDebugLogFile("After Serializing the Engineer List, the serialized data is ' " + js.Serialize(lstEngineerList) + " '", sFuncName);

                }
                else
                {
                    List<EngineerList> lstEngineerList = new List<EngineerList>();
                    Context.Response.Output.Write(js.Serialize(lstEngineerList));
                }
            }
            catch (Exception ex)
            {
                sErrDesc = ex.Message.ToString();
                oLog.WriteToErrorLogFile(sErrDesc, sFuncName);
                oLog.WriteToDebugLogFile("Completed With ERROR  ", sFuncName);
                result objResult = new result();
                objResult.Result = "Error";
                objResult.DisplayMessage = sErrDesc;
                lstResult.Add(objResult);
                Context.Response.Output.Write(js.Serialize(lstResult));
            }
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = true)]
        public void SaveEngineerAvailabilitySetUp(string sJsonInput, string sCompany)
        {
            string sFuncName = string.Empty;
            DataView oDTView = new DataView();
            try
            {
                sFuncName = "SaveEngineerAvailabilitySetUp()";
                oLog.WriteToDebugLogFile("Starting Function ", sFuncName);

                //sJsonInput = "[" + sJsonInput + "]";
                //Convert JSON to Array
                oLog.WriteToDebugLogFile("Getting the Json Input from Mobile  '" + sJsonInput + "'", sFuncName);
                oLog.WriteToDebugLogFile("Before Converting the Json Input to datatable", sFuncName);
                DataTable dtResult = JsonStringToDataTable(sJsonInput);
                oLog.WriteToDebugLogFile("After Converting the Json Input to datatable", sFuncName);

                DataSet dsCompanyList = oDataAccess.Get_CompanyList();
                oLog.WriteToDebugLogFile("After Retrieving the Company list", sFuncName);

                // Check engineer data is already availabe for the particular date
                oDataAccess.Check_EngineerAvailablityCount(dsCompanyList, dtResult.Rows[0]["Date"].ToString(), sCompany);

                oDTView = dsCompanyList.Tables[0].DefaultView;

                oDTView.RowFilter = "U_DBName= '" + sCompany + "'";
                oLog.WriteToDebugLogFile("Before Connecting to SQL", sFuncName);
                if (oDTView != null && oDTView.Count > 0)
                {
                    using (SqlConnection connection = new SqlConnection(oDTView[0]["U_ConnString"].ToString()))
                    {
                        SqlBulkCopy bulkCopy = new SqlBulkCopy
                        (
                        connection,
                        SqlBulkCopyOptions.TableLock |
                        SqlBulkCopyOptions.FireTriggers |
                        SqlBulkCopyOptions.UseInternalTransaction,
                        null
                        );

                        bulkCopy.DestinationTableName = EngineerAvailabilitySetup.ToString();
                        connection.Open();
                        oLog.WriteToDebugLogFile("Before Writing to SQL", sFuncName);
                        // write the data in the "dataTable"
                        bulkCopy.WriteToServer(dtResult);
                        oLog.WriteToDebugLogFile("After Writing to SQL", sFuncName);
                        connection.Close();
                        oLog.WriteToDebugLogFile("Completed With SUCCESS  ", sFuncName);
                        result objResult = new result();
                        objResult.Result = "Success";
                        objResult.DisplayMessage = "Engineer Availability Setup Successfully";
                        lstResult.Add(objResult);
                        Context.Response.Output.Write(js.Serialize(lstResult));
                    }
                }
            }
            catch (Exception ex)
            {
                sErrDesc = ex.Message.ToString();
                oLog.WriteToErrorLogFile(sErrDesc, sFuncName);
                oLog.WriteToDebugLogFile("Completed With ERROR  ", sFuncName);
                result objResult = new result();
                objResult.Result = "Error";
                objResult.DisplayMessage = sErrDesc;
                lstResult.Add(objResult);
                Context.Response.Output.Write(js.Serialize(lstResult));
            }
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = true)]
        public void GetAssignedEngineer(string sJsonInput)
        {
            string sFuncName = string.Empty;
            try
            {
                sFuncName = "GetAssignedEngineer()";
                oLog.WriteToDebugLogFile("Starting Function ", sFuncName);

                string sCompany = string.Empty;
                string sDate = string.Empty;

                sJsonInput = "[" + sJsonInput + "]";
                //Split JSON to Individual String
                oLog.WriteToDebugLogFile("Getting the Json Input from Mobile  '" + sJsonInput + "'", sFuncName);
                oLog.WriteToDebugLogFile("Before Deserialize the Json Input ", sFuncName);
                List<Json_EngineerList> lstDeserialize = js.Deserialize<List<Json_EngineerList>>(sJsonInput);
                oLog.WriteToDebugLogFile("After Deserialize the Json Input ", sFuncName);
                if (lstDeserialize.Count > 0)
                {
                    Json_EngineerList objEnggList = lstDeserialize[0];
                    sCompany = objEnggList.sCompany;
                    sDate = objEnggList.sDate;
                }

                oLog.WriteToDebugLogFile("Before Retrieving the Company list from session ", sFuncName);

                DataSet dsCompanyList = oDataAccess.Get_CompanyList();
                oLog.WriteToDebugLogFile("After Retrieving the Company list from session ", sFuncName);
                oLog.WriteToDebugLogFile("Before Calling the method Get_AssignedEngineer() ", sFuncName);
                DataSet ds = oDataAccess.Get_AssignedEngineer(dsCompanyList, sCompany, sDate);
                oLog.WriteToDebugLogFile("After Calling the method Get_AssignedEngineer() ", sFuncName);
                if (ds != null && ds.Tables.Count > 0)
                {
                    List<AssignedEngineer> lstAssignedEngineer = new List<AssignedEngineer>();
                    foreach (DataRow r in ds.Tables[0].Rows)
                    {
                        AssignedEngineer _AssignedEngineer = new AssignedEngineer();
                        _AssignedEngineer.EngineerId = r["EngineerId"].ToString();
                        _AssignedEngineer.EngineerName = r["EngineerName"].ToString();
                        _AssignedEngineer.CurrentStatus = r["CurrentStatus"].ToString();
                        _AssignedEngineer.LastKnownCaseCustomerID = r["LastKnownCaseCustomerID"].ToString();
                        _AssignedEngineer.ESTETA = r["ESTETA"].ToString();
                        _AssignedEngineer.EndTime = r["EndTime"].ToString();
                        _AssignedEngineer.LastKnownCaseCustomerAddress = r["LastKnownCaseCustomerAddress"].ToString();
                        _AssignedEngineer.CustomerName = r["CustomerName"].ToString();
                        lstAssignedEngineer.Add(_AssignedEngineer);
                    }
                    oLog.WriteToDebugLogFile("Before Serializing the Assigned Engineer", sFuncName);
                    Context.Response.Output.Write(js.Serialize(lstAssignedEngineer));
                    oLog.WriteToDebugLogFile("After Serializing the Assigned Engineer, the serialized data is ' " + js.Serialize(lstAssignedEngineer) + " '", sFuncName);

                }
                else
                {
                    List<AssignedEngineer> lstAssignedEngineer = new List<AssignedEngineer>();
                    Context.Response.Output.Write(js.Serialize(lstAssignedEngineer));
                }
            }
            catch (Exception ex)
            {
                sErrDesc = ex.Message.ToString();
                oLog.WriteToErrorLogFile(sErrDesc, sFuncName);
                oLog.WriteToDebugLogFile("Completed With ERROR  ", sFuncName);
                result objResult = new result();
                objResult.Result = "Error";
                objResult.DisplayMessage = sErrDesc;
                lstResult.Add(objResult);
                Context.Response.Output.Write(js.Serialize(lstResult));
            }
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = true)]
        public void GetEngineerCalenderView(string sDate, string sCompany)
        {
            string sFuncName = string.Empty;
            try
            {
                sFuncName = "GetEngineerCalenderView()";
                oLog.WriteToDebugLogFile("Starting Function ", sFuncName);

                oLog.WriteToDebugLogFile("Before Retrieving the Company list from session ", sFuncName);
                DataSet dsCompanyList = oDataAccess.Get_CompanyList();
                oLog.WriteToDebugLogFile("After Retrieving the Company list from session ", sFuncName);
                oLog.WriteToDebugLogFile("Before Calling the method Get_EngineerCalenderView() ", sFuncName);
                DataSet ds = oDataAccess.Get_EngineerCalenderView(dsCompanyList,sDate, sCompany);
                oLog.WriteToDebugLogFile("After Calling the method Get_EngineerCalenderView() ", sFuncName);
                if (ds != null && ds.Tables.Count > 0)
                {
                    List<EngineerCalenderView> lstEngineerCalenderView = new List<EngineerCalenderView>();
                    foreach (DataRow r in ds.Tables[0].Rows)
                    {
                        EngineerCalenderView _EngineerCalenderView = new EngineerCalenderView();
                        _EngineerCalenderView.RowNum = r["RowNum"].ToString();
                        _EngineerCalenderView.Status = r["Status"].ToString();
                        _EngineerCalenderView.CallId = r["CallId"].ToString();
                        _EngineerCalenderView.ClgCode = r["ClgCode"].ToString();
                        _EngineerCalenderView.CustomerName = r["CustomerName"].ToString();
                        _EngineerCalenderView.RespDate = r["RespDate"].ToString();
                        _EngineerCalenderView.RespTime = r["RespTime"].ToString();
                        _EngineerCalenderView.EndDate = r["EndDate"].ToString();
                        _EngineerCalenderView.EndTime = r["EndTime"].ToString();
                        _EngineerCalenderView.Subject = r["Subject"].ToString();
                        _EngineerCalenderView.InternalSN = r["InternalSN"].ToString();
                        _EngineerCalenderView.ItemName = r["ItemName"].ToString();
                        _EngineerCalenderView.ActivityType = r["ActivityType"].ToString();
                        _EngineerCalenderView.EmployeeId = r["EmployeeId"].ToString();
                        _EngineerCalenderView.EmployeeName = r["EmployeeName"].ToString();
                        _EngineerCalenderView.Result = r["Result"].ToString();
                        _EngineerCalenderView.Address = r["Address"].ToString();
                        _EngineerCalenderView.ETA = r["ETA"].ToString();
                        lstEngineerCalenderView.Add(_EngineerCalenderView);
                    }
                    oLog.WriteToDebugLogFile("Before Serializing the Engineer calender view", sFuncName);
                    Context.Response.Output.Write(js.Serialize(lstEngineerCalenderView));
                    oLog.WriteToDebugLogFile("After Serializing the Engineer calender view, the serialized data is ' " + js.Serialize(lstEngineerCalenderView) + " '", sFuncName);

                }
                else
                {
                    List<EngineerCalenderView> lstEngineerCalenderView = new List<EngineerCalenderView>();
                    Context.Response.Output.Write(js.Serialize(lstEngineerCalenderView));
                }
            }
            catch (Exception ex)
            {
                sErrDesc = ex.Message.ToString();
                oLog.WriteToErrorLogFile(sErrDesc, sFuncName);
                oLog.WriteToDebugLogFile("Completed With ERROR  ", sFuncName);
                result objResult = new result();
                objResult.Result = "Error";
                objResult.DisplayMessage = sErrDesc;
                lstResult.Add(objResult);
                Context.Response.Output.Write(js.Serialize(lstResult));
            }
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = true)]
        public void SubmitTEAssignment(string sJsonInput, string sCompany)
        {
            string sFuncName = string.Empty;
            try
            {
                sFuncName = "SubmitTEAssignment()";
                oLog.WriteToDebugLogFile("Starting Function ", sFuncName);

                oLog.WriteToDebugLogFile("Getting the Json Input from Mobile  '" + sJsonInput + "'", sFuncName);
                oLog.WriteToDebugLogFile("Before Converting the Json Input to datatable", sFuncName);
                DataTable dtResult = JsonStringToDataTable(sJsonInput);
                oLog.WriteToDebugLogFile("After Converting the Json Input to datatable", sFuncName);

                // Pass the Dataset to Table to update in SCL5 and create a Activity for the service call.
                string sResult = oDataAccess.CreateActivityDocument(dtResult, sCompany);

                if (sResult == "SUCCESS")
                {
                    result objResult = new result();
                    objResult.Result = "SUCCESS";
                    objResult.DisplayMessage = "Activity Added Successfully";
                    lstResult.Add(objResult);
                    Context.Response.Output.Write(js.Serialize(lstResult));
                }
                else
                {
                    result objResult = new result();
                    objResult.Result = "FAILURE";
                    objResult.DisplayMessage = sResult;
                    lstResult.Add(objResult);
                    Context.Response.Output.Write(js.Serialize(lstResult));
                }
            }
            catch (Exception ex)
            {
                sErrDesc = ex.Message.ToString();
                oLog.WriteToErrorLogFile(sErrDesc, sFuncName);
                oLog.WriteToDebugLogFile("Completed With ERROR  ", sFuncName);
                result objResult = new result();
                objResult.Result = "Error";
                objResult.DisplayMessage = sErrDesc;
                lstResult.Add(objResult);
                Context.Response.Output.Write(js.Serialize(lstResult));
            }
        }

        #endregion

        #endregion

        #region Classes

        class result
        {
            public string Result { get; set; }
            public string DisplayMessage { get; set; }
        }

        class Company
        {
            public string U_DBName { get; set; }
            public string U_CompName { get; set; }
            public string U_SAPUserName { get; set; }
            public string U_SAPPassword { get; set; }
            public string U_DBUserName { get; set; }
            public string U_DBPassword { get; set; }
            public string U_ConnString { get; set; }
            public string U_Server { get; set; }
            public string U_LicenseServer { get; set; }
        }

        class UserInfo
        {
            public string UserId { get; set; }
            public string UserName { get; set; }
            public string CompanyCode { get; set; }
            public string Message { get; set; }
        }

        class Json_UserInfo
        {
            public string sUserName { get; set; }
            public string sPassword { get; set; }
            public string sCompany { get; set; }
        }

        class Json_CallType
        {
            public string sCompany { get; set; }
        }

        class Json_EngineerList
        {
            public string sCompany { get; set; }
            public string sDate { get; set; }
        }

        class CallType
        {
            public string Id { get; set; }
            public string Name { get; set; }
        }

        class ServiceCall
        {
            public string RowNumber { get; set; }
            public string ServiceCallId { get; set; }
            public string CustomerName { get; set; }
            public string StartDate { get; set; }
            public string StartTime { get; set; }
            public string Issue { get; set; }
            public string SerialNo { get; set; }
            public string Model { get; set; }
            public string CallType { get; set; }
            public string ActivityNo { get; set; }
            public string LineNo { get; set; }
            public string ClgCode { get; set; }
        }

        class EngineerList
        {
            public string Id { get; set; }
            public string Date { get; set; }
            public string EngineerId { get; set; }
            public string EngineerName { get; set; }
            public string Availability { get; set; }
            public string DisplayCalender { get; set; }
        }

        class AssignedEngineer
        {
            public string EngineerId { get; set; }
            public string EngineerName { get; set; }
            public string CurrentStatus { get; set; }
            public string LastKnownCaseCustomerID { get; set; }
            public string ESTETA { get; set; }
            public string EndTime { get; set; }
            public string LastKnownCaseCustomerAddress { get; set; }
            public string CustomerName { get; set; }
        }

        class EngineerCalenderView
        {
            public string RowNum { get; set; }
            public string Status { get; set; }
            public string CallId { get; set; }
            public string ClgCode { get; set; }
            public string CustomerName { get; set; }
            public string RespDate { get; set; }
            public string RespTime { get; set; }
            public string EndDate { get; set; }
            public string EndTime { get; set; }
            public string Subject { get; set; }
            public string InternalSN { get; set; }
            public string ItemName { get; set; }
            public string ActivityType { get; set; }
            public string EmployeeId { get; set; }
            public string EmployeeName { get; set; }
            public string Result { get; set; }
            public string Address { get; set; }
            public string ETA { get; set; }
        }

        #endregion

        #region public methods

        public DataTable JsonStringToDataTable(string jsonString)
        {
            DataTable dt = new DataTable();
            string sFuncName = string.Empty;
            try
            {
                sFuncName = "JsonStringToDataTable()";
                oLog.WriteToDebugLogFile("Starting Function ", sFuncName);

                string[] jsonStringArray = Regex.Split(jsonString.Replace("[", "").Replace("]", ""), "},{");
                List<string> ColumnsName = new List<string>();
                foreach (string jSA in jsonStringArray)
                {
                    string[] jsonStringData = Regex.Split(jSA.Replace("{", "").Replace("}", ""), ",");
                    foreach (string ColumnsNameData in jsonStringData)
                    {
                        try
                        {
                            int idx = ColumnsNameData.IndexOf(":");
                            string ColumnsNameString = ColumnsNameData.Substring(0, idx - 1).Replace("\"", "");
                            if (!ColumnsName.Contains(ColumnsNameString.Trim()))
                            {
                                ColumnsName.Add(ColumnsNameString.Trim());
                            }
                        }
                        catch (Exception ex)
                        {
                            throw new Exception(string.Format("Error Parsing Column Name : {0}", ColumnsNameData));
                        }
                    }
                    break;
                }
                foreach (string AddColumnName in ColumnsName)
                {
                    if (AddColumnName.Contains("Date"))
                    { dt.Columns.Add(AddColumnName, typeof(DateTime)); }
                    else
                    { dt.Columns.Add(AddColumnName); }

                }
                foreach (string jSA in jsonStringArray)
                {
                    string[] RowData = Regex.Split(jSA.Replace("{", "").Replace("}", ""), ",");
                    DataRow nr = dt.NewRow();
                    foreach (string rowData in RowData)
                    {
                        try
                        {
                            int idx = rowData.Trim().IndexOf(":");
                            string RowColumns = rowData.Trim().Substring(0, idx - 1).Replace("\"", "");
                            string RowDataString = rowData.Trim().Substring(idx + 1).Replace("\"", "");
                            nr[RowColumns] = RowDataString.Trim();
                        }
                        catch (Exception ex)
                        {
                            continue;
                        }
                    }
                    dt.Rows.Add(nr);
                }
            }
            catch (Exception ex)
            {
                sErrDesc = ex.Message.ToString();
                oLog.WriteToErrorLogFile(sErrDesc, sFuncName);
                oLog.WriteToDebugLogFile("Completed With ERROR  ", sFuncName);
            }
            return dt;
        }

        #endregion
    }
}
