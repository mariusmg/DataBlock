/*
	   file : Log.cs
description : DataBlock log implementation

  
 (c) 2004 - 2008 Marius Gheorghe. All rights reserved.

*/

using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;

namespace voidsoft.DataBlock
{
	/// <summary>
	///     File based logging and formatting.
	/// </summary>
	internal class Log
	{
		//used for thread sync
		private static object lockedLog = new object();

		/// <summary>
		///     Constructor
		/// </summary>
		private Log()
		{
		}

		/// <summary>
		///     Logs the specified data
		/// </summary>
		/// <param name="data"></param>
		[MethodImpl(MethodImplOptions.Synchronized)]
		public static void LogMessage(object data)
		{
			if (Configuration.LogEnabled == false)
			{
				return;
			}

			if (data is Exception)
			{
				LogMessage((Exception) data);
			}
			else if (data is List<ExecutionQuery>)
			{
				LogMessage((List<ExecutionQuery>) data);
			}
			else if (data is ExecutionQuery)
			{
				List<ExecutionQuery> listExecQueries = new List<ExecutionQuery>();
				listExecQueries.Add((ExecutionQuery) data);
				LogMessage(listExecQueries);
			}
			else
			{
				LogMessage("Message", data.ToString());
			}
		}

		/// <summary>
		///     Logs the strings
		/// </summary>
		/// <param name="header">The header.</param>
		/// <param name="lines">The lines.</param>
		private static void LogMessage(string header, params string[] lines)
		{
			FileStream fs = null;
			StreamWriter writer = null;

			try
			{
				fs = new FileStream(Configuration.LogFilePath, FileMode.Append, FileAccess.Write);
				writer = new StreamWriter(fs);

				lock (lockedLog)
				{
					writer.WriteLine("  ");
					writer.WriteLine(header);

					foreach (string data in lines)
					{
						writer.WriteLine(data);
					}

					writer.WriteLine("  ");
					writer.WriteLine("  ");
				}

				writer.Flush();

				fs.Close();
			}
			catch
			{
				//swallow the logging exception
			}
			finally
			{
				if (fs != null)
				{
					fs.Close();
				}
			}
		}

		/// <summary>
		///     Logs the message.
		/// </summary>
		/// <param name="ex">The exception</param>
		private static void LogMessage(Exception ex)
		{
			string formattedData = FormatExceptionForLogging(ex);
			LogMessage("Exception", formattedData);
		}

		/// <summary>
		///     Logs the message.
		/// </summary>
		/// <param name="listQueries">The list queries.</param>
		private static void LogMessage(List<ExecutionQuery> listQueries)
		{
			string header = "Queries";

			string formattedData = FormatQueryListForLogging(listQueries);

			LogMessage(header, formattedData);
		}

		/// <summary>
		///     Formats the exception for logging.
		/// </summary>
		/// <param name="ex">The exception to be formatted</param>
		/// <returns></returns>
		internal static string FormatExceptionForLogging(Exception ex)
		{
			int size = ex.InnerException == null ? 2 : 4;

			string[] lines = new string[size];
			lines[0] = ex + " " + ex.Message;
			lines[1] = ex.StackTrace;

			if (lines.Length == 4)
			{
				lines[2] = ex.InnerException.Message;
				lines[3] = ex.InnerException.StackTrace;
			}

			StringBuilder builder = new StringBuilder();

			foreach (string line in lines)
			{
				builder.Append(line);
			}

			return builder.ToString();
		}

		/// <summary>
		///     Formats the query list for logging.
		/// </summary>
		/// <param name="listQueries">The list queries.</param>
		/// <returns></returns>
		internal static string FormatQueryListForLogging(List<ExecutionQuery> listQueries)
		{
			StringBuilder builder = new StringBuilder();

			foreach (ExecutionQuery query in listQueries)
			{
				builder.Append(query.Query);

				if (query.Parameters != null)
				{
					foreach (IDataParameter parameter in query.Parameters)
					{
						builder.Append(parameter.ParameterName + "=" + parameter.Value);
					}
				}

				builder.Append(Environment.NewLine);
			}

			return builder.ToString();
		}
	}
}