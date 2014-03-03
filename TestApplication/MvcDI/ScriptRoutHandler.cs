using System.Web.Routing;

namespace MvcDI
{
    /// <summary>
    /// JavaScriptのルートマッピングハンドラー
    /// </summary>
    public class ScriptRoutHandler : IRouteHandler
    {
        #region IRouteHandler メンバ
        /// <summary>
        /// 要求を処理するオブジェクトを提供します。
        /// </summary>
        /// <param name="requestContext">要求に関する情報をカプセル化しているオブジェクト。</param>
        /// <returns>要求を処理するオブジェクト。</returns>
        System.Web.IHttpHandler IRouteHandler.GetHttpHandler(RequestContext requestContext)
        {
            string scriptName = requestContext.RouteData.GetRequiredString("scriptName");
            string scriptPath = string.Format("~/Scripts/{0}", scriptName);
            return new ScriptFileHandler(scriptPath);
        }

        #endregion
    }
}
