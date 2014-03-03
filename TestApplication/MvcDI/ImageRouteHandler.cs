using System.Web.Routing;

namespace MvcDI
{
    /// <summary>
    /// 画像ファイルのルートマッピングハンドラー
    /// </summary>
    public class ImageRouteHandler : IRouteHandler
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
            string scriptPath = string.Format("~/Content/Image/{0}", scriptName);
            return new ImageFileHandler(scriptPath);
        }

        #endregion
    }
}
