/*

      file : ExecutionQuery
description: The ExecutionQuery is a single query which is runned by the execution engine.
             It contains the query string and a array of parameters.
    
   (c) 2004 - 2006 Marius Gheorghe. All rights reserved.
 */

using System;
using System.Data;
using System.Text;

namespace voidsoft.DataBlock
{
	/// <summary>
	///     Represents the execution context of a query
	/// </summary>
	public struct ExecutionQuery
	{
		/// <summary>
		///     IDataParameters associated with the query
		/// </summary>
		public IDataParameter[] Parameters;

		/// <summary>
		///     SQL query to be executed
		/// </summary>
		public string Query;

		/// <summary>
		///     Constructor
		/// </summary>
		/// <param name="query">The Sql query</param>
		/// <param name="parameters">IDataParameters associated with the query</param>
		public ExecutionQuery(string query, IDataParameter[] parameters)
		{
			Query = query;
			Parameters = parameters;
		}

		/// <summary>
		///     Gets the ExecutionQuery details as a string.
		/// </summary>
		/// <returns>
		///     A <see cref="T:System.String"></see> containing a fully qualified type name.
		/// </returns>
		public override string ToString()
		{
			StringBuilder builder = new StringBuilder();

			builder.Append(Query);
			builder.Append(Environment.NewLine + "Parameter list: ");
			if (Parameters != null)
			{
				foreach (IDataParameter var in Parameters)
				{
					builder.Append(" " + var.ParameterName + "=" + var.Value);
					builder.Append(Environment.NewLine);
				}
			}

			return builder.ToString();
		}
	}
}