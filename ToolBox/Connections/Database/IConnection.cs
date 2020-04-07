using System;
using System.Collections.Generic;
using System.Data;

namespace ToolBox.Connections
{
    public interface IConnection
    {
        int ExecuteNonQuery(Command command);
        IEnumerable<T> ExecuteReader<T>(Command command, Func<IDataRecord, T> selector);
        object ExecuteScalar(Command command);
    }
}