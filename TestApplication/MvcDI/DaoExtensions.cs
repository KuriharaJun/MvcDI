using System.Data.Common;
using System.Data.SqlClient;

namespace MvcDI
{
    public static class DaoExtensions
    {
        public static SqlType ServerType { get; private set; }
        public static DbCommand CommandTransaction(this IDao dao, DbTransaction tx)
        {
            switch (ServerType)
            {
                case SqlType.SqlServer:
                    {
                        var command = new SqlCommand();
                        command.Connection = (SqlConnection)dao.con;
                        command.Transaction = (SqlTransaction)tx;
                        return command;
                    }
                default:
                    {
                        return null;
                    }
            }
        }

        public static DbCommand CommandNonTransaction(this IDao dao)
        {
            switch (ServerType)
            {
                case SqlType.SqlServer:
                    {
                        var command = new SqlCommand();
                        command.Connection = (SqlConnection) dao.con;
                        return command;
                    }
                default:
                    {
                        return null;
                    }
            }
        }
    }
}
