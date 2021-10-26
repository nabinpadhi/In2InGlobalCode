using System.Web;
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