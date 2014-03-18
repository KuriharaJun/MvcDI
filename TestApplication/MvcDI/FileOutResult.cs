using System.IO;
using System.Web.Mvc;
using log4net;
using System.Reflection;

namespace MvcDI
{
    /// <summary>
    /// ファイル出力ActionResult
    /// </summary>
    public class FileOutResult : FileStreamResult
    {
        /// <summary>
        /// ログ
        /// </summary>
        private static ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        #region constructor
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="fileStream"></param>
        /// <param name="contentType"></param>
        public FileOutResult(Stream fileStream, string contentType)
            : base(fileStream, contentType)
        { }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="fileStream"></param>
        /// <param name="contentType"></param>
        /// <param name="fileName"></param>
        public FileOutResult(Stream fileStream, string contentType, string fileName)
            : base(fileStream, contentType)
        {
            base.FileDownloadName = fileName;
        }
        #endregion

        public override void ExecuteResult(ControllerContext context)
        {
            var fileName = base.FileDownloadName;
            var response = context.HttpContext.Response;
            response.ContentType = base.ContentType;
            response.AddHeader("content-disposition", "attachment; filename=" + fileName);
            this.WriteFile(response);
            if (log.IsInfoEnabled)
            {
                log.Info("Output file name is " + fileName);
                if (log.IsDebugEnabled)
                {
                    log.Debug("Output file info:" + response.ToString());
                }
            }
        }
    }
}
