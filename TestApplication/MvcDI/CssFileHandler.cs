using System.Web;

namespace MvcDI
{
    /// <summary>
    /// CSSの論理パスと物理パスのマッピング
    /// </summary>
    public class CssFilehandler : IHttpHandler
    {
        /// <summary>
        /// 論理パス
        /// </summary>
        string virtualPath;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="path">仮想パス</param>
        public CssFilehandler(string path)
        {
            virtualPath = path;
        }

        #region IHttpHandler メンバ
        /// <summary>
        /// 別の要求で IHttpHandler インスタンスを使用できるかどうかを示す値を取得します。
        /// </summary>
        bool IHttpHandler.IsReusable
        {
            get { return true; }
        }

        /// <summary>
        /// IHttpHandler インターフェイスを実装するカスタム HttpHandler によって、HTTP Web 要求の処理を有効にします。
        /// </summary>
        /// <param name="context">HTTP 要求を処理するために使用する、組み込みのサーバー オブジェクト (Request、Response、Session、Server など) への参照を提供する HttpContext オブジェクト。</param>
        void IHttpHandler.ProcessRequest(HttpContext context)
        {
            var path = context.Server.MapPath(virtualPath);

            context.Response.ContentType = "text/css";
            context.Response.TransmitFile(path);
        }

        #endregion
    }
}
