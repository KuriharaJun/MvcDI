using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MvcDI
{
    public enum SqlType : byte
    {
        ODBC,
        SqlServer,
        Oracle,
        PostgreSQL,
        MySql
    }
}
