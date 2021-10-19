using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
namespace InGlobal.presentation
{
    public static class Common
    {
        public static bool HasSession()
        {
            if (HttpContext.Current.Session.Count != 0 && (null != HttpContext.Current.Session["UserID"]))
            {
                return true;
            }
            else
                return false;
        }

    }

}