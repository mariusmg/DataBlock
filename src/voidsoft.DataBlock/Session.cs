/*
       file: Session.cs
description: The session allows to chain multiples business objects operations in a single transaction. To do that the business objects 
             (PersistentObject) must be created using a Session.
      notes:  all the  database operations in a Session are running on the same database connection. 

  (c) 2004 - 2008 Marius Gheorghe. All rights reserved.
*/

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;

namespace voidsoft.DataBlock
{
	/// <summary>
	///     The session represents a database connection which supports chaining  multiple business objects operations
	///     in transactions.
	/// </summary>
	[Serializable]
	public class Session : IDisposable
	{
		//the database server type
		//connection string used to connect to the database

		//connection string used to connect to the database
		private string connectionString;
		private DatabaseServer database;

		//ADO.NET Connection object which we'll connect to the database.    
		private DbConnection icon;

		//list of queries which we'll be executed.

		//flag to know if the session is in a transaction.
		private bool isInTransaction;

		//the transaction's isolation level.
		private IsolationLevel isolationLevel;
		private List<ExecutionQuery> listQueries;

		/// <summary>
		///     Constructor
		/// </summary>
		private Session()
		{
		}

		/// <summary>
		///     Database server type for which the current session
		///     has been initialized.
		/// </summary>
		/// <value></value>
		public DatabaseServer Database
		{
			get
			{
				return database;
			}
		}

		/// <summary>
		///     Connection string used to initialize this session
		/// </summary>
		/// <value></value>
		public string ConnectionString
		{
			get
			{
				return connectionString;
			}
		}

		/// <summary>
		///     The underlying datbase connection used by this session
		/// </summary>
		/// <value></value>
		public DbConnection DatabaseConnection
		{
			get
			{
				return icon;
			}
		}

		/// <summary>
		///     Returns true if the current session is in a transaction
		/// </summary>
		/// <value></value>
		public bool IsInTransaction
		{
			get
			{
				return isInTransaction;
			}
		}

		/// <summary>
		///     Returns the list of pending queries for the current transaction
		/// </summary>
		/// <value></value>
		public List<ExecutionQuery> Queries
		{
			get
			{
				return listQueries;
			}
		}

		/// <summary>
		///     Dispose the current session
		/// </summary>
		public void Dispose()
		{
			if (icon != null)
			{
				icon.Close();
			}
		}

		/// <summary>
		///     Close the current session.
		/// </summary>
		public void Close()
		{
			Dispose();
		}

		/// <summary>
		///     Creates a new session.
		/// </summary>
		/// <param name="database">Database server type</param>
		/// <param name="connectionString">Database connection string</param>
		/// <returns>Returns a new session</returns>
		public static Session CreateNewSession(DatabaseServer database, string connectionString)

		{
			lock (typeof (Session))
			{
				try
				{
					Session newSession = new Session();

					newSession.listQueries = new List<ExecutionQuery>();

					newSession.connectionString = connectionString;

					newSession.database = database;

					DataFactory.InitializeConnection(newSession.database, ref newSession.icon);

					newSession.icon.ConnectionString = connectionString;

					//open the connection.
					newSession.icon.Open();

					return newSession;
				}
				catch
				{
					throw;
				}
			}
		}

		/// <summary>
		///     Creates a new session which used the default Configuration data.
		/// </summary>
		/// <returns></returns>
		public static Session CreateNewSession()
		{
			lock (typeof (Session))
			{
				try
				{
					Session newSession = new Session();

					newSession.listQueries = new List<ExecutionQuery>();

					newSession.connectionString = Configuration.ConnectionString;

					newSession.database = Configuration.DatabaseServerType;

					DataFactory.InitializeConnection(newSession.database, ref newSession.icon);

					newSession.icon.ConnectionString = Configuration.ConnectionString;

					//open the connection
					newSession.icon.Open();

					return newSession;
				}
				catch
				{
					throw;
				}
			}
		}

		/// <summary>
		///     Begins a new transaction.
		/// </summary>
		/// <param name="isolationLevel">Transaction isolation level</param>
		public void BeginTransaction(IsolationLevel isolationLevel)
		{
			this.isolationLevel = isolationLevel;
			isInTransaction = true;
		}

		/// <summary>
		///     Begins a new transaction.
		/// </summary>
		public void BeginTransaction()
		{
			isolationLevel = Configuration.DefaultTransactionIsolationLevel;
			isInTransaction = true;
		}

		/// <summary>
		///     Rollback the current transaction
		/// </summary>
		public void Rollback()
		{
			isInTransaction = false;
		}

		/// <summary>
		///     Commits the current transaction.
		/// </summary>
		public void Commit()
		{
			DbConnection connection = null;
			DbCommand icmd = null;
			DbTransaction itrans = null;

			int affectedRows = 0;

			try
			{
				//check if the current session is in a transaction
				if (!IsInTransaction)
				{
					throw new InvalidOperationException("No transaction is active");
				}

				//initialize objects

				connection = DatabaseConnection;

				//initialize the ADO.NET objects and set the active transaction.
				DataFactory.InitializeDbCommand(Database, ref icmd);
				icmd.Connection = connection;
				itrans = connection.BeginTransaction(isolationLevel);
				icmd.Transaction = itrans;

				affectedRows = ExecutionEngine.ExecuteNonQueryConstrained(database, ref connection, ref icmd, listQueries);

				//commit the transaction
				itrans.Commit();
			}
			catch
			{
				itrans.Rollback();
				throw;
			}
			finally
			{
				listQueries.Clear();
				isInTransaction = false;
			}
		}
	}
}