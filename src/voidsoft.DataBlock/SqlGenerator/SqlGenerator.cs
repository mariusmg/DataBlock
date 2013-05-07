/*
      file : SqlGenerator.cs
description: SQL generator for supported RDBMs. 
   
   (c) 2004 - 2007 Marius Gheorghe. All rights reserved.
*/

using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace voidsoft.DataBlock
{
	/// <summary>
	///     This is the SqlGenerator which generates sql queries for the specified
	///     database server.
	/// </summary>
	public class SqlGenerator
	{
		//represents a foreign key place holder value
		internal const string FOREIGN_KEY_PLACEHOLDER_VALUE = "???";

		private const string COMMA = ",";

		/// <summary>
		///     Generates a query based on a QueryCriteria. This function generates only the
		///     query header without the citeria
		/// </summary>
		/// <param name="database">Database type for which we generate the query</param>
		/// <param name="criteria">QueryCriteria based on which the query is generated</param>
		/// <returns>Generated ExecutionQuery</returns>
		public ExecutionQuery GenerateSelectQuery(DatabaseServer database, QueryCriteria criteria)
		{
			StringBuilder sqlBuilder = new StringBuilder();

			DataFactory factory = new DataFactory();

			ISqlGenerator isql = factory.InitializeSqlGenerator(database);

			sqlBuilder.Append(" SELECT ");

			//add field's name for the criteria mai9n table
			for (int i = 0; i < criteria.Fields.Length; i++)
			{
				sqlBuilder.Append(" " + GetTableName(database, criteria.TableName) + "." + criteria.Fields[i].fieldName);

				//check for alias
				for (int j = 0; j < criteria.Aliases.Length; j++)
				{
					if (criteria.Fields[i].fieldName == criteria.Aliases[j].fieldName && criteria.TableName == criteria.Aliases[j].tableName)
					{
						sqlBuilder.Append(" AS " + criteria.Aliases[j].aliasName);
						break;
					}
				}

				if (i != criteria.Fields.Length - 1)
				{
					sqlBuilder.Append(" ,");
				}
			}

			//check if the criteria has join criteria conditions. If it has then
			//add comma.
			if (criteria.JoinCriteriaConditions.Length > 0)
			{
				sqlBuilder.Append(" , ");
			}

			//now add the joins fields
			for (int i = 0; i < criteria.JoinCriteriaConditions.Length; i++)
			{
				for (int j = 0; j < criteria.JoinCriteriaConditions[i].Criteria.Fields.Length; j++)
				{
					sqlBuilder.Append(GetTableName(DatabaseServer.PostgreSql, criteria.JoinCriteriaConditions[i].Criteria.TableName) + "." + criteria.JoinCriteriaConditions[i].Criteria.Fields[j].fieldName);

					if (criteria.JoinCriteriaConditions[i].Criteria.Aliases.Length > 0)
					{
						for (int x = 0; x < criteria.JoinCriteriaConditions[i].Criteria.Aliases.Length; x++)
						{
							if (criteria.JoinCriteriaConditions[i].Criteria.Fields[j].fieldName == criteria.JoinCriteriaConditions[i].Criteria.Aliases[x].fieldName)
							{
								sqlBuilder.Append(" AS " + criteria.JoinCriteriaConditions[i].Criteria.Aliases[x].aliasName);
								break;
							}
						}
					}

					sqlBuilder.Append(COMMA);
				}
			}

			string temp = sqlBuilder.ToString().TrimEnd();

			//remove the last comma if it exists
			if (temp.EndsWith(COMMA))
			{
				temp = temp.Remove(temp.Length - COMMA.Length, COMMA.Length);

				sqlBuilder.Remove(0, sqlBuilder.Length);

				sqlBuilder.Append(temp);
			}

			sqlBuilder.Append(" FROM " + GetTableName(database, criteria.TableName));

			ExecutionQuery execQuery = new ExecutionQuery();
			execQuery.Query = sqlBuilder.ToString();
			return execQuery;
		}

		/// <summary>
		///     Generates a SELECT query
		/// </summary>
		/// <param name="database">Database server type</param>
		/// <param name="mainTable">TableMetadata based on which we generate the query</param>
		/// <param name="generateConditionByPrimaryKey">Flag used to know if we generate a condition by the primary key field</param>
		/// <returns>Returns the generated SELECT ExecutionQuery</returns>
		public ExecutionQuery GenerateSelectQuery(DatabaseServer database, TableMetadata mainTable, bool generateConditionByPrimaryKey)
		{
			return GenerateSelectQuery(database, mainTable.TableName, mainTable.TableFields, generateConditionByPrimaryKey);
		}

		/// <summary>
		///     Generates a SELECT query
		/// </summary>
		/// <param name="database">Database server type</param>
		/// <param name="tableName">Name of the table</param>
		/// <param name="fields">List of DatabaseFields based on which we generate the condition</param>
		/// <param name="generateConditionByPrimaryKey">Flag used to know if we generate a condition by the primary key field</param>
		/// <returns>Returns the SELECT query</returns>
		public ExecutionQuery GenerateSelectQuery(DatabaseServer database, string tableName, DatabaseField[] fields, bool generateConditionByPrimaryKey)
		{
			ExecutionQuery execQuery;

			DataConvertor converter = new DataConvertor();

			DataFactory factory = new DataFactory();

			List<IDataParameter> listParameter = new List<IDataParameter>();

			ISqlGenerator isql = factory.InitializeSqlGenerator(database);

			StringBuilder sbuild = new StringBuilder();

			sbuild.Append("SELECT ");

			//separate code paths for code generation depending on the "WHERE" condition

			//generate condition by primary key
			if (generateConditionByPrimaryKey)
			{

				for (int i = 0; i < fields.Length; i++)
				{
					if (!fields[i].isPrimaryKey)
					{
						if (i == fields.Length - 1)
						{
							sbuild.Append(GetTableName(database, tableName) + "." + fields[i].fieldName);
						}
						else
						{
							sbuild.Append(GetTableName(database, tableName) + "." + fields[i].fieldName + ",");
						}
					}
				}
				sbuild.Append(" FROM " + GetTableName(database, tableName));

				//generate "where" condition
				bool isFirst = true;

				for (int i = 0; i < fields.Length; i++)
				{
					if (fields[i].isPrimaryKey)
					{
						IDataParameter param = converter.ConvertToDataParameter(database, GetTableName(database, tableName), fields[i]);

						if (isFirst)
						{
							sbuild.Append(" WHERE " + GetTableName(database, tableName) + "." + fields[i].fieldName + isql.GetValueWithComparationOperator(param));
							isFirst = false;
							listParameter.Add(param);
						}
						else
						{
							sbuild.Append(" AND " + GetTableName(database, tableName) + "." + fields[i].fieldName + isql.GetValueWithComparationOperator(param));
							listParameter.Add(param);
						}
					}
				}

				execQuery = new ExecutionQuery();
				execQuery.Query = sbuild.ToString();
				IDataParameter[] pmc = new IDataParameter[listParameter.Count];
				listParameter.CopyTo(pmc);

				return execQuery;
			}


			for (int i = 0; i < fields.Length; i++)
			{
				if (i == fields.Length - 1)
				{
					sbuild.Append(" " + GetTableName(database, tableName) + "." + fields[i].fieldName);
				}
				else
				{
					sbuild.Append(" " + GetTableName(database, tableName) + "." + fields[i].fieldName + ",");
				}
			}

			sbuild.Append(" FROM " + tableName);

			execQuery = new ExecutionQuery();
			execQuery.Query = sbuild.ToString();
			IDataParameter[] pmca = new IDataParameter[listParameter.Count];
			listParameter.CopyTo(pmca);

			return execQuery;

		}

		/// <summary>
		///     Generates a SELECT query
		/// </summary>
		/// <param name="database">Database server type</param>
		/// <param name="mainTable">TableMetadata from which we generate the sql query</param>
		/// <param name="conditionalFields">List of conditional DatbaseFields</param>
		/// <returns>Returns the SELECT query</returns>
		public ExecutionQuery GenerateSelectQuery(DatabaseServer database, TableMetadata mainTable, params DatabaseField[] conditionalFields)
		{
			return GenerateSelectQuery(database, mainTable.TableName, mainTable.TableFields, conditionalFields);
		}

		/// <summary>
		///     Generates a SELECT query
		/// </summary>
		/// <param name="database">Database server type</param>
		/// <param name="tableName">Table's name</param>
		/// <param name="fields">DatabaseFields from which the query is generated</param>
		/// <param name="conditionalFields">Fields from which the condition is generated</param>
		/// <returns>Returns the SELECT query</returns>
		public ExecutionQuery GenerateSelectQuery(DatabaseServer database, string tableName, DatabaseField[] fields, params DatabaseField[] conditionalFields)
		{
			ISqlGenerator isql = null;

			StringBuilder sbuild = null;

			ExecutionQuery execQuery;

			DataConvertor converter = new DataConvertor();

			DataFactory factory = new DataFactory();

			List<IDataParameter> listParameters = new List<IDataParameter>();

			if (conditionalFields != null && conditionalFields.Length == 0)
			{
				return GenerateSelectQuery(database, tableName, fields, false);
			}


			sbuild = new StringBuilder();
			isql = factory.InitializeSqlGenerator(database);

			sbuild.Append(" SELECT ");

			for (int i = 0; i < fields.Length; i++)
			{
				if (i == fields.Length - 1)
				{
					sbuild.Append(" " + GetTableName(database, tableName) + "." + fields[i].fieldName);
				}
				else
				{
					sbuild.Append(" " + GetTableName(database, tableName) + "." + fields[i].fieldName + ",");
				}
			}

			sbuild.Append(" FROM " + GetTableName(database, tableName));

			if (conditionalFields != null)
			{
				sbuild.Append(" WHERE ");

				//generate conditions
				for (int i = 0; i < conditionalFields.Length; i++)
				{
					IDataParameter param = converter.ConvertToDataParameter(database, tableName, conditionalFields[i]);

					if (i == conditionalFields.Length - 1)
					{
						sbuild.Append(GetTableName(database, tableName) + "." + conditionalFields[i].fieldName + isql.GetValueWithComparationOperator(param));
					}
					else
					{
						sbuild.Append(GetTableName(database, tableName) + "." + conditionalFields[i].fieldName + isql.GetValueWithComparationOperator(param) + " AND ");
					}

					listParameters.Add(param);
				}
			}

			execQuery = new ExecutionQuery();

			execQuery.Query = sbuild.ToString();
			IDataParameter[] pmca = new IDataParameter[listParameters.Count];
			listParameters.CopyTo(pmca);
			execQuery.Parameters = pmca;
			return execQuery;
		}

		/// <summary>
		///     Generates a SELECT(*) query
		/// </summary>
		/// <param name="database">Database server dataType</param>
		/// <param name="tableName">Name of the table</param>
		/// <param name="conditionalFields">DatabaseFields from which the condition is generated</param>
		/// <returns>Returns the SELECT query</returns>
		public ExecutionQuery GenerateSelectQuery(DatabaseServer database, string tableName, params DatabaseField[] conditionalFields)
		{
			ISqlGenerator isql = null;
			ExecutionQuery execQuery;
			
			DataConvertor converter = new DataConvertor();

			DataFactory factory = new DataFactory();

			List<IDataParameter>  listParameters = new List<IDataParameter>();

			StringBuilder sbuild = new StringBuilder();
			sbuild.Append(" SELECT * FROM " + GetTableName(database, tableName));

			if (conditionalFields.Length > 0)
			{
				sbuild.Append(" WHERE ");
			}
			else
			{
				execQuery = new ExecutionQuery();
				execQuery.Query = sbuild.ToString();

				return execQuery;
			}

			isql = factory.InitializeSqlGenerator(database);

			if (conditionalFields != null)
			{
				for (int i = 0; i < conditionalFields.Length; i++)
				{
					IDataParameter param = converter.ConvertToDataParameter(database, tableName, conditionalFields[i]);

					if (i == conditionalFields.Length - 1)
					{
						sbuild.Append(GetTableName(database, tableName) + "." + conditionalFields[i].fieldName + isql.GetValueWithAttributionOperator(param));
					}
					else
					{
						sbuild.Append(GetTableName(database, tableName) + "." + conditionalFields[i].fieldName + isql.GetValueWithAttributionOperator(param) + ",");
					}

					listParameters.Add(param);
				}
			}

			execQuery = new ExecutionQuery();
			execQuery.Query = sbuild.ToString();
			IDataParameter[] pmca = new IDataParameter[listParameters.Count];
			listParameters.CopyTo(pmca);
			execQuery.Parameters = pmca;
			return execQuery;
		}

		/// <summary>
		///     Generates the select paginated query.
		/// </summary>
		/// <param name="database">The database.</param>
		/// <param name="metadata">The metadata.</param>
		/// <param name="numberOfItems">The number of items.</param>
		/// <param name="pageNumber">The page number.</param>
		/// <returns></returns>
		public ExecutionQuery GenerateSelectPaginatedQuery(DatabaseServer database, TableMetadata metadata, int numberOfItems, int pageNumber)
		{

			DataFactory factory = new DataFactory();
			ISqlGenerator isql = factory.InitializeSqlGenerator(database);
			string result = isql.GeneratePaginatedQuery(metadata, numberOfItems, pageNumber);

			return new ExecutionQuery(result, new IDataParameter[0]);
		}

		/// <summary>
		///     Generates a INSERT query for a single table
		/// </summary>
		/// <param name="database">Database server</param>
		/// <param name="fields">Array of DatabaseFields from which the query is generated</param>
		/// <param name="tableName">Name of the table</param>
		/// <returns>The generated query</returns>
		public ExecutionQuery GenerateInsertQuery(DatabaseServer database, DatabaseField[] fields, string tableName)
		{
			ISqlGenerator isql = null;

			ExecutionQuery execQuery;

			DataConvertor converter = new DataConvertor();

			DataFactory factory = new DataFactory();

			List<IDataParameter> listParameters = new List<IDataParameter>();

			StringBuilder sbuild = new StringBuilder();

			isql = factory.InitializeSqlGenerator(database);

			sbuild.Append("INSERT INTO " + GetTableName(database, tableName) + "(");

			for (int i = 0; i < fields.Length; i++)
			{
				//skip the PK field if is autogenerated.
				if (fields[i].isPrimaryKey && fields[i].isValueAutogenerated)
				{
					continue;
				}

				//check if this is the last field
				if (i == fields.Length - 1)
				{
					sbuild.Append(fields[i].fieldName);
				}
				else
				{
					sbuild.Append(fields[i].fieldName + ",");
				}
			}

			sbuild.Append(") VALUES(");

			//generate the execution query

			for (int i = 0; i < fields.Length; i++)
			{
				//skip the PK field if is autogenerated.
				if (fields[i].isPrimaryKey && fields[i].isValueAutogenerated)
				{
					continue;
				}

				//check if this is the last field
				if (i == fields.Length - 1)
				{
					//check for PK placeholder
					if (fields[i].fieldValue != null && fields[i].fieldValue.ToString() == FOREIGN_KEY_PLACEHOLDER_VALUE)
					{
						sbuild.Append(isql.GetValue(fields[i].fieldType, fields[i].fieldValue) + ")");
					}
					else
					{
						//add the name of the field
						sbuild.Append(factory.GetParameterChar(database) + fields[i].fieldName + ")");

						//add the corresponding parameter.
						IDataParameter[] iparams = converter.ConvertToDataParameter(database, fields[i]);
						listParameters.Add(iparams[0]);
					}
				}
				else
				{
					//check for PK placeholder
					if (fields[i].fieldValue != null && fields[i].fieldValue.ToString() == FOREIGN_KEY_PLACEHOLDER_VALUE)
					{
						sbuild.Append(isql.GetValue(fields[i].fieldType, fields[i].fieldValue) + ",");
					}
					else
					{
						//add the name of the field
						sbuild.Append(factory.GetParameterChar(database) + fields[i].fieldName + ",");

						//add the coresponding parameter.
						IDataParameter[] iparams = converter.ConvertToDataParameter(database, fields[i]);
						listParameters.Add(iparams[0]);
					}
				}
			}


			execQuery = new ExecutionQuery();

			IDataParameter[] par = new IDataParameter[listParameters.Count];
			listParameters.CopyTo(par);
			execQuery.Parameters = par;
			execQuery.Query = sbuild.ToString();

			return execQuery;
		}

		/// <summary>
		///     Generates a INSERT query for a single table
		/// </summary>
		/// <param name="database">Database server</param>
		/// <param name="entity">TableMetadata from which the query is generated</param>
		/// <returns>The generated query</returns>
		public ExecutionQuery GenerateInsertQuery(DatabaseServer database, TableMetadata entity)
		{
			////HACK : check here if the current entity has a 

			//List<ChildTableRelation> listChilds = MetadataManager.GetChildRelations(entity);

			//foreach (ChildTableRelation child in listChilds)
			//{
			//    DatabaseField field = entity.GetField(child.ForeignKeyName);

			//    if (field.fieldValue == null)
			//    {
			//        field.fieldValue = "???";
			//    }
			//}

			return GenerateInsertQuery(database, entity.TableFields, entity.TableName);
		}

		/// <summary>
		///     Generates a DELETE query for a single table
		/// </summary>
		/// <param name="database">Database server type</param>
		/// <param name="table">TableMetadata from which we generate the statement</param>
		/// <param name="generateConditionOnlyByPrimaryKey">Boolean flag to know if we generate the "WHERE" clause by the primary key or by all the fields </param>
		/// <returns>Returns the DELETE query</returns>
		public ExecutionQuery GenerateDeleteQuery(DatabaseServer database, TableMetadata table, bool generateConditionOnlyByPrimaryKey)
		{
			ISqlGenerator isql = null;
			StringBuilder sbuild = null;
			ExecutionQuery execQuery;
			List<IDataParameter> listParameters = null;

			DataConvertor converter = new DataConvertor();

			DataFactory factory = new DataFactory();

			//generate condition by all the DatabaseFields.
			if (!generateConditionOnlyByPrimaryKey)
			{
				return GenerateDeleteQuery(database, table.TableName, table.TableFields);
			}


			//generate conditions by all the fields.
			listParameters = new List<IDataParameter>();
			sbuild = new StringBuilder();

			isql = factory.InitializeSqlGenerator(database);

			sbuild.Append(" DELETE ");

			if (database == DatabaseServer.Access)
			{
				sbuild.Append(" * ");
			}

			sbuild.Append(" FROM " + GetTableName(database, table.TableName) + " WHERE ");

			//flag used if we have multiple primary keys
			bool isFirst = true;

			for (int i = 0; i < table.TableFields.Length; i++)
			{
				if (table.TableFields[i].isPrimaryKey)
				{
					IDataParameter[] parameter = converter.ConvertToDataParameter(database, table.TableFields[i]);
					listParameters.Add(parameter[0]);

					if (isFirst)
					{
						sbuild.Append(table.TableFields[i].fieldName + isql.GetValueWithComparationOperator(parameter[0]));
						isFirst = false;
					}
					else
					{
						sbuild.Append(" AND " + table.TableFields[i].fieldName + isql.GetValueWithComparationOperator(parameter[0]));
					}
				}
			}


			execQuery = new ExecutionQuery();

			IDataParameter[] par = new IDataParameter[listParameters.Count];
			listParameters.CopyTo(par);
			execQuery.Parameters = par;
			execQuery.Query = sbuild.ToString();

			return execQuery;
		}

		/// <summary>
		///     Generates a DELETE query for a single table
		/// </summary>
		/// <param name="database">Database server type</param>
		/// <param name="table">TableMetadata from which we generate the statement</param>
		/// <param name="conditionalFields">DatabaseFields from which the condition is generated</param>
		/// <returns>Returns the DELETE query</returns>
		public ExecutionQuery GenerateDeleteQuery(DatabaseServer database, TableMetadata table, params DatabaseField[] conditionalFields)
		{
			return GenerateDeleteQuery(database, table.TableName, conditionalFields);
		}

		/// <summary>
		///     Generates a DELETE query for a single table
		/// </summary>
		/// <param name="database">Database server type </param>
		/// <param name="tableName">Name of the table</param>
		/// <param name="conditionalField">DatabaseFields array from which we generate the statement</param>
		/// <returns>Returns the DELETE query</returns>
		public ExecutionQuery GenerateDeleteQuery(DatabaseServer database, string tableName, params DatabaseField[] conditionalField)
		{
			ISqlGenerator isql = null;

			ExecutionQuery execQuery;

			DataConvertor converter = new DataConvertor();

			DataFactory factory = new DataFactory();

			List<IDataParameter> listParameters = new List<IDataParameter>();

			StringBuilder sbuild = new StringBuilder();

			isql = factory.InitializeSqlGenerator(database);

			sbuild.Append(" DELETE ");

			if (database == DatabaseServer.Access)
			{
				sbuild.Append(" * ");
			}

			sbuild.Append(" FROM " + GetTableName(database, tableName)); // + " WHERE ");

			bool isFirst = true;

			if (conditionalField.Length > 0)
			{
				sbuild.Append(" WHERE ");

				for (int i = 0; i < conditionalField.Length; i++)
				{
					IDataParameter[] parameter = converter.ConvertToDataParameter(database, conditionalField[i]);
					listParameters.Add(parameter[0]);

					if (isFirst)
					{
						sbuild.Append(conditionalField[i].fieldName + isql.GetValueWithComparationOperator(parameter[0]));
						isFirst = false;
					}
					else
					{
						sbuild.Append(" AND " + conditionalField[i].fieldName + isql.GetValueWithComparationOperator(parameter[0]));
					}
				}
			}


			execQuery = new ExecutionQuery();
			IDataParameter[] par = new IDataParameter[listParameters.Count];
			listParameters.CopyTo(par);
			execQuery.Parameters = par;
			execQuery.Query = sbuild.ToString();

			return execQuery;
		}

		/// <summary>
		///     Generates a UPDATE query for a single table
		/// </summary>
		/// <param name="database">Database server</param>
		/// <param name="tableName">Name of the table</param>
		/// <param name="fields">DatabaseField array from which the query is generated </param>
		/// <param name="generateConditionByPrimaryKey">Boolean flag which signals if a "WHERE" condition is added based on the primary key</param>
		/// <returns>The generated execution query</returns>
		public ExecutionQuery GenerateUpdateQuery(DatabaseServer database, string tableName, DatabaseField[] fields, bool generateConditionByPrimaryKey)
		{
			ISqlGenerator isql = null;
			ExecutionQuery execQuery;

			DataConvertor converter = new DataConvertor();

			DataFactory factory = new DataFactory();

			List<IDataParameter> listParameters = new List<IDataParameter>();

			StringBuilder sbuild = new StringBuilder();

			isql = factory.InitializeSqlGenerator(database);

			sbuild.Append(" UPDATE " + GetTableName(database, tableName) + " SET ");

			//generate update fields
			for (int i = 0; i < fields.Length; i++)
			{
				if (fields[i].isPrimaryKey)
				{
					continue;
				}

				if (i == fields.Length - 1)
				{
					IDataParameter[] parameters = converter.ConvertToDataParameter(database, fields[i]);
					sbuild.Append(" " + fields[i].fieldName + isql.GetValueWithAttributionOperator(parameters[0]));
					listParameters.Add(parameters[0]);
				}
				else
				{
					IDataParameter[] parameter = converter.ConvertToDataParameter(database, fields[i]);
					sbuild.Append(" " + fields[i].fieldName + isql.GetValueWithAttributionOperator(parameter[0]) + ",");
					listParameters.Add(parameter[0]);
				}
			}

			//flag used if we have more than a primary key field
			bool isFirst = true;

			//check if we generate condition by the primary key
			if (generateConditionByPrimaryKey)
			{
				//generate condition
				sbuild.Append(" WHERE ");

				for (int i = 0; i < fields.Length; i++)
				{
					//check if this is the first primary key
					if (fields[i].isPrimaryKey)
					{
						IDataParameter[] parameter = converter.ConvertToDataParameter(database, fields[i]);
						listParameters.Add(parameter[0]);

						if (isFirst)
						{
							sbuild.Append(fields[i].fieldName + isql.GetValueWithComparationOperator(parameter[0]));
							isFirst = false;
						}
						else
						{
							sbuild.Append("," + fields[i].fieldName + isql.GetValueWithComparationOperator(parameter[0]));
						}
					}
				}
			}


			execQuery = new ExecutionQuery();
			execQuery.Query = sbuild.ToString();

			IDataParameter[] prm = new IDataParameter[listParameters.Count];
			listParameters.CopyTo(prm);
			execQuery.Parameters = prm;

			return execQuery;
		}

		/// <summary>
		///     Generates a UPDATE query for a single table
		/// </summary>
		/// <param name="database">Database server type</param>
		/// <param name="table">TableMetadata from which we generate the statement.</param>
		/// <param name="generateConditionByPrimaryKey">Flag used to know if a condition is generated using the primary key</param>
		/// <returns>Returns the UPDATE query</returns>
		public ExecutionQuery GenerateUpdateQuery(DatabaseServer database, TableMetadata table, bool generateConditionByPrimaryKey)
		{
			return GenerateUpdateQuery(database, table.TableName, table.TableFields, generateConditionByPrimaryKey);
		}

		/// <summary>
		///     Generates a UPDATE query for a single table
		/// </summary>
		/// <param name="database">Database server type</param>
		/// <param name="tableName">Name of the table</param>
		/// <param name="skipConditionalFieldsFromUpdateList">Flag used to determine if the conditional fields are included in the list of the updated fields</param>
		/// <param name="conditionalField">DatabaseField from which the condition statement is generated</param>
		/// <param name="updateFields">DatabaseFields which will be updated</param>
		/// <returns>Returns the UPDATE query</returns>
		public ExecutionQuery GenerateUpdateQuery(DatabaseServer database, string tableName, bool skipConditionalFieldsFromUpdateList, DatabaseField conditionalField, params DatabaseField[] updateFields)
		{
			DatabaseField[] fields = new DatabaseField[1];
			fields[0] = conditionalField;
			return GenerateUpdateQuery(database, tableName, skipConditionalFieldsFromUpdateList, fields, updateFields);
		}

		/// <summary>
		///     Generates a UPDATE query for a single table
		/// </summary>
		/// <param name="database">Database server type</param>
		/// <param name="tableName">Name of the table</param>
		/// <param name="skipConditionalFieldsFromUpdateList">Flag used to determine if the conditional fields are included in the list of the updated fields</param>
		/// <param name="conditionalFields">Database fields from which the "WHERE" clause is generated</param>
		/// <param name="updatedFields">DatabaseFields which will be updated.</param>
		/// <returns>Returns the UPDATE query</returns>
		public ExecutionQuery GenerateUpdateQuery(DatabaseServer database, string tableName, bool skipConditionalFieldsFromUpdateList, DatabaseField[] conditionalFields, params DatabaseField[] updatedFields)
		{
			ISqlGenerator isql = null;
			ExecutionQuery execQuery;

			DataConvertor converter = new DataConvertor();

			DataFactory factory = new DataFactory();

			//check params
			if (updatedFields.Length == 0)
			{
				throw new ArgumentException("Invalid fields to update");
			}

			List<IDataParameter> listParameters = new List<IDataParameter>();

			isql = factory.InitializeSqlGenerator(database);

			StringBuilder sbuild = new StringBuilder();
			sbuild.Append("UPDATE " + GetTableName(database, tableName) + " SET ");

			bool isConditionalField = false;


			isql = factory.InitializeSqlGenerator(database);

			for (int i = 0; i < updatedFields.Length; i++)
			{
				//skip conditional fields.
				if (skipConditionalFieldsFromUpdateList)
				{
					isConditionalField = false;
					//check if this is a conditional field. If it is skip it...

					for (int j = 0; j < conditionalFields.Length; j++)
					{
						//check if it's the same field
						if (updatedFields[i].Equals(conditionalFields[j]))
						{
							isConditionalField = true;
							break;
						}
					}

					if (isConditionalField)
					{
						continue;
					}
				}

				IDataParameter[] parameter = converter.ConvertToDataParameter(database, updatedFields[i]);
				sbuild.Append("  " + updatedFields[i].fieldName + isql.GetValueWithAttributionOperator(parameter[0]) + " , ");
				listParameters.Add(parameter[0]);
			}

			//remove trailing
			sbuild.Remove(sbuild.Length - 2, 1);

			if (conditionalFields != null)
			{
				//generate conditon
				sbuild.Append(" WHERE ");

				for (int i = 0; i < conditionalFields.Length; i++)
				{
					IDataParameter[] parameter = converter.ConvertToDataParameter(database, conditionalFields[i]);
					listParameters.Add(parameter[0]);

					if (i == conditionalFields.Length - 1)
					{
						sbuild.Append(conditionalFields[i].fieldName + isql.GetValueWithComparationOperator(parameter[0]));
					}
					else
					{
						sbuild.Append(conditionalFields[i].fieldName + isql.GetValueWithComparationOperator(parameter[0]) + " AND ");
					}
				}
			}


			execQuery = new ExecutionQuery();
			execQuery.Query = sbuild.ToString();

			IDataParameter[] prm = new IDataParameter[listParameters.Count];
			listParameters.CopyTo(prm);
			execQuery.Parameters = prm;

			return execQuery;

		}

		/// <summary>
		///     Generates DELETE queries for the parent table and her related tables.
		/// </summary>
		/// <param name="database">Database server type</param>
		/// <param name="table">TableMetadata based on which we generate the query</param>
		/// <returns>List containing the queries</returns>
		public List<ExecutionQuery> GenerateMultipleDeleteQueries(DatabaseServer database, TableMetadata table)
		{
			GraphBuilder builder = null;
			List<ExecutionQuery> listQueries = null;

			try
			{
				builder = new GraphBuilder();
				listQueries = new List<ExecutionQuery>();

				//generate delete for the main table.
				listQueries.Add(GenerateDeleteQuery(database, table, true));

				//get the PK of the main table.
				DatabaseField primaryKeyField = table.GetPrimaryKeyField();

				//get the relation where out table is parent
				List<ParentTableRelation> listParent = MetadataManager.GetParentRelations(table);

				foreach (ParentTableRelation relation in listParent)
				{
					//check the cascade delete flag
					if (relation.CascadeDeleteChildren)
					{
						//reconstruct the foreign key.
						DatabaseField field = new DatabaseField(primaryKeyField.fieldType, relation.ForeignKeyName, false, false, primaryKeyField.fieldValue);
						listQueries.Add(GenerateDeleteQuery(database, GetTableName(database, relation.RelatedTableName), field));
					}
				}

				return listQueries;
			}
			catch
			{
				throw;
			}
		}

		/// <summary>
		///     Generates INSERT queries for the parent table and her related tables.
		/// </summary>
		/// <param name="database">Database server type</param>
		/// <param name="mainTable">TableMetadata based on which we generate the query</param>
		/// <returns>List containing the queries</returns>
		public List<ExecutionQuery> GenerateMultipleInsertQueries(DatabaseServer database, TableMetadata mainTable)
		{
			GraphBuilder builder = null;
			List<ExecutionQuery> listQueries = null;

			List<object> listGraph = null;
			List<string> listTableNames = null;

			try
			{
				//no deep recursion implementation yet.

				listQueries = new List<ExecutionQuery>();
				builder = new GraphBuilder();

				listGraph = new List<object>();
				listTableNames = new List<string>();

				builder.BuildGraphList(mainTable, ref listGraph, ref listTableNames);

				foreach (object var in listGraph)
				{
					if (var is TableMetadata)
					{
						ExecutionQuery execQuery = GenerateInsertQuery(database, (TableMetadata)var);
						listQueries.Add(execQuery);
					}
					else
					{
						ExecutionQuery execQuery = new ExecutionQuery(var.ToString(), null);
						listQueries.Add(execQuery);
					}
				}

				return listQueries;
			}
			finally
			{
				if (listGraph != null)
				{
					listGraph.Clear();
				}

				if (listTableNames != null)
				{
					listTableNames.Clear();
				}
			}
		}

		/// <summary>
		///     Generates syncronization queries for the parent table and her related tables.
		/// </summary>
		/// <param name="database">Database server type</param>
		/// <param name="mainTable">TableMetadata based on which we generate the queries</param>
		/// <returns>List containing the queries</returns>
		public List<ExecutionQuery> GenerateMultipleUpdateQueries(DatabaseServer database, TableMetadata mainTable)
		{
			List<ExecutionQuery> listQueries = null;
			GraphBuilder builder = null;

			try
			{
				listQueries = new List<ExecutionQuery>();
				builder = new GraphBuilder();

				List<object> listGraph = new List<object>();
				List<string> listTableNames = new List<string>();

				//generate 
				listQueries.Add(GenerateUpdateQuery(database, mainTable, true));

				//generate the deletes rows.

				TableMetadata[] removedChildTables = mainTable.RemovedData;

				for (int i = 0; i < removedChildTables.Length; i++)
				{
					//generate DELETE by the primary key
					try
					{
						//string foreigKeyName = builder.GetForeignKeyName(mappedObject, removedChildTables[i]);
						//DatabaseField fkField = removedChildTables[i].GetField(foreigKeyName);

						listQueries.Add(GenerateDeleteQuery(database, removedChildTables[i], true));
					}
					catch
					{
						continue;
					}
				}

				//generate the insert/update rows.
				TableMetadata[] attachedData = mainTable.AttachedData;

				List<TableMetadata> listData = builder.GetAttachedDataBasedOnRelationType(mainTable, true);

				string foreignKeyNsame = string.Empty;

				for (int i = 0; i < attachedData.Length; i++)
				{
					//check for update or insert

					DatabaseField field = MetadataManager.GetForeignKeyField(mainTable, attachedData[i]);

					//check the value of the FK to determine if it's insert or update

					//insert    
					if (field.fieldValue == null || field.fieldValue == DBNull.Value)
					{
						//set the value of the PK

						////set the value of the PK
						attachedData[i].SetFieldValue(field.fieldName, mainTable.GetPrimaryKeyField().fieldValue);

						listQueries.Add(GenerateInsertQuery(database, attachedData[i]));
					}
					else
					{
						//update
						listQueries.Add(GenerateUpdateQuery(database, attachedData[i], true));
					}
				}

				return listQueries;
			}
			catch
			{
				throw;
			}
		}

		/// <summary>
		///     Returns the proper name for a table's name to be used in a query.
		///     This is for used for table name's that have blank spaces in their names.
		/// </summary>
		/// <param name="database">Database server dataType for which we return the modified table name</param>
		/// <param name="tableName">Table Name</param>
		/// <returns>Returns the table's name</returns>
		internal string GetTableName(DatabaseServer database, string tableName)
		{
			//first remove the schema from the table's name
			if (tableName.IndexOf('.') > -1)
			{
				tableName = tableName.Substring(tableName.IndexOf('.') + 1);
			}

			int index = tableName.IndexOf(" ");

			if (index > -1)
			{
				switch (database)
				{
					case DatabaseServer.Access:
					case DatabaseServer.SqlServer:
					case DatabaseServer.PostgreSql:
						return "[" + tableName + "]";

					case DatabaseServer.MySQL:
						return "`" + tableName + "`";

					default:
						return tableName;
				}
			}
			else
			{
				return tableName;
			}
		}

		internal string GetTableName(string tableName)
		{
			//first remove the schema from the table's name
			if (tableName.IndexOf('.') > -1)
			{
				tableName = tableName.Substring(tableName.IndexOf('.') + 1);
			}

			return tableName;
		}
	}
}