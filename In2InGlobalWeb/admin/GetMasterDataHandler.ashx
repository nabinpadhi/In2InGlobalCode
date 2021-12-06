<%@ WebHandler Language="C#" Class="GetMasterDataHandler" %>

using System;
using System.Web;
using In2InGlobal.businesslogic;
using In2InGlobal.presentation.Tools;
using In2InGlobalBL;
using In2InGlobalBusinessEL;
using System.Collections.Generic;
using System.Data;    
using Newtonsoft.Json;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Web.UI;
using System.Web.UI.WebControls;

public class GetMasterDataHandler : IHttpHandler
{

    public void ProcessRequest(HttpContext context)
    {

        context.Response.ContentType = "text/plain";
        context.Response.Write("nothing");
        context.Response.End();

    }
   
    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}  