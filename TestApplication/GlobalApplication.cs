using System;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using log4net;

namespace MvcDI
{
    /// <summary>
    /// 共通アプリケーション設定クラス
    /// </summary>
    public class GlobalApplication : HttpApplication
    {
        /// <summary>
        /// ログ出力クラス
        /// </summary>
        protected static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// 共通ルート設定
        /// </summary>
        /// <param name="routes"></param>
        public static void CommonMapRoute(RouteCollection routes)
        {
            routes.MapRoute("Scripts",
                "Scripts/{*scriptName}",
                null,
                new { scriptName = @"[\w]*\/[.|\w]*\.js" }
                ).RouteHandler = new ScriptRoutHandler();

            routes.MapRoute(
                "Css",
                "Content/Css/{*cssName}",
                null,
                new { cssName = @"[\w]*\/[.|\w]*\.css" }
                ).RouteHandler = new CssRouteHandler();
            routes.MapRoute(
                "Image",
                "Content/images/{*imageName}",
                null,
                new { imageName = @"[\w]*\/[.|\w]*\.(jpg|jpeg|gif|png)" }
                ).RouteHandler = new ImageRouteHandler();

            routes.MapRoute(
                "ErrorRoute",
                "{*anything}",
                new { controller = "Error", action = "Index" });
        }

        /// <summary>
        /// コントローラファクトリの登録
        /// </summary>
        public static void RegistControllerFactory()
        {
            var factory = new MvcDIControllerFactory();
            ControllerBuilder.Current.SetControllerFactory(factory);
        }

        protected virtual void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RegistControllerFactory();
        }

        /// <summary>
        /// アプリケーションエラー発生時処理済みでない例外をキャッチする
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Application_Error(object sender, EventArgs e)
        {
            var exception = Server.GetLastError();

            if (exception == null)
            {
                return;
            }
            else
            {
                log.Fatal("システムエラー：", exception);
            }
        }
    }
}
