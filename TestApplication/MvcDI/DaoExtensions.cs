using System.Data.Common;
using System.Data.SqlClient;

namespace MvcDI
{
    public static class DaoExtensions
    {
        public static DbCommand CommandTransaction(this IDao dao, DbTransaction tx, SqlType serverType)
        {
            switch (serverType)
            {
                case SqlType.SqlServer:
                    {
                        var command = new SqlCommand();
                        command.Connection = (SqlConnection)dao.Con;
                        command.Transaction = (SqlTransaction)tx;
                        return command;
                    }
                default:
                    {
                        return null;
                    }
            }
        }

        public static DbCommand CommandNonTransaction(this IDao dao, SqlType serverType)
        {
            switch (serverType)
            {
                case SqlType.SqlServer:
                    {
                        var command = new SqlCommand();
                        command.Connection = (SqlConnection) dao.Con;
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
