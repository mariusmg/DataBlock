/*

	   file: DataFactory.cs
description: Data Factory for DatabaseServer. Contains convertors and factories.
		     Supports plugin based factory based on ADO.NET providers (the providers are loaded at runtime).
  
 
  
   (c) 2004 - 2008 Marius Gheorghe. All rights reserved.

*/

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.Odbc;
using System.Data.OleDb;
using System.Data.OracleClient;
using System.Data.SqlClient;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace voidsoft.DataBlock
{
	/// <summary>
	///     This is a factory class for provider specific objects.
	///     Most of the object are initialized based on the DatabaseServer enumeration.
	/// </summary>
	public sealed class DataFactory
	{
		/// <summary>
		///     Flag which describes the ODBC driver
		/// </summary>
		internal const string ODBC_DRIVER = "odbc";

		private static object lockedDbDataAdapterInitialization = new object();
		private static object lockedQueryCriteriaInitialization = new object();
		private static object lockedDataTypeInitialization = new object();
		private static object lockedProviderLoader = new object();

		//default prefix parameter chars for the suported database servers.    
		internal static char sqlServerParameterChar = '@';
		internal static char mySqlParameterChar = '?';
		internal static char accessParameterChar = '@';
		internal static char postgreSqlParameterChar = '@';
		internal static char oracleSqlParameterChar = '@';

		/// <summary>
		///     Dictionary which hold the providers
		/// </summary>
		private static Dictionary<string, string> loadedProviders;

		/// <summary>
		///     Static ctor
		/// </summary>
		static DataFactory()
		{
			loadedProviders = new Dictionary<string, string>();

			//load the custom providers the first time this class is called
			LoadCustomProviders();
		}

		/// <summary>
		///     Loads the custom providers information from the config file
		/// </summary>
		internal static void LoadCustomProviders()
		{
			try
			{
				lock (lockedProviderLoader)
				{
					AppSettingsReader reader = new AppSettingsReader();

					//mysql support
					try
					{
						string mySqlProviderAssembly = (string) reader.GetValue("ProviderMySql", typeof (string));
						char mySqlCommandParameterChar = (char) reader.GetValue("ProviderMySqlParameterChar", typeof (char));
						loadedProviders.Add("mysql", mySqlProviderAssembly);

						mySqlParameterChar = mySqlCommandParameterChar;
					}
					catch
					{
						//no provider data. Ignore exception
					}

					//postgresql support
					try
					{
						string postgreSqlProviderAssembly = (string) reader.GetValue("ProviderPostgreSql", typeof (string));
						char postgreSqlCommandParameterChar = (char) reader.GetValue("ProviderPostgreSqlParameterChar", typeof (char));
						loadedProviders.Add("postgresql", postgreSqlProviderAssembly);

						postgreSqlParameterChar = postgreSqlCommandParameterChar;
					}
					catch
					{
						//no provider data. Ignore exception
					}

					//access support
					try
					{
						string accessProviderAssembly = (string) reader.GetValue("ProviderAccess", typeof (string));
						char accessCommandParameterChar = (char) reader.GetValue("ProviderAccessParameterChar", typeof (char));

						loadedProviders.Add("access", accessProviderAssembly);
						accessParameterChar = accessCommandParameterChar;
					}
					catch
					{
						//no provider data. Ignore exception
					}

					//sqlserver support
					try
					{
						string sqlServerProviderAssembly = (string) reader.GetValue("ProviderSqlServer", typeof (string));
						char sqlServerCommandParameterChar = (char) reader.GetValue("ProviderSqlServerParameterChar", typeof (char));

						loadedProviders.Add("sqlserver", sqlServerProviderAssembly);
						sqlServerParameterChar = sqlServerCommandParameterChar;
					}
					catch
					{
						//no provider data. Ignore exception
					}
				}
			}
			catch (Exception ex)
			{
				Log.LogMessage(ex);

				throw;
			}
		}

		/// <summary>
		///     Initializes the ODBC data parameter
		/// </summary>
		/// <param name="iparam">IDataParameter to be initialized</param>
		private static void InitializeODBCDataParameter(ref IDataParameter iparam)
		{
			iparam = new OdbcParameter();
		}

		/// <summary>
		///     Initializes the ODBC data adapter
		/// </summary>
		/// <param name="iadapter">IDbDataAdapter to be initialized</param>
		/// <param name="icommand">Command to be initialized</param>
		private static void InitializeODBCDataAdapter(ref IDbDataAdapter iadapter, IDbCommand icommand)
		{
			iadapter = new OdbcDataAdapter((OdbcCommand) icommand);
		}

		/// <summary>
		///     Initializes the ODBC db data adapter
		/// </summary>
		/// <param name="adapter">DbDataAdapter to be initialized</param>
		/// <param name="command">DbCommand to be initialized</param>
		private static void InitializeODBCDbDataAdapter(ref DbDataAdapter adapter, DbCommand command)
		{
			adapter = new OdbcDataAdapter((OdbcCommand) command);
		}

		/// <summary>
		///     Initializes the ODBC db command
		/// </summary>
		/// <param name="icommand">DBCommand  to be initialized</param>
		private static void InitializeODBCDbCommand(ref DbCommand icommand)
		{
			icommand = new OdbcCommand();
		}

		/// <summary>
		///     Initializes the ODBC command
		/// </summary>
		/// <param name="icommand">IDbcommand to be initialized</param>
		private static void InitializeODBCCommand(ref IDbCommand icommand)
		{
			icommand = new OdbcCommand();
		}

		/// <summary>
		///     Initializes the ODBC connection
		/// </summary>
		/// <param name="iconnection">IDbConnection to be initialized</param>
		private static void InitializeODBCConnection(ref IDbConnection iconnection)
		{
			iconnection = new OdbcConnection();
		}

		/// <summary>
		///     Initializes the ODBC DBConnection
		/// </summary>
		/// <param name="iconnection">DbConnection to be initialized</param>
		private static void InitializeODBCDbConnection(ref DbConnection iconnection)
		{
			iconnection = new OdbcConnection();
		}

		/// <summary>
		///     Initializes a DataParameter.
		/// </summary>
		/// <param name="database">Database servert type</param>
		/// <param name="iparam">DataParameter which will be initialized</param>
		[MethodImpl(MethodImplOptions.Synchronized)]
		public static void InitializeDataParameter(DatabaseServer database, ref IDataParameter iparam)
		{
			//check for new provider in the custom providers list.
			if (loadedProviders.ContainsKey(database.ToString().ToLower()))
			{
				string fileName = string.Empty;
				loadedProviders.TryGetValue(database.ToString().ToLower(), out fileName);

				if (fileName == ODBC_DRIVER)
				{
					InitializeODBCDataParameter(ref iparam);
				}
				else
				{
					InitializeDataParameter(database, fileName, ref iparam);
				}
			}
			else
			{
				//initialize the IDbParameter using the normal provider.
				switch (database)
				{
					case DatabaseServer.Access:
						iparam = new OleDbParameter();
						break;

					case DatabaseServer.SqlServer:
						iparam = new SqlParameter();
						break;

					case DatabaseServer.Oracle:
						iparam = new OracleParameter();
						break;
				}
			}
		}

		/// <summary>
		///     Initializes a DataAdapter.
		/// </summary>
		/// <param name="database">Database server type</param>
		/// <param name="idap">The IDbDataAdapter interface which will be initialized.</param>
		/// <param name="icmd">Data Command associated with this Data Adapter</param>
		public static void InitializeDataAdapter(DatabaseServer database, ref IDbDataAdapter idap, IDbCommand icmd)
		{
			//check for new provider
			if (loadedProviders.ContainsKey(database.ToString().ToLower()))
			{
				string fileName = string.Empty;
				loadedProviders.TryGetValue(database.ToString().ToLower(), out fileName);

				if (fileName == ODBC_DRIVER)
				{
					InitializeODBCDataAdapter(ref idap, icmd);
				}
				else
				{
					InitializeDataAdapter(database, fileName, ref idap, icmd);
				}
			}
			else
			{
				switch (database)
				{
					case DatabaseServer.Access:
						idap = new OleDbDataAdapter((OleDbCommand) icmd);
						break;

					case DatabaseServer.SqlServer:
						idap = new SqlDataAdapter((SqlCommand) icmd);
						break;

					case DatabaseServer.Oracle:
						idap = new OracleDataAdapter((OracleCommand) icmd);
						break;
				}
			}
		}

		/// <summary>
		///     Initializes the DbDataAdapter
		/// </summary>
		/// <param name="database">The database.</param>
		/// <param name="adapter">The adapter.</param>
		/// <param name="command">The command.</param>
		[MethodImpl(MethodImplOptions.Synchronized)]
		public static void InitializeDbDataAdapter(DatabaseServer database, ref DbDataAdapter adapter, DbCommand command)
		{
			//check for new provider
			if (loadedProviders.ContainsKey(database.ToString().ToLower()))
			{
				string fileName = string.Empty;
				loadedProviders.TryGetValue(database.ToString().ToLower(), out fileName);

				if (fileName == ODBC_DRIVER)
				{
					InitializeODBCDbDataAdapter(ref adapter, command);
				}
				else
				{
					InitializeDbDataAdapter(database, fileName, ref adapter, command);
				}
			}
			else
			{
				switch (database)
				{
					case DatabaseServer.Access:
						adapter = new OleDbDataAdapter((OleDbCommand) command);
						break;

					case DatabaseServer.SqlServer:
						adapter = new SqlDataAdapter((SqlCommand) command);
						break;

					case DatabaseServer.Oracle:
						adapter = new OracleDataAdapter((OracleCommand) command);
						break;
				}
			}
		}

		/// <summary>
		///     Initializes a Data Command.
		/// </summary>
		/// <param name="database">Database server type</param>
		/// <param name="icmd">IDBcommand which will be initialized</param>
		[MethodImpl(MethodImplOptions.Synchronized)]
		public static void InitializeCommand(DatabaseServer database, ref IDbCommand icmd)
		{
			//check for new provider
			if (loadedProviders.ContainsKey(database.ToString().ToLower()))
			{
				string fileName = string.Empty;
				loadedProviders.TryGetValue(database.ToString().ToLower(), out fileName);

				if (fileName == ODBC_DRIVER)
				{
					InitializeODBCCommand(ref icmd);
				}
				else
				{
					InitializeCommand(database, fileName, ref icmd);
				}
			}
			else
			{
				switch (database)
				{
					case DatabaseServer.Access:
						icmd = new OleDbCommand();
						icmd.CommandTimeout = Configuration.CommandTimeout;
						break;

					case DatabaseServer.SqlServer:
						icmd = new SqlCommand();
						icmd.CommandTimeout = Configuration.CommandTimeout;
						break;

					case DatabaseServer.Oracle:
						icmd = new OracleCommand();
						icmd.CommandTimeout = Configuration.CommandTimeout;
						break;
				}
			}
		}

		/// <summary>
		///     Initializes a DbCommand
		/// </summary>
		/// <param name="database">The database.</param>
		/// <param name="icmd">The icmd.</param>
		[MethodImpl(MethodImplOptions.Synchronized)]
		public static void InitializeDbCommand(DatabaseServer database, ref DbCommand icmd)
		{
			//check for new provider
			if (loadedProviders.ContainsKey(database.ToString().ToLower()))
			{
				string fileName = string.Empty;
				loadedProviders.TryGetValue(database.ToString().ToLower(), out fileName);

				if (fileName == ODBC_DRIVER)
				{
					InitializeODBCDbCommand(ref icmd);
				}
				else
				{
					InitializeDbCommand(database, fileName, ref icmd);
				}
			}
			else
			{
				switch (database)
				{
					case DatabaseServer.Access:
						icmd = new OleDbCommand();
						icmd.CommandTimeout = Configuration.CommandTimeout;
						break;

					case DatabaseServer.SqlServer:
						icmd = new SqlCommand();
						icmd.CommandTimeout = Configuration.CommandTimeout;
						break;

					case DatabaseServer.Oracle:
						icmd = new OracleCommand();
						icmd.CommandTimeout = Configuration.CommandTimeout;
						break;
				}
			}
		}

		/// <summary>
		///     Initializes a Database Connection.
		/// </summary>
		/// <param name="database">Database server dataType</param>
		/// <param name="icon">IDbConnection interface</param>
		[MethodImpl(MethodImplOptions.Synchronized)]
		public static void InitializeConnection(DatabaseServer database, ref DbConnection icon)
		{
			//check for new provider
			if (loadedProviders.ContainsKey(database.ToString().ToLower()))
			{
				string fileName = string.Empty;
				loadedProviders.TryGetValue(database.ToString().ToLower(), out fileName);

				if (fileName == ODBC_DRIVER)
				{
					InitializeODBCDbConnection(ref icon);
				}
				else
				{
					InitializeDbConnection(database, fileName, ref icon);
				}
			}
			else
			{
				switch (database)
				{
					case DatabaseServer.Access:
						icon = new OleDbConnection();
						break;

					case DatabaseServer.SqlServer:
						icon = new SqlConnection();
						break;

					case DatabaseServer.Oracle:
						icon = new OracleConnection();
						break;
				}
			}
		}

		/// <summary>
		///     Initializes the db connection.
		/// </summary>
		/// <param name="database">Database server type</param>
		/// <param name="icon">DbConnection to be initialized</param>
		[MethodImpl(MethodImplOptions.Synchronized)]
		public static void InitializeDbConnection(DatabaseServer database, ref DbConnection icon)
		{
			//check for new provider
			if (loadedProviders.ContainsKey(database.ToString().ToLower()))
			{
				string fileName = string.Empty;
				loadedProviders.TryGetValue(database.ToString().ToLower(), out fileName);

				if (fileName == ODBC_DRIVER)
				{
					InitializeODBCDbConnection(ref icon);
				}
				else
				{
					InitializeDbConnection(database, fileName, ref icon);
				}
			}
			else
			{
				switch (database)
				{
					case DatabaseServer.Access:
						icon = new OleDbConnection();
						break;

					case DatabaseServer.SqlServer:
						icon = new SqlConnection();
						break;

					case DatabaseServer.Oracle:
						icon = new OracleConnection();
						break;
				}
			}
		}

		/// <summary>
		///     Returns the char used as a prefix for parameters.
		/// </summary>
		/// <param name="database"></param>
		/// <returns>The char used for sql parameters</returns>
		[MethodImpl(MethodImplOptions.Synchronized)]
		public static char GetParameterChar(DatabaseServer database)
		{
			switch (database)
			{
				case DatabaseServer.Access:
					return accessParameterChar;

				case DatabaseServer.SqlServer:
					return sqlServerParameterChar;

				case DatabaseServer.MySQL:
					return mySqlParameterChar;

				case DatabaseServer.PostgreSql:
					return postgreSqlParameterChar;

				case DatabaseServer.Oracle:
					return oracleSqlParameterChar;

				default:
					throw new ArgumentException("Invalid database type");
			}
		}

		/// <summary>
		///     Initializes a IDataParameter
		/// </summary>
		/// <param name="database">Database server type</param>
		/// <param name="filePath">Path to the assembly which contains the provider </param>
		/// <param name="idp">IDataParameter which will be initialized</param>
		private static void InitializeDataParameter(DatabaseServer database, string filePath, ref IDataParameter idp)
		{
			Assembly asm = Assembly.LoadFrom(filePath);

			Type[] tp = asm.GetTypes();

			for (int i = 0; i < tp.Length; i++)
			{
				Type t = tp[i].GetInterface("IDataParameter");

				if (t != null)
				{
					idp = (IDataParameter) Activator.CreateInstance(tp[i]);
					break;
				}
			}
		}

		/// <summary>
		///     Initializes a IDdbConnection
		/// </summary>
		/// <param name="database">Database server type</param>
		/// <param name="filePath">Path to the assembly which contains the provider</param>
		/// <param name="icon">IDbConnection which will be initialized</param>
		private static void InitializeConnection(DatabaseServer database, string filePath, ref IDbConnection icon)
		{
			Assembly asm = Assembly.LoadFrom(filePath);

			Type[] tp = asm.GetTypes();

			for (int i = 0; i < tp.Length; i++)
			{
				Type t = tp[i].GetInterface("IDbConnection");

				if (t != null)
				{
					icon = (IDbConnection) Activator.CreateInstance(tp[i]);
					break;
				}
			}
		}

		/// <summary>
		///     Initializes the connection.
		/// </summary>
		/// <param name="database">The database.</param>
		/// <param name="filePath">The file path.</param>
		/// <param name="icon">The icon.</param>
		private static void InitializeDbConnection(DatabaseServer database, string filePath, ref DbConnection icon)
		{
			Assembly asm = Assembly.LoadFrom(filePath);

			Type[] tp = asm.GetTypes();

			for (int i = 0; i < tp.Length; i++)
			{
				bool isSubclass = tp[i].IsSubclassOf(typeof (DbConnection));

				if (isSubclass)
				{
					icon = (DbConnection) Activator.CreateInstance(tp[i]);
					break;
				}
			}
		}

		/// <summary>
		///     Initializes a IDbCommand
		/// </summary>
		/// <param name="database">Database server type</param>
		/// <param name="filePath">Path to the assembly which contains the provider</param>
		/// <param name="icmd">IDbCommand which will be initialized</param>
		private static void InitializeCommand(DatabaseServer database, string filePath, ref IDbCommand icmd)
		{
			Assembly asm = Assembly.LoadFrom(filePath);

			Type[] tp = asm.GetTypes();

			for (int i = 0; i < tp.Length; i++)
			{
				Type t = tp[i].GetInterface("IDbCommand");

				if (t != null)
				{
					icmd = (IDbCommand) Activator.CreateInstance(tp[i]);

					//HACK : the MySql official provider does not supports timeout.
					try
					{
						icmd.CommandTimeout = Configuration.CommandTimeout;
					}
					catch
					{
						//ignore timeout exception
					}

					break;
				}
			}
		}

		/// <summary>
		///     Initializes the db data command.
		/// </summary>
		/// <param name="database">The database.</param>
		/// <param name="filePath">The file path.</param>
		/// <param name="command">The command.</param>
		private static void InitializeDbCommand(DatabaseServer database, string filePath, ref DbCommand command)
		{
			Assembly asm = Assembly.LoadFrom(filePath);

			Type[] tp = asm.GetTypes();

			for (int i = 0; i < tp.Length; i++)
			{
				bool isSubclass = tp[i].IsSubclassOf(typeof (DbCommand));

				if (isSubclass)
				{
					command = (DbCommand) Activator.CreateInstance(tp[i]);

					//HACK : the MySql official provider does not supports timeout.
					try
					{
						command.CommandTimeout = Configuration.CommandTimeout;
					}
					catch
					{
						//ignore timeout exception
					}

					break;
				}
			}
		}

		/// <summary>
		///     Initializes a new IDbDataAdapter
		/// </summary>
		/// <param name="database">Database server type</param>
		/// <param name="filePath">Path to the assembly which contains the provider</param>
		/// <param name="idap">IDBDataAdapter which will be initialized</param>
		/// <param name="icmd">IDbCommand associated with IDbDataAdapter </param>
		private static void InitializeDataAdapter(DatabaseServer database, string filePath, ref IDbDataAdapter idap, IDbCommand icmd)
		{
			Assembly asm = Assembly.LoadFrom(filePath);

			Type[] tp = asm.GetTypes();

			for (int i = 0; i < tp.Length; i++)
			{
				Type t = tp[i].GetInterface("IDbDataAdapter");

				if (t != null)
				{
					idap = (IDbDataAdapter) Activator.CreateInstance(tp[i]);
					break;
				}
			}
		}

		/// <summary>
		///     Initializes the db data adapter.
		/// </summary>
		/// <param name="server">The server.</param>
		/// <param name="filePath">The file path.</param>
		/// <param name="adapter">The adapter.</param>
		/// <param name="command">The command.</param>
		private static void InitializeDbDataAdapter(DatabaseServer server, string filePath, ref DbDataAdapter adapter, DbCommand command)
		{
			Assembly asm = Assembly.LoadFrom(filePath);

			Type[] tp = asm.GetTypes();

			for (int i = 0; i < tp.Length; i++)
			{
				bool isInherited = tp[i].IsSubclassOf(typeof (DbDataAdapter));

				if (isInherited)
				{
					adapter = (DbDataAdapter) Activator.CreateInstance(tp[i]);
					break;
				}
			}
		}

		/// <summary>
		///     Converts a DbType into a Type.
		/// </summary>
		/// <param name="type"></param>
		/// <returns>Returns the new type</returns>
		internal static Type InitializeDataType(DbType type)
		{
			try
			{
				lock (lockedDbDataAdapterInitialization)
				{
					switch (type)
					{
						case DbType.AnsiString:
						case DbType.AnsiStringFixedLength:
						case DbType.String:
						case DbType.StringFixedLength:
							return typeof (String);

						case DbType.Binary:
							return typeof (Byte[]);

						case DbType.Boolean:
							return typeof (Boolean);

						case DbType.Byte:
							return typeof (Byte);

						case DbType.Currency:
							return typeof (Decimal);

						case DbType.Date:
							return typeof (DateTime);

						case DbType.DateTime:
							return typeof (DateTime);

						case DbType.Decimal:
							return typeof (Decimal);

						case DbType.Double:
							return typeof (Double);

						case DbType.Guid:
							return typeof (Guid);

						case DbType.Int16:
							return typeof (Int16);

						case DbType.Int32:
							return typeof (Int32);

						case DbType.Int64:
							return typeof (Int64);

						case DbType.Object:
							return typeof (Object);

						case DbType.SByte:
							return typeof (SByte);

						case DbType.Single:
							return typeof (Single);

						case DbType.Time:
							return typeof (DateTime);

						case DbType.UInt16:
							return typeof (UInt16);

						case DbType.UInt32:
							return typeof (UInt32);

						case DbType.UInt64:
							return typeof (UInt64);

						case DbType.VarNumeric:
							return typeof (Decimal);

							//                    case DbType.Xml:
							//                        return typeof(System.String);
						default:
							return null;
					}
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		/// <summary>
		///     Converts a type into a DbType
		/// </summary>
		/// <param name="tip"></param>
		/// <returns>DbType value</returns>
		internal static DbType InitializeDataType(Type tip)
		{
			lock (lockedDataTypeInitialization)
			{
				if (tip.GetType() == typeof (Int32))
				{
					return DbType.Int32;
				}
				else if (tip.GetType() == typeof (Int16))
				{
					return DbType.Int16;
				}
				else if (tip.GetType() == typeof (String))
				{
					return DbType.String;
				}
				else if (tip.GetType() == typeof (Decimal))
				{
					return DbType.Decimal;
				}
				else if (tip.GetType() == typeof (Double))
				{
					return DbType.Double;
				}
				else if (tip.GetType() == typeof (Single))
				{
					return DbType.Single;
				}
				else if (tip.GetType() == typeof (UInt16))
				{
					return DbType.UInt16;
				}
				else if (tip.GetType() == typeof (UInt32))
				{
					return DbType.UInt32;
				}
				else if (tip.GetType() == typeof (UInt64))
				{
					return DbType.UInt64;
				}
				else if (tip.GetType() == typeof (DateTime))
				{
					return DbType.DateTime;
				}
				else if (tip.GetType() == typeof (Boolean))
				{
					return DbType.Boolean;
				}
				else if (tip.GetType() == typeof (Byte))
				{
					return DbType.Byte;
				}
				else
				{
					return DbType.Object;
				}
			}
		}

		/// <summary>
		///     Creates a new IQueryCriteriaGenerator
		/// </summary>
		/// <param name="database">Database server type</param>
		/// <returns>Returns a new IQueryCriteriaGenerator</returns>
		public static IQueryCriteriaGenerator InitializeQueryCriteriaGenerator(DatabaseServer database)
		{
			IQueryCriteriaGenerator iql = null;

			lock (lockedQueryCriteriaInitialization)
			{
				switch (database)
				{
					case DatabaseServer.Access:
						iql = new AccessQueryCriteriaGenerator();
						break;

					case DatabaseServer.MySQL:
						iql = new MySqlQueryCriteriaGenerator();
						break;

					case DatabaseServer.SqlServer:
						iql = new SqlServerQueryCriteriaGenerator();
						break;

					case DatabaseServer.PostgreSql:
						iql = new PostgreSqlQueryCriteriaGenerator();
						break;

					case DatabaseServer.Oracle:
						iql = new OracleQueryCriteriaGenerator();
						break;

					default:
						throw new ArgumentException("Invalid database type");
				}

				return iql;
			}
		}

		/// <summary>
		///     Initializes a ISqlGenerator based on the DatabaseServer.
		/// </summary>
		/// <param name="database">Database server type</param>
		/// <returns></returns>
		[MethodImpl(MethodImplOptions.Synchronized)]
		internal static ISqlGenerator InitializeSqlGenerator(DatabaseServer database)
		{
			ISqlGenerator isql = null;

			switch (database)
			{
				case DatabaseServer.Access:
					isql = new AccessGenerator();
					break;

				case DatabaseServer.MySQL:
					isql = new MySqlGenerator();
					break;

				case DatabaseServer.SqlServer:
					isql = new SqlServerGenerator();
					break;

				case DatabaseServer.PostgreSql:
					isql = new PostgreSqlGenerator();
					break;

				case DatabaseServer.Oracle:
					isql = new OracleGenerator();
					break;

				default:
					throw new ArgumentException("Invalid database type");
			}

			return isql;
		}
	}
}