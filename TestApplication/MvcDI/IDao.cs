using System.Data.Common;

namespace MvcDI
{
    public interface IDao
    {
        /// <summary>
        /// DB接続
        /// </summary>
        DbConnection Con { get; }
    }
}
