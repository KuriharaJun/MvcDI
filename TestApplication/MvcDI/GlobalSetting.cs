using System.Web.Mvc;
using System.Web.Routing;

namespace MvcDI
{
    /// <summary>
    /// 共通アプリケーション設定クラス
    /// </summary>
    public class GlobalSetting
    {
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
                "Content/Image/{*imageName}",
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
    }
}
