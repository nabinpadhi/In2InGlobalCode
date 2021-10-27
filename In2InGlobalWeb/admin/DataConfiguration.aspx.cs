﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace In2InGlobal.presentation.admin
{
    public partial class DataConfiguration : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string target = Request.QueryString.Get("t");
                if (target != "a")
                {
                    Response.Redirect("Login.aspx");

                }
                else
                {

                    BindCompany();
                    // BindRoles();
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
                Object ob = drv["Address"];
                if (!Convert.IsDBNull(ob))
                {
                    TableCell cell = ea.Row.Cells[1];
                    TableCell cell2 = ea.Row.Cells[2];
                    if (cell2.Text.Length > 49)
                    {
                        cell2.Text = cell2.Text.Substring(0, 50) + "...."; // format cell as percentage
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
    }
}