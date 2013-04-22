/*
 
       file: AccessQueryCriteriaGenerator.cs
description: Microsoft Access (97/2000/2003) implementation for QueryCriteria.
    

   (c) 2004 - 2006 Marius Gheorghe. All rights reserved.
 
*/

using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace voidsoft.DataBlock
{
	/// <summary>
	///     Microsoft Access QueryCriteria implementation
	/// </summary>
	internal class AccessQueryCriteriaGenerator : IQueryCriteriaGenerator
	{
		//consts
		private const int WHERE_FIELD_LENGTH = 6;
		private const int SELECT_FIELD_LENGTH = 7;

		private QueryCriteriaGeneratorType generatorType = QueryCriteriaGeneratorType.Select;

		/// <summary>
		///     Generates the query based on the specified query criteria.
		/// </summary>
		/// <param name="criteria">QueryCriteria upon which the query is generated </param>
		/// <returns>Sql Query</returns>
		public ExecutionQuery GenerateSelect(QueryCriteria criteria)
		{
			try
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
			catch
			{
				throw;
			}
		}

		/// <summary>
		///     Generates a UPDATE SQL query
		/// </summary>
		/// <param name="criteria"></param>
		/// <returns></returns>
		public ExecutionQuery GenerateUpdate(QueryCriteria criteria)
		{
			try
			{
				//set the generation type
				generatorType = QueryCriteriaGeneratorType.Update;
				return GenerateWithoutJoin(criteria);
			}
			catch
			{
				throw;
			}
		}

		/// <summary>
		///     Generates a DELETE SQL query
		/// </summary>
		/// <param name="criteria"></param>
		/// <returns></returns>
		public ExecutionQuery GenerateDelete(QueryCriteria criteria)
		{
			try
			{
				generatorType = QueryCriteriaGeneratorType.Delete;
				return GenerateWithoutJoin(criteria);
			}
			catch
			{
				throw;
			}
		}

		/// <summary>
		///     GENERATE a SELECT query with joins
		/// </summary>
		/// <param name="criteria"></param>
		/// <remarks>Generste</remarks>
		/// <returns></returns>
		internal ExecutionQuery GenerateWithJoin(QueryCriteria criteria)
		{
			StringBuilder sbuild = new StringBuilder();
			bool appendWhere = false;

			ExecutionQuery execQuery;
			List<IDataParameter> listParameters = null;
			try
			{
				listParameters = new List<IDataParameter>();

				//generate the head of the SELECT query.

				//we'll used this temporary structure (tempQuery) and pass it around
				//to the generator functions.
				execQuery = SqlGenerator.GenerateSelectQuery(DatabaseServer.Access, criteria);

				//add from the head query to the temporary objects.
				sbuild.Append(execQuery.Query);
				if (execQuery.Parameters != null)
				{
					foreach (IDataParameter var in execQuery.Parameters)
					{
						listParameters.Add(var);
					}
				}

				int joinsCount = criteria.JoinCriteriaConditions.Length;

				StringBuilder parantheses = null;

				if (joinsCount > 1)
				{
					parantheses = new StringBuilder(joinsCount);
					for (int i = 0; i < parantheses.Capacity; i++)
					{
						parantheses.Append("(");
					}

					//HACK : here we need to add the colons for the joins.
					//Because the name of the first table is already added we must
					//add the parantheses before.

					string generatedQuery = sbuild.ToString();
					int lastIndex = generatedQuery.LastIndexOf(" ");
					generatedQuery = generatedQuery.Insert(lastIndex, parantheses.ToString());

					//clear the generated string so far
					sbuild.Remove(0, sbuild.Length);

					//add the new string
					sbuild.Append(generatedQuery);
				}

				//add the JOINS
				for (int i = 0; i < criteria.JoinCriteriaConditions.Length; i++)
				{
					switch (criteria.JoinCriteriaConditions[i].Join)
					{
						case JoinType.Inner:
							sbuild.Append(" Inner Join ");
							break;

						case JoinType.Left:
							sbuild.Append(" Left Join ");
							break;

						case JoinType.Right:
							sbuild.Append(" Right Join ");
							break;
					}

					sbuild.Append(SqlGenerator.GetTableName(DatabaseServer.Access, criteria.JoinCriteriaConditions[i].ForeignKeyFieldTableName) + " ON " +
					              SqlGenerator.GetTableName(DatabaseServer.Access, criteria.JoinCriteriaConditions[i].PrimaryKeyFieldTableName) + "." +
					              criteria.JoinCriteriaConditions[i].PrimaryKey.fieldName + "=" +
					              SqlGenerator.GetTableName(DatabaseServer.Access, criteria.JoinCriteriaConditions[i].Criteria.TableName) + "." +
					              criteria.JoinCriteriaConditions[i].ForeignKey.fieldName);

					if (parantheses != null)
					{
						sbuild.Append(")");
					}
				}

				//add conditions
				if (criteria.CriteriaConditions.Length > 0)
				{
					sbuild.Append(" WHERE ");
					appendWhere = true;
					sbuild.Append(GenerateCondition(criteria.TableName, criteria.CriteriaConditions, ref sbuild, ref listParameters));
				}

				for (int i = 0; i < criteria.JoinCriteriaConditions.Length; i++)
				{
					if ((i == 0) && (appendWhere == false))
					{
						sbuild.Append(" WHERE ");
					}

					if (criteria.JoinCriteriaConditions[i].Criteria.CriteriaConditions.Length > 0)
					{
						if (sbuild.ToString().EndsWith("WHERE "))
						{
							sbuild.Append(GenerateCondition(criteria.JoinCriteriaConditions[i].Criteria.TableName, criteria.JoinCriteriaConditions[i].Criteria.CriteriaConditions,
							                                ref sbuild, ref listParameters));
						}
						else
						{
							sbuild.Append(" AND " +
							              GenerateCondition(criteria.JoinCriteriaConditions[i].Criteria.TableName, criteria.JoinCriteriaConditions[i].Criteria.CriteriaConditions,
							                                ref sbuild, ref listParameters));
						}
					}
				}

				//checks for where and and
				if (sbuild.ToString().EndsWith(" WHERE "))
				{
					sbuild.Remove(sbuild.Length - WHERE_FIELD_LENGTH, WHERE_FIELD_LENGTH);
				}

				execQuery.Query = sbuild.ToString();
				IDataParameter[] pmc = new IDataParameter[listParameters.Count];
				listParameters.CopyTo(pmc);
				execQuery.Parameters = pmc;

				return execQuery;
			}
			catch (Exception ex)
			{
				throw ex;
			}
			finally
			{
				if (listParameters != null)
				{
					listParameters.Clear();
					listParameters = null;
				}
			}
		}

		/// <summary>
		///     Generates a SELECT query
		/// </summary>
		/// <param name="criteria"></param>
		/// <returns></returns>
		internal ExecutionQuery GenerateWithoutJoin(QueryCriteria criteria)
		{
			ISqlGenerator isql = null;
			List<IDataParameter> listParameters = null;
			ExecutionQuery execQuery;

			try
			{
				listParameters = new List<IDataParameter>();
				StringBuilder sbuild = new StringBuilder();

				execQuery = new ExecutionQuery();

				if (generatorType == QueryCriteriaGeneratorType.Select)
				{
					execQuery = SqlGenerator.GenerateSelectQuery(DatabaseServer.Access, criteria);
				}
				else if (generatorType == QueryCriteriaGeneratorType.Update)
				{
					execQuery = SqlGenerator.GenerateUpdateQuery(DatabaseServer.Access, criteria.TableName, criteria.Fields, false);
				}
				else if (generatorType == QueryCriteriaGeneratorType.Delete)
				{
					execQuery = SqlGenerator.GenerateDeleteQuery(DatabaseServer.Access, criteria.TableName);
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
				isql = DataFactory.InitializeSqlGenerator(DatabaseServer.Access);

				//append where clause
				sbuild.Append(" WHERE ");

				//generate the condition based on criteria
				string condition = GenerateCondition(criteria.TableName, criteria.CriteriaConditions, ref sbuild, ref listParameters);

				//more checks

				if (sbuild.ToString().EndsWith(" WHERE ") && condition.StartsWith(" ORDER BY "))
				{
					if (condition.StartsWith(" ORDER BY"))
					{
						sbuild.Remove(sbuild.Length - WHERE_FIELD_LENGTH, WHERE_FIELD_LENGTH);
					}
				}

				sbuild.Append(condition);

				//last check to prevent invalid sql queries
				if (sbuild.ToString().EndsWith(" WHERE "))
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
			catch (Exception ex)
			{
				throw ex;
			}
			finally
			{
				if (listParameters != null)
				{
					listParameters.Clear();
					listParameters = null;
				}
			}
		}

		/// <summary>
		///     Generates the sql query condition
		/// </summary>
		/// <param name="tableName">Name of the datbase table</param>
		/// <param name="conditions">Criteria conditions </param>
		/// <param name="sbSqlHeader">StringBuilder which contains the SELECT part of the sql query build so far</param>
		/// <param name="listParameters">List with used parameters</param>
		/// <returns></returns>
		internal string GenerateCondition(string tableName, CriteriaCondition[] conditions, ref StringBuilder sbSqlHeader, ref List<IDataParameter> listParameters)
		{
			//keeps the order by part of the query
			StringBuilder sbOrderByCriteria = new StringBuilder();
			//holds the generated query
			StringBuilder sbuild = new StringBuilder();
			ISqlGenerator isql = null;

			//temporary vars
			string fieldName = string.Empty;
			int index = -1;
			string tempString = string.Empty;

			List<string> listParameterNames = null;

			try
			{
				listParameterNames = new List<string>();

				//initialize generator
				isql = DataFactory.InitializeSqlGenerator(DatabaseServer.Access);

				//generate conditions
				for (int i = 0; i < conditions.Length; i++)
				{
					//check if we generate "AND" operator
					if (i > 0)
					{
						if ((conditions[i].CriteriaOperator != CriteriaOperator.OrderBy) && (conditions[i].CriteriaOperator != CriteriaOperator.Or) &&
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

							IDataParameter paramBetweenFirst = DataConvertor.ConvertToDataParameter(DatabaseServer.Access, tableName, field, ref listParameterNames);
							IDataParameter paramBetweenSecond = DataConvertor.ConvertToDataParameter(DatabaseServer.Access, tableName, field, ref listParameterNames);

							paramBetweenFirst.ParameterName = paramBetweenFirst.ParameterName + "First";
							paramBetweenSecond.ParameterName = paramBetweenSecond.ParameterName + "Second";

							//set the parameter's value and add it to the list
							paramBetweenFirst.Value = conditions[i].Values[0];
							listParameters.Add(paramBetweenFirst);

							sbuild.Append(" " + SqlGenerator.GetTableName(DatabaseServer.Access, tableName) + "." + conditions[i].Field.fieldName + " BETWEEN " +
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
							IDataParameter paramDifferent = DataConvertor.ConvertToDataParameter(DatabaseServer.Access, tableName, field, ref listParameterNames);
							listParameters.Add(paramDifferent);
							sbuild.Append(" " + SqlGenerator.GetTableName(DatabaseServer.Access, tableName) + "." + field.fieldName + "<>" + isql.GetValue(paramDifferent));
							break;

						case CriteriaOperator.Like:
							field.fieldValue = "*" + conditions[i].Values[0] + "*";
							IDataParameter paramLike = DataConvertor.ConvertToDataParameter(DatabaseServer.Access, tableName, field, ref listParameterNames);
							listParameters.Add(paramLike);
							sbuild.Append(" " + SqlGenerator.GetTableName(DatabaseServer.Access, tableName) + "." + field.fieldName + " LIKE " + isql.GetValue(paramLike));
							break;

						case CriteriaOperator.LikeEnd:
							field.fieldValue = "*" + conditions[i].Values[0];
							IDataParameter paramLikeEnd = DataConvertor.ConvertToDataParameter(DatabaseServer.Access, tableName, field, ref listParameterNames);
							listParameters.Add(paramLikeEnd);
							sbuild.Append(" " + SqlGenerator.GetTableName(DatabaseServer.Access, tableName) + "." + field.fieldName + " LIKE " + isql.GetValue(paramLikeEnd));
							break;

						case CriteriaOperator.LikeStart:
							field.fieldValue = conditions[i].Values[0] + "*";
							IDataParameter paramLikeStart = DataConvertor.ConvertToDataParameter(DatabaseServer.Access, tableName, field, ref listParameterNames);
							listParameters.Add(paramLikeStart);
							sbuild.Append(" " + SqlGenerator.GetTableName(DatabaseServer.Access, tableName) + "." + field.fieldName + " LIKE " + isql.GetValue(paramLikeStart));
							break;

						case CriteriaOperator.Equality:
							field.fieldValue = conditions[i].Values[0];
							IDataParameter paramEquality = DataConvertor.ConvertToDataParameter(DatabaseServer.Access, tableName, field, ref listParameterNames);
							listParameters.Add(paramEquality);
							sbuild.Append(" " + SqlGenerator.GetTableName(DatabaseServer.Access, tableName) + "." + field.fieldName + "=" + isql.GetValue(paramEquality));
							break;

						case CriteriaOperator.IsNull:
							sbuild.Append(" " + SqlGenerator.GetTableName(DatabaseServer.Access, tableName) + "." + field.fieldName + " is null");
							break;

						case CriteriaOperator.IsNotNull:
							sbuild.Append(" " + SqlGenerator.GetTableName(DatabaseServer.Access, tableName) + "." + field.fieldName + " is not null");
							break;

						case CriteriaOperator.Or:
							sbuild.Append(" OR");
							break;

						case CriteriaOperator.Smaller:
							field.fieldValue = conditions[i].Values[0];

							IDataParameter paramSmaller = DataConvertor.ConvertToDataParameter(DatabaseServer.Access, tableName, field, ref listParameterNames);
							listParameters.Add(paramSmaller);

							sbuild.Append(" " + SqlGenerator.GetTableName(DatabaseServer.Access, tableName) + "." + field.fieldName + " < " + isql.GetValue(paramSmaller));
							break;

						case CriteriaOperator.SmallerOrEqual:
							field.fieldValue = conditions[i].Values[0];

							IDataParameter paramSmallerOrEqual = DataConvertor.ConvertToDataParameter(DatabaseServer.Access, tableName, field, ref listParameterNames);
							listParameters.Add(paramSmallerOrEqual);

							sbuild.Append(" " + SqlGenerator.GetTableName(DatabaseServer.Access, tableName) + "." + field.fieldName + " <= " + isql.GetValue(paramSmallerOrEqual));
							break;

						case CriteriaOperator.Higher:
							field.fieldValue = conditions[i].Values[0];

							IDataParameter paramHigher = DataConvertor.ConvertToDataParameter(DatabaseServer.Access, tableName, field, ref listParameterNames);
							listParameters.Add(paramHigher);

							sbuild.Append(" " + SqlGenerator.GetTableName(DatabaseServer.Access, tableName) + "." + field.fieldName + " > " + isql.GetValue(paramHigher));
							break;

						case CriteriaOperator.HigherOrEqual:
							field.fieldValue = conditions[i].Values[0];

							IDataParameter paramHigherOrEqual = DataConvertor.ConvertToDataParameter(DatabaseServer.Access, tableName, field, ref listParameterNames);
							listParameters.Add(paramHigherOrEqual);

							sbuild.Append(" " + SqlGenerator.GetTableName(DatabaseServer.Access, tableName) + "." + field.fieldName + " >= " + isql.GetValue(paramHigherOrEqual));
							break;

						case CriteriaOperator.OrderBy:
							if (sbOrderByCriteria.Length == 0)
							{
								//add the operator for the first criteria
								sbOrderByCriteria.Append("ORDER BY " + SqlGenerator.GetTableName(DatabaseServer.Access, tableName) + "." + field.fieldName + " " +
								                         conditions[i].Values[0]);
							}
							else
							{
								//add "," for the subsequent criterias
								sbOrderByCriteria.Append(", " + SqlGenerator.GetTableName(DatabaseServer.Access, tableName) + "." + field.fieldName + " " + conditions[i].Values[0]);
							}
							break;

							//NOTE :  DISTICT requires modification of the sql header. Also DISTINCT clause requires that
							//the distinct field should be the first one in the list.
						case CriteriaOperator.Distinct:
							//get the field
							fieldName = SqlGenerator.GetTableName(DatabaseServer.Access, tableName) + "." + conditions[i].Field.fieldName;

							//we have the field name now search for it in the fields list
							index = sbSqlHeader.ToString().IndexOf(fieldName);

							//now remove the field from the list.
							if (index == -1)
							{
								throw new ArgumentException("Invalid Distinct clause");
							}

							tempString = sbSqlHeader.ToString();

							tempString = tempString.Remove(index, fieldName.Length);

							//add it at the beginning of the select
							tempString = tempString.Insert(SELECT_FIELD_LENGTH,
							                               " distinct " + SqlGenerator.GetTableName(DatabaseServer.Access, tableName) + "." + conditions[i].Field.fieldName);

							sbSqlHeader.Remove(0, sbSqlHeader.Length);
							sbSqlHeader.Append(tempString);
							break;

							//NOTE: MAX fields must be after SELECT statement
						case CriteriaOperator.Max:
							//get the field
							fieldName = SqlGenerator.GetTableName(DatabaseServer.Access, tableName) + "." + conditions[i].Field.fieldName;

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
							tempString = tempString.Insert(SELECT_FIELD_LENGTH,
							                               " max(" + SqlGenerator.GetTableName(DatabaseServer.Access, tableName) + "." + conditions[i].Field.fieldName + ")");

							sbSqlHeader.Remove(0, sbSqlHeader.Length);
							sbSqlHeader.Append(tempString);
							break;

							//NOTE: MIN fields must be after SELECT statement
						case CriteriaOperator.Min:
							//get the field
							fieldName = SqlGenerator.GetTableName(DatabaseServer.Access, tableName) + "." + conditions[i].Field.fieldName;

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
							tempString = tempString.Insert(SELECT_FIELD_LENGTH,
							                               " min(" + SqlGenerator.GetTableName(DatabaseServer.Access, tableName) + "." + conditions[i].Field.fieldName + ")");

							sbSqlHeader.Remove(0, sbSqlHeader.Length);
							sbSqlHeader.Append(tempString);
							break;

							//NOTE: COUNT fields must be after SELECT statement
						case CriteriaOperator.Count:
							//get the field
							fieldName = SqlGenerator.GetTableName(DatabaseServer.Access, tableName) + "." + conditions[i].Field.fieldName;

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
							tempString = tempString.Insert(SELECT_FIELD_LENGTH,
							                               " count(" + SqlGenerator.GetTableName(DatabaseServer.Access, tableName) + "." + conditions[i].Field.fieldName + ")");

							sbSqlHeader.Remove(0, sbSqlHeader.Length);
							sbSqlHeader.Append(tempString);
							break;
					}
				}

				//last check to prevent invalid sql queries
				//conditions remove the "WHERE".
				if (sbuild.ToString().EndsWith(" WHERE "))
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
			catch (Exception ex)
			{
				throw ex;
			}
			finally
			{
				if (listParameterNames != null)
				{
					listParameterNames.Clear();
					listParameterNames = null;
				}
			}
		}
	}
}