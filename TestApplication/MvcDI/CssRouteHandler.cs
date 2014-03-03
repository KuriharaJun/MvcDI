using System.Web;
using System.Web.Routing;

namespace MvcDI
{
    /// <summary>
    /// CSSのルートマッピングハンドラー
    /// </summary>
    public class CssRouteHandler : IRouteHandler
    {
        #region IRouteHandler メンバ
        /// <summary>
        /// 要求を処理するオブジェクトを提供します。
        /// </summary>
        /// <param name="requestContext">要求に関する情報をカプセル化しているオブジェクト。</param>
        /// <returns>要求を処理するオブジェクト。</returns>
        IHttpHandler IRouteHandler.GetHttpHandler(RequestContext requestContext)
        {
            string cssName = requestContext.RouteData.GetRequiredString("cssName");
            string cssPath = string.Format("~/Content/Css/{0}", cssName);
            return new CssFilehandler(cssPath);
        }
        #endregion
    }
}
