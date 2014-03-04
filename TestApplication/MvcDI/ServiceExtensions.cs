using System.Transactions;

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
        TransactionScope tx { get; set; }
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
            service.tx = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions()
            {
                IsolationLevel= IsolationLevel.ReadCommitted,
                Timeout = TransactionManager.DefaultTimeout
            });
        }

        /// <summary>
        /// コミット処理
        /// </summary>
        /// <param name="service"></param>
        public static void Commit(this IService service)
        {
            try
            {
                service.tx.Complete();
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
