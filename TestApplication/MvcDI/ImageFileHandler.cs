using System.Web;

namespace MvcDI
{
    /// <summary>
    /// 画像ファイルの論理パスと物理パスのマッピング
    /// </summary>
    public class ImageFileHandler : IHttpHandler
    {
        /// <summary>
        /// 論理パス
        /// </summary>
        string virtualPath;

        #region constructor
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="path">論理パス</param>
        public ImageFileHandler(string path)
        {
            virtualPath = path;
        }
        #endregion

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

            // 画像ファイルの種類に応じてContentTypeを変更する
            if (path.IndexOf(".jpg") >= 0 || path.IndexOf(".jpeg") >= 0)
            {
                context.Response.ContentType = "image/jpeg";
            }
            else if (path.IndexOf(".gif") >= 0)
            {
                context.Response.ContentType = "image/gif";
            }
            else if (path.IndexOf(".png") >= 0)
            {
                context.Response.ContentType = "image/png";
            }

            context.Response.TransmitFile(path);
        }

        #endregion
    }
}
