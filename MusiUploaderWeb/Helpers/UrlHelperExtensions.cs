using System.Web;
using System.Web.Mvc;

namespace MusiUploaderWeb.Helpers
{
    public static class UrlHelperExtensions
    {
        public static string GetImage(this UrlHelper helper, string imageFileName, bool localizable = true)
        {
            string strUrlPath = string.Empty;
            string strFilePath = string.Empty;

            if (localizable)
            {
                strUrlPath = string.Format("/Content/Images/{0}/{1}", GlobalHelper.CurrentCulture, imageFileName);
                strFilePath = HttpContext.Current.Server.MapPath(strUrlPath);

                if (!System.IO.File.Exists(strFilePath))
                {
                    //default culture
                    strUrlPath = string.Format("/Content/Images/{0}/{1}", GlobalHelper.DefaultCulture, imageFileName);
                }

                return strUrlPath;
            }

            strUrlPath = string.Format("/Content/Images/{0}", imageFileName);
            strFilePath = HttpContext.Current.Server.MapPath(strUrlPath);
            return strUrlPath;
        }
    }
}