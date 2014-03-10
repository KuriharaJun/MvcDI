using System.Data.Common;

namespace MvcDI
{
    /// <summary>
    /// ワーカーサービス実装インターフェース
    /// </summary>
    public interface IService
    {
        /// <summary>
        /// トランザクション
        /// </summary>
        DbTransaction tx { get; set; }

        /// <summary>
        /// DBコネクション
        /// </summary>
        DbConnection con { get; set; }
    }

    /// <summary>
    /// サービス拡張クラス
    /// </summary>
    public static class ServiceExtensions
    {
        /// <summary>
        /// トランザクション開始処理
        /// </summary>
        /// <param name="service"></param>
        public static void BeginTransaction(this IService service)
        {
            service.tx = service.con.BeginTransaction(System.Data.IsolationLevel.ReadCommitted);
        }

        /// <summary>
        /// コミット処理
        /// </summary>
        /// <param name="service"></param>
        public static void Commit(this IService service)
        {
            try
            {
                service.tx.Commit();
                EndTransaction(service);
            }
            catch { }
        }

        /// <summary>
        /// ロールバック処理
        /// </summary>
        /// <param name="service"></param>
        public static void Roolback(this IService service)
        {
            try
            {
                service.tx.Rollback();
                EndTransaction(service);
            }
            catch { }
        }

        /// <summary>
        /// トランザクション終了処理
        /// </summary>
        /// <param name="service"></param>
        public static void EndTransaction(this IService service)
        {
            try
            {
                service.tx.Dispose();
            }
            catch { }
        }
    }
}
