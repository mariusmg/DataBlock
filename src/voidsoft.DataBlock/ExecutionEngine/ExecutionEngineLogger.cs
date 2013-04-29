using System.Collections.Generic;
using System.Data;

namespace voidsoft.DataBlock
{
	internal class ExecutionEngineLogger
	{
		/// <summary>
		///     Logs the execution context.
		/// </summary>
		/// <param name="storedProcedureName">Name of the stored procedure.</param>
		/// <param name="dataParameters">The data parameters.</param>
		public void LogContext(string storedProcedureName, params IDataParameter[] dataParameters)
		{
			ExecutionQuery query = new ExecutionQuery("exec " + storedProcedureName, dataParameters);
			Log.LogMessage(query);
			Configuration.RaiseQueryLoggingEvent(query);
		}

		/// <summary>
		///     Logs the execution context.
		/// </summary>
		/// <param name="query">The query.</param>
		public void LogContext(ExecutionQuery query)
		{
			Log.LogMessage(query);
			Configuration.RaiseQueryLoggingEvent(query);
		}

		/// <summary>
		///     Logs the execution context.
		/// </summary>
		/// <param name="listQueries">The list queries.</param>
		public void LogContext(List<ExecutionQuery> listQueries)
		{
			Log.LogMessage(listQueries);
			Configuration.RaiseQueryLoggingEvent(listQueries);
		}
	}
}