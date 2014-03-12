using System.IO;
using System.Web.Mvc;

namespace MvcDI
{
    public class FileOutResult : FileStreamResult
    {
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
        }
    }
}
