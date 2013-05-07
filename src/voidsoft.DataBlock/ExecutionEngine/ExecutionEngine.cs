/*

	   file: ExecutionEngine.cs
description: DataBlock's DAL. This contains operation with the database.
             The ExecutionEngine operates in 2 modes:
            
             - disconnected mode : in this mode an operation with the database opens a 
                                   connection and then it closes it.  This mode is accessed using
                                   the static methods of the class.
        
             - connected mode : is accessed by creating an instanced of the class. In this mode 
                                a connection is opened and is kept "alive" until the instance
                                is disposed. This is the mode in which a "Session" operates.        
	
  
  (c) 2004 - 2007 Marius Gheorghe. All rights reserved. 
*/

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;

namespace voidsoft.DataBlock
{
	[Serializable]
	public partial class ExecutionEngine : IDisposable
	{
		//private consts
		private const int SLASHES_LENGTH = 2;
		private const int LINE_LENGTH = 1;

		///     Database command.
		private DbCommand command;

		///     Database connection.
		private DbConnection connection;

		///     Connection string used to connect to the database.
		private string connectionString;

		///     Database server dataType.
		private DatabaseServer database;

		///     Flag used to know if the current instance is initialized using a Session.
		private bool isContextSession;

		

		public ExecutionEngine()
		{
			this.database = Configuration.DatabaseServerType;
			this.connectionString = Configuration.ConnectionString;

			DataFactory factory = new DataFactory();

			factory.InitializeDbConnection(Configuration.DatabaseServerType, ref connection);
			factory.InitializeDbCommand(Configuration.DatabaseServerType, ref command);
			this.command.Connection = connection;
			this.connection.ConnectionString = connectionString;
			this.connection.Open();
		}


		public ExecutionEngine (DatabaseServer database, string connectionString)
		{
			this.database = database;
			this.connectionString = connectionString;

			DataFactory factory = new DataFactory();

			factory.InitializeDbConnection(database, ref connection);
			factory.InitializeDbCommand(database, ref command);
			this.command.Connection = this.connection;
			this.connection.ConnectionString = this.connectionString;
			this.connection.Open();
		}

		public ExecutionEngine (Session session)
		{
			
			this.isContextSession = true;
			this.connection = session.DatabaseConnection;
			this.database = session.Database;
			this.connectionString = session.ConnectionString;
			(new DataFactory()).InitializeDbCommand(database, ref command);
			this.command.Connection = session.DatabaseConnection;
		}


		///// <summary>
		/////     Creates the new execution engine.
		///// </summary>
		///// <returns></returns>
		//public static ExecutionEngine CreateNewExecutionEngine()
		//{
		//	ExecutionEngine dal = new ExecutionEngine();

		//	dal.database = Configuration.DatabaseServerType;
		//	dal.connectionString = Configuration.ConnectionString;
			
		//	DataFactory factory = new DataFactory();

		//	factory.InitializeDbConnection(Configuration.DatabaseServerType, ref dal.connection);
		//	factory.InitializeDbCommand(Configuration.DatabaseServerType, ref dal.command);
		//	dal.command.Connection = dal.connection;
		//	dal.connection.ConnectionString = dal.connectionString;
		//	dal.connection.Open();

		//	return dal;
		//}

		///// <summary>
		/////     Creates a new instance of the ExecutionEngine.
		///// </summary>
		///// <param name="database">Database server</param>
		///// <param name="connectionString"></param>
		///// <returns>ExecutionEngine instance</returns>
		//public static ExecutionEngine CreateNewExecutionEngine(DatabaseServer database, string connectionString)
		//{
		//	ExecutionEngine dal = new ExecutionEngine();

		//	dal.database = database;
		//	dal.connectionString = connectionString;

		//	DataFactory factory = new DataFactory();

		//	factory.InitializeDbConnection(database, ref dal.connection);
		//	factory.InitializeDbCommand(database, ref dal.command);
		//	dal.command.Connection = dal.connection;
		//	dal.connection.ConnectionString = dal.connectionString;
		//	dal.connection.Open();

		//	return dal;
		//}

		///// <summary>
		/////     Creates a new instance of the ExecutionEngine.
		///// </summary>
		///// <param name="session">The session based on which the instance is created</param>
		///// <returns>ExecutionEngine instance</returns>
		//public static ExecutionEngine CreateNewExecutionEngine(Session session)
		//{
		//	ExecutionEngine dal = new ExecutionEngine();
		//	dal.isContextSession = true;
		//	dal.connection = session.DatabaseConnection;
		//	dal.database = session.Database;
		//	dal.connectionString = session.ConnectionString;
		//	(new DataFactory()).InitializeDbCommand(dal.database, ref dal.command);
		//	dal.command.Connection = session.DatabaseConnection;

		//	return dal;
		//}

		/// <summary>
		///     Dispose the class instance
		/// </summary>
		public void Dispose()
		{
			if (!isContextSession)
			{
				if (connection.State != ConnectionState.Closed)
				{
					connection.Close();
				}
			}

			command.Dispose();
		}


		/// <summary>
		///     Executes a sql statement against the connection and returns the number
		///     of rows affected.
		/// </summary>
		/// <param name="executableQuery">Sql command text which will be executed</param>
		/// <returns>Number of affected rows</returns>
		public int ExecuteNonQuery(ExecutionQuery executableQuery)
		{
			command.Parameters.Clear();

			if (executableQuery.Parameters != null)
			{
				foreach (IDataParameter var in executableQuery.Parameters)
				{
					command.Parameters.Add(var);
				}
			}

			command.CommandType = CommandType.Text;
			command.CommandText = executableQuery.Query;

			//log the ExecutionQuery
			(new ExecutionEngineLogger()).LogContext(executableQuery);

			return command.ExecuteNonQuery();
		}

		/// <summary>
		///     Executes a sql statement against the connection and returns the number of rows affected.
		/// </summary>
		/// <param name="storedProcedureName">The name of the stores procedure which will be executed</param>
		/// <param name="iparams">parameters associated with the stored procedure</param>
		/// <returns>Number of affected row</returns>
		public int ExecuteNonQuery(string storedProcedureName, params IDataParameter[] iparams)
		{
			command.CommandType = CommandType.StoredProcedure;
			command.CommandText = storedProcedureName;
			command.Parameters.Clear();

			if (iparams != null)
			{
				for (int i = 0; i < iparams.Length; i++)
				{
					command.Parameters.Add(iparams[i]);
				}
			}

			//log the stored procedure
			(new ExecutionEngineLogger()).LogContext(storedProcedureName, iparams);

			return (command.ExecuteNonQuery());
		}

		/// <summary>
		///     Execute all the queries into a transaction using the default configurable isolation level
		/// </summary>
		/// <param name="listQueries">List of queries to be executed</param>
		/// <returns>The number of affected rows</returns>
		public int ExecuteNonQuery(List<ExecutionQuery> listQueries)
		{
			return ExecuteNonQuery(listQueries, Configuration.DefaultTransactionIsolationLevel);
		}

		/// <summary>
		///     Execute the queries into a transaction using
		///     the specified IsolationLevel
		/// </summary>
		/// <param name="listQueries">List of queries which will be executed</param>
		/// <param name="isolationLevel">IsolationLevel for transaction</param>
		/// <returns>The number of affected rows</returns>
		public int ExecuteNonQuery(List<ExecutionQuery> listQueries, IsolationLevel isolationLevel)
		{
			DbTransaction itrans = null;
			int affectedRows = 0;

			try
			{
				itrans = connection.BeginTransaction(isolationLevel);
				command.Transaction = itrans;
				command.CommandType = CommandType.Text;

				affectedRows = ExecuteNonQueryConstrained(ref connection, ref command, listQueries);

				itrans.Commit();

				return affectedRows;
			}
			catch (Exception ex)
			{
				if (itrans != null)
				{
					itrans.Rollback();
				}

				(new ExecutionEngineLogger()).LogContext(ex.Message + ex.StackTrace);
				throw;
			}
			finally
			{
				if (itrans != null)
				{
					itrans.Dispose();
				}
			}
		}

		/// <summary>
		///     ExecuteNonQuery overload which returns the last inserted Id
		/// </summary>
		/// <param name="executableQuery">SELECT query to be executed</param>
		/// <param name="tableName">Name of the table</param>
		/// <param name="primaryKeyFieldName">Name of the primary key field</param>
		/// <param name="insertedPrimaryKeyValue">Reference to a object who's value will be updated with the value of the primary key</param>
		/// <returns>Number of affected rows</returns>
		public int ExecuteNonQuery(ExecutionQuery executableQuery, string tableName, string primaryKeyFieldName, ref object insertedPrimaryKeyValue)
		{
			//the implementation of the instance methos is using the implementation of the static method.
			return ExecuteNonQueryWithPrimaryKey(database, ref connection, ref command, executableQuery, primaryKeyFieldName, tableName, ref insertedPrimaryKeyValue);
		}

		/// <summary>
		///     Executes a DataReader
		/// </summary>
		/// <param name="queryCommand">ExecutionQuery </param>
		/// <returns>IDataReader instance</returns>
		public IDataReader ExecuteReader(ExecutionQuery queryCommand)
		{
			return ExecuteReader(queryCommand, CommandBehavior.Default);
		}

		/// <summary>
		///     Executes a DataReader
		/// </summary>
		/// <param name="executableQuery">ExecutionQuery</param>
		/// <param name="cmd">Command Behaviour for DataReader</param>
		/// <returns>IDataReader instance</returns>
		public IDataReader ExecuteReader(ExecutionQuery executableQuery, CommandBehavior cmd)
		{
			IDataReader iread = null;

			if (cmd == CommandBehavior.CloseConnection)
			{
				throw new ArgumentException("CloseConnection is not a valid command behaviour");
			}

			command.Parameters.Clear();

			if (executableQuery.Parameters != null)
			{
				foreach (IDataParameter var in executableQuery.Parameters)
				{
					command.Parameters.Add(var);
				}
			}

			command.CommandType = CommandType.Text;
			command.CommandText = executableQuery.Query;

			//log the execution query
			(new ExecutionEngineLogger()).LogContext(executableQuery);

			iread = command.ExecuteReader(cmd);
			return iread;
		}

		/// <summary>
		///     Executes a DataReader
		/// </summary>
		/// <param name="storedProcedureName">Stored procedure name</param>
		/// <param name="commandBehaviour">DataReader CommandBehaviour</param>
		/// <param name="iparam">List of parameters for stored procedure. In case you want to call a stored procedure without parameters you must pass null</param>
		/// <returns>IDataReader instance</returns>
		public IDataReader ExecuteReader(string storedProcedureName, CommandBehavior commandBehaviour, params IDataParameter[] iparam)
		{
			IDataReader iread = null;

			command.CommandType = CommandType.StoredProcedure;
			command.CommandText = storedProcedureName;
			command.Parameters.Clear();

			if (iparam != null)
			{
				for (int i = 0; i < iparam.Length; i++)
				{
					command.Parameters.Add(iparam[i]);
				}
			}

			//log the stored procedure
			(new ExecutionEngineLogger()).LogContext(storedProcedureName, iparam);

			return iread = command.ExecuteReader(commandBehaviour);
		}

		/// <summary>
		///     Fills a dataset
		/// </summary>
		/// <param name="executableQuery">SELECT ExecutionQuery based on which data is selected</param>
		/// <returns>Resulting DataSet</returns>
		public DataSet ExecuteDataSet(ExecutionQuery executableQuery)
		{
			DataSet dset = null;
			IDbDataAdapter idap = null;
			DataFactory factory = new DataFactory();

			factory.InitializeDataAdapter(database, ref idap, command);

			dset = new DataSet();

			command.CommandType = CommandType.Text;

			command.CommandText = executableQuery.Query;

			command.Parameters.Clear();

			if (executableQuery.Parameters != null)
			{
				foreach (IDataParameter var in executableQuery.Parameters)
				{
					command.Parameters.Add(var);
				}
			}

			(new ExecutionEngineLogger()).LogContext(executableQuery);

			idap.Fill(dset);

			return dset;
		}

		/// <summary>
		///     Fills a dataset
		/// </summary>
		/// <param name="executableQuery">SELECT ExecutionQuery based on which data is selected</param>
		/// <param name="ds">Resulting DataSet</param>
		public void ExecuteDataSet(ExecutionQuery executableQuery, ref DataSet ds)
		{
			IDbDataAdapter idap = null;

			DataFactory factory = new DataFactory();

			factory.InitializeDataAdapter(database, ref idap, command);
			command.CommandType = CommandType.Text;
			command.CommandText = executableQuery.Query;

			command.Parameters.Clear();

			if (executableQuery.Parameters != null)
			{
				foreach (IDataParameter var in executableQuery.Parameters)
				{
					command.Parameters.Add(var);
				}
			}

			(new ExecutionEngineLogger()).LogContext(executableQuery);

			idap.Fill(ds);
		}

		/// <summary>
		///     Fills a dataset
		/// </summary>
		/// <param name="storedProcedureName">The name of the stored procedure used to fills the dataset.</param>
		/// <param name="iparams">The stored procedure parameters</param>
		/// <returns>The dataset</returns>
		public DataSet ExecuteDataSet(string storedProcedureName, params IDataParameter[] iparams)
		{
			DataSet ds = null;
			IDbDataAdapter idap = null;

			(new DataFactory()).InitializeDataAdapter(database, ref idap, command);
			command.CommandType = CommandType.StoredProcedure;
			command.CommandText = storedProcedureName;
			command.Parameters.Clear();

			if (iparams != null)
			{
				for (int i = 0; i < iparams.Length; i++)
				{
					command.Parameters.Add(iparams[i]);
				}
			}

			ds = new DataSet();

			(new ExecutionEngineLogger()).LogContext(storedProcedureName);

			idap.Fill(ds);
			return ds;
		}

		/// <summary>
		///     Fills a DataTable
		/// </summary>
		/// <param name="executableQuery">SELECT ExecutionQuery based on which data is selected</param>
		/// <returns>Resulting DataTable</returns>
		public DataTable ExecuteDataTable(ExecutionQuery executableQuery)
		{
			DataTable table = null;
			DbDataAdapter idap = null;

			try
			{
				(new DataFactory()).InitializeDbDataAdapter(database, ref idap, command);
				table = new DataTable();
				command.CommandType = CommandType.Text;
				command.CommandText = executableQuery.Query;

				command.Parameters.Clear();

				if (executableQuery.Parameters != null)
				{
					foreach (IDataParameter var in executableQuery.Parameters)
					{
						command.Parameters.Add(var);
					}
				}

				//log data
				(new ExecutionEngineLogger()).LogContext(executableQuery);

				idap.Fill(table);

				return table;
			}
			finally
			{
				if (idap != null)
				{
					idap.Dispose();
				}
			}
		}

		/// <summary>
		///     Fills a DataTable
		/// </summary>
		/// <param name="storedProcedureName">The name of the stored procedure used to fills the DataTable</param>
		/// <param name="iparams">The stored procedure parameters</param>
		/// <returns>Resulting DataTable</returns>
		public DataTable ExecuteDataTable(string storedProcedureName, params IDataParameter[] iparams)
		{
			DataTable table = null;
			DbDataAdapter idap = null;

			(new DataFactory()).InitializeDbDataAdapter(database, ref idap, command);

			command.CommandType = CommandType.StoredProcedure;
			command.CommandText = storedProcedureName;
			command.Parameters.Clear();

			if (iparams != null)
			{
				for (int i = 0; i < iparams.Length; i++)
				{
					command.Parameters.Add(iparams[i]);
				}
			}

			table = new DataTable();

			//log the name
			(new ExecutionEngineLogger()).LogContext(storedProcedureName);

			idap.Fill(table);

			return table;
		}

		/// <summary>
		///     Executes the query, and returns the first column of the first row  in the resultset returned by the query.
		///     Extra columns or rows are  ignored.
		/// </summary>
		/// <param name="executableQuery">Query to be executed</param>
		/// <returns>Returns the selected object</returns>
		public object ExecuteScalar(ExecutionQuery executableQuery)
		{
			command.Parameters.Clear();

			if (executableQuery.Parameters != null)
			{
				foreach (IDataParameter var in executableQuery.Parameters)
				{
					command.Parameters.Add(var);
				}
			}

			command.CommandType = CommandType.Text;
			command.CommandText = executableQuery.Query;

			//log data
			(new ExecutionEngineLogger()).LogContext(executableQuery);

			return command.ExecuteScalar();
		}

		/// <summary>
		///     Executes the query, and returns the first column of the first row in the resultset returned by the query.
		///     Extra columns or rows are ignored.
		/// </summary>
		/// <param name="storedProcedureName">Name of the stored procedure</param>
		/// <param name="iparam">Array of IDataParameters</param>
		/// <returns>Returns the selected object</returns>
		public object ExecuteScalar(string storedProcedureName, params IDataParameter[] iparam)
		{
			command.CommandType = CommandType.StoredProcedure;
			command.CommandText = storedProcedureName;
			command.Parameters.Clear();

			if (iparam != null)
			{
				for (int i = 0; i < iparam.Length; i++)
				{
					command.Parameters.Add(iparam[i]);
				}
			}

			//log data
			(new ExecutionEngineLogger()).LogContext(storedProcedureName);

			return command.ExecuteScalar();
		}

		/// <summary>
		///     Disposes the ADO.NET objects
		/// </summary>
		/// <param name="con">Database connection to be disposed</param>
		/// <param name="cmd">Database command to be disposed</param>
		private void DisposeObjects(ref DbConnection con, ref DbCommand cmd)
		{
			if (con != null)
			{
				con.Close();
			}

			if (cmd != null)
			{
				cmd.Dispose();
			}
		}
	}
}