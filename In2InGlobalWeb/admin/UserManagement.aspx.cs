using Newtonsoft.Json;
using System;
using System.Data;
using System.IO;
using System.Net;
using System.Text;
using System.Web.UI.WebControls;

namespace In2InGlobal.presentation.admin
{
    public partial class UserManagement : System.Web.UI.Page
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
                    BindUsers();
                    BindCompany();
                    BindRoles();                  
                }
            }

        }
      
        private void BindUsers()
        {

            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
            string json = (new WebClient()).DownloadString("http://localhost:26677/admin/json-data/users.json");
            grdUsers.DataSource = JsonConvert.DeserializeObject<DataTable>(json);
            grdUsers.DataBind();
        }
        private void BindCompany()
        {

           
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
            string json = (new WebClient()).DownloadString("http://localhost:26677/admin/json-data/Companies.json");
            ddlCompanyName.DataSource = JsonConvert.DeserializeObject<DataTable>(json);            
            ddlCompanyName.DataBind();
            ddlCompanyName.Items.Insert(0, "--Select Company--");
//            ddlCompanyName.AppendDataBoundItems = true;
            ddlCompanyName.SelectedIndex = 0;

        }
        private void BindRoles()
        {

            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
            string json = (new WebClient()).DownloadString("http://localhost:26677/admin/json-data/Roles.json");
            ddlRoleName.DataSource = JsonConvert.DeserializeObject<DataTable>(json);
            ddlRoleName.DataValueField = "RoleName";
            ddlRoleName.DataBind();
            ddlRoleName.Items.Insert(0, "--Select a Role--");
            //            ddlCompanyName.AppendDataBoundItems = true;
            ddlRoleName.SelectedIndex = 0;
        }
      
        protected void grdUsers_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            BindUsers();
            grdUsers.PageIndex = e.NewPageIndex;
            grdUsers.DataBind();
        }

        protected void grdUsers_RowEditing(object sender, GridViewEditEventArgs e)
        {
            grdUsers.EditIndex = e.NewEditIndex;
            BindUsers();
        }

        protected void grdUsers_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string email = grdUsers.DataKeys[e.RowIndex].Value.ToString();
            DeleteUser(email);
            BindUsers();
        }

        protected void grdUsers_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            string useremailid = grdUsers.DataKeys[e.RowIndex].Value.ToString();
            GridViewRow row = (GridViewRow)grdUsers.Rows[e.RowIndex];           
           
            TextBox textFirstName = (TextBox)row.Cells[0].Controls[0];
            TextBox textLastName = (TextBox)row.Cells[1].Controls[0];
            TextBox textCompany = (TextBox)row.Cells[2].Controls[0];
            UpdateUser(textFirstName.Text,textLastName.Text,textCompany.Text,useremailid);
            grdUsers.EditIndex = -1;
           
           BindUsers();
        }

        private void UpdateUser(string Fname, string Lname, string company,string email)
        {
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
            string json = (new WebClient()).DownloadString("http://localhost:26677/admin/json-data/Users.json");
            DataTable usrTable = JsonConvert.DeserializeObject<DataTable>(json);
            DataRow dr =  usrTable.Select("Email ='" + email + "'")[0];
            dr[0] = Fname;
            dr[1] = Lname;
            dr[2] = company;
            usrTable.AcceptChanges();
            dr.SetModified();
            string output = Newtonsoft.Json.JsonConvert.SerializeObject(usrTable, Newtonsoft.Json.Formatting.Indented);
            File.WriteAllText(Server.MapPath("json-data/Users.json"), output);

        }
        private void DeleteUser(string email)
        {
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
            string json = (new WebClient()).DownloadString("http://localhost:26677/admin/json-data/Users.json");
            DataTable usrTable = JsonConvert.DeserializeObject<DataTable>(json);
            if (usrTable.Select("Email ='" + email + "'").Length > 0)
            {
                usrTable.Select("Email ='" + email + "'")[0].Delete();
                usrTable.AcceptChanges();
                string output = Newtonsoft.Json.JsonConvert.SerializeObject(usrTable, Newtonsoft.Json.Formatting.Indented);
                File.WriteAllText(Server.MapPath("json-data/Users.json"), output);
            }
        }

        protected void grdUsers_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            grdUsers.EditIndex = -1;
            BindUsers();
        }

        protected void AddNewUser(object sender, EventArgs e)
        {
            
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
            string json = (new WebClient()).DownloadString("http://localhost:26677/admin/json-data/Users.json");
            DataTable usrTable = JsonConvert.DeserializeObject<DataTable>(json);
            DataRow dr = usrTable.Rows.Add(txtFName.Value,txtLName.Value,ddlCompanyName.SelectedValue.ToString(),txtEmail.Value, ddlRoleName.SelectedValue.ToString(),txtPassword.Value) ;
            usrTable.AcceptChanges();
            dr.SetModified();
            string output = Newtonsoft.Json.JsonConvert.SerializeObject(usrTable, Newtonsoft.Json.Formatting.Indented);
            File.WriteAllText(Server.MapPath("json-data/Users.json"), output);
            BindUsers();
        }
    }
}