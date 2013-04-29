/*

       file : SqlServerGeenrator.cs
description : SqlServer specific ISqlGenerator implementation.
    
   (c) 2004 - 2008 Marius Gheorghe. All rights reserved.
 

*/

using System;
using System.Data;
using System.Text;

namespace voidsoft.DataBlock
{
	/// <summary>
	///     Sql Server generator implementation
	/// </summary>
	internal sealed class SqlServerGenerator : ISqlGenerator
	{
		/// <summary>
		///     Get the value based on the specified type
		/// </summary>
		/// <param name="t">The type</param>
		/// <param name="value">The value</param>
		/// <returns>The value in string format</returns>
		public string GetValue(DbType t, object value)
		{
			//first check the value for null
			if (value == null || value == DBNull.Value)
			{
				return " null ";
			}

			if (t == DbType.Boolean)
			{
				return (Convert.ToInt32(Boolean.Parse(value.ToString())).ToString());
			}
			else if (t == DbType.Int32 || t == DbType.UInt32 || t == DbType.SByte || t == DbType.Int16 || t == DbType.UInt64 || t == DbType.Byte || t == DbType.Int16 ||
			         t == DbType.Int64)
			{
				return value.ToString();
			}
			else if (t == DbType.DateTime)
			{
				if (value != DBNull.Value)
				{
					//return "DateValue('" + value.ToString() + "')";
					return " '" + value + "'";
				}
				else
				{
					return " DateValue('00/00/0000')";
				}
			}
			else if (t == DbType.Binary)
			{
				StringBuilder sbuild = new StringBuilder();

				byte[] bits = (byte[]) value;

				for (int i = 0; i < bits.Length; i++)
				{
					sbuild.Append(bits[i].ToString());
				}
				return "'" + sbuild + "'";
			}
			else if (t == DbType.Decimal || t == DbType.Currency || t == DbType.Double)
			{
				if (value != DBNull.Value)
				{
					return " '" + value + "'";
				}
				else
				{
					return " 0";
				}
			}
			else
			{
				//string || char
				return (" '" + value + "'");
			}
		}

		/// <summary>
		///     Gets the value from the specified IDataParameter
		/// </summary>
		/// <param name="param"></param>
		/// <returns>Parameter's name</returns>
		public string GetValue(IDataParameter param)
		{
			return param.ParameterName;
		}

		/// <summary>
		///     Get the value based on the specified type with the attribution operator
		/// </summary>
		/// <param name="t">The type</param>
		/// <param name="value">The value</param>
		/// <returns>The value in string format</returns>
		public string GetValueWithAttributionOperator(DbType t, object value)
		{
			//first check the value for null
			if (value == null || value == DBNull.Value)
			{
				return " = " + GetValue(t, value);
			}
			else
			{
				return " = " + GetValue(t, value);
			}
		}

		public string GetValueWithAttributionOperator(IDataParameter parameter)
		{
			return " = " + parameter.ParameterName;
		}

		/// <summary>
		///     Get the value based on the specified type with the comparasion operator
		/// </summary>
		/// <param name="t">The type</param>
		/// <param name="value">The value</param>
		/// <returns>The value in string format</returns>
		public string GetValueWithComparationOperator(DbType t, object value)
		{
			//first check the value for null
			if (value == null || value == DBNull.Value)
			{
				return " is " + GetValue(t, value);
			}
			else
			{
				return " = " + GetValue(t, value);
			}
		}

		/// <summary>
		///     Generates a query using paging
		/// </summary>
		/// <param name="metadata">The metadata.</param>
		/// <param name="numberOfItems">The number of items.</param>
		/// <param name="pageNumber">The page number.</param>
		/// <returns></returns>
		public string GeneratePaginatedQuery(TableMetadata metadata, int numberOfItems, int pageNumber)
		{
			StringBuilder builder = new StringBuilder();

			SqlGenerator generator = new SqlGenerator();


			//SELECT TOP 5 * FROM items WHERE intitemid NOT IN (SELECT TOP 10 intitemid FROM items ORDER BY intitemid) ORDER BY intitemid asc

			DatabaseField field = metadata.GetPrimaryKeyField();

			string tableName = generator.GetTableName(DatabaseServer.SqlServer, metadata.TableName);

			builder.Append("SELECT TOP ");
			builder.Append(numberOfItems.ToString());
			builder.Append(" FROM ");
			builder.Append(tableName);
			builder.Append(" WHERE ");
			builder.Append(field.fieldName + " NOT IN (");
			builder.Append(" SELECT TOP " + (pageNumber*numberOfItems).ToString());
			builder.Append(" " + field.fieldName + " ");
			builder.Append(" FROM ");
			builder.Append(tableName);
			builder.Append(" ORDER BY " + field.fieldName + ")");
			builder.Append(" ORDER BY " + field.fieldName + " asc");

			return builder.ToString();
		}

		/// <summary>
		///     Get the value based on the specified type with the comparasion operator
		/// </summary>
		/// <param name="parameter">IDataParameter from which we return the value</param>
		/// <returns>The value in string format</returns>
		public string GetValueWithComparationOperator(IDataParameter parameter)
		{
			if (parameter == null || parameter.Value == DBNull.Value)
			{
				return " is null";
			}
			else
			{
				return " = " + parameter.ParameterName;
			}
		}
	}
}