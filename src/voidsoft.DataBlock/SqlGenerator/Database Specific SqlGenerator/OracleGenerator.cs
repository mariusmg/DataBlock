/*

       file : OracleGenerator.cs
description : Oracle specific ISqlGenerator implementation.
    
       (c) 2004 - 2006 Marius Gheorghe. All rights reserved.


*/

using System;
using System.Data;
using System.Text;

namespace voidsoft.DataBlock
{
	/// <summary>
	///     Sql Server generator implementation
	/// </summary>
	internal sealed class OracleGenerator : ISqlGenerator
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

		public string GeneratePaginatedQuery(TableMetadata metadata, int numberOfItems, int pageNumber)
		{
			throw new NotImplementedException();
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