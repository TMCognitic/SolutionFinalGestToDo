using System;
using System.Collections.Generic;
using System.Text;

namespace ToolBox.Connections
{
    public class Command
    {
        internal string Query { get; private set; }
        internal bool IsStoredProcedure { get; private set; }
        internal Dictionary<string, object> Parameters { get; private set; }

        public Command(string query, bool isStoredProcedure = false)
        {
            if (string.IsNullOrWhiteSpace(query))
                throw new ArgumentException("Invalid Query");

            Query = query;
            IsStoredProcedure = isStoredProcedure;
            Parameters = new Dictionary<string, object>();
        }

        public void AddParameter(string parameterName, object value)
        {
            if (string.IsNullOrWhiteSpace(parameterName))
                throw new ArgumentException("Invalid Parameter Name");

            Parameters.Add(parameterName, value ?? DBNull.Value);
        }
    }
}
