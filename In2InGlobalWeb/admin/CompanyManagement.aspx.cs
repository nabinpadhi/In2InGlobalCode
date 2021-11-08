using Newtonsoft.Json;
using System;
using System.Data;
using System.IO;
using System.Net;
using System.Web;
using System.Web.Services;
using System.Web.UI.WebControls;

namespace In2InGlobal.presentation.admin
{
    public partial class CompanyManagement : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string usrRole = Session["UserRole"].ToString();
                if (usrRole != "Admin")
                {
                    Response.Redirect("Login.aspx");

                }
                else
                {
                    BindCompany();
                }
            }
            
        }
        private void BindCompany()
        {

            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
            string json = (new WebClient()).DownloadString("http://localhost:26677/admin/json-data/Companies.json");
            grdCompany.DataSource = JsonConvert.DeserializeObject<DataTable>(json);
            grdCompany.DataBind();
        }

        protected void grdCompany_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            GridViewRowEventArgs ea = e as GridViewRowEventArgs;
            if (ea.Row.RowType == DataControlRowType.DataRow)
            {
                
                DataRowView drv = ea.Row.DataItem as DataRowView;
                Object ob = drv["PhoneNo"];
                if (!Convert.IsDBNull(ob))
                {                    
                    TableCell cell2 = ea.Row.Cells[3];
                    if (cell2.Text.Length > 1)
                    {
                        cell2.Text = "<img src='assets/img/mobile.png' style='width:25px;height:20px;'></span>" + " " + cell2.Text; 
                    }
                }
            }

        }

        protected void grdCompany_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
                BindCompany();
                grdCompany.PageIndex = e.NewPageIndex;
                grdCompany.DataBind();
            

        }
        [WebMethod(EnableSession = true)]
        public static string AddNewCompany(string companyname,string email,string phoneno)
        {
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
            string json = (new WebClient()).DownloadString("http://localhost:26677/admin/json-data/Companies.json");
            DataTable usrTable = JsonConvert.DeserializeObject<DataTable>(json);

            int _newNumber = usrTable.Rows.Count + 1;
            string _companyID = "COM-";
            if (_newNumber > 9 && _newNumber < 99)
            {
                _companyID = _companyID + "0" + _newNumber.ToString();
            }
            else
            {
                _companyID = _companyID + _newNumber.ToString();
            }

            DataRow dr = usrTable.Rows.Add(_companyID, companyname, email, phoneno);
            usrTable.AcceptChanges();
            dr.SetModified();
            string output = Newtonsoft.Json.JsonConvert.SerializeObject(usrTable, Newtonsoft.Json.Formatting.Indented);
            File.WriteAllText(HttpContext.Current.Server.MapPath("json-data/Companies.json"), output);
            return "Success";
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
            string json = (new WebClient()).DownloadString("http://localhost:26677/admin/json-data/Companies.json");
            DataTable usrTable = JsonConvert.DeserializeObject<DataTable>(json);

            int _newNumber = usrTable.Rows.Count + 1;
            string _companyID = "COM-";
            if (_newNumber > 9 && _newNumber < 99)
            {
                _companyID = _companyID + "0" + _newNumber.ToString();
            }
            else
            {
                _companyID = _companyID + _newNumber.ToString();
            }

            DataRow dr = usrTable.Rows.Add(_companyID, txtCompanyName.Value, txtEmail.Value, txtPhoneNo.Value);
            usrTable.AcceptChanges();
            dr.SetModified();
            string output = Newtonsoft.Json.JsonConvert.SerializeObject(usrTable, Newtonsoft.Json.Formatting.Indented);
            File.WriteAllText(Server.MapPath("json-data/Companies.json"), output);
            
        }
    }
}