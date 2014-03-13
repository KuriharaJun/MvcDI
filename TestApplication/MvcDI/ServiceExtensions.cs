using System;
using System.Data.Common;
using System.Reflection;
using log4net;

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
        DbTransaction Tx { get; set; }

        /// <summary>
        /// DBコネクション
        /// </summary>
        DbConnection Con { get; }
    }

    /// <summary>
    /// サービス拡張クラス
    /// </summary>
    public static class ServiceExtensions
    {
        /// <summary>
        /// ログ
        /// </summary>
        private static ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().ReflectedType);

        /// <summary>
        /// トランザクション開始処理
        /// </summary>
        /// <param name="service"></param>
        public static void BeginTransaction(this IService service)
        {
            service.Tx = service.Con.BeginTransaction(System.Data.IsolationLevel.ReadCommitted);
            if (log.IsInfoEnabled)
            {
                log.Info("Transaction start. IsolationLevel is ReadCommitted.");
            }
        }

        /// <summary>
        /// コミット処理
        /// </summary>
        /// <param name="service"></param>
        public static void Commit(this IService service)
        {
            try
            {
                service.Tx.Commit();
                EndTransaction(service);
                if (log.IsInfoEnabled)
                {
                    log.Info("Commit success. Transaction close.");
                }
            }
            catch (Exception ex)
            {
                log.Fatal("Commit failed.", ex);
            }
        }

        /// <summary>
        /// ロールバック処理
        /// </summary>
        /// <param name="service"></param>
        public static void Roolback(this IService service)
        {
            try
            {
                service.Tx.Rollback();
                EndTransaction(service);
                if (log.IsInfoEnabled)
                {
                    log.Info("Rollback success. Transaction close.");
                }
            }
            catch (Exception ex)
            {
                log.Fatal("Rollback failed.", ex);
            }
        }

        /// <summary>
        /// トランザクション終了処理
        /// </summary>
        /// <param name="service"></param>
        public static void EndTransaction(this IService service)
        {
            try
            {
                service.Tx.Dispose();
                if (log.IsInfoEnabled)
                {
                    log.Info("Transaction dispose success.");
                }
            }
            catch (Exception ex)
            {
                log.Fatal("Transaction dispose fail.", ex);
            }
        }
    }
}
