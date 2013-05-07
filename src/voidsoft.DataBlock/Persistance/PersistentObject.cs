/*
  	    
        file: PersistentObject class 
 description: Contains object persistence logic. This is the class upon which the BusinessObject are created.	 
     

  (c) 2004 - 2006 Marius Gheorghe. All rights reserved.
  
 */

using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;

namespace voidsoft.DataBlock
{
	/// <summary>
	///     Object persistence
	/// </summary>
	[Serializable]
	public class PersistentObject : IDisposable
	{
		//flag for dispose

		/// <summary>
		///     Connection string used to connect to the database.
		/// </summary>
		protected string connectionString;

		/// <summary>
		///     Current context session.
		/// </summary>
		protected Session contextSession = null;

		/// <summary>
		///     The dataType of the database server.
		/// </summary>
		protected DatabaseServer database;

		/// <summary>
		///     Execution Engine instance.This is valid only when the PersistentObject is instantiated with a Session.
		/// </summary>
		protected ExecutionEngine execEngine = null;

		/// <summary>
		///     TableMetadata associated with a instance of this class
		/// </summary>
		protected TableMetadata mappedObject;

		/// <summary>
		///     Initialize a new PersistentObject
		/// </summary>
		/// <param name="database">Database server dataType</param>
		/// <param name="connectionString">Connection string</param>
		/// <param name="mainTable">TableMetadata associated to this PersistentObject</param>
		public PersistentObject(DatabaseServer database, string connectionString, TableMetadata mainTable)
		{
			this.database = database;
			this.connectionString = connectionString;
			mappedObject = mainTable;
		}

		/// <summary>
		///     Initialize a new PersistentObject
		/// </summary>
		/// <param name="contextSession">Session in the context of which the PersistentObject is initialized</param>
		/// <param name="mainTable">TableMetadata associated to this PersistentObject</param>
		public PersistentObject(Session contextSession, TableMetadata mainTable)
		{
			this.contextSession = contextSession;
			mappedObject = mainTable;
			execEngine = new ExecutionEngine(this.contextSession);
			database = contextSession.Database;
		}

		/// <summary>
		///     Initialize a new PersistentObject
		/// </summary>
		/// <param name="mainTable"></param>
		public PersistentObject(TableMetadata mainTable)
			: this(Configuration.DatabaseServerType, Configuration.ConnectionString, mainTable)
		{
		}

		/// <summary>
		///     Gets a value indicating whether this instance is running in a session.
		/// </summary>
		/// <value>
		///     <c>True</c> if this instance is in session; otherwise, <c>false</c>.
		/// </value>
		public bool IsInSession
		{
			get
			{
				return contextSession != null ? true : false;
			}
		}

		/// <summary>
		///     Gets the session execution engine. This is available only if the current object runs in a session.
		/// </summary>
		/// <value>The session execution engine.</value>
		public ExecutionEngine SessionExecutionEngine
		{
			get
			{
				return execEngine;
			}
		}

		/// <summary>
		///     Dispose
		/// </summary>
		public void Dispose()
		{
			if (execEngine != null)
			{
				execEngine.Dispose();
			}
		}

		public object GetLastInsertedPK()
		{
			using (ExecutionEngine e = new ExecutionEngine())
			{
				return e.ExecuteScalar(new ExecutionQuery("SELECT IDENT_CURRENT ('" + mappedObject.TableName + "') AS Id", new IDataParameter[0]));
			}
		}

		public object GetLastInsertedPK(string tableName)
		{
			using (ExecutionEngine e = new ExecutionEngine())
			{
				return e.ExecuteScalar(new ExecutionQuery("SELECT IDENT_CURRENT ('" + tableName + "') AS Id", new IDataParameter[0]));
			}
		}

		/// <summary>
		///     Gets a DataTable by running the specified criteria
		/// </summary>
		/// <param name="criteria">Specified criteria</param>
		/// <returns>Resulting DataTable</returns>
		public DataTable GetDataTable(QueryCriteria criteria)
		{
			IQueryCriteriaGenerator iql = null;

			DataFactory factory = new DataFactory();

			iql = factory.InitializeQueryCriteriaGenerator(database);

			DataTable table = null;

			ExecutionQuery selectQuery = iql.GenerateSelect(criteria);

			//check for session
			if (contextSession != null)
			{
				table = execEngine.ExecuteDataTable(selectQuery);
			}
			else
			{
				using (ExecutionEngine e = new ExecutionEngine())
				{
					//no session 
					table = e.ExecuteDataTable(selectQuery);
				}
			}

			return table;
		}

		/// <summary>
		///     Gets a DataTable which contains all the data for current TableMetadata
		/// </summary>
		/// <returns>Resulting DataTable</returns>
		public DataTable GetDataTable()
		{
			DataTable dsTemp = null;

			SqlGenerator generator = new SqlGenerator();

			ExecutionQuery selectQuery = generator.GenerateSelectQuery(database, mappedObject, false);

			//check for session
			if (contextSession != null)
			{
				dsTemp = execEngine.ExecuteDataTable(selectQuery);
			}
			else
			{
				using (ExecutionEngine e = new ExecutionEngine())
				{
					dsTemp = e.ExecuteDataTable(selectQuery);
				}
			}

			return dsTemp;
		}

		/// <summary>
		///     Gets the data set which contains the data for the specified fields
		/// </summary>
		/// <param name="fields">The fields</param>
		/// <returns>Resulting DataTable</returns>
		public DataTable GetDataTable(params DatabaseField[] fields)
		{
			DataTable ds = null;

			SqlGenerator generator = new SqlGenerator();
			if (fields.Length == 0)
			{
				throw new ArgumentException("Invalid fields number");
			}

			ds = new DataTable();

			ExecutionQuery selectQuery = generator.GenerateSelectQuery(database, mappedObject.TableName, fields, null);

			if (contextSession != null)
			{
				ds = execEngine.ExecuteDataTable(selectQuery);
			}
			else
			{
				using (ExecutionEngine e = new ExecutionEngine())
				{
					ds = e.ExecuteDataTable(selectQuery);
				}
			}

			return ds;
		}

		/// <summary>
		///     Gets the data table paginated
		/// </summary>
		/// <param name="numberOfItems">The number of items.</param>
		/// <param name="pageNumber">The page number.</param>
		/// <returns></returns>
		public DataTable GetDataTablePaginated(int numberOfItems, int pageNumber)
		{
			SqlGenerator generator = new SqlGenerator();

			ExecutionQuery query = generator.GenerateSelectPaginatedQuery(database, mappedObject, numberOfItems, pageNumber);

			DataTable table = null;

			if (contextSession != null)
			{
				table = execEngine.ExecuteDataTable(query);
			}
			else
			{
				using (ExecutionEngine e = new ExecutionEngine())
				{
					table = e.ExecuteDataTable(query);
				}
			}

			return table;
		}

		/// <summary>
		///     Returns a dataset which contains data specified in the criteria query
		/// </summary>
		/// <param name="criteria">QueryCriteria based upon which the data is selected</param>
		/// <returns>DataSet containing the selected data</returns>
		public DataSet GetDataSet(QueryCriteria criteria)
		{
			IQueryCriteriaGenerator iql = null;

			DataFactory factory = new DataFactory();

			iql = factory.InitializeQueryCriteriaGenerator(database);

			DataSet dsTemp = null;

			ExecutionQuery selectQuery = iql.GenerateSelect(criteria);

			//check for session
			if (contextSession != null)
			{
				dsTemp = execEngine.ExecuteDataSet(selectQuery);
			}
			else
			{
				using (ExecutionEngine e = new ExecutionEngine())
				{
					dsTemp = e.ExecuteDataSet(database, connectionString, selectQuery);
				}
			}

			return dsTemp;
		}

		/// <summary>
		///     Returns a dataset with all the data from our business object
		/// </summary>
		/// <returns>DataSet containing all the data</returns>
		public DataSet GetDataSet()
		{
			DataSet dsTemp = null;

			SqlGenerator generator = new SqlGenerator();

			try
			{
				ExecutionQuery selectQuery = generator.GenerateSelectQuery(database, mappedObject, false);

				dsTemp = new DataSet();

				//check for session
				if (contextSession != null)
				{
					dsTemp = execEngine.ExecuteDataSet(selectQuery);
				}
				else
				{
					using (ExecutionEngine e = new ExecutionEngine())
					{
						dsTemp = e.ExecuteDataSet(database, connectionString, selectQuery);
					}
				}

				return dsTemp;
			}
			catch (Exception ex)
			{
				Log.LogMessage(ex.Message + ex.StackTrace);
				throw;
			}
		}

		/// <summary>
		///     Returns a DataSet which contains data from the related table
		/// </summary>
		/// <param name="relatedTableName">Name of the related table</param>
		/// <param name="foreignKeyValue">Value of the foreign key</param>
		/// <returns>DataSet containing data from the related table</returns>
		public DataSet GetDataSet(string relatedTableName, object foreignKeyValue)
		{
			SqlGenerator generator = new SqlGenerator();

			DataSet ds = new DataSet();

			ExecutionQuery selectQuery = new ExecutionQuery();

			TableRelation[] relations = mappedObject.Relations;

			for (int i = 0; i < relations.Length; i++)
			{
				if (relations[i].RelatedTableName == relatedTableName.Trim())
				{
					DatabaseField keyField;

					//check if we have a ParentRelation or a ChildRelation
					if (relations[i] is ParentTableRelation)
					{
						DatabaseField primaryKeyField = mappedObject.GetPrimaryKeyField();

						//this is the parent so we select from the child table.
						keyField = new DatabaseField(primaryKeyField.fieldType, ((ParentTableRelation)relations[i]).ForeignKeyName, false, false, foreignKeyValue);
					}
					else
					{
						//child relation 
						ChildTableRelation childRelation = (ChildTableRelation)relations[i];

						//this is the child so get data from the parent
						keyField = new DatabaseField(mappedObject.GetPrimaryKeyField().fieldType, childRelation.RelatedTableKeyName, true, false, foreignKeyValue);
					}

					selectQuery = generator.GenerateSelectQuery(database, relations[i].RelatedTableName, keyField);
					break;
				}
			}

			if (selectQuery.Query == string.Empty)
			{
				throw new ArgumentException("Invalid relation name");
			}

			//run the query in the associated context
			if (contextSession != null)
			{
				ds = execEngine.ExecuteDataSet(selectQuery);
			}
			else
			{
				using (ExecutionEngine e = new ExecutionEngine())
				{
					ds = e.ExecuteDataSet(selectQuery);
				}
			}

			return ds;
		}

		/// <summary>
		///     Returns a dataset with the data from the specified fields.
		/// </summary>
		/// <param name="fields">DatabaseFields which will be included in the select</param>
		/// <returns>DataSet with results</returns>
		public DataSet GetDataSet(params DatabaseField[] fields)
		{
			DataSet ds = null;
			SqlGenerator generator = new SqlGenerator();

			if (fields.Length == 0)
			{
				throw new ArgumentException("Invalid fields number");
			}

			ds = new DataSet();

			ExecutionQuery selectQuery = generator.GenerateSelectQuery(database, mappedObject.TableName, fields, null);

			if (contextSession != null)
			{
				ds = execEngine.ExecuteDataSet(selectQuery);
			}
			else
			{
				using (ExecutionEngine e = new ExecutionEngine())
				{
					ds = e.ExecuteDataSet(selectQuery);
				}
			}

			return ds;
		}

		/// <summary>
		///     Get data as a TableMetadata array
		/// </summary>
		/// <param name="criteria">QueryCriteria based upon which data is selected</param>
		/// <returns>A TableMetadata array containing all the data</returns>
		public Array GetTableMetadata(QueryCriteria criteria)
		{
			if (criteria.TableName != mappedObject.TableName)
			{
				throw new ArgumentException("Invalid criteria query. Must be the same as current table metadata");
			}

			DataFactory factory = new DataFactory();

			IQueryCriteriaGenerator iql = factory.InitializeQueryCriteriaGenerator(database);

			ExecutionQuery selectQuery = iql.GenerateSelect(criteria);

			return GetTableMetadata(selectQuery);
		}

		/// <summary>
		///     Get data as a TableMetadata array
		/// </summary>
		/// <returns>TableMetadata Array</returns>
		public Array GetTableMetadata()
		{
			try
			{
				SqlGenerator generator = new SqlGenerator();
				ExecutionQuery selectQuery = generator.GenerateSelectQuery(database, mappedObject, false);
				return GetTableMetadata(selectQuery);
			}
			catch (Exception ex)
			{
				Log.LogMessage(ex.Message + ex.StackTrace);
				throw new DataBlockException(ex.Message, ex);
			}
		}

		/// <summary>
		///     Gets a single table metadata based on a QueryCriteria
		/// </summary>
		/// <param name="criteria">The criteria.</param>
		/// <returns>The selected TableMetadata</returns>
		public TableMetadata GetSingleTableMetadata(QueryCriteria criteria)
		{
			if (criteria.TableName != mappedObject.TableName)
			{
				throw new ArgumentException("Invalid criteria query. Must be the same as current table metadata");
			}

			DataFactory factory = new DataFactory();

			IQueryCriteriaGenerator iql = factory.InitializeQueryCriteriaGenerator(database);

			ExecutionQuery selectQuery = iql.GenerateSelect(criteria);

			TableMetadata[] metadatas = (TableMetadata[])GetTableMetadata(selectQuery);

			return metadatas[0];
		}

		/// <summary>
		///     Get data from a child table based on the relation name and the primary key's fieldValue from the parent table.
		/// </summary>
		/// <param name="primaryKeyValue">The fieldValue of the primary key</param>
		/// <returns>The selected TableMetadata </returns>
		public TableMetadata GetTableMetadata(object primaryKeyValue)
		{
			SqlGenerator generator = new SqlGenerator();

			DataFactory factory = new DataFactory();

			//generate select statement
			if (primaryKeyValue == null)
			{
				throw new ArgumentException("Invalid fieldValue for primary key");
			}

			ISqlGenerator isql = factory.InitializeSqlGenerator(database);

			DatabaseField pkField = mappedObject.GetPrimaryKeyField();

			pkField.fieldValue = primaryKeyValue;

			//generate select query
			ExecutionQuery selectQuery = generator.GenerateSelectQuery(database, mappedObject.TableName, mappedObject.TableFields, pkField);

			TableMetadata table = (TableMetadata)Activator.CreateInstance(mappedObject.GetType());

			ArrayList alList = MapDataReaderToTableMetadata(selectQuery, table);

			table = (TableMetadata)alList[0];

			return table;
		}

		/// <summary>
		///     Get data from a related table (doen't matter if parent of child) based
		///     on the relation name and the primary key's fieldValue from the related table.
		/// </summary>
		/// <param name="relatedTableName">The name of the related table class name</param>
		/// <param name="classType">Class type of the related TableMetadata entity</param>
		/// <param name="foreignKeyValue">Foreign key's fieldValue</param>
		/// <returns>TableMetadata array which contains the specified data </returns>
		public Array GetTableMetadata(string relatedTableName, Type classType, object foreignKeyValue)
		{
			ArrayList alList = null;

			SqlGenerator generator = new SqlGenerator();

			try
			{
				ExecutionQuery selectQuery = new ExecutionQuery();

				//hold the table's relations.
				TableRelation[] relations = mappedObject.Relations;

				//loop and get the relation

				for (int i = 0; i < relations.Length; i++)
				{
					if (relations[i].RelatedTableName == relatedTableName.Trim())
					{
						DatabaseField keyField;

						//check if we habe a ParentRelation or a ChildRelation
						if (relations[i] is ParentTableRelation)
						{
							DatabaseField primaryKeyField = mappedObject.GetPrimaryKeyField();

							//this is the parent so we select from the child table.
							keyField = new DatabaseField(primaryKeyField.fieldType, ((ParentTableRelation)relations[i]).ForeignKeyName, false, false, foreignKeyValue);
						}
						else
						{
							//child relation 
							ChildTableRelation childRelation = (ChildTableRelation)relations[i];

							//this is the child so get data from the parent
							keyField = new DatabaseField(mappedObject.GetPrimaryKeyField().fieldType, childRelation.RelatedTableKeyName, true, false, foreignKeyValue);
						}

						selectQuery = generator.GenerateSelectQuery(database, relations[i].RelatedTableName, keyField);

						break;
					}
				}

				if (selectQuery.Query == string.Empty)
				{
					throw new Exception("Invalid related table name");
				}

				object tableMetadata = Activator.CreateInstance(classType);

				alList = MapDataReaderToTableMetadata(selectQuery, (TableMetadata)tableMetadata);

				Array array = Array.CreateInstance(classType, alList.Count);

				alList.CopyTo(array);

				return array;
			}
			finally
			{
				if (alList != null)
				{
					alList.Clear();
				}
			}
		}

		/// <summary>
		///     Returns a ArraList with the data from the specified field using the specified QueryCriteria
		/// </summary>
		/// <param name="criteria">QueryCriteria based upon which data is selected</param>
		/// <returns>ArrayList which contains the selected result</returns>
		public virtual ArrayList GetFieldList(QueryCriteria criteria)
		{
			IQueryCriteriaGenerator iql = null;
			IDataReader iread = null;
			ArrayList scData = null;

			ExecutionEngine exec = null;

			try
			{
				if (criteria.Fields.Length > 1)
				{
					throw new ArgumentException("Invalid field length. Must have only one field ");
				}

				scData = new ArrayList();

				DataFactory factory = new DataFactory();

				iql = factory.InitializeQueryCriteriaGenerator(database);

				ExecutionQuery selectQuery = iql.GenerateSelect(criteria);

				if (contextSession != null)
				{
					iread = execEngine.ExecuteReader(selectQuery);
				}
				else
				{
					exec = new ExecutionEngine(database, connectionString);

					iread = exec.ExecuteReader(selectQuery);
				}

				while (iread.Read())
				{
					scData.Add(iread.GetValue(0));
				}

				iread.Close();

				return scData;
			}
			finally
			{
				if (iread != null)
				{
					iread.Close();
				}

				if (exec != null)
				{
					exec.Dispose();
				}
			}
		}

		/// <summary>
		///     Returns a list with all the data from the specified field
		/// </summary>
		/// <param name="field">DatabaseField based upon which data is selected</param>
		/// <returns>ArrayList which contains the selected data</returns>
		public virtual ArrayList GetFieldList(DatabaseField field)
		{
			IDataReader iread = null;
			ArrayList alData = null;

			SqlGenerator generator = new SqlGenerator();

			ExecutionEngine exec = null;

			try
			{
				alData = new ArrayList();

				ExecutionQuery selectQuery = generator.GenerateSelectQuery(database, mappedObject.TableName, new DatabaseField[1] { field }, null);

				if (contextSession != null)
				{
					iread = execEngine.ExecuteReader(selectQuery);
				}
				else
				{
					exec = new ExecutionEngine();

					iread = exec.ExecuteReader(database, connectionString, selectQuery);
				}

				while (iread.Read())
				{
					alData.Add(iread.GetValue(0));
				}

				iread.Close();

				return alData;
			}
			finally
			{
				if (iread != null)
				{
					iread.Close();
				}

				if (exec != null)
				{
					exec.Dispose();
				}
			}
		}

		/// <summary>
		///     Return a sorted list with all the data from the specified 2 fields
		/// </summary>
		/// <param name="idField">First field</param>
		/// <param name="descriptionField">Second field.</param>
		/// <returns>StringDictionary which contains the selected data</returns>
		public virtual SortedList GetFieldList(DatabaseField idField, DatabaseField descriptionField)
		{
			IDataReader iread = null;
			SortedList scData = null;

			SqlGenerator generator = new SqlGenerator();

			ExecutionEngine exec = null;

			try
			{
				DatabaseField[] fields = new DatabaseField[2];
				fields[0] = idField;
				fields[1] = descriptionField;

				ExecutionQuery selectQuery = generator.GenerateSelectQuery(database, mappedObject.TableName, fields, null);

				scData = new SortedList();

				if (contextSession != null)
				{
					iread = execEngine.ExecuteReader(selectQuery);
				}
				else
				{
					exec = new ExecutionEngine(database, connectionString);

					iread = exec.ExecuteReader(database, connectionString, selectQuery);
				}

				while (iread.Read())
				{
					scData.Add(iread.GetValue(0), iread.GetValue(1));
				}

				iread.Close();

				return scData;
			}
			finally
			{
				if (iread != null)
				{
					iread.Close();
				}

				if (exec != null)
				{
					exec.Dispose();
				}
			}
		}


		/// <summary>
		///     Returns a single value from the database using the specified QueryCriteria
		/// </summary>
		/// <param name="criteria">QueryCriteria based upon which data is selected</param>
		/// <returns>The selected fieldValue</returns>
		public object GetValue(QueryCriteria criteria)
		{
			IQueryCriteriaGenerator iql = null;

			object result = null;

			ExecutionEngine exec = null;

			try
			{
				if (criteria.Fields.Length > 1)
				{
					throw new ArgumentException("Invalid fields length. Must have only one field");
				}

				DataFactory factory = new DataFactory();

				iql = factory.InitializeQueryCriteriaGenerator(database);

				ExecutionQuery selectQuery = iql.GenerateSelect(criteria);

				if (contextSession != null)
				{
					result = execEngine.ExecuteScalar(selectQuery);
				}
				else
				{
					exec = new ExecutionEngine();

					result = exec.ExecuteScalar(selectQuery);
				}

				return result;
			}
			finally
			{
				if (exec != null)
				{
					exec.Dispose();
				}
			}
		}

		/// <summary>
		///     Checks if the specified fieldValue exists in the database. Returns true if the
		///     fieldValue doesn't exists in the database and false if it exists
		/// </summary>
		/// <param name="field">The field to which the specified fieldValue belongs</param>
		/// <param name="value">Value to search for</param>
		/// <returns>Returns true if a field with the specified fieldValue is found</returns>
		public bool IsUnique(DatabaseField field, object value)
		{
			SqlGenerator generator = new SqlGenerator();

			ExecutionEngine exec = null;

			try
			{
				//TODO: implement this with count
				object oldValue = field.fieldValue; //save the initial fieldValue of the field

				field.fieldValue = value; //set the new fieldValue to the field

				//get the execution query
				ExecutionQuery selectQuery = generator.GenerateSelectQuery(database, mappedObject.TableName, field);

				object resultValue = null;

				//check execution context
				if (contextSession != null)
				{
					resultValue = execEngine.ExecuteScalar(selectQuery);

					field.fieldValue = oldValue;

					if (resultValue == null)
					{
						return true;
					}

					return false;
				}

				exec = new ExecutionEngine();

				resultValue = exec.ExecuteScalar(database, connectionString, selectQuery);

				//set the original fieldValue back
				field.fieldValue = oldValue;

				if (resultValue == null)
				{
					return true;
				}

				return false;
			}
			finally
			{
				if (exec != null)
				{
					exec.Dispose();
				}
			}
		}

		/// <summary>
		///     Max
		/// </summary>
		/// <param name="field">The field.</param>
		/// <returns></returns>
		public object GetMax(DatabaseField field)
		{
			return (int)RunIntrinsecFunction(CriteriaOperator.Max, field);
		}

		/// <summary>
		///     Min
		/// </summary>
		/// <param name="field">The field.</param>
		/// <returns></returns>
		public object GetMin(DatabaseField field)
		{
			return (int)RunIntrinsecFunction(CriteriaOperator.Min, field);
		}

		/// <summary>
		///     Count
		/// </summary>
		/// <returns></returns>
		public int GetCount()
		{
			return (int)RunIntrinsecFunction(CriteriaOperator.Count, mappedObject[0]);
		}

		/// <summary>
		///     Inserts a new object into the database.
		/// </summary>
		/// <param name="metaTable">TableMatadata from which the object is created</param>
		public int Create(TableMetadata metaTable)
		{
			//collection of queries which will be executed
			List<ExecutionQuery> listQueries = null;

			int resultCounter = 0;

			DatabaseField field = metaTable.GetPrimaryKeyField();

			SqlGenerator generator = new SqlGenerator();

			ExecutionEngine exec = null;

			try
			{
				listQueries = new List<ExecutionQuery>();

				//get the attached tabels
				TableMetadata[] attachedData = metaTable.AttachedData;

				//check if the table has attached tables.
				//If not generate the insert only for a single table.
				if (attachedData.Length == 0)
				{
					//generate the sql command
					ExecutionQuery insertQuery = generator.GenerateInsertQuery(database, metaTable);

					//add PK constraint if necessary
					if (field.isValueAutogenerated)
					{
						insertQuery.Query = ConstraintManager.GeneratePrimaryKeyConstraint(field.fieldName, metaTable.TableName, insertQuery.Query);
					}

					listQueries.Add(insertQuery);
				}
				else
				{
					//generate the multiple table's insert.
					List<ExecutionQuery> multipleQueries = generator.GenerateMultipleInsertQueries(database, metaTable);
					// containsSpecialModifications = true;

					//add the queries to the 
					foreach (ExecutionQuery var in multipleQueries)
					{
						listQueries.Add(var);
					}
				}

				//run in the  current session 
				if (contextSession != null)
				{
					//the context is in a transaction so just cache the inserts.
					if (contextSession.IsInTransaction)
					{
						foreach (ExecutionQuery var in listQueries)
						{
							contextSession.Queries.Add(var);
						}
					}
					else
					{
						resultCounter = execEngine.ExecuteNonQuery(listQueries);
					}
				}
				else
				{
					//
					BeforeExecutingQueries(Operation.Create, ref listQueries);

					exec = new ExecutionEngine();

					//check if we need the PK or not
					if (field.isValueAutogenerated && listQueries.Count == 1)
					{
						List<object> listPrimaryKeysValues = new List<object>();

						resultCounter = exec.ExecuteNonQuery(listQueries, Configuration.DefaultTransactionIsolationLevel, out listPrimaryKeysValues);

						if (listPrimaryKeysValues.Count > 0)
						{
							metaTable.SetFieldValue(field.fieldName, listPrimaryKeysValues[0]);
						}
					}
					else
					{
						resultCounter = exec.ExecuteNonQuery(listQueries, Configuration.DefaultTransactionIsolationLevel);
					}
				}

				return resultCounter;
			}
			finally
			{
				if (exec != null)
				{
					exec.Dispose();
				}
			}
		}

		/// <summary>
		///     Updates the specified entity
		/// </summary>
		/// <param name="mainTable"></param>
		public int Update(TableMetadata mainTable)
		{
			List<ExecutionQuery> scQueries = null;

			SqlGenerator generator = new SqlGenerator();

			ExecutionEngine exec = null;

			int resultCounter = 0;

			try
			{
				scQueries = new List<ExecutionQuery>();

				List<ExecutionQuery> listQueries = generator.GenerateMultipleUpdateQueries(database, mainTable);

				foreach (ExecutionQuery var in listQueries)
				{
					scQueries.Add(var);
				}

				//check the execution context
				if (contextSession != null)
				{
					if (contextSession.IsInTransaction)
					{
						foreach (ExecutionQuery var in scQueries)
						{
							contextSession.Queries.Add(var);
						}
					}
					else
					{
						resultCounter = execEngine.ExecuteNonQuery(scQueries);
					}
				}
				else
				{
					BeforeExecutingQueries(Operation.Update, ref scQueries);

					exec = new ExecutionEngine();

					resultCounter = exec.ExecuteNonQuery(scQueries, Configuration.DefaultTransactionIsolationLevel);
				}

				return resultCounter;
			}
			finally
			{
				if (exec != null)
				{
					exec.Dispose();
				}
			}
		}

		/// <summary>
		///     Updates the specified entities using the specified QueryCriteria
		/// </summary>
		/// <param name="criteria">QueryCriteria based upon which data is updated</param>
		/// <returns>Number of affected rows</returns>
		public int Update(QueryCriteria criteria)
		{
			DataFactory factory = new DataFactory();

			ExecutionEngine exec = null;

			try
			{
				IQueryCriteriaGenerator iql = factory.InitializeQueryCriteriaGenerator(database);

				ExecutionQuery query = iql.GenerateUpdate(criteria);

				if (contextSession != null)
				{
					if (contextSession.IsInTransaction)
					{
						contextSession.Queries.Add(query);
						return 0;
					}

					return execEngine.ExecuteNonQuery(query);
				}


				exec = new ExecutionEngine();

				return exec.ExecuteNonQuery(query);
			}
			finally
			{
				if (exec != null)
				{
					exec.Dispose();
				}
			}
		}

		/// <summary>
		///     Deletes the row by the specified primary key value.
		/// </summary>
		/// <param name="primaryKeyValue">The primary key value.</param>
		/// <returns>Number of affected rows</returns>
		public int Delete(object primaryKeyValue)
		{
			IQueryCriteriaGenerator queryCriteriaGenerator = null;

			DataFactory factory = new DataFactory();

			int affectedRows = 0;

			ExecutionEngine exec = null;

			try
			{
				queryCriteriaGenerator = factory.InitializeQueryCriteriaGenerator(database);

				DatabaseField field = mappedObject.GetPrimaryKeyField();

				field.fieldValue = primaryKeyValue;

				QueryCriteria criteria = new QueryCriteria(mappedObject.TableName);

				criteria.Add(CriteriaOperator.Equality, field, primaryKeyValue);

				ExecutionQuery query = queryCriteriaGenerator.GenerateDelete(criteria);

				if (contextSession != null)
				{
					if (contextSession.IsInTransaction)
					{
						contextSession.Queries.Add(query);
					}
					else
					{
						affectedRows = execEngine.ExecuteNonQuery(query);
					}
				}
				else
				{
					exec = new ExecutionEngine();

					affectedRows = exec.ExecuteNonQuery(query);
				}

				return affectedRows;
			}
			finally
			{
				if (exec != null)
				{
					exec.Dispose();
				}
			}
		}

		/// <summary>
		///     Delete multiple rows from the table using the specified criteria from a QueryCriteria
		/// </summary>
		/// <param name="criteria">QueryCriteria based on which data is deleted</param>
		public int Delete(QueryCriteria criteria)
		{
			int affectedRows = 0;

			DataFactory factory = new DataFactory();

			ExecutionEngine exec = null;

			try
			{

				IQueryCriteriaGenerator iql = factory.InitializeQueryCriteriaGenerator(database);

				ExecutionQuery query = iql.GenerateDelete(criteria);

				if (contextSession != null)
				{
					if (contextSession.IsInTransaction)
					{
						contextSession.Queries.Add(query);
					}
					else
					{
						affectedRows = execEngine.ExecuteNonQuery(query);
					}
				}
				else
				{
					exec = new ExecutionEngine();

					affectedRows = exec.ExecuteNonQuery(query);
				}

				return affectedRows;
			}
			finally
			{
				if (exec != null)
				{
					exec.Dispose();
				}
			}
		}

		/// <summary>
		///     Deletes the specified data and data from related child tables.
		/// </summary>
		/// <param name="mainTable">TableMetadata to be deleted</param>
		public int Delete(TableMetadata mainTable)
		{
			SqlGenerator generator = new SqlGenerator();

			ExecutionEngine exec = null;

			try
			{
				//generated sql command to delete data from the parent table.
				List<ExecutionQuery> listQueries = generator.GenerateMultipleDeleteQueries(database, mainTable);

				int affectedRows = 0;

				//check for session context
				if (contextSession != null)
				{
					//check for session transaction
					if (contextSession.IsInTransaction)
					{
						//cache the queries for transaction
						for (int i = 0; i < listQueries.Count; i++)
						{
							contextSession.Queries.Add(listQueries[i]);
						}
					}
					else
					{
						//run the queries into a local transaction
						affectedRows = execEngine.ExecuteNonQuery(listQueries, Configuration.DefaultTransactionIsolationLevel);
					}
				}
				else
				{
					BeforeExecutingQueries(Operation.Delete, ref listQueries);

					exec = new ExecutionEngine();

					//run everything in a local transaction
					affectedRows = exec.ExecuteNonQuery(listQueries, Configuration.DefaultTransactionIsolationLevel);
				}

				return affectedRows;
			}
			finally
			{
				if (exec != null)
				{
					exec.Dispose();
				}
			}
		}


		private object RunIntrinsecFunction(CriteriaOperator criteria, DatabaseField field)
		{
			IQueryCriteriaGenerator iql = null;

			object result;

			ExecutionEngine exec = null;

			try
			{
				QueryCriteria qc = new QueryCriteria(mappedObject.TableName, field);
				qc.Add(criteria, field);

				DataFactory factory = new DataFactory();

				iql = factory.InitializeQueryCriteriaGenerator(database);

				ExecutionQuery query = iql.GenerateSelect(qc);

				if (contextSession != null)
				{
					result = execEngine.ExecuteScalar(query);
				}
				else
				{
					exec = new ExecutionEngine();
					result = exec.ExecuteScalar(database, connectionString, query);
				}

				return result;
			}
			finally
			{
				if (exec != null)
				{
					exec.Dispose();
				}
			}
		}

		/// <summary>
		///     This method is called before executing the queries. By overriding it in the derived
		///     classes you have runtime access to the list of queries which will be executed
		/// </summary>
		/// <param name="operation">Type of operation</param>
		/// <param name="listQueries">List of queries which will be executed</param>
		/// <param name="args">Various contextual arguments</param>
		protected virtual void BeforeExecutingQueries(Operation operation, ref List<ExecutionQuery> listQueries, params object[] args)
		{
		}

		/// <summary>
		///     Maps a DataReader to a TableMetadata implementation.
		/// </summary>
		/// <param name="selectQuery">The ExecutionQuery</param>
		/// <param name="table">TableMetadata on which the query results will be mapped </param>
		/// <returns>ArrayList with TableMetadata results</returns>
		internal ArrayList MapDataReaderToTableMetadata(ExecutionQuery selectQuery, TableMetadata table)
		{
			IDataReader iread = null;
			ArrayList alTables = null;

			ExecutionEngine exec = null;

			try
			{
				alTables = new ArrayList();

				//check if we run it in the context session
				if (contextSession != null)
				{
					iread = execEngine.ExecuteReader(selectQuery);
				}
				else
				{
					exec = new ExecutionEngine();
					iread = exec.ExecuteReader(database, connectionString, selectQuery);
				}

				int columnCount = iread.FieldCount;

				while (iread.Read())
				{
					//create a instance of the table metadata
					TableMetadata tempTable = (TableMetadata)Activator.CreateInstance(table.GetType());

					//set the field's fieldValue
					for (int i = 0; i < columnCount; i++)
					{
						//tempTable.SetFieldValue(tempTable.TableFields[i].fieldName, iread.GetValue(i));
						tempTable.SetFieldValue(i, iread.GetValue(i));
					}

					alTables.Add(tempTable);
				}

				iread.Close();

				return alTables;
			}
			finally
			{
				if (iread != null && iread.IsClosed == false)
				{
					iread.Close();
				}

				if (exec != null)
				{
					exec.Dispose();
				}
			}
		}

		/// <summary>
		///     Maps a select query on a TableMetadata[]
		/// </summary>
		/// <param name="selectQuery">Select query</param>
		/// <returns>TableMetadata[]</returns>
		internal Array GetTableMetadata(ExecutionQuery selectQuery)
		{
			ArrayList alData = MapDataReaderToTableMetadata(selectQuery, mappedObject);
			Array tempArray = Array.CreateInstance(mappedObject.GetType(), alData.Count);

			for (int i = 0; i < tempArray.Length; i++)
			{
				tempArray.SetValue(alData[i], i);
			}

			return tempArray;
		}
	}
}