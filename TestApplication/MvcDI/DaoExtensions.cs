using System;
using System.Data.Common;
using System.Data.SqlClient;
using System.Reflection;
using log4net;

namespace MvcDI
{
    /// <summary>
    /// Dao拡張クラス
    /// </summary>
    public static class DaoExtensions
    {
        /// <summary>
        /// ログ
        /// </summary>
        private static ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// トランザクション付きコマンドの取得
        /// </summary>
        /// <param name="dao"></param>
        /// <param name="tx"></param>
        /// <param name="serverType"></param>
        /// <returns></returns>
        public static DbCommand CommandTransaction(this IDao dao, DbTransaction tx, SqlType serverType)
        {
            switch (serverType)
            {
                case SqlType.SqlServer:
                    {
                        var command = new SqlCommand();
                        command.Connection = (SqlConnection)dao.Con;
                        command.Transaction = (SqlTransaction)tx;
                        if (log.IsInfoEnabled)
                        {
                            log.Info("command type is SqlCommand.");
                        }
                        return command;
                    }
                default:
                    {
                        if (log.IsErrorEnabled)
                        {
                            log.Error("Don't supported command type. Type is " + Enum.GetName(typeof(SqlType), serverType));
                        }
                        return null;
                    }
            }
        }

        /// <summary>
        /// トランザクション無しコマンドの取得
        /// </summary>
        /// <param name="dao"></param>
        /// <param name="serverType"></param>
        /// <returns></returns>
        public static DbCommand CommandNonTransaction(this IDao dao, SqlType serverType)
        {
            switch (serverType)
            {
                case SqlType.SqlServer:
                    {
                        var command = new SqlCommand();
                        command.Connection = (SqlConnection) dao.Con;
                        if (log.IsInfoEnabled)
                        {
                            log.Info("command type is SqlCommand.");
                        }
                        return command;
                    }
                default:
                    {
                        if (log.IsErrorEnabled)
                        {
                            log.Error("Don't supported command type. Type is " + Enum.GetName(typeof(SqlType), serverType));
                        }
                        return null;
                    }
            }
        }
    }
}
