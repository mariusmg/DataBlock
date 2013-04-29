/*

  
       file: ExecutionEngineStatic.cs
description: contains the static impllementation of the data access layer.
  
  
     (c) 2004 - 2008 Marius Gheorghe. All rights reserved.
  
   
  
 NOTES :

  
- List of operations supported by the  PersistentObject:
    
   1. FOREIGN_KEY_CONSTRAINT

       - "ForeignKeyConstraint//" -> type of constraint
        - "x//" -> where x is the numer of rows on which the primary key will be inserted.
        - "fieldName//tableName" -> where field name is the name of the primary key field 
                                    and table name is the name of the table.

        Example : ForeignKeyConstraint//1//First//Id


   2. PRIMARY_KEY_CONSTRAINT - get the inserted primary key value.

       - "PrimaryKeyConstraint//"  -> type of constraint
       - "tableName//fieldName//" -> where field name is the name of the primary key field 
                                   and table name is the name of the table.
       - sql query

        Example :  GetPrimaryKey//Customer-CustomerId;Add INTO Customer values('gogu');

  
*/

using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Runtime.CompilerServices;
using System.Xml;

namespace voidsoft.DataBlock
{
	/// <summary>
	///     ExecutionEngine
	/// </summary>
	public partial class ExecutionEngine
	{
		//This represents a ForeignKey constraint. With this constraint enabled the
		//value of the primary key is retrieved and inserted in the child tables.
		internal const string FOREIGN_KEY_CONSTRAINT = "ForeignKeyConstraint";

		//This represents a PrimaryKey constraint. With this constraint enabled the 
		//value of the PK is returned.
		internal const string PRIMARY_KEY_CONSTRAINT = "PrimaryKeyConstraint";

		/// <summary>
		///     Executes a constrained list of queries
		/// </summary>
		/// <param name="database">Database server type</param>
		/// <param name="connection">Connection object</param>
		/// <param name="command">Command object</param>
		/// <param name="listQueries">List of ExecutionQueries which must be executed</param>
		/// <returns></returns>
		public static int ExecuteNonQueryConstrained(DatabaseServer database, ref DbConnection connection, ref DbCommand command, List<ExecutionQuery> listQueries)
		{
			//NOTE : the Queue<T> doesn't have an indexer so we can't modify the ForeignConstraint.
			//That's why will use a List<T>.

			List<ForeignKeyConstraint> fkConstraints = new List<ForeignKeyConstraint>();
			List<PrimaryKeyConstraint> pkConstraints = new List<PrimaryKeyConstraint>();

			//the value of the primary key.
			object primaryKeyValue = null;

			//total number of modified rows.
			int modifiedRows = 0;

			//log data
			(new ExecutionEngineLogger()).LogContext(listQueries);

			for (int i = 0; i < listQueries.Count; i++)
			{
				//check for a constraints first

				if (listQueries[i].Query.StartsWith(FOREIGN_KEY_CONSTRAINT))
				{
					ForeignKeyConstraint fk = ConstraintManager.ParseForeignKeyConstraint(listQueries[i]);
					fkConstraints.Add(fk);
				}
				else if (listQueries[i].Query.StartsWith(PRIMARY_KEY_CONSTRAINT))
				{
					PrimaryKeyConstraint pk = ConstraintManager.ParsePrimaryKeyConstraint(listQueries[i]);
					pkConstraints.Add(pk);

					List<object> listGeneratedPks = new List<object>();

					List<ExecutionQuery> listExecQueries = new List<ExecutionQuery>();
					listExecQueries.Add(listQueries[i]);

					modifiedRows += ExecuteNonQueryWithPrimaryKeyConstraints(database, ref connection, ref command, listExecQueries, out listGeneratedPks);

					primaryKeyValue = listGeneratedPks[0];
				}
				else
				{
					//we have no constraints so just run the query.
					if (fkConstraints.Count == 0)
					{
						command.Parameters.Clear();

						if (listQueries[i].Parameters != null)
						{
							for (int j = 0; j < listQueries[i].Parameters.Length; j++)
							{
								command.Parameters.Add(listQueries[i].Parameters[j]);
							}
						}

						command.CommandType = CommandType.Text;
						command.CommandText = listQueries[i].Query;
						modifiedRows += command.ExecuteNonQuery();

						primaryKeyValue = null;
					}
					else if (fkConstraints.Count > 0)
					{
						//we have constraints enabled.

						//get the current query and modify it
						string currentQuery = listQueries[i].Query;

						//get the current constraint.
						ForeignKeyConstraint fk = fkConstraints[0];

						//check if we have already a primary key.
						if (primaryKeyValue != null)
						{
							//replace the value with the value of the primary key.
							ReplaceForeignKeyValue(ref currentQuery, primaryKeyValue.ToString());

							//we have modified the running query. Now decresed the number of running queries.
							--fk.NumerOfRunningQueries;

							//check if it's the last running query.
							if (fk.NumerOfRunningQueries == 0)
							{
								//remove the constraint
								fkConstraints.RemoveAt(0);
							}
							else
							{
								//set back the constraint with the new value
								fkConstraints[0] = fk;
							}
						}

						ExecutionQuery execQuery = new ExecutionQuery(currentQuery, listQueries[i].Parameters);

						//run the query and return the value of the primary key
						modifiedRows += ExecuteNonQueryWithPrimaryKey(database, ref connection, ref command, execQuery, fk.NameOfPrimaryKeyField, fk.TableName,
																	  ref primaryKeyValue);
					}
				}
			}

			return modifiedRows;
		}

		/// <summary>
		///     Executes the non query with primary key constraints.
		/// </summary>
		/// <param name="database">The database.</param>
		/// <param name="connection">The connection.</param>
		/// <param name="command">The command.</param>
		/// <param name="listQueries">The list queries.</param>
		/// <param name="listPrimaryKeyValues">The list primary key values.</param>
		/// <returns></returns>
		public static int ExecuteNonQueryWithPrimaryKeyConstraints(DatabaseServer database, ref DbConnection connection, ref DbCommand command,
																   List<ExecutionQuery> listQueries, out List<object> listPrimaryKeyValues)
		{
			List<object> listValues = new List<object>();

			int modifiedRows = 0;

			foreach (ExecutionQuery executionQuery in listQueries)
			{
				if (executionQuery.Query.StartsWith(PRIMARY_KEY_CONSTRAINT))
				{
					PrimaryKeyConstraint keyConstraint = ConstraintManager.ParsePrimaryKeyConstraint(executionQuery);

					ExecutionQuery currentQuery = ConstraintManager.StripPrimaryKeyConstraint(executionQuery);

					object pkValue = null;

					modifiedRows += ExecuteNonQueryWithPrimaryKey(database, ref connection, ref command, currentQuery, keyConstraint.PrimaryKeyFieldName,
																  keyConstraint.TableName, ref pkValue);

					listValues.Add(pkValue);
				}
			}

			listPrimaryKeyValues = listValues;

			return modifiedRows;
		}

		/// <summary>
		///     Executes the query, and returns the first column of the first row
		///     in the result set returned by the query. Extra columns or rows are ignored.
		/// </summary>
		/// <param name="edt">Provider Type</param>
		/// <param name="connectionString">Connection String</param>
		/// <param name="storedProcedureName">The stored procedure's name</param>
		/// <param name="iparams">Stored Procedure's parameters.</param>
		/// <returns></returns>
		[MethodImpl(MethodImplOptions.Synchronized)]
		public static object ExecuteScalar(DatabaseServer edt, string connectionString, string storedProcedureName, params IDataParameter[] iparams)
		{
			DbConnection icon = null;
			DbCommand icmd = null;
			object result = null;

			try
			{
				DataFactory.InitializeDbConnection(edt, ref icon);
				DataFactory.InitializeDbCommand(edt, ref icmd);


				icon.ConnectionString = connectionString;
				icmd.Connection = icon;
				icon.Open();

				icmd.CommandType = CommandType.StoredProcedure;
				icmd.CommandText = storedProcedureName;

				if (iparams != null)
				{
					for (int i = 0; i < iparams.Length; i++)
					{
						icmd.Parameters.Add(iparams[i]);
					}
				}


				(new ExecutionEngineLogger()).LogContext(storedProcedureName);

				return (result = icmd.ExecuteScalar());
			}
			finally
			{
				DisposeObjects(ref icon, ref icmd);
			}
		}

		/// <summary>
		///     Execute scalar method
		/// </summary>
		/// <param name="database">Database provider dataType</param>
		/// <param name="connectionString">Connection string </param>
		/// <param name="query">query command</param>
		/// <returns></returns>
		public static object ExecuteScalar(DatabaseServer database, string connectionString, ExecutionQuery query)
		{
			List<ExecutionQuery> listQueries = new List<ExecutionQuery>();
			listQueries.Add(query);
			object[] obj = null;

			ExecuteScalar(database, connectionString, listQueries, out obj);

			return obj[0];
		}

		/// <summary>
		///     ExecuteScalar method
		/// </summary>
		/// <param name="edt">Database provider dataType</param>
		/// <param name="connectionString">Connection string</param>
		/// <param name="queries">queries</param>
		/// <param name="results">query results</param>
		[MethodImpl(MethodImplOptions.Synchronized)]
		public static void ExecuteScalar(DatabaseServer edt, string connectionString, List<ExecutionQuery> queries, out object[] results)
		{
			DbConnection icon = null;
			DbCommand icmd = null;
			object[] res = null;

			try
			{
				res = new object[queries.Count];

				DataFactory.InitializeDbConnection(edt, ref icon);
				DataFactory.InitializeDbCommand(edt, ref icmd);


				//log data
				(new ExecutionEngineLogger()).LogContext(queries);

				icon.ConnectionString = connectionString;
				icmd.Connection = icon;
				icon.Open();

				for (int i = 0; i < queries.Count; i++)
				{
					icmd.Parameters.Clear();

					if (queries[i].Parameters != null)
					{
						foreach (IDataParameter var in queries[i].Parameters)
						{
							icmd.Parameters.Add(var);
						}
					}

					icmd.CommandType = CommandType.Text;
					icmd.CommandText = queries[i].Query;

					object result = icmd.ExecuteScalar();
					res[i] = result;
				}

				results = res;

			}
			finally
			{
				DisposeObjects(ref icon, ref icmd);
			}
		}

		/// <summary>
		///     Executes a query
		/// </summary>
		/// <param name="database">Database server</param>
		/// <param name="connectionString">The connection string</param>
		/// <param name="storedProcedureName">Name of the stored procedure</param>
		/// <param name="iparams">Array of Parameters</param>
		/// <returns>Number of affected rows</returns>
		[MethodImpl(MethodImplOptions.Synchronized)]
		public static int ExecuteNonQuery(DatabaseServer database, string connectionString, string storedProcedureName, params IDataParameter[] iparams)
		{
			DbConnection icon = null;
			DbCommand icmd = null;

			try
			{
				DataFactory.InitializeDbConnection(database, ref icon);
				DataFactory.InitializeDbCommand(database, ref icmd);
				icmd.Connection = icon;


				icon.ConnectionString = connectionString;

				icon.Open();

				icmd.CommandType = CommandType.StoredProcedure;

				if (iparams != null)
				{
					for (int i = 0; i < iparams.Length; i++)
					{
						icmd.Parameters.Add(iparams[i]);
					}
				}

				icmd.CommandText = storedProcedureName;


				//logs the message
				(new ExecutionEngineLogger()).LogContext(storedProcedureName);

				return icmd.ExecuteNonQuery();
			}
			finally
			{
				DisposeObjects(ref icon, ref icmd);
			}
		}

		/// <summary>
		///     Executes a query
		/// </summary>
		/// <param name="database">Database provider Type</param>
		/// <param name="connectionString">connection string</param>
		/// <param name="executionQuery">query command</param>
		/// <returns>Returns number of affected rows</returns>
		[MethodImpl(MethodImplOptions.Synchronized)]
		public static int ExecuteNonQuery(DatabaseServer database, string connectionString, ExecutionQuery executionQuery)
		{
			DbConnection icon = null;
			DbCommand icmd = null;

			try
			{
				DataFactory.InitializeDbConnection(database, ref icon);
				DataFactory.InitializeDbCommand(database, ref icmd);

				icmd.Connection = icon;
				icon.ConnectionString = connectionString;


				icmd.Parameters.Clear();

				if (executionQuery.Parameters != null)
				{
					foreach (IDataParameter var in executionQuery.Parameters)
					{
						icmd.Parameters.Add(var);
					}
				}

				icmd.CommandType = CommandType.Text;
				icmd.CommandText = executionQuery.Query;

				icon.Open();


				//log query
				(new ExecutionEngineLogger()).LogContext(executionQuery);

				return icmd.ExecuteNonQuery();
			}
			finally
			{
				DisposeObjects(ref icon, ref icmd);
			}
		}

		/// <summary>
		///     Executes multiple queries in a transaction.
		/// </summary>
		/// <param name="database">Database provider dataType</param>
		/// <param name="connectionString">database connection string</param>
		/// <param name="listQueries">List of queries to be executed</param>
		/// <param name="isolationLevel">Transaction isolation level</param>
		[MethodImpl(MethodImplOptions.Synchronized)]
		public static int ExecuteNonQuery(DatabaseServer database, string connectionString, List<ExecutionQuery> listQueries, IsolationLevel isolationLevel)
		{
			DbConnection icon = null;
			DbCommand icmd = null;
			DbTransaction itrans = null;

			int queriesExecuted = 0;

			try
			{
				if (listQueries.Count == 1)
				{
					return ExecuteNonQuery(database, connectionString, listQueries[0]);
				}

				DataFactory.InitializeDbConnection(database, ref icon);
				DataFactory.InitializeDbCommand(database, ref icmd);

				icmd.Connection = icon;


				icon.ConnectionString = connectionString;
				icon.Open();

				itrans = icon.BeginTransaction(isolationLevel);
				icmd.Transaction = itrans;

				queriesExecuted = ExecuteNonQueryConstrained(database, ref icon, ref icmd, listQueries);


				itrans.Commit();

				return queriesExecuted;
			}
			catch
			{
				if (itrans != null)
				{
					itrans.Rollback();
				}

				throw;
			}
			finally
			{
				DisposeObjects(ref icon, ref icmd);
			}
		}

		/// <summary>
		///     Executes the  query.
		/// </summary>
		/// <param name="database">The database.</param>
		/// <param name="connectionString">The connection string.</param>
		/// <param name="listQueries">The list queries.</param>
		/// <param name="isolationLevel">The isolation level.</param>
		/// <param name="listPrimaryKeys">The list primary keys.</param>
		/// <returns></returns>
		[MethodImpl(MethodImplOptions.Synchronized)]
		public static int ExecuteNonQuery(DatabaseServer database, string connectionString, List<ExecutionQuery> listQueries, IsolationLevel isolationLevel,
										  out List<object> listPrimaryKeys)
		{
			DbConnection icon = null;
			DbCommand icmd = null;
			DbTransaction itrans = null;

			int queriesExecuted = 0;

			try
			{
				DataFactory.InitializeDbConnection(database, ref icon);
				DataFactory.InitializeDbCommand(database, ref icmd);

				icmd.Connection = icon;


				icon.ConnectionString = connectionString;
				icon.Open();

				itrans = icon.BeginTransaction(isolationLevel);
				icmd.Transaction = itrans;

				queriesExecuted = ExecuteNonQueryWithPrimaryKeyConstraints(database, ref icon, ref icmd, listQueries, out listPrimaryKeys);


				itrans.Commit();

				return queriesExecuted;
			}
			catch
			{
				if (itrans != null)
				{
					itrans.Rollback();
				}

				throw;
			}
			finally
			{
				DisposeObjects(ref icon, ref icmd);
			}
		}

		/// <summary>
		///     ExecuteXmlReader. Supported only by SQL Server 2000(5).
		/// </summary>
		/// <param name="connectionString">Connection string</param>
		/// <param name="executableQuery">Query to be executed</param>
		[MethodImpl(MethodImplOptions.Synchronized)]
		public static XmlReader ExecuteXmlReader(string connectionString, ExecutionQuery executableQuery)
		{
			SqlConnection icon = new SqlConnection(connectionString);
			SqlCommand icmd = new SqlCommand(executableQuery.Query, icon);
			XmlReader xreader = null;


			//log queries
			(new ExecutionEngineLogger()).LogContext(executableQuery);

			icon.Open();

			//must also clse the underlying connectino when closing the reader
			return (xreader = icmd.ExecuteXmlReader());

		}

		/// <summary>
		///     Executes a DataReader using the CommandBehaviour.CloseConnection
		/// </summary>
		/// <param name="edt">Database provider dataType</param>
		/// <param name="connection">Connection strings</param>
		/// <param name="storedProcedureName">Name of the stored procedure</param>
		/// <param name="iparams">Stored PRocedure Parameters</param>
		/// <returns>Resulting IDataReader</returns>
		[MethodImpl(MethodImplOptions.Synchronized)]
		public static IDataReader ExecuteReader(DatabaseServer edt, string connection, string storedProcedureName, params IDataParameter[] iparams)
		{
			//ADO.NET objects are not disposed because of the CloseConnection CommandBehaviour.

			IDataReader iread = null;
			DbConnection icon = null;
			DbCommand icmd = null;

			DataFactory.InitializeDbConnection(edt, ref icon);
			icon.ConnectionString = connection;

			DataFactory.InitializeDbCommand(edt, ref icmd);
			icmd.CommandType = CommandType.StoredProcedure;
			icmd.Connection = icon;

			icmd.CommandText = storedProcedureName;

			icon.Open();

			if (iparams != null)
			{
				for (int i = 0; i < iparams.Length; i++)
				{
					icmd.Parameters.Add(iparams[i]);
				}
			}

			(new ExecutionEngineLogger()).LogContext(storedProcedureName);

			icmd.Connection = icon;
			iread = icmd.ExecuteReader(CommandBehavior.CloseConnection);
			return iread;

		}

		/// <summary>
		///     Executes a DataReader using the CommandBehaviour.CloseConnection connection.
		/// </summary>
		/// <param name="edt">Database provider dataType</param>
		/// <param name="connection">Connection string </param>
		/// <param name="executableQuery">Query to be executed</param>
		/// <returns>Resulting IDataReader</returns>
		[MethodImpl(MethodImplOptions.Synchronized)]
		public static IDataReader ExecuteReader(DatabaseServer edt, string connection, ExecutionQuery executableQuery)
		{
			//ADO.NET objects are not disposed because of the CloseConnection CommandBehaviour.

			IDataReader iread = null;
			DbConnection icon = null;
			DbCommand icmd = null;

			DataFactory.InitializeDbConnection(edt, ref icon);
			icon.ConnectionString = connection;

			DataFactory.InitializeDbCommand(edt, ref icmd);
			icmd.Connection = icon;


			icmd.Parameters.Clear();

			if (executableQuery.Parameters != null)
			{
				foreach (IDataParameter var in executableQuery.Parameters)
				{
					icmd.Parameters.Add(var);
				}
			}

			icmd.CommandType = CommandType.Text;
			icmd.CommandText = executableQuery.Query;

			(new ExecutionEngineLogger()).LogContext(executableQuery);

			icon.Open();

			iread = icmd.ExecuteReader(CommandBehavior.CloseConnection);


			return iread;
		}

		/// <summary>
		///     Executes a DataSet
		/// </summary>
		/// <param name="edt">Provider Type</param>
		/// <param name="connectionString">Connection Strinng</param>
		/// <param name="storedProcedure">Stored Procedure's name</param>
		/// <param name="iparam">Stored Procedure Data Parameters</param>
		/// <returns>Resulting DataSet</returns>
		[MethodImpl(MethodImplOptions.Synchronized)]
		public static DataSet ExecuteDataSet(DatabaseServer edt, string connectionString, string storedProcedure, params IDataParameter[] iparam)
		{
			DbConnection icon = null;
			DbCommand icmd = null;
			DataSet dset = null;
			DbDataAdapter idap = null;

			try
			{
				DataFactory.InitializeDbConnection(edt, ref icon);
				DataFactory.InitializeDbCommand(edt, ref icmd);
				DataFactory.InitializeDbDataAdapter(edt, ref idap, icmd);


				icon.ConnectionString = connectionString;
				icmd.Connection = icon;
				icon.Open();

				dset = new DataSet();

				if (iparam != null)
				{
					for (int i = 0; i < iparam.Length; i++)
					{
						icmd.Parameters.Add(iparam[i]);
					}
				}

				(new ExecutionEngineLogger()).LogContext(storedProcedure);

				icmd.CommandType = CommandType.StoredProcedure;
				icmd.CommandText = storedProcedure;

				idap.SelectCommand = icmd;
				idap.Fill(dset);
				return dset;

			}
			finally
			{
				DisposeObjects(ref icon, ref icmd);
			}
		}

		/// <summary>
		///     Fills the specified dataset with a new DataTable called Table.
		/// </summary>
		/// <param name="edt">Database server type</param>
		/// <param name="connectionString">Connection string used to connect to the database</param>
		/// <param name="executableQuery">Query to be executed</param>
		/// <param name="ds">The dataset which will be filled.</param>
		[MethodImpl(MethodImplOptions.Synchronized)]
		public static void ExecuteDataSet(DatabaseServer edt, string connectionString, ExecutionQuery executableQuery, ref DataSet ds)
		{
			DbConnection icon = null;
			DbCommand icmd = null;
			DbDataAdapter idap = null;

			try
			{
				DataFactory.InitializeDbConnection(edt, ref icon);
				DataFactory.InitializeDbCommand(edt, ref icmd);
				DataFactory.InitializeDbDataAdapter(edt, ref idap, icmd);

				icmd.Connection = icon;


				icon.ConnectionString = connectionString;

				icmd.Parameters.Clear();

				if (executableQuery.Parameters != null)
				{
					foreach (IDataParameter var in executableQuery.Parameters)
					{
						icmd.Parameters.Add(var);
					}
				}

				icmd.CommandType = CommandType.Text;
				icmd.CommandText = executableQuery.Query;

				idap.SelectCommand = icmd;

				//log the query
				(new ExecutionEngineLogger()).LogContext(executableQuery);

				icon.Open();

				idap.Fill(ds);

			}
			finally
			{
				DisposeObjects(ref icon, ref icmd);
			}
		}

		/// <summary>
		///     Executes a DataSet
		/// </summary>
		/// <param name="edt">Database provider dataType</param>
		/// <param name="connectionString">Connection string</param>
		/// <param name="executableQuery">Query to be executed</param>
		/// <returns>Resulting DataSet</returns>
		[MethodImpl(MethodImplOptions.Synchronized)]
		public static DataSet ExecuteDataSet(DatabaseServer edt, string connectionString, ExecutionQuery executableQuery)
		{
			DbConnection icon = null;
			DbCommand icmd = null;
			DataSet dset = null;
			DbDataAdapter idap = null;

			try
			{
				DataFactory.InitializeDbConnection(edt, ref icon);
				DataFactory.InitializeDbCommand(edt, ref icmd);
				DataFactory.InitializeDbDataAdapter(edt, ref idap, icmd);

				icmd.Connection = icon;
				dset = new DataSet();

				icon.ConnectionString = connectionString;

				icmd.Parameters.Clear();

				if (executableQuery.Parameters != null)
				{
					foreach (IDataParameter var in executableQuery.Parameters)
					{
						icmd.Parameters.Add(var);
					}
				}

				icmd.CommandText = executableQuery.Query;
				idap.SelectCommand = icmd;

				//log the queries
				(new ExecutionEngineLogger()).LogContext(executableQuery);

				icon.Open();

				idap.Fill(dset);

				return dset;

			}
			finally
			{
				DisposeObjects(ref icon, ref icmd);
			}
		}

		/// <summary>
		///     Executes a DataTable
		/// </summary>
		/// <param name="edt">Provider Type</param>
		/// <param name="connectionString">Connection Strinng</param>
		/// <param name="storedProcedure">Stored Procedure's name</param>
		/// <param name="iparam">Stored Procedure Data Parameters</param>
		/// <returns>DataSet</returns>
		[MethodImpl(MethodImplOptions.Synchronized)]
		public static DataTable ExecuteDataTable(DatabaseServer edt, string connectionString, string storedProcedure, params IDataParameter[] iparam)
		{
			DbConnection icon = null;
			DbCommand icmd = null;
			DataTable table = null;
			DbDataAdapter idap = null;

			try
			{
				DataFactory.InitializeDbConnection(edt, ref icon);
				DataFactory.InitializeDbCommand(edt, ref icmd);
				DataFactory.InitializeDbDataAdapter(edt, ref idap, icmd);


				icon.ConnectionString = connectionString;
				icmd.Connection = icon;
				icon.Open();

				table = new DataTable();

				if (iparam != null)
				{
					for (int i = 0; i < iparam.Length; i++)
					{
						icmd.Parameters.Add(iparam[i]);
					}
				}

				//log the message
				(new ExecutionEngineLogger()).LogContext(storedProcedure);

				icmd.CommandType = CommandType.StoredProcedure;
				icmd.CommandText = storedProcedure;

				idap.SelectCommand = icmd;
				idap.Fill(table);

				return table;

			}
			finally
			{
				DisposeObjects(ref icon, ref icmd);
			}
		}

		/// <summary>
		///     Executes a DataSet.
		/// </summary>
		/// <param name="edt">Database provider dataType</param>
		/// <param name="connectionString">Connection string</param>
		/// <param name="executableQuery">Query to be executed</param>
		/// <returns>DataSet</returns>
		[MethodImpl(MethodImplOptions.Synchronized)]
		public static DataTable ExecuteDataTable(DatabaseServer edt, string connectionString, ExecutionQuery executableQuery)
		{
			DbConnection icon = null;
			DbCommand icmd = null;
			DataTable dset = null;
			DbDataAdapter idap = null;

			try
			{
				DataFactory.InitializeDbConnection(edt, ref icon);
				DataFactory.InitializeDbCommand(edt, ref icmd);
				DataFactory.InitializeDbDataAdapter(edt, ref idap, icmd);

				icmd.Connection = icon;
				dset = new DataTable();


				icon.ConnectionString = connectionString;

				icmd.Parameters.Clear();

				if (executableQuery.Parameters != null)
				{
					foreach (IDataParameter var in executableQuery.Parameters)
					{
						icmd.Parameters.Add(var);
					}
				}

				icmd.CommandText = executableQuery.Query;
				idap.SelectCommand = icmd;

				//log the queries
				(new ExecutionEngineLogger()).LogContext(executableQuery);

				icon.Open();

				idap.Fill(dset);

				return dset;

			}
			finally
			{
				DisposeObjects(ref icon, ref icmd);
			}
		}

		/// <summary>
		///     Executes the query and returns the value of the primary key
		/// </summary>
		/// <param name="database">Database server type</param>
		/// <param name="connection">IDbConnection object</param>
		/// <param name="command">IDbCommand object</param>
		/// <param name="executableQuery">Query to be executed</param>
		/// <param name="primaryKeyFieldName">Name of the primary key field</param>
		/// <param name="tableName">Name of the table</param>
		/// <param name="insertedPrimaryKeyValue">Value of the inserted primary key</param>
		/// <returns>Returns the number of the affected rows</returns>
		[MethodImpl(MethodImplOptions.Synchronized)]
		internal static int ExecuteNonQueryWithPrimaryKey(DatabaseServer database, ref DbConnection connection, ref DbCommand command, ExecutionQuery executableQuery,
														  string primaryKeyFieldName, string tableName, ref object insertedPrimaryKeyValue)
		{
			command.CommandType = CommandType.Text;

			int affectedRows = 0;


			//log data
			(new ExecutionEngineLogger()).LogContext(executableQuery);

			command.Parameters.Clear();

			if (executableQuery.Parameters != null)
			{
				foreach (IDataParameter var in executableQuery.Parameters)
				{
					command.Parameters.Add(var);
				}
			}

			command.CommandText = executableQuery.Query;

			affectedRows = command.ExecuteNonQuery();

			command.Parameters.Clear();

			if (database == DatabaseServer.SqlServer)
			{
				//use IDENT_CURRENT
				command.CommandText = "SELECT IDENT_CURRENT('" + tableName + "')";
				insertedPrimaryKeyValue = command.ExecuteScalar();
			}
			else if (database == DatabaseServer.MySQL)
			{
				command.CommandText = "select LAST_INSERT_ID()";
				insertedPrimaryKeyValue = command.ExecuteScalar();
			}
			else if (database == DatabaseServer.Access)
			{
				command.CommandText = "SELECT @@IDENTITY";
				insertedPrimaryKeyValue = command.ExecuteScalar();
			}
			else
			{
				//temporary....use MAX()
				command.CommandText = "SELECT MAX('" + primaryKeyFieldName + "') FROM " + tableName;
				insertedPrimaryKeyValue = command.ExecuteScalar();
			}


			return affectedRows;
		}

		/// <summary>
		///     Determines whether the specified queries has constraints.
		/// </summary>
		/// <param name="queries">The queries.</param>
		/// <returns>
		///     <c>true</c> if the specified queries has constraints; otherwise, <c>false</c>.
		/// </returns>
		[MethodImpl(MethodImplOptions.Synchronized)]
		public static bool HasExecutionConstraints(List<ExecutionQuery> queries)
		{
			foreach (ExecutionQuery executionQuery in queries)
			{
				if ((executionQuery.Query.StartsWith(FOREIGN_KEY_CONSTRAINT) || (executionQuery.Query.StartsWith(PRIMARY_KEY_CONSTRAINT))))
				{
					return true;
				}
			}
			return false;
		}

		/// <summary>
		///     Replaces the substitute value with the value of the primary key.
		/// </summary>
		/// <param name="query"></param>
		/// <param name="primaryKeyValue"></param>
		[MethodImpl(MethodImplOptions.Synchronized)]
		private static void ReplaceForeignKeyValue(ref string query, string primaryKeyValue)
		{
			//get the index of the place holder value
			int index = query.IndexOf(SqlGenerator.FOREIGN_KEY_PLACEHOLDER_VALUE);

			//remove the place holder values.
			query = query.Remove(index, SqlGenerator.FOREIGN_KEY_PLACEHOLDER_VALUE.Length);

			//insert the primary key value
			query = query.Insert(index, primaryKeyValue);
		}
	}
}