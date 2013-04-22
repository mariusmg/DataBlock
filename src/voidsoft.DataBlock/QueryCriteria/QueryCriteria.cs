/*

      file : QueryCriteria.cs
description: QueryCriteria allows the creation of complex SQL queries.
             A queryCriteria is made by :
 
             1. CriteriaCondition - add criteria to the queries using the supported opperators.
             2. Joins - add joins with another query criteria.
             3. Alias - add a column alias using the specified alias  
 
   
      (c) 2004 - 2006 Marius Gheorghe. All rights reserved.

 
 */

using System;
using System.Collections.Generic;

namespace voidsoft.DataBlock
{
	/// <summary>
	///     QueryCriteria is a object oriented method of writing RDBMS independent queries
	/// </summary>
	public class QueryCriteria
	{
		/// <summary>
		///     Selected database fields in the query
		/// </summary>
		private DatabaseField[] fields;

		/// <summary>
		///     List with field aliasese
		/// </summary>
		private List<CriteriaAlias> listAliases;

		/// <summary>
		///     List which holds the current criterias
		/// </summary>
		private List<CriteriaCondition> listCriterias;

		/// <summary>
		///     ArrayList which holds the join criterias
		/// </summary>
		private List<JoinCriteriaCondition> listJoinCriterias;

		/// <summary>
		///     Table name
		/// </summary>
		private string tableName;

		/// <summary>
		///     Creates a new instance of QueryCriteria
		/// </summary>
		/// <param name="mainTable"></param>
		public QueryCriteria(TableMetadata mainTable)
		{
			fields = mainTable.TableFields;
			tableName = mainTable.TableName;
			listCriterias = new List<CriteriaCondition>();
			listJoinCriterias = new List<JoinCriteriaCondition>();
			listAliases = new List<CriteriaAlias>();
		}

		/// <summary>
		///     Create a new instance of QueryCriteria
		/// </summary>
		/// <param name="tableName">The name of the table</param>
		/// <param name="fields">DatabaseFields included in the query</param>
		public QueryCriteria(string tableName, params DatabaseField[] fields)
		{
			this.fields = fields;
			this.tableName = tableName;
			listCriterias = new List<CriteriaCondition>();
			listJoinCriterias = new List<JoinCriteriaCondition>();
			listAliases = new List<CriteriaAlias>();
		}

		/// <summary>
		///     The name of the database table associated with this criteria
		/// </summary>
		/// <value></value>
		public string TableName
		{
			get
			{
				return tableName;
			}
		}

		/// <summary>
		///     The DatabaseFields which will be selected in the QueryCriteria
		/// </summary>
		/// <value></value>
		public DatabaseField[] Fields
		{
			get
			{
				return fields;
			}
		}

		/// <summary>
		///     Criteria Conditions
		/// </summary>
		/// <value></value>
		internal CriteriaCondition[] CriteriaConditions
		{
			get
			{
				CriteriaCondition[] criterias = new CriteriaCondition[listCriterias.Count];

				for (int i = 0; i < listCriterias.Count; i++)
				{
					criterias[i] = listCriterias[i];
				}

				return criterias;
			}
		}

		/// <summary>
		///     JoinCriteriaConditions
		/// </summary>
		/// <value></value>
		internal JoinCriteriaCondition[] JoinCriteriaConditions
		{
			get
			{
				JoinCriteriaCondition[] criterias = new JoinCriteriaCondition[listJoinCriterias.Count];

				for (int i = 0; i < listJoinCriterias.Count; i++)
				{
					criterias[i] = listJoinCriterias[i];
				}

				return criterias;
			}
		}

		/// <summary>
		///     Aliases.
		/// </summary>
		internal CriteriaAlias[] Aliases
		{
			get
			{
				CriteriaAlias[] aliases = new CriteriaAlias[listAliases.Count];

				for (int i = 0; i < listAliases.Count; i++)
				{
					aliases[i] = listAliases[i];
				}

				return aliases;
			}
		}

		/// <summary>
		///     Add a new criteria condition to the QueryCriteria
		/// </summary>
		/// <param name="criteriaOperator">Specified criteria operator</param>
		/// <param name="field">The DatabaseField to which we add the criteria operator</param>
		/// <param name="values">The value(s)</param>
		public void Add(CriteriaOperator criteriaOperator, DatabaseField field, params object[] values)
		{
			//checks
			switch (criteriaOperator)
			{
				case CriteriaOperator.Between:
					if (values.Length != 2)
					{
						throw new ArgumentException("Invalid fields for criteria operator between. Must be 2 values");
					}
					break;
				case CriteriaOperator.Not:
					break;
				case CriteriaOperator.Like:
					break;
				case CriteriaOperator.Equality:
					break;
				case CriteriaOperator.IsNull:
					break;
				case CriteriaOperator.IsNotNull:
					break;
				case CriteriaOperator.Or:
					break;

				case CriteriaOperator.Distinct:
					//don't allow for two distinct types.
					for (int i = 0; i < CriteriaConditions.Length; i++)
					{
						if (CriteriaConditions[i].CriteriaOperator == CriteriaOperator.Distinct)
						{
							throw new ArgumentException("Can't have more than one Distinct criteria operators");
						}
					}
					break;

				case CriteriaOperator.OrderBy:
					//check for asc || desc
					if ((values[0].ToString().ToLower() == "asc") || (values[0].ToString().ToLower() == "desc"))
					{
					}
					else
					{
						throw new ArgumentException("Missing asc or desc clauses from the order by criteria");
					}
					break;
			}

			listCriterias.Add(new CriteriaCondition(criteriaOperator, field, values));
		}

		/// <summary>
		///     Add a new join to the QueryCriteria
		/// </summary>
		/// <param name="joinType">The join type</param>
		/// <param name="primaryKeyFieldTableName">Name of the table to which the primary key belongs</param>
		/// <param name="primaryKey">The Primary key field</param>
		/// <param name="foreignKeyFieldTableName">Name of the table to which the foreign key belongs</param>
		/// <param name="foreignKey">The foreign key field</param>
		/// <param name="criteria">The query criteria with which we join</param>
		public void AddJoin(JoinType joinType, string primaryKeyFieldTableName, DatabaseField primaryKey, string foreignKeyFieldTableName, DatabaseField foreignKey,
		                    QueryCriteria criteria)
		{
			JoinCriteriaCondition joinCriteria = new JoinCriteriaCondition(joinType, primaryKeyFieldTableName, primaryKey, foreignKeyFieldTableName, foreignKey, criteria);
			listJoinCriterias.Add(joinCriteria);
		}

		/// <summary>
		///     Add an column alias to the query criteria
		/// </summary>
		/// <param name="fieldName">The name of the field</param>
		/// <param name="aliasName">The alias</param>
		public void AddAlias(string fieldName, string aliasName)
		{
			try
			{
				bool isField = false;

				//check if the field is in the list of selected fields
				for (int i = 0; i < fields.Length; i++)
				{
					if (fieldName == fields[i].fieldName)
					{
						isField = true;
						break;
					}
				}

				if (isField == false)
				{
					throw new ArgumentException("Invalid field name. The field name is not in the list of selected fields.");
				}

				CriteriaAlias alias = new CriteriaAlias(fieldName, aliasName, tableName);
				listAliases.Add(alias);
			}
			catch
			{
				throw;
			}
		}
	}
}