using System;
using System.Web.Mvc;

namespace MvcDI
{
    /// <summary>
    /// DIコントローラファクトリ
    /// </summary>
    public class MvcDIControllerFactory : DefaultControllerFactory
    {
        /// <summary>
        /// コントローラのインスタンス生成を制御
        /// </summary>
        /// <param name="requestContext"></param>
        /// <param name="controllerName"></param>
        /// <returns></returns>
        public override IController CreateController(System.Web.Routing.RequestContext requestContext, string controllerName)
        {
            var controllerType = base.GetControllerType(requestContext, controllerName);
            var controllerInstance = base.GetControllerInstance(requestContext, controllerType);

            // Implement属性を参照してServiceのインジェクション
            InjectService(controllerInstance);

            return controllerInstance;
        }

        /// <summary>
        /// サービスの依存性を注入
        /// </summary>
        /// <param name="controller"></param>
        private void InjectService(IController controller)
        {
            var controllerType = controller.GetType();
            var fields = controllerType.GetFields(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            foreach (var field in fields)
            {
                // 依存性注入対象の属性取得
                var implAttr = field.GetCustomAttributes(typeof(ImplementAttribute), true);
                if (implAttr != null)
                {
                    var attr = implAttr[0] as ImplementAttribute;
                    if (attr.Debug == false || attr.DebugImplementType == null)
                    {
                        // 本番環境用インスタンスの生成
                        var impleInstance = Activator.CreateInstance(attr.ImplementType);
                        field.SetValue(controller, impleInstance);
                    }
                    else
                    {
                        // デバッグ環境用インスタンスの生成
                        var debugInstance = Activator.CreateInstance(attr.DebugImplementType);
                        field.SetValue(controller, debugInstance);
                    }
                }
            }
        }
    }
}
