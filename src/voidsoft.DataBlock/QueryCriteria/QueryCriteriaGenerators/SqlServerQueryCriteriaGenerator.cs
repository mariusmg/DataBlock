/*

       file: SqlServerQueryCriteriaGenerator.cs
description: Query Criteria Generator implementation for Sql Server.
    
   (c) 2004 - 2008 Marius Gheorghe. All rights reserved.
 
*/

using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace voidsoft.DataBlock
{
	/// <summary>
	///     SqlServer Query Criteria Generator implementation
	/// </summary>
	internal class SqlServerQueryCriteriaGenerator : IQueryCriteriaGenerator
	{
		private const int WHERE_FIELD_LENGTH = 6;
		private const int SELECT_FIELD_LENGTH = 7;

		private const string WHERE_KEYWORD = " WHERE ";

		private const string INNER_JOIN_KEYWORD = " Inner Join ";

		private const string LEFT_JOIN_KEYWORD = " Left Join ";

		private const string RIGHT_JOIN_KEYWORD = " Right Join ";

		private QueryCriteriaGeneratorType generatorType = QueryCriteriaGeneratorType.Select;

		/// <summary>
		///     Generates the query based on the specified query criteria.
		/// </summary>
		/// <param name="criteria">QueryCriteria upon which the query is generated </param>
		/// <returns>Sql Query</returns>
		public ExecutionQuery GenerateSelect(QueryCriteria criteria)
		{
			//set the generation type
			generatorType = QueryCriteriaGeneratorType.Select;

			if (criteria.JoinCriteriaConditions.Length == 0)
			{
				//no joins so generate the normal query criteria
				return GenerateWithoutJoin(criteria);
			}
			else
			{
				//generate the criteria with joins
				return GenerateWithJoin(criteria);
			}
		}

		/// <summary>
		///     Generates a UPDATE query based on the specified QueryCriteria
		/// </summary>
		/// <param name="criteria">QueryCriteria based upon which the query is generated</param>
		/// <returns>The resulting ExecutionQuery</returns>
		public ExecutionQuery GenerateUpdate(QueryCriteria criteria)
		{
			//set the generation type
			generatorType = QueryCriteriaGeneratorType.Update;

			//no joins so generate the normal query criteria
			return GenerateWithoutJoin(criteria);
		}

		/// <summary>
		///     Generates a DELETE query based on the specified QueryCriteria.
		/// </summary>
		/// <param name="criteria"></param>
		/// <returns></returns>
		public ExecutionQuery GenerateDelete(QueryCriteria criteria)
		{
			generatorType = QueryCriteriaGeneratorType.Delete;

			return GenerateWithoutJoin(criteria);
		}

		/// <summary>
		///     Generates the query(including joins) based on the specified query criteria
		/// </summary>
		/// <param name="criteria">QueryCriteria based on which the QueryCriteria is generated</param>
		/// <returns>Execution Query</returns>
		internal ExecutionQuery GenerateWithJoin(QueryCriteria criteria)
		{
			StringBuilder sbuild = new StringBuilder();
			bool appendWhere = false;

			ExecutionQuery execQuery;
			List<IDataParameter> listParameters = null;

			SqlGenerator generator = new SqlGenerator();

			try
			{
				listParameters = new List<IDataParameter>();

				//generate the head of the SELECT query.

				//we'll used this temporary structure (tempQuery) and pass it around
				//to the generator functions.
				execQuery = generator.GenerateSelectQuery(DatabaseServer.SqlServer, criteria);

				//add from the head query to the temporary objects.
				sbuild.Append(execQuery.Query);
				if (execQuery.Parameters != null)
				{
					foreach (IDataParameter var in execQuery.Parameters)
					{
						listParameters.Add(var);
					}
				}

				//add the JOINS from the main tableMetadata
				for (int i = 0; i < criteria.JoinCriteriaConditions.Length; i++)
				{
					switch (criteria.JoinCriteriaConditions[i].Join)
					{
						case JoinType.Inner:
							sbuild.Append(INNER_JOIN_KEYWORD);
							break;

						case JoinType.Left:
							sbuild.Append(LEFT_JOIN_KEYWORD);
							break;

						case JoinType.Right:
							sbuild.Append(RIGHT_JOIN_KEYWORD);
							break;
					}

					sbuild.Append(generator.GetTableName(DatabaseServer.SqlServer, criteria.JoinCriteriaConditions[i].ForeignKeyFieldTableName) + " ON " +
								  generator.GetTableName(DatabaseServer.SqlServer, criteria.JoinCriteriaConditions[i].PrimaryKeyFieldTableName) + "." +
								  criteria.JoinCriteriaConditions[i].PrimaryKey.fieldName + "=" +
								  generator.GetTableName(DatabaseServer.SqlServer, criteria.JoinCriteriaConditions[i].Criteria.TableName) + "." +
								  criteria.JoinCriteriaConditions[i].ForeignKey.fieldName);
				}

				//add "WHERE" condition from the first criteria
				if (criteria.CriteriaConditions.Length > 0)
				{
					sbuild.Append(WHERE_KEYWORD);
					appendWhere = true;
					sbuild.Append(GenerateCondition(generator.GetTableName(DatabaseServer.SqlServer, criteria.TableName), criteria.CriteriaConditions, ref sbuild, ref listParameters));
				}

				//add the join criterias
				for (int i = 0; i < criteria.JoinCriteriaConditions.Length; i++)
				{
					if ((i == 0) && (appendWhere == false))
					{
						sbuild.Append(WHERE_KEYWORD);
					}

					if (criteria.JoinCriteriaConditions[i].Criteria.CriteriaConditions.Length > 0)
					{
						if (sbuild.ToString().EndsWith(WHERE_KEYWORD))
						{
							sbuild.Append(GenerateCondition(generator.GetTableName(DatabaseServer.SqlServer, criteria.JoinCriteriaConditions[i].Criteria.TableName), criteria.JoinCriteriaConditions[i].Criteria.CriteriaConditions, ref sbuild, ref listParameters));
						}
						else
						{
							sbuild.Append(" AND " + GenerateCondition(generator.GetTableName(DatabaseServer.SqlServer, criteria.JoinCriteriaConditions[i].Criteria.TableName), criteria.JoinCriteriaConditions[i].Criteria.CriteriaConditions, ref sbuild, ref listParameters));
						}
					}
				}

				//checks for where and and
				if (sbuild.ToString().EndsWith(WHERE_KEYWORD))
				{
					sbuild.Remove(sbuild.Length - WHERE_FIELD_LENGTH, WHERE_FIELD_LENGTH);
				}

				execQuery.Query = sbuild.ToString();
				IDataParameter[] pmc = new IDataParameter[listParameters.Count];
				listParameters.CopyTo(pmc);
				execQuery.Parameters = pmc;

				return execQuery;
			}
			finally
			{
				if (listParameters != null)
				{
					listParameters.Clear();
				}
			}
		}

		/// <summary>
		///     Generates the query without a join
		/// </summary>
		/// <param name="criteria">The criteria.</param>
		/// <returns></returns>
		internal ExecutionQuery GenerateWithoutJoin(QueryCriteria criteria)
		{
			List<IDataParameter> listParameters = null;

			ExecutionQuery execQuery = new ExecutionQuery();

			SqlGenerator generator = new SqlGenerator();

			listParameters = new List<IDataParameter>();

			StringBuilder sbuild = new StringBuilder();

			DataFactory factory = new DataFactory();

			
			if (generatorType == QueryCriteriaGeneratorType.Select)
			{
				execQuery = generator.GenerateSelectQuery(DatabaseServer.SqlServer, criteria);
			}
			else if (generatorType == QueryCriteriaGeneratorType.Update)
			{
				execQuery = generator.GenerateUpdateQuery(DatabaseServer.SqlServer, criteria.TableName, criteria.Fields, false);
			}
			else if (generatorType == QueryCriteriaGeneratorType.Delete)
			{
				execQuery = generator.GenerateDeleteQuery(DatabaseServer.SqlServer, criteria.TableName);
			}

			//add to the intermediary objects
			if (execQuery.Parameters != null)
			{
				foreach (IDataParameter var in execQuery.Parameters)
				{
					listParameters.Add(var);
				}
			}
			sbuild.Append(execQuery.Query);

			//initialize generator
			ISqlGenerator isql = factory.InitializeSqlGenerator(DatabaseServer.SqlServer);

			//append where clause
			sbuild.Append(WHERE_KEYWORD);

			//generate the condition based on criteria
			string condition = GenerateCondition(generator.GetTableName(DatabaseServer.SqlServer, criteria.TableName), criteria.CriteriaConditions, ref sbuild,
												 ref listParameters);

			//more checks

			if (sbuild.ToString().EndsWith(WHERE_KEYWORD) && condition.StartsWith(" ORDER BY "))
			{
				if (condition.StartsWith(" ORDER BY"))
				{
					sbuild.Remove(sbuild.Length - WHERE_FIELD_LENGTH, WHERE_FIELD_LENGTH);
				}
			}

			sbuild.Append(condition);

			//last check to prevent invalid sql queries
			if (sbuild.ToString().EndsWith(WHERE_KEYWORD))
			{
				sbuild.Remove(sbuild.Length - WHERE_FIELD_LENGTH, WHERE_FIELD_LENGTH);
			}

			execQuery = new ExecutionQuery();
			execQuery.Query = sbuild.ToString();
			IDataParameter[] pmc = new IDataParameter[listParameters.Count];
			listParameters.CopyTo(pmc);
			execQuery.Parameters = pmc;

			return execQuery;
		}

		/// <summary>
		///     Generates the sql query condition
		/// </summary>
		/// <param name="tableName">Name of the datbase table</param>
		/// <param name="conditions">Criteria conditions </param>
		/// <param name="sbSqlHeader">StringBuilder which contains the SELECT part of the sql query build so far</param>
		/// <param name="listParameters">List with the IDataParameters used in the query</param>
		/// <returns>The querie's condition </returns>
		internal string GenerateCondition(string tableName, CriteriaCondition[] conditions, ref StringBuilder sbSqlHeader, ref List<IDataParameter> listParameters)
		{
			//keeps the order by part of the query
			StringBuilder sbOrderByCriteria = new StringBuilder();

			//holds the generated query
			StringBuilder sbuild = new StringBuilder();

			ISqlGenerator isql = null;

			List<string> listParameterNames = null;

			//temporary vars
			string fieldName = string.Empty;
			int index = -1;
			string tempString = string.Empty;

			SqlGenerator generator = new SqlGenerator();

			DataConvertor converter = new DataConvertor();

			DataFactory factory = new DataFactory();

			try
			{
				listParameterNames = new List<string>();

				//initialize generator
				isql = factory.InitializeSqlGenerator(DatabaseServer.SqlServer);

				//generate conditions
				for (int i = 0; i < conditions.Length; i++)
				{
					//check if we generate "AND" operator
					if (i > 0)
					{
						/*excluse "AND for the following operators
                        
                        -Order by
                        -Or
                        -Not
                        -Count 
                        -Distinct
                         
                        */

						if ((conditions[i - 1].CriteriaOperator != CriteriaOperator.Distinct) && (conditions[i].CriteriaOperator != CriteriaOperator.OrderBy) &&
							(conditions[i].CriteriaOperator != CriteriaOperator.Or) && (conditions[i - 1].CriteriaOperator != CriteriaOperator.Count) &&
							(conditions[i - 1].CriteriaOperator != CriteriaOperator.Or) && (conditions[i - 1].CriteriaOperator != CriteriaOperator.Not))
						{
							sbuild.Append(" AND ");
						}
					}

					DatabaseField field = conditions[i].Field;

					switch (conditions[i].CriteriaOperator)
					{
						case CriteriaOperator.Between:
							//here we must have 2 parameters with two diffferent values and name. These
							//parameters must be generated based on a single name.

							IDataParameter paramBetweenFirst = converter.ConvertToDataParameter(DatabaseServer.SqlServer, tableName, field, ref listParameterNames);
							IDataParameter paramBetweenSecond = converter.ConvertToDataParameter(DatabaseServer.SqlServer, tableName, field, ref listParameterNames);

							paramBetweenFirst.ParameterName = paramBetweenFirst.ParameterName + "First";
							paramBetweenSecond.ParameterName = paramBetweenSecond.ParameterName + "Second";

							//set the parameter's value and add it to the list
							paramBetweenFirst.Value = conditions[i].Values[0];
							listParameters.Add(paramBetweenFirst);

							sbuild.Append(" " + generator.GetTableName(DatabaseServer.SqlServer, tableName) + "." + conditions[i].Field.fieldName + " BETWEEN " +
										  isql.GetValue(paramBetweenFirst));
							sbuild.Append(" AND ");

							//set the  value of the second parameter
							paramBetweenSecond.Value = conditions[i].Values[1];
							listParameters.Add(paramBetweenSecond);

							sbuild.Append(isql.GetValue(paramBetweenSecond));
							break;

						case CriteriaOperator.Not:
							sbuild.Append(" NOT");
							break;

						case CriteriaOperator.Different:
							field.fieldValue = conditions[i].Values[0];
							IDataParameter paramDifferent = converter.ConvertToDataParameter(DatabaseServer.SqlServer, tableName, field, ref listParameterNames);
							listParameters.Add(paramDifferent);
							sbuild.Append(" " + generator.GetTableName(DatabaseServer.SqlServer, tableName) + "." + field.fieldName + "<>" + isql.GetValue(paramDifferent));
							break;

						case CriteriaOperator.Like:
							field.fieldValue = "%" + conditions[i].Values[0] + "%";
							IDataParameter paramLike = converter.ConvertToDataParameter(DatabaseServer.SqlServer, tableName, field, ref listParameterNames);
							listParameters.Add(paramLike);
							sbuild.Append(" " + generator.GetTableName(DatabaseServer.SqlServer, tableName) + "." + field.fieldName + " LIKE " + isql.GetValue(paramLike));
							break;

						case CriteriaOperator.LikeEnd:
							field.fieldValue = "%" + conditions[i].Values[0];
							IDataParameter paramLikeEnd = converter.ConvertToDataParameter(DatabaseServer.SqlServer, tableName, field, ref listParameterNames);
							listParameters.Add(paramLikeEnd);
							sbuild.Append(" " + generator.GetTableName(DatabaseServer.SqlServer, tableName) + "." + field.fieldName + " LIKE " + isql.GetValue(paramLikeEnd));
							break;

						case CriteriaOperator.LikeStart:
							field.fieldValue = conditions[i].Values[0] + "%";
							IDataParameter paramLikeStart = converter.ConvertToDataParameter(DatabaseServer.SqlServer, tableName, field, ref listParameterNames);
							listParameters.Add(paramLikeStart);
							sbuild.Append(" " + generator.GetTableName(DatabaseServer.SqlServer, tableName) + "." + field.fieldName + " LIKE " + isql.GetValue(paramLikeStart));
							break;

						case CriteriaOperator.Equality:
							field.fieldValue = conditions[i].Values[0];
							IDataParameter paramEquality = converter.ConvertToDataParameter(DatabaseServer.SqlServer, tableName, field, ref listParameterNames);
							listParameters.Add(paramEquality);
							sbuild.Append(" " + generator.GetTableName(DatabaseServer.SqlServer, tableName) + "." + field.fieldName + "=" + isql.GetValue(paramEquality));
							break;

						case CriteriaOperator.IsNull:
							sbuild.Append(" " + generator.GetTableName(DatabaseServer.SqlServer, tableName) + "." + field.fieldName + " is null");
							break;

						case CriteriaOperator.IsNotNull:
							sbuild.Append(" " + generator.GetTableName(DatabaseServer.SqlServer, tableName) + "." + field.fieldName + " is not null");
							break;

						case CriteriaOperator.Or:
							sbuild.Append(" OR");
							break;

						case CriteriaOperator.Smaller:
							field.fieldValue = conditions[i].Values[0];

							IDataParameter paramSmaller = converter.ConvertToDataParameter(DatabaseServer.SqlServer, tableName, field, ref listParameterNames);
							listParameters.Add(paramSmaller);

							sbuild.Append(" " + generator.GetTableName(DatabaseServer.SqlServer, tableName) + "." + field.fieldName + " < " + isql.GetValue(paramSmaller));
							break;

						case CriteriaOperator.SmallerOrEqual:
							field.fieldValue = conditions[i].Values[0];

							IDataParameter paramSmallerOrEqual = converter.ConvertToDataParameter(DatabaseServer.SqlServer, tableName, field, ref listParameterNames);
							listParameters.Add(paramSmallerOrEqual);

							sbuild.Append(" " + generator.GetTableName(DatabaseServer.SqlServer, tableName) + "." + field.fieldName + " <= " + isql.GetValue(paramSmallerOrEqual));
							break;

						case CriteriaOperator.Higher:
							field.fieldValue = conditions[i].Values[0];

							IDataParameter paramHigher = converter.ConvertToDataParameter(DatabaseServer.SqlServer, tableName, field, ref listParameterNames);
							listParameters.Add(paramHigher);

							sbuild.Append(" " + generator.GetTableName(DatabaseServer.SqlServer, tableName) + "." + field.fieldName + " > " + isql.GetValue(paramHigher));
							break;

						case CriteriaOperator.HigherOrEqual:
							field.fieldValue = conditions[i].Values[0];

							IDataParameter paramHigherOrEqual = converter.ConvertToDataParameter(DatabaseServer.SqlServer, tableName, field, ref listParameterNames);
							listParameters.Add(paramHigherOrEqual);

							sbuild.Append(" " + generator.GetTableName(DatabaseServer.SqlServer, tableName) + "." + field.fieldName + " >= " + isql.GetValue(paramHigherOrEqual));
							break;

						case CriteriaOperator.OrderBy:
							if (sbOrderByCriteria.Length == 0)
							{
								//add the operator for the first criteria
								sbOrderByCriteria.Append("ORDER BY " + generator.GetTableName(DatabaseServer.SqlServer, tableName) + "." + field.fieldName + " " + conditions[i].Values[0]);
							}
							else
							{
								//add "," for the subsequent criterias
								sbOrderByCriteria.Append(", " + generator.GetTableName(DatabaseServer.SqlServer, tableName) + "." + field.fieldName + " " + conditions[i].Values[0]);
							}
							break;

						// NOTE :  DISTICT requires modification of the sql header. Also
						// DISTINCT clause requires that the distinct field should be 
						// the first one in the list.
						case CriteriaOperator.Distinct:

							//get the field
							fieldName = generator.GetTableName(DatabaseServer.SqlServer, tableName) + "." + conditions[i].Field.fieldName;

							//we have the field name now search for it in the fields list
							index = sbSqlHeader.ToString().IndexOf(fieldName);

							//now remove the field from the list.
							if (index == -1)
							{
								throw new ArgumentException("Invalid Distinct clause");
							}

							tempString = sbSqlHeader.ToString();

							//determine the position of the next coma after the distinct field 
							int comaLength = 0;
							int startIndex = index + fieldName.Length;

							for (int ff = startIndex; ff < tempString.Length; ff++)
							{
								if (tempString[ff] == ' ')
								{
									++comaLength;
								}
								else if (tempString[ff] == ',')
								{
									++comaLength;
									break;
								}
								else
								{
									break;
								}
							}

							tempString = tempString.Remove(index, fieldName.Length + comaLength);

							//add it at the beginning of the select
							tempString = tempString.Insert(SELECT_FIELD_LENGTH, " distinct " + generator.GetTableName(DatabaseServer.SqlServer, tableName) + "." + conditions[i].Field.fieldName + ",");

							//remove the "," before "FROM" if it's the case
							int iix = tempString.IndexOf("FROM");

							while (--iix > 0)
							{
								if (tempString[iix] != ' ')
								{
									if (tempString[iix] == ',')
									{
										tempString = tempString.Remove(iix, 1);
										break;
									}
									else
									{
										break;
									}
								}
							}

							sbSqlHeader.Remove(0, sbSqlHeader.Length);
							sbSqlHeader.Append(tempString);
							break;

						//NOTE: MAX fields must be after SELECT statement
						case CriteriaOperator.Max:
							//get the field
							fieldName = generator.GetTableName(DatabaseServer.SqlServer, tableName) + "." + conditions[i].Field.fieldName;

							//we have the field name now search for it in the fields list
							index = sbSqlHeader.ToString().IndexOf(fieldName);

							//now remove the field from the list.
							if (index == -1)
							{
								throw new ArgumentException("Invalid MAX clause");
							}

							tempString = sbSqlHeader.ToString();

							tempString = tempString.Remove(index, fieldName.Length);

							//add it at the beginning of the select
							tempString = tempString.Insert(SELECT_FIELD_LENGTH, " max(" + generator.GetTableName(DatabaseServer.SqlServer, tableName) + "." + conditions[i].Field.fieldName + ")");

							sbSqlHeader.Remove(0, sbSqlHeader.Length);
							sbSqlHeader.Append(tempString);
							break;

						//NOTE: MIN fields must be after SELECT statement
						case CriteriaOperator.Min:
							//get the field
							fieldName = generator.GetTableName(DatabaseServer.SqlServer, tableName) + "." + conditions[i].Field.fieldName;

							//we have the field name now search for it in the fields list
							index = sbSqlHeader.ToString().IndexOf(fieldName);

							//now remove the field from the list.
							if (index == -1)
							{
								throw new ArgumentException("Invalid MIN clause");
							}

							tempString = sbSqlHeader.ToString();

							tempString = tempString.Remove(index, fieldName.Length);

							//add it at the beginning of the select
							tempString = tempString.Insert(SELECT_FIELD_LENGTH, " min(" + generator.GetTableName(DatabaseServer.SqlServer, tableName) + "." + conditions[i].Field.fieldName + ")");

							sbSqlHeader.Remove(0, sbSqlHeader.Length);
							sbSqlHeader.Append(tempString);
							break;

						//NOTE: COUNT fields must be after SELECT statement
						case CriteriaOperator.Count:
							//get the field
							fieldName = generator.GetTableName(DatabaseServer.SqlServer, tableName) + "." + conditions[i].Field.fieldName;

							//we have the field name now search for it in the fields list
							index = sbSqlHeader.ToString().IndexOf(fieldName);

							//now remove the field from the list.
							if (index == -1)
							{
								throw new ArgumentException("Invalid count clause");
							}

							tempString = sbSqlHeader.ToString();

							tempString = tempString.Remove(index, fieldName.Length);

							//add it at the beginning of the select
							tempString = tempString.Insert(SELECT_FIELD_LENGTH, " count(" + generator.GetTableName(DatabaseServer.SqlServer, tableName) + "." + conditions[i].Field.fieldName + ")");

							sbSqlHeader.Remove(0, sbSqlHeader.Length);
							sbSqlHeader.Append(tempString);
							break;
					}
				}

				//last check to prevent invalid sql queries. If we don't have any
				//conditions remove the "WHERE".
				if (sbuild.ToString().EndsWith(WHERE_KEYWORD))
				{
					sbuild.Remove(sbuild.Length - WHERE_FIELD_LENGTH, WHERE_FIELD_LENGTH);
				}

				//check if we have conditions which don't require a "WHERE" clause
				if (sbuild.Length == 0 && sbOrderByCriteria.Length > 0)
				{
					//remove from query header
					sbSqlHeader.Remove(sbSqlHeader.Length - WHERE_FIELD_LENGTH, WHERE_FIELD_LENGTH);
				}

				if (sbOrderByCriteria.Length > 0)
				{
					sbuild.Append(" " + sbOrderByCriteria);
				}

				return sbuild.ToString();
			}
			finally
			{
				if (listParameterNames != null)
				{
					listParameterNames.Clear();
				}
			}
		}
	}
}