/*


       file : ConstraintManager.cs
description : Handles query constraints creation/parsing
     author : Marius Gheorghe
  

*/

using System;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

namespace voidsoft.DataBlock
{
	/// <summary>
	///     Manages constraints
	/// </summary>
	internal static class ConstraintManager
	{
		private const string CONSTRAINT_DATA_SEPARATOR = "//";

		/// <summary>
		///     Modifies a SQL query to include a PK constraint
		/// </summary>
		/// <param name="primaryKeyFieldName">Name of the primary key field.</param>
		/// <param name="tableName">Name of the table.</param>
		/// <param name="sqlQuery">The SQL query.</param>
		/// <returns></returns>
		[MethodImpl(MethodImplOptions.Synchronized)]
		public static string GeneratePrimaryKeyConstraint(string primaryKeyFieldName, string tableName, string sqlQuery)
		{
			return ExecutionEngine.PRIMARY_KEY_CONSTRAINT + CONSTRAINT_DATA_SEPARATOR + tableName + CONSTRAINT_DATA_SEPARATOR + primaryKeyFieldName +
			       CONSTRAINT_DATA_SEPARATOR + sqlQuery;
		}

		//[MethodImpl(MethodImplOptions.Synchronized)]
		//public static string GenerateForeignKeyConstraint(int numberOfAffectedQueries)
		//{
		//    return ExecutionEngine.FOREIGN_KEY_CONSTRAINT + "//" + numberOfAffectedQueries + "//" + entity.TableName + "//" + primaryKey.fieldName);
		//}

		/// <summary>
		///     Strips the primary key constraint.
		/// </summary>
		/// <param name="query">The query.</param>
		/// <returns></returns>
		[MethodImpl(MethodImplOptions.Synchronized)]
		public static ExecutionQuery StripPrimaryKeyConstraint(ExecutionQuery query)
		{
			//parse the constraint
			string[] values = Regex.Split(query.Query, CONSTRAINT_DATA_SEPARATOR);

			query.Query = values[3];

			return query;
		}

		/// <summary>
		///     Parses the foreign key constraint.
		/// </summary>
		/// <param name="query">The query.</param>
		/// <returns></returns>
		[MethodImpl(MethodImplOptions.Synchronized)]
		public static ForeignKeyConstraint ParseForeignKeyConstraint(string query)
		{
			//parse the constraint and create a ForeignKeyConstraint
			string[] values = Regex.Split(query, CONSTRAINT_DATA_SEPARATOR);

			ForeignKeyConstraint fk = new ForeignKeyConstraint();
			fk.NumerOfRunningQueries = Int32.Parse(values[1]);
			fk.TableName = values[2];
			fk.NameOfPrimaryKeyField = values[3];

			return fk;
		}

		/// <summary>
		///     Parses the foreign key constraint.
		/// </summary>
		/// <param name="query">The query.</param>
		/// <returns></returns>
		public static ForeignKeyConstraint ParseForeignKeyConstraint(ExecutionQuery query)
		{
			return ParseForeignKeyConstraint(query.Query);
		}

		/// <summary>
		///     Parses the primary key constraint.
		/// </summary>
		/// <param name="query">The query.</param>
		/// <returns></returns>
		[MethodImpl(MethodImplOptions.Synchronized)]
		public static PrimaryKeyConstraint ParsePrimaryKeyConstraint(string query)
		{
			//parse the constraint
			string[] values = Regex.Split(query, CONSTRAINT_DATA_SEPARATOR);

			PrimaryKeyConstraint keyConstraint = new PrimaryKeyConstraint();
			keyConstraint.TableName = values[1];
			keyConstraint.PrimaryKeyFieldName = values[2];

			return keyConstraint;
		}

		/// <summary>
		///     Parses the primary key constraint.
		/// </summary>
		/// <param name="query">The query.</param>
		/// <returns></returns>
		[MethodImpl(MethodImplOptions.Synchronized)]
		public static PrimaryKeyConstraint ParsePrimaryKeyConstraint(ExecutionQuery query)
		{
			return ParsePrimaryKeyConstraint(query.Query);
		}
	}
}