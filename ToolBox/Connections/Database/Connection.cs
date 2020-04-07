using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;

namespace ToolBox.Connections
{
    public class Connection : IConnection
    {
        private string _connectionString;
        private DbProviderFactory _factory;

        public Connection(string connectionString, DbProviderFactory factory)
        {
            _factory = factory ?? throw new ArgumentNullException();

            if (string.IsNullOrWhiteSpace(connectionString))
                throw new ArgumentException("Invalid ConnectionString!");

            _connectionString = connectionString;
            using (DbConnection connection = CreateConnection())
            {
                connection.Open();
            }
        }

        public IEnumerable<T> ExecuteReader<T>(Command command, Func<IDataRecord, T> selector)
        {
            using (DbConnection connection = CreateConnection())
            {
                using (DbCommand dbCommand = CreateCommand(connection, command))
                {
                    connection.Open();
                    using (DbDataReader dataReader = dbCommand.ExecuteReader())
                    {
                        while (dataReader.Read())
                        {
                            yield return selector(dataReader);
                        }
                    }
                }
            }
        }

        public object ExecuteScalar(Command command)
        {
            using (DbConnection connection = CreateConnection())
            {
                using (DbCommand dbCommand = CreateCommand(connection, command))
                {
                    connection.Open();
                    object o = dbCommand.ExecuteScalar();
                    return (o is DBNull) ? null : o;
                }
            }
        }

        public int ExecuteNonQuery(Command command)
        {
            using (DbConnection connection = CreateConnection())
            {
                using (DbCommand dbCommand = CreateCommand(connection, command))
                {
                    connection.Open();
                    return dbCommand.ExecuteNonQuery();
                }
            }
        }

        private DbConnection CreateConnection()
        {
            DbConnection connection = _factory.CreateConnection();
            connection.ConnectionString = _connectionString;

            return connection;
        }

        private DbCommand CreateCommand(DbConnection connection, Command command)
        {
            DbCommand dbCommand = connection.CreateCommand();
            dbCommand.CommandText = command.Query;

            if (command.IsStoredProcedure)
                dbCommand.CommandType = CommandType.StoredProcedure;

            foreach (KeyValuePair<string, object> kvp in command.Parameters)
            {
                DbParameter dbParameter = dbCommand.CreateParameter();
                dbParameter.ParameterName = kvp.Key;
                dbParameter.Value = kvp.Value;

                dbCommand.Parameters.Add(dbParameter);
            }

            return dbCommand;
        }
    }
}
