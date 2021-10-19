using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Collections;
using System.Data;
using System.Linq;

namespace InGlobal.presentation
{
    public class XSSValidator
    {
        private string fs_ErrorMessage = "";
        string[] ma_BlackList = new string[] { ".html", ".htm", ".asp", ".aspx", ".php", "&amp;", "&amp", "&lt", "&gt", "&lt;", "&gt;", "'';!", "&#x", "&#0", "<", ">", "javascript", "<script", "src=", "http:", ".js", ".vb", "alert(", "alert&#40;", "alert&#40", "&#62;", "&#63;", "&#62", "&#63", "vbscript", "mocha:", "¼", "¾", "base64", "xss", "fromCharCode", "@import", "#exec", "echo", "\x3C", "\u003c", "\0" };

        public string ErrorMessage
        {
            get { return fs_ErrorMessage; }
        }
        private ArrayList fa_ExculdeException;

        public ArrayList pa_ExculdeException
        {
            get { return fa_ExculdeException; }
            set { fa_ExculdeException = value; }
        }

        private ArrayList fa_ErrorMessage = new ArrayList();

        public ArrayList pa_ErrorMessage
        {
            get { return fa_ErrorMessage; }

        }

        public XSSValidator(string rs_ErrorMessage)
        {
            if (rs_ErrorMessage != "")
            {
                fs_ErrorMessage = rs_ErrorMessage;
            }
            else
            {
                fs_ErrorMessage = "Submitted form contains invalid data, please validate the same.@Error@Invalid Data";
            }
        }
        public bool StartValidate(object ro_InputObject)
        {

            bool mb_Result = true;
            PropertyInfo[] propertiesInfo;
            Assembly mo_cbl = Assembly.GetExecutingAssembly();
            Type targetType = ro_InputObject.GetType();

            propertiesInfo = ro_InputObject.GetType().GetProperties();
            foreach (PropertyInfo propertyInfo in propertiesInfo)
            {
                if (propertyInfo != null)
                {
                    object innerObject = propertyInfo.GetValue(ro_InputObject, null);
                    if (innerObject != null)
                    {
                        if (innerObject.GetType().BaseType.Name != "DataSet")
                        {
                            string ms_TargetValue = Convert.ToString(innerObject).ToLower();
                            Regex r = new Regex(@"\s*");
                            ms_TargetValue = r.Replace(ms_TargetValue, string.Empty);
                            foreach (string ms_blackListStr in ma_BlackList)
                            {
                                if (fa_ExculdeException == null)
                                {
                                    if (ms_TargetValue.IndexOf(ms_blackListStr) >= 0)
                                    {
                                        mb_Result = false;
                                        break;
                                    }
                                }
                                else if (!this.fa_ExculdeException.Contains(ms_blackListStr))
                                {
                                    if (ms_TargetValue.IndexOf(ms_blackListStr) >= 0)
                                    {
                                        mb_Result = false;
                                        break;
                                    }
                                }
                            }
                        }
                        else
                        {
                            DataSet mo_testdataset = (DataSet)innerObject;
                            mb_Result = ValidateDataSet(mo_testdataset);
                        }
                    }

                }
            }
            return mb_Result;
        }
        public bool ValidateDataTable(DataTable ro_TargetDataTable)
        {
            bool mb_Result = true;
            foreach (DataRow dr in ro_TargetDataTable.Rows)
            {
                foreach (DataColumn dc in ro_TargetDataTable.Columns)
                {
                    if (dc.ColumnName != "ModifiedColumns")
                    {
                        string ms_targetValue = Convert.ToString(dr[dc]).ToLower();
                        Regex r = new Regex(@"\s*");
                        ms_targetValue = r.Replace(ms_targetValue, string.Empty);
                        foreach (string ms_blackListStr in ma_BlackList)
                        {
                            if (fa_ExculdeException == null)
                            {
                                if (ms_targetValue.IndexOf(ms_blackListStr) >= 0)
                                {
                                    mb_Result = false;
                                    break;
                                }
                            }
                            else if (!this.fa_ExculdeException.Contains(ms_blackListStr))
                            {
                                if (ms_targetValue.IndexOf(ms_blackListStr) >= 0)
                                {
                                    mb_Result = false;
                                    break;
                                }
                            }
                            if (mb_Result == false)
                                break;
                        }
                        if (mb_Result == false)
                            break;
                    }
                    if (mb_Result == false)
                        break;
                }
            }
            return mb_Result;
        }
        public bool ValidateDataSet(DataSet ro_TargetDataSet)
        {
            bool mb_Result = true;
            foreach (DataTable dt in ro_TargetDataSet.Tables)
            {
                mb_Result = ValidateDataTable(dt);
                if (!mb_Result)
                    break;
            }
            return mb_Result;
        }
        public bool StartValidateString(string ro_InputString)
        {
            bool IsValid = true;
            foreach (string ms_blackListStr in ma_BlackList)
            {
                if (ro_InputString.IndexOf(ms_blackListStr) >= 0)
                {
                    IsValid = false;
                    break;
                }

            }
            return IsValid;
        }

        public bool StartValidateRow(DataRow dr)
        {
            bool mb_Result = true;

            foreach (DataColumn dc in dr.Table.Columns)
            {

                string ms_targetValue = Convert.ToString(dr[dc]).ToLower();
                Regex r = new Regex(@"\s*");
                ms_targetValue = r.Replace(ms_targetValue, string.Empty);
                foreach (string ms_blackListStr in ma_BlackList)
                {
                    if (fa_ExculdeException == null)
                    {
                        if (ms_targetValue.IndexOf(ms_blackListStr) >= 0)
                        {
                            mb_Result = false;
                            break;
                        }
                    }
                    else if (!this.fa_ExculdeException.Contains(ms_blackListStr))
                    {
                        if (ms_targetValue.IndexOf(ms_blackListStr) >= 0)
                        {
                            mb_Result = false;
                            break;
                        }
                    }
                    if (mb_Result == false)
                        break;
                }
                if (mb_Result == false)
                    break;

            }

            return mb_Result;
        }
        
        public static string SafeSqlLiteral(string theValue, int theLevel)
        {
            // intLevel represent how thorough the value will be checked for dangerous code
            // intLevel (1) - Do just the basic. This level will already counter most of the SQL injection attacks
            // intLevel (2) -   (non breaking space) will be added to most words used in SQL queries to prevent unauthorized access to the database. Safe to be printed back into HTML code. Don't use for usernames or passwords

            string strValue = theValue;
            int intLevel = theLevel;

            if (strValue != null)
            {
                if (intLevel > 0)
                {
                    // Most important one! This line alone can prevent most injection attacks
                    strValue = strValue.Replace("--", "");
                    strValue = strValue.Replace("[", "[[]");
                    strValue = strValue.Replace("%", "[%]");
                }
                if (intLevel > 1)
                {
                    string[] myArray = new string[] { "xp_ ", "update ", "insert ", "select ", "drop ", "alter ", "create ", "rename ", "delete ", "replace " };
                    int i = 0;
                    int i2 = 0;
                    int intLenghtLeft = 0;
                    for (i = 0; i < myArray.Length; i++)
                    {
                        string strWord = myArray[i];
                        Regex rx = new Regex(strWord, RegexOptions.Compiled | RegexOptions.IgnoreCase);
                        MatchCollection matches = rx.Matches(strValue);
                        i2 = 0;
                        foreach (Match match in matches)
                        {
                            GroupCollection groups = match.Groups;
                            intLenghtLeft = groups[0].Index + myArray[i].Length + i2;
                            strValue = strValue.Substring(0, intLenghtLeft - 1) + "&nbsp;" + strValue.Substring(strValue.Length - (strValue.Length - intLenghtLeft), strValue.Length - intLenghtLeft);
                            i2 += 5;
                        }
                    }
                }
                return strValue;
            }
            else
            {
                return strValue;
            }
        }
    }
}
