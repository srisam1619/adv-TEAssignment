using System;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;

namespace TE_WebService_V001
{
    public class clsDataAccess
    {
        clsLog oLog = new clsLog();
        public string sErrDesc = string.Empty;
        SAPbobsCOM.Company oDICompany;
        string RTN_SUCCESS = "SUCCESS";

        public static string ConnectionString = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;

        #region Login
        public DataSet Get_CompanyList()
        {
            DataSet oDataset;
            string sFuncName = string.Empty;
            string sProcName = string.Empty;

            try
            {
                sFuncName = "Get_CompanyList()";
                sProcName = "VS_SP001_Web_GetCompanyList";
                oLog.WriteToDebugLogFile("Starting Function ", sFuncName);

                oLog.WriteToDebugLogFile("Calling Run_StoredProcedure() " + sProcName, sFuncName);

                oDataset = SqlHelper.ExecuteDataSet(ConnectionString, CommandType.StoredProcedure, sProcName);

                oLog.WriteToDebugLogFile("Completed With SUCCESS  ", sFuncName);

                return oDataset;
            }
            catch (Exception Ex)
            {
                sErrDesc = Ex.Message.ToString();
                oLog.WriteToErrorLogFile(sErrDesc, sFuncName);
                oLog.WriteToDebugLogFile("Completed With ERROR  ", sFuncName);
                throw Ex;
            }
        }

        public DataSet LoginValidation(DataSet oDTCompanyList, string sUserName, string sPassword, string sCompany)
        {
            DataSet oDataset = new DataSet();
            string sFuncName = string.Empty;
            string sProcName = string.Empty;
            DataView oDTView = new DataView();

            try
            {
                sFuncName = "LoginValidation()";
                sProcName = "VS_SP002_Web_LoginValidation";

                oLog.WriteToDebugLogFile("Starting Function ", sFuncName);

                oLog.WriteToDebugLogFile("Calling Run_StoredProcedure() " + sProcName, sFuncName);

                if (oDTCompanyList != null && oDTCompanyList.Tables.Count > 0)
                {
                    oDTView = oDTCompanyList.Tables[0].DefaultView;

                    oDTView.RowFilter = "U_DBName= '" + sCompany + "'";
                    if (oDTView != null && oDTView.Count > 0)
                    {
                        oDataset = SqlHelper.ExecuteDataSet(oDTView[0]["U_ConnString"].ToString(), CommandType.StoredProcedure, sProcName,
                            Data.CreateParameter("@UserCode", sUserName), Data.CreateParameter("@Password", sPassword), Data.CreateParameter("@Company", sCompany));
                        oLog.WriteToDebugLogFile("Completed With SUCCESS  ", sFuncName);
                    }
                    else
                    {
                        oLog.WriteToDebugLogFile("No Data in OUSR table for the selected Company", sFuncName);
                        return oDataset;
                    }
                }
                else
                {
                    oLog.WriteToDebugLogFile("There is No Company List in the Holding Company ", sFuncName);
                    return oDataset;
                }

                return oDataset;
            }
            catch (Exception Ex)
            {
                sErrDesc = Ex.Message.ToString();
                oLog.WriteToErrorLogFile(sErrDesc, sFuncName);
                oLog.WriteToDebugLogFile("Completed With ERROR  ", sFuncName);
                throw Ex;
            }
        }
        #endregion

        #region TE Case Assignment

        public DataSet Get_CallType(DataSet oDTCompanyList, string sCompany)
        {
            DataSet oDataset = new DataSet();
            string sFuncName = string.Empty;
            string sProcName = string.Empty;
            DataView oDTView = new DataView();

            try
            {
                sFuncName = "Get_CallType()";
                sProcName = "VS_TE_SP013_Web_CallType";
                oLog.WriteToDebugLogFile("Starting Function ", sFuncName);

                oLog.WriteToDebugLogFile("Calling Run_StoredProcedure() " + sProcName, sFuncName);
                if (oDTCompanyList != null && oDTCompanyList.Tables.Count > 0)
                {
                    oDTView = oDTCompanyList.Tables[0].DefaultView;

                    oDTView.RowFilter = "U_DBName= '" + sCompany + "'";
                    if (oDTView != null && oDTView.Count > 0)
                    {
                        oDataset = SqlHelper.ExecuteDataSet(oDTView[0]["U_ConnString"].ToString(), CommandType.StoredProcedure, sProcName,
                            Data.CreateParameter("@Company", sCompany));

                        oLog.WriteToDebugLogFile("Completed With SUCCESS ", sFuncName);
                    }
                    else
                    {
                        oLog.WriteToDebugLogFile("No Data in OUSR table for the selected Company", sFuncName);
                        return oDataset;
                    }
                }
                else
                {
                    oLog.WriteToDebugLogFile("There is No Company List in the Holding Company ", sFuncName);
                    return oDataset;
                }
                return oDataset;
            }
            catch (Exception Ex)
            {
                sErrDesc = Ex.Message.ToString();
                oLog.WriteToErrorLogFile(sErrDesc, sFuncName);
                oLog.WriteToDebugLogFile("Completed With ERROR  ", sFuncName);
                throw Ex;
            }
        }

        public DataSet Get_ServiceCall(DataSet oDTCompanyList, string sCompany)
        {
            DataSet oDataset = new DataSet();
            string sFuncName = string.Empty;
            string sProcName = string.Empty;
            DataView oDTView = new DataView();

            try
            {
                sFuncName = "Get_ServiceCall()";
                sProcName = "VS_TE_SP014_Web_ServiceCallInfo";
                oLog.WriteToDebugLogFile("Starting Function ", sFuncName);

                oLog.WriteToDebugLogFile("Calling Run_StoredProcedure() " + sProcName, sFuncName);
                if (oDTCompanyList != null && oDTCompanyList.Tables.Count > 0)
                {
                    oDTView = oDTCompanyList.Tables[0].DefaultView;

                    oDTView.RowFilter = "U_DBName= '" + sCompany + "'";
                    if (oDTView != null && oDTView.Count > 0)
                    {
                        oDataset = SqlHelper.ExecuteDataSet(oDTView[0]["U_ConnString"].ToString(), CommandType.StoredProcedure, sProcName,
                            Data.CreateParameter("@Company", sCompany));

                        oLog.WriteToDebugLogFile("Completed With SUCCESS ", sFuncName);
                    }
                    else
                    {
                        oLog.WriteToDebugLogFile("No Data in OUSR table for the selected Company", sFuncName);
                        return oDataset;
                    }
                }
                else
                {
                    oLog.WriteToDebugLogFile("There is No Company List in the Holding Company ", sFuncName);
                    return oDataset;
                }
                return oDataset;
            }
            catch (Exception Ex)
            {
                sErrDesc = Ex.Message.ToString();
                oLog.WriteToErrorLogFile(sErrDesc, sFuncName);
                oLog.WriteToDebugLogFile("Completed With ERROR  ", sFuncName);
                throw Ex;
            }
        }

        public DataSet Get_EngineerList(DataSet oDTCompanyList, string sCompany, string sDate)
        {
            DataSet oDataset = new DataSet();
            string sFuncName = string.Empty;
            string sProcName = string.Empty;
            DataView oDTView = new DataView();

            try
            {
                sFuncName = "Get_EngineerList()";
                sProcName = "VS_TE_SP015_Web_GetEngineerList";
                oLog.WriteToDebugLogFile("Starting Function ", sFuncName);

                oLog.WriteToDebugLogFile("Calling Run_StoredProcedure() " + sProcName, sFuncName);
                if (oDTCompanyList != null && oDTCompanyList.Tables.Count > 0)
                {
                    oDTView = oDTCompanyList.Tables[0].DefaultView;

                    oDTView.RowFilter = "U_DBName= '" + sCompany + "'";
                    if (oDTView != null && oDTView.Count > 0)
                    {
                        oDataset = SqlHelper.ExecuteDataSet(oDTView[0]["U_ConnString"].ToString(), CommandType.StoredProcedure, sProcName,
                            Data.CreateParameter("@Company", sCompany), Data.CreateParameter("@Date", sDate));

                        oLog.WriteToDebugLogFile("Completed With SUCCESS ", sFuncName);
                    }
                    else
                    {
                        oLog.WriteToDebugLogFile("No Data in OUSR table for the selected Company", sFuncName);
                        return oDataset;
                    }
                }
                else
                {
                    oLog.WriteToDebugLogFile("There is No Company List in the Holding Company ", sFuncName);
                    return oDataset;
                }
                return oDataset;
            }
            catch (Exception Ex)
            {
                sErrDesc = Ex.Message.ToString();
                oLog.WriteToErrorLogFile(sErrDesc, sFuncName);
                oLog.WriteToDebugLogFile("Completed With ERROR  ", sFuncName);
                throw Ex;
            }
        }

        public DataSet Get_AssignedEngineer(DataSet oDTCompanyList, string sCompany, string sDate)
        {
            DataSet oDataset = new DataSet();
            string sFuncName = string.Empty;
            string sProcName = string.Empty;
            DataView oDTView = new DataView();

            try
            {
                sFuncName = "Get_AssignedEngineer()";
                sProcName = "VS_TE_SP017_Web_GetAssignedEngineer";
                oLog.WriteToDebugLogFile("Starting Function ", sFuncName);

                oLog.WriteToDebugLogFile("Calling Run_StoredProcedure() " + sProcName, sFuncName);
                if (oDTCompanyList != null && oDTCompanyList.Tables.Count > 0)
                {
                    oDTView = oDTCompanyList.Tables[0].DefaultView;

                    oDTView.RowFilter = "U_DBName= '" + sCompany + "'";
                    if (oDTView != null && oDTView.Count > 0)
                    {
                        oDataset = SqlHelper.ExecuteDataSet(oDTView[0]["U_ConnString"].ToString(), CommandType.StoredProcedure, sProcName,
                            Data.CreateParameter("@Company", sCompany), Data.CreateParameter("@Date", sDate));

                        oLog.WriteToDebugLogFile("Completed With SUCCESS ", sFuncName);
                    }
                    else
                    {
                        oLog.WriteToDebugLogFile("No Data in OUSR table for the selected Company", sFuncName);
                        return oDataset;
                    }
                }
                else
                {
                    oLog.WriteToDebugLogFile("There is No Company List in the Holding Company ", sFuncName);
                    return oDataset;
                }
                return oDataset;
            }
            catch (Exception Ex)
            {
                sErrDesc = Ex.Message.ToString();
                oLog.WriteToErrorLogFile(sErrDesc, sFuncName);
                oLog.WriteToDebugLogFile("Completed With ERROR  ", sFuncName);
                throw Ex;
            }
        }

        public string Check_EngineerAvailablityCount(DataSet oDTCompanyList, string sDate, string sCompany)
        {
            DataSet oDataset = new DataSet();
            string sResult = string.Empty;
            string sFuncName = string.Empty;
            string sProcName = string.Empty;
            DataView oDTView = new DataView();

            try
            {
                sFuncName = "Check_EngineerAvailablityCount()";
                sProcName = "VS_TE_SP016_Web_CheckEngineerAvailablityCount";
                oLog.WriteToDebugLogFile("Starting Function ", sFuncName);

                oLog.WriteToDebugLogFile("Calling Run_StoredProcedure() " + sProcName, sFuncName);
                if (oDTCompanyList != null && oDTCompanyList.Tables.Count > 0)
                {
                    oDTView = oDTCompanyList.Tables[0].DefaultView;

                    oDTView.RowFilter = "U_DBName= '" + sCompany + "'";
                    if (oDTView != null && oDTView.Count > 0)
                    {
                        oDataset = SqlHelper.ExecuteDataSet(oDTView[0]["U_ConnString"].ToString(), CommandType.StoredProcedure, sProcName,
                            Data.CreateParameter("@Date", sDate), Data.CreateParameter("@Company", sCompany));
                        sResult = "SUCCESS";
                        oLog.WriteToDebugLogFile("Completed With SUCCESS ", sFuncName);
                    }
                    else
                    {
                        oLog.WriteToDebugLogFile("No Data in OUSR table for the selected Company", sFuncName);
                    }
                }
                else
                {
                    oLog.WriteToDebugLogFile("There is No Company List in the Holding Company ", sFuncName);
                }
                return sResult;
            }
            catch (Exception Ex)
            {
                sErrDesc = Ex.Message.ToString();
                oLog.WriteToErrorLogFile(sErrDesc, sFuncName);
                oLog.WriteToDebugLogFile("Completed With ERROR  ", sFuncName);
                return sErrDesc;
            }
        }

        public DataSet Get_EngineerCalenderView(DataSet oDTCompanyList, string sDate, string sCompany)
        {
            DataSet oDataset = new DataSet();
            string sResult = string.Empty;
            string sFuncName = string.Empty;
            string sProcName = string.Empty;
            DataView oDTView = new DataView();

            try
            {
                sFuncName = "Get_EngineerCalenderView()";
                sProcName = "VS_TE_SP017_Web_EngineerCalenderView";
                oLog.WriteToDebugLogFile("Starting Function ", sFuncName);

                oLog.WriteToDebugLogFile("Calling Run_StoredProcedure() " + sProcName, sFuncName);
                if (oDTCompanyList != null && oDTCompanyList.Tables.Count > 0)
                {
                    oDTView = oDTCompanyList.Tables[0].DefaultView;

                    oDTView.RowFilter = "U_DBName= '" + sCompany + "'";
                    if (oDTView != null && oDTView.Count > 0)
                    {
                        oDataset = SqlHelper.ExecuteDataSet(oDTView[0]["U_ConnString"].ToString(), CommandType.StoredProcedure, sProcName,
                            Data.CreateParameter("@Date", sDate), Data.CreateParameter("@Company", sCompany));
                        oLog.WriteToDebugLogFile("Completed With SUCCESS ", sFuncName);
                    }
                    else
                    {
                        oLog.WriteToDebugLogFile("No Data in OUSR table for the selected Company", sFuncName);
                        return oDataset;
                    }
                }
                else
                {
                    oLog.WriteToDebugLogFile("There is No Company List in the Holding Company ", sFuncName);
                    return oDataset;
                }
                return oDataset;
            }
            catch (Exception Ex)
            {
                sErrDesc = Ex.Message.ToString();
                oLog.WriteToErrorLogFile(sErrDesc, sFuncName);
                oLog.WriteToDebugLogFile("Completed With ERROR  ", sFuncName);
                throw Ex;
            }
        }

        public string CreateActivityDocument(DataTable dtResult, string sCompany)
        {
            string sResult = string.Empty;
            string sFuncName = "CreateActivityDocument()";
            try
            {
                oLog.WriteToDebugLogFile("Starting Function  ", sFuncName);
                DataView dView = new DataView(dtResult);
                sResult = AddActivityUsingActivityService(dView, sCompany);
                oLog.WriteToDebugLogFile("Ending Function  ", sFuncName);
                return sResult;
            }
            catch (Exception Ex)
            {
                sErrDesc = Ex.Message.ToString();
                oLog.WriteToErrorLogFile(sErrDesc, sFuncName);
                oLog.WriteToDebugLogFile("Completed With ERROR  ", sFuncName);
                return sErrDesc;
            }
        }

        #endregion

        #region CompanyConnection
        public SAPbobsCOM.Company ConnectToTargetCompany(string sCompanyDB)
        {
            string sFuncName = string.Empty;
            string sReturnValue = string.Empty;
            DataSet oDTCompanyList = new DataSet();
            DataSet oDSResult = new DataSet();
            string sConnString = string.Empty;
            DataView oDTView = new DataView();

            try
            {
                sFuncName = "ConnectToTargetCompany()";

                oLog.WriteToDebugLogFile("Starting Function ", sFuncName);


                if (oDICompany != null)
                {
                    if (oDICompany.CompanyDB == sCompanyDB)
                    {
                        oLog.WriteToDebugLogFile("ODICompany Name " + oDICompany.CompanyDB, sFuncName);
                        oLog.WriteToDebugLogFile("SCompanyDB " + sCompanyDB, sFuncName);
                        return oDICompany;
                    }

                }

                oLog.WriteToDebugLogFile("Calling Get_Company_Details() ", sFuncName);
                oDTCompanyList = Get_CompanyList();

                oLog.WriteToDebugLogFile("Calling Filter Based on Company DB() ", sFuncName);
                oDTView = oDTCompanyList.Tables[0].DefaultView;
                oDTView.RowFilter = "U_DBName= '" + sCompanyDB + "'";

                oLog.WriteToDebugLogFile("Calling ConnectToTargetCompany() ", sFuncName);

                sConnString = oDTView[0]["U_ConnString"].ToString();

                oDICompany = ConnectToTargetCompany(oDICompany, oDTView[0]["U_SAPUserName"].ToString(), oDTView[0]["U_SAPPassword"].ToString()
                                   , oDTView[0]["U_DBName"].ToString(), oDTView[0]["U_Server"].ToString(), oDTView[0]["U_LicenseServer"].ToString()
                                   , oDTView[0]["U_DBUserName"].ToString(), oDTView[0]["U_DBPassword"].ToString(), sErrDesc);

                oLog.WriteToDebugLogFile("Completed With SUCCESS  ", sFuncName);

                return oDICompany;
            }
            catch (Exception Ex)
            {
                sErrDesc = Ex.Message.ToString();
                oLog.WriteToErrorLogFile(sErrDesc, sFuncName);
                oLog.WriteToDebugLogFile("Completed With ERROR  ", sFuncName);
                throw Ex;
            }

        }

        public SAPbobsCOM.Company ConnectToTargetCompany(SAPbobsCOM.Company oCompany, string sUserName, string sPassword, string sDBName,
                                                        string sServer, string sLicServerName, string sDBUserName
                                                       , string sDBPassword, string sErrDesc)
        {
            string sFuncName = string.Empty;
            long lRetCode;

            try
            {
                sFuncName = "ConnectToTargetCompany()";

                oLog.WriteToDebugLogFile("Starting Function ", sFuncName);

                if (oCompany != null)
                {
                    oLog.WriteToDebugLogFile("Disconnecting the Company object - Company Name " + oCompany.CompanyName, sFuncName);
                    oCompany.Disconnect();
                }
                oLog.WriteToDebugLogFile("Before initializing ", sFuncName);

                oCompany = new SAPbobsCOM.Company();
                oLog.WriteToDebugLogFile("After Initializing Company Connection ", sFuncName);
                oCompany.Server = sServer;
                oCompany.LicenseServer = sLicServerName;
                oCompany.DbUserName = sDBUserName;
                oCompany.DbPassword = sDBPassword;
                oCompany.language = SAPbobsCOM.BoSuppLangs.ln_English;
                oCompany.UseTrusted = false;
                oCompany.DbServerType = SAPbobsCOM.BoDataServerTypes.dst_MSSQL2008;


                oCompany.CompanyDB = sDBName;// sDataBaseName;
                oCompany.UserName = sUserName;
                oCompany.Password = sPassword;

                oLog.WriteToDebugLogFile("Connecting the Database...", sFuncName);

                lRetCode = oCompany.Connect();

                if (lRetCode != 0)
                {
                    throw new ArgumentException(oCompany.GetLastErrorDescription());
                }
                else
                {
                    oLog.WriteToDebugLogFile("Company Connection Established", sFuncName);
                    oLog.WriteToDebugLogFile("Completed With SUCCESS", sFuncName);
                    return oCompany;
                }
            }
            catch (Exception Ex)
            {

                sErrDesc = Ex.Message.ToString();
                oLog.WriteToErrorLogFile(sErrDesc, sFuncName);
                oLog.WriteToDebugLogFile("Completed With ERROR  ", sFuncName);
                throw Ex;
            }
        }
        #endregion

        #region Add Activity
        private string AddActivityUsingActivityService(DataView oDv, string sCompany)
        {
            string functionReturnValue = string.Empty;

            string sFuncName = "AddActivityUsingActivityService";
            string sSQL = string.Empty;
            int sServCallId;
            SAPbobsCOM.Recordset oRecordSet = null;

            try
            {

                oLog.WriteToDebugLogFile("Starting Function ", sFuncName);

                oLog.WriteToDebugLogFile("Connecting Company", sFuncName);
                oDICompany = ConnectToTargetCompany(sCompany);
                oLog.WriteToDebugLogFile("Company Connected successfully", sFuncName);

                SAPbobsCOM.ServiceCalls oService = default(SAPbobsCOM.ServiceCalls);
                oService = oDICompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oServiceCalls);
                oRecordSet = oDICompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);

                for (int i = 0; i <= oDv.Count - 1; i++)
                {
                    sServCallId = Convert.ToInt32(oDv[i]["ServiceCallId"].ToString());

                    if (oService.GetByKey(sServCallId))
                    {

                        oLog.WriteToDebugLogFile("Processing Service call " + sServCallId, sFuncName);

                        //****************ACTIVITY PHONE CALL
                        if (!(oDv[i]["ServiceCallId"].ToString().Trim() == string.Empty))
                        {
                            //1) Update the existing activity UDF 

                            using (SqlConnection connection = new SqlConnection(ConnectionString))
                            using (SqlCommand command = connection.CreateCommand())
                            {
                                oLog.WriteToDebugLogFile("Before Updating UDF", sFuncName);
                                string sTime = System.DateTime.Now.TimeOfDay.ToString().Substring(0, 5).Replace(":", "");
                                command.CommandText = "UPDATE SCL5 SET u_ob_enddate = '" + DateTime.Now.Date + "',u_ob_resp_site_time = '" + sTime + "',u_ob_result = 'Yes'," +
                                                      "u_ob_success = 'Yes' from SCL5 where ClgID = " + oDv[i]["ClgCode"] + "";
                                connection.Open();
                                command.ExecuteNonQuery();
                                connection.Close();
                                command.Dispose();
                                oLog.WriteToDebugLogFile("After Updating UDF", sFuncName);
                            }

                            //2) Adding the Activity
                            oLog.WriteToDebugLogFile("Adding Activity for Phone Call", sFuncName);

                            string sActivityId = string.Empty;
                            int iLine = 0;
                            string sActivityTime = System.DateTime.Now.TimeOfDay.ToString().Substring(0, 5).Replace(":", "");
                            SAPbobsCOM.Contacts oActivity = default(SAPbobsCOM.Contacts);
                            oActivity = oDICompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oContacts);

                            oActivity.Activity = SAPbobsCOM.BoActivities.cn_Conversation;
                            sSQL = "SELECT Code FROM OCLT WHERE UPPER(Name) = 'General'";
                            oRecordSet.DoQuery(sSQL);
                            if (oRecordSet.RecordCount > 0)
                            {
                                oActivity.ActivityType = oRecordSet.Fields.Item("Code").Value;
                            }
                            oActivity.CardCode = oService.CustomerCode;
                            oActivity.StartDate = DateTime.Now.Date;
                            oActivity.EndDuedate = DateTime.Now.Date;
                            oActivity.StartTime = DateTime.Now;
                            oActivity.EndTime = DateTime.Now.AddMinutes(5);
                            oActivity.HandledBy = Convert.ToInt32(oDv[i]["AssignedEngineerId"]); // Attend User

                            //oActivity.UserFields.Fields.Item("u_ob_resp_sms_date").Value = DateTime.Now.Date;
                            //oActivity.UserFields.Fields.Item("u_ob_resp_sms_time").Value = sActivityTime;
                            //oActivity.UserFields.Fields.Item("U_ob_activitytype").Value = oDv[i]["TypeOfSLA"];

                            oActivity.Add();

                            sActivityId = oDICompany.GetNewObjectKey();

                            sSQL = "SELECT COUNT(ClgCode) [LineCount] FROM OCLG WHERE parentType = '191' AND parentId = '" + sServCallId + "'";
                            oRecordSet.DoQuery(sSQL);
                            if (oRecordSet.RecordCount > 0)
                            {
                                iLine = oRecordSet.Fields.Item("LineCount").Value;
                            }

                            oService.Activities.Add();
                            oService.Activities.SetCurrentLine(iLine);
                            oService.Activities.ActivityCode = Convert.ToInt32(sActivityId);
                            //oService.Activities.UserFields.Fields.Item("U_OB_RESP_SMS_DATE").Value = DateTime.Now.Date;
                            //oService.Activities.UserFields.Fields.Item("U_OB_RESP_SMS_TIME").Value = sActivityTime;
                            //oLog.WriteToDebugLogFile("Adding Activity for Phone Call", sFuncName);
                            //oService.Activities.UserFields.Fields.Item("U_OB_ACTIVITYTYPE").Value = oDv[i]["TypeOfSLA"];

                            if (oService.Update() != 0)
                            {
                                sErrDesc = "ERROR WHILE UPDATING THE SERVICE CALL(PHONE CALL ACTIVITY) ID " + sServCallId + " / " + oDICompany.GetLastErrorDescription();
                                oLog.WriteToErrorLogFile(sErrDesc, sFuncName);

                                oLog.WriteToDebugLogFile(sErrDesc, sFuncName);
                                //Throw New ArgumentException(sErrDesc)
                                continue;
                            }

                            oLog.WriteToDebugLogFile("Activity - Job Added successfully to Service call", sFuncName);

                            //3) Update the activity UDF 

                            using (SqlConnection connection = new SqlConnection(ConnectionString))
                            using (SqlCommand command = connection.CreateCommand())
                            {
                                oLog.WriteToDebugLogFile("Before Updating activity UDF", sFuncName);
                                string sTime = System.DateTime.Now.TimeOfDay.ToString().Substring(0, 5).Replace(":", "");
                                string sQuery = "UPDATE SCL5 SET U_OB_RESP_SMS_DATE = '" + DateTime.Now.Date + "',U_OB_RESP_SMS_TIME = '" + sTime + "'," +
                                                      "U_OB_ACTIVITYTYPE = '" + oDv[i]["TypeOfSLA"] + "' from SCL5 where ClgID = " + Convert.ToInt32(sActivityId) + "";
                                oLog.WriteToDebugLogFile("Query : " + sQuery, sFuncName);
                                command.CommandText = sQuery;
                                connection.Open();
                                command.ExecuteNonQuery();
                                connection.Close();
                                command.Dispose();
                                oLog.WriteToDebugLogFile("After Updating activity UDF", sFuncName);
                            }
                        }
                    }
                }

                oLog.WriteToDebugLogFile("Completed with SUCCESS", sFuncName);
                functionReturnValue = RTN_SUCCESS;
            }
            catch (Exception ex)
            {
                sErrDesc = ex.Message;
                oLog.WriteToErrorLogFile(sErrDesc, sFuncName);
                oLog.WriteToDebugLogFile("Completed with ERROR", sFuncName);
                functionReturnValue = sErrDesc;
            }
            return functionReturnValue;
        }
        #endregion

    }
}