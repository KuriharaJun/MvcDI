using System;
using System.Reflection;
using System.Web.Mvc;
using log4net;

namespace MvcDI
{
    /// <summary>
    /// DIコントローラファクトリ
    /// </summary>
    public class MvcDIControllerFactory : DefaultControllerFactory
    {
        /// <summary>
        /// ログ
        /// </summary>
        private static ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// コントローラのインスタンス生成を制御
        /// </summary>
        /// <param name="requestContext"></param>
        /// <param name="controllerName"></param>
        /// <returns></returns>
        public override IController CreateController(System.Web.Routing.RequestContext requestContext, string controllerName)
        {
            if (log.IsInfoEnabled)
            {
                log.Info("Create controller instance name is " + controllerName);
                if (log.IsDebugEnabled)
                {
                    log.Debug("RequestContext: " + requestContext.ToString());
                }
            }

            var controllerType = base.GetControllerType(requestContext, controllerName);
            if (log.IsDebugEnabled)
            {
                if (controllerType != null)
                {
                    log.Debug("ControllerType: " + controllerType.ToString());
                }
                else
                {
                    log.Debug("ControllerType is null.");
                }
            }
            var controllerInstance = base.GetControllerInstance(requestContext, controllerType);

            // Implement属性を参照してServiceのインジェクション
            this.InjectService(controllerInstance);

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
                        if (log.IsInfoEnabled)
                        {
                            log.Info("Staging instance is created.");
                            if (log.IsDebugEnabled)
                            {
                                log.Debug("Create instance type is " + impleInstance.GetType().Name);
                            }
                        }
                    }
                    else
                    {
                        // デバッグ環境用インスタンスの生成
                        var debugInstance = Activator.CreateInstance(attr.DebugImplementType);
                        field.SetValue(controller, debugInstance);
                        if (log.IsInfoEnabled)
                        {
                            log.Info("Debug instance is created.");
                            if (log.IsDebugEnabled)
                            {
                                log.Debug("Create instance type is " + debugInstance.GetType().Name);
                            }
                        }
                    }
                }
            }
        }
    }
}
