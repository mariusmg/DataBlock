/*

       file: Configuration.cs
description: Configuration details for DataBlock.
             
            The following configurations are used:
 
             - database connection.
  
             - database server type
                    The supported values are:
                    1. sqlserver
                    2. access
                    3. mysql                   
                    4. postgresql

         - default transaction isolation.

                 Supported values are:
                    1 -> Chaos
                    2 -> ReadCommited
                    3 -> ReadUncommited
                    4 -> RepeatableRead 
                    5 -> Serializable
                    6 -> Unspecified 
  
		    - log mode 

            - log file path 

            - command timeout
 
    (c) 2004 - 2008 Marius Gheorghe. All rights reserved.
*/

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Runtime.CompilerServices;

namespace voidsoft.DataBlock
{
	/// <summary>
	///     Contains configuration details for DataBlock
	/// </summary>
	[Serializable]
	public class Configuration
	{
		/// <summary>
		///     Delegate for query logging
		/// </summary>
		public delegate void QueryLoggingEventHandler(string dataToLog);

		private static string connectionString;

		private static DatabaseServer databaseServerType;

		private static IsolationLevel defaultTransactionIsolationLevel;

		private static bool logEnabled;

		private static string logFilePath = "datablocklog.txt";

		private static int commandTimeout = 30;

		/// <summary>
		///     Initializes the <see cref="Configuration" /> class.
		/// </summary>
		static Configuration()
		{
			databaseServerType = DatabaseServer.SqlServer;
			defaultTransactionIsolationLevel = IsolationLevel.ReadCommitted;
			connectionString = string.Empty;
		}

		/// <summary>
		///     Get or set the path to the log file
		/// </summary>
		public static string LogFilePath
		{
			get
			{
				return logFilePath;
			}

			set
			{
				logFilePath = value;
			}
		}

		/// <summary>
		///     Enable or disable logging
		/// </summary>
		public static bool LogEnabled
		{
			get
			{
				return logEnabled;
			}

			set
			{
				logEnabled = value;
			}
		}

		/// <summary>
		///     Get or set the database connection string
		/// </summary>
		public static string ConnectionString
		{
			get
			{
				return connectionString;
			}
			set
			{
				connectionString = value;
			}
		}

		/// <summary>
		///     Get or set the database server type
		/// </summary>
		public static DatabaseServer DatabaseServerType
		{
			get
			{
				return databaseServerType;
			}

			set
			{
				databaseServerType = value;
			}
		}

		/// <summary>
		///     Get or set the default isolation level for transactions
		/// </summary>
		public static IsolationLevel DefaultTransactionIsolationLevel
		{
			get
			{
				return defaultTransactionIsolationLevel;
			}

			set
			{
				defaultTransactionIsolationLevel = value;
			}
		}

		/// <summary>
		///     Get or set the default commmand timeout
		/// </summary>
		public static int CommandTimeout
		{
			get
			{
				return commandTimeout;
			}

			set
			{
				commandTimeout = value;
			}
		}

		/// <summary>
		///     Occurs for query logging
		/// </summary>
		public static event QueryLoggingEventHandler OnQueryLogging;

		/// <summary>
		///     Read the configuration data from the application config file
		/// </summary>
		public static void ReadConfigurationFromConfigFile()
		{
			AppSettingsReader reader = new AppSettingsReader();

			//configure the connection string
			try
			{
				string cstring = (string) reader.GetValue("ConnectionString", typeof (string));
				ConnectionString = cstring;
			}
			catch (Exception exConnection)
			{
				Log.LogMessage(exConnection.Message + exConnection.StackTrace);
			}

			//configure the server type
			try
			{
				string dtype = (string) reader.GetValue("ServerType", typeof (string));
				ConfigureDatabaseServerType(dtype);
			}
			catch (Exception exServerType)
			{
				Log.LogMessage(exServerType.Message + exServerType.StackTrace);
			}

			//configure the transaction isolation level
			try
			{
				string isolation = (string) reader.GetValue("IsolationLevel", typeof (string));
				ConfigureTransactionIsolationLevel(isolation);
			}
			catch (Exception exTransaction)
			{
				Log.LogMessage(exTransaction.Message + exTransaction.StackTrace);
			}

			//enable the logging 
			try
			{
				string loggingEnabled = (string) reader.GetValue("LogEnabled", typeof (string));

				if (loggingEnabled.ToLower() == "true")
				{
					LogEnabled = true;
				}
			}
			catch (Exception exLog)
			{
				Log.LogMessage(exLog.Message + exLog.StackTrace);
			}

			//enable the logging 
			try
			{
				string logFilePath = (string) reader.GetValue("LogFilePath", typeof (string));
				LogFilePath = logFilePath;
			}
			catch
			{
			}

			try
			{
				int timeout = Convert.ToInt32(reader.GetValue("CommandTimeout", typeof (Int32)));
				CommandTimeout = timeout;
			}
			catch (Exception exTimeout)
			{
				Log.LogMessage(exTimeout.Message + exTimeout.StackTrace);
			}
		}

		/// <summary>
		///     Configure the database server type.
		/// </summary>
		/// <param name="value">Value based on which the server type is configured</param>
		private static void ConfigureDatabaseServerType(string value)
		{
			try
			{
				switch (value.ToLower())
				{
					case "sqlserver":
						databaseServerType = DatabaseServer.SqlServer;
						break;

					case "access":
						databaseServerType = DatabaseServer.Access;
						break;

					case "mysql":
						databaseServerType = DatabaseServer.MySQL;
						break;

					case "postgresql":
						databaseServerType = DatabaseServer.PostgreSql;
						break;

					default:
						throw new ArgumentException("invalid database server type");
				}
			}
			catch
			{
				throw;
			}
		}

		/// <summary>
		///     Configure the TransactionIsolation level.
		/// </summary>
		/// <param name="value"></param>
		private static void ConfigureTransactionIsolationLevel(string value)
		{
			try
			{
				//1 -> Chaos
				//2 -> ReadCommited
				//3 -> ReadUncommited
				//4 -> RepeatableRead 
				//5 -> Serializable
				//6 -> Unspecified 

				switch (value)
				{
					case "1":
						defaultTransactionIsolationLevel = IsolationLevel.Chaos;
						return;

					case "2":
						defaultTransactionIsolationLevel = IsolationLevel.ReadCommitted;
						return;

					case "3":
						defaultTransactionIsolationLevel = IsolationLevel.ReadUncommitted;
						return;

					case "4":
						defaultTransactionIsolationLevel = IsolationLevel.RepeatableRead;
						return;

					case "5":
						defaultTransactionIsolationLevel = IsolationLevel.Serializable;
						return;

					case "6":
						defaultTransactionIsolationLevel = IsolationLevel.Unspecified;
						return;

					default:
						throw new ArgumentException("invalid isolation level");
				}
			}
			catch
			{
				throw;
			}
		}

		/// <summary>
		///     Returns the value
		/// </summary>
		/// <param name="line">Configuration line</param>
		/// <returns></returns>
		private static string GetValue(string line)
		{
			int index = line.IndexOf("=");

			if (index == -1)
			{
				throw new ArgumentException("Invalid configuration file line " + line);
			}
			else
			{
				return line.Substring(index);
			}
		}

		/// <summary>
		///     Raises the query logging event.
		/// </summary>
		/// <param name="list">The list.</param>
		[MethodImpl(MethodImplOptions.Synchronized)]
		internal static void RaiseQueryLoggingEvent(List<ExecutionQuery> list)
		{
			if (OnQueryLogging != null)
			{
				string formattedData = Log.FormatQueryListForLogging(list);

				OnQueryLogging(formattedData);
			}
		}

		[MethodImpl(MethodImplOptions.Synchronized)]
		internal static void RaiseQueryLoggingEvent(ExecutionQuery query)
		{
			if (OnQueryLogging != null)
			{
				List<ExecutionQuery> listQueries = new List<ExecutionQuery>();
				listQueries.Add(query);

				RaiseQueryLoggingEvent(listQueries);
			}
		}
	}
}