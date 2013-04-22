/*
 	   file: TableMetadata.cs

description: abstract class which describes the metadata (DatabaseFields[] and TableName) for a database table. 
  
	 (c) 2004 - 2006 Marius Gheorghe. All rights reserved.
*/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Reflection;
using System.Text;

namespace voidsoft.DataBlock
{
	/// <summary>
	///     Describes the metadata for a database table mapped object
	/// </summary>
	[Serializable]
	public class TableMetadata
	{
		/// <summary>
		///     Constant which represents the start index
		/// </summary>
		private const int START_INDEX_FIELD = 2;

		/// <summary>
		///     Table name field.
		/// </summary>
		protected string currentTableName;

		/// <summary>
		///     Table fields
		/// </summary>
		protected DatabaseField[] fields;

		/// <summary>
		///     List with attached related table data.
		/// </summary>
		protected List<TableMetadata> listAttachedData;

		/// <summary>
		///     List with table defined relations.
		/// </summary>
		protected List<TableRelation> listRelations;

		/// <summary>
		///     List with removed related data.
		/// </summary>
		protected List<TableMetadata> listRemovedData;

		///// <summary>
		///// State of the object. By default is unchanged which corresponds
		///// with readed from dataabse
		///// </summary>
		//private ObjectState currentState = ObjectState.Unchanged;

		/// <summary>
		///     Constructor
		/// </summary>
		public TableMetadata()
		{
			listRelations = new List<TableRelation>();
		}

		/// <summary>
		///     Gets the <see cref="DatabaseField" /> by the specified enum value.
		/// </summary>
		/// <value>The specified DatabaseField</value>
		[Browsable(false)]
		public DatabaseField this[Enum enumValue]
		{
			get
			{
				for (int j = 0; j < TableFields.Length; j++)
				{
					if (enumValue.ToString() == TableFields[j].fieldName)
					{
						return TableFields[j];
					}
				}

				throw new ArgumentException("Invalid field name");
			}
		}

		/// <summary>
		///     Gets the <see cref="DatabaseField" /> at the specified index.
		/// </summary>
		/// <param name="index">Index of the DatabaseField</param>
		/// <returns>The specified DatabaseField</returns>
		[Browsable(false)]
		public DatabaseField this[int index]
		{
			get
			{
				if (index < 0 || index > TableFields.Length)
				{
					throw new ArgumentException("Invalid index");
				}
				else
				{
					return TableFields[index];
				}
			}
		}

		/// <summary>
		///     Gets or sets the database fields
		/// </summary>
		[Browsable(false)]
		public virtual DatabaseField[] TableFields
		{
			get
			{
				return fields;
			}

			set
			{
				fields = value;
			}
		}

		/// <summary>
		///     Gets or sets the name of the table
		/// </summary>
		[Browsable(false)]
		public virtual string TableName
		{
			get
			{
				return currentTableName;
			}

			set
			{
				currentTableName = value;
			}
		}

		/// <summary>
		///     Get the table relations
		/// </summary>
		/// <fieldValue></fieldValue>
		[Browsable(false)]
		public TableRelation[] Relations
		{
			get
			{
				TableRelation[] relations = new TableRelation[listRelations.Count];
				listRelations.CopyTo(relations);
				return relations;
			}
		}

		/// <summary>
		///     Gets the attached data of the current table
		/// </summary>
		[Browsable(false)]
		public TableMetadata[] AttachedData
		{
			get
			{
				if (listAttachedData == null)
				{
					listAttachedData = new List<TableMetadata>();
				}

				TableMetadata[] attachedData = new TableMetadata[listAttachedData.Count];
				listAttachedData.CopyTo(attachedData);

				return attachedData;
			}
		}

		/// <summary>
		///     Gets a array with the removed data
		/// </summary>
		[Browsable(false)]
		public TableMetadata[] RemovedData
		{
			get
			{
				if (listRemovedData == null)
				{
					listRemovedData = new List<TableMetadata>();
				}

				TableMetadata[] removedData = new TableMetadata[listRemovedData.Count];
				listRemovedData.CopyTo(removedData);

				return removedData;
			}
		}

		/// <summary>
		///     Creates a new object that is a copy of the current instance (shallow copy).
		/// </summary>
		/// <returns>
		///     A new object that is a copy of this instance.
		/// </returns>
		public T Clone<T>() where T : TableMetadata
		{
			T mapped = Activator.CreateInstance<T>();

			mapped.TableName = TableName;

			mapped.TableFields = new DatabaseField[TableFields.Length];

			for (int i = 0; i < mapped.TableFields.Length; i++)
			{
				mapped.TableFields[i] = new DatabaseField();
				mapped.TableFields[i].fieldName = TableFields[i].fieldName;
				mapped.TableFields[i].fieldType = TableFields[i].fieldType;
				mapped.TableFields[i].fieldValue = TableFields[i].fieldValue;
				mapped.TableFields[i].isPrimaryKey = TableFields[i].isPrimaryKey;
				mapped.TableFields[i].isValueAutogenerated = TableFields[i].isValueAutogenerated;
			}

			return mapped;
		}

		/// <summary>
		///     Returns true if the specified DatabaseField has a null fieldValue
		/// </summary>
		/// <param name="fieldName">Field name</param>
		/// <returns>Boolean fieldValue which is true if the field has a null fieldValue. </returns>
		public bool IsNull(string fieldName)
		{
			int index = -1;

			for (int i = 0; i < TableFields.Length; i++)
			{
				if (TableFields[i].fieldName.ToLower() == fieldName.ToLower())
				{
					index = i;
					break;
				}
			}

			if (index == -1)
			{
				throw new ArgumentException("Invalid field name");
			}

			return IsNull(index);
		}

		/// <summary>
		///     Returns true if the specified DatabaseField has a null fieldValue.
		/// </summary>
		/// <param name="index">Field index</param>
		/// <returns>Boolean fieldValue which is true if the field has a null fieldValue</returns>
		public bool IsNull(int index)
		{
			if (index < 0 || index > TableFields.Length)
			{
				throw new ArgumentException("Invalid index");
			}

			if ((TableFields[index].fieldValue == null) || (TableFields[index].fieldValue == DBNull.Value))
			{
				return true;
			}
			else
			{
				return false;
			}
		}

		/// <summary>
		///     Set the null fieldValue for the specified field
		/// </summary>
		/// <param name="index">Field index</param>
		public void SetNullValue(int index)
		{
			if (index < 0 || index > TableFields.Length)
			{
				throw new ArgumentException("Invalid field index");
			}

			TableFields[index].fieldValue = null;
		}

		/// <summary>
		///     Set the null fieldValue for the specified field
		/// </summary>
		/// <param name="fieldName">Field's name</param>
		public void SetNullValue(string fieldName)
		{
			int index = -1;

			for (int i = 0; i < TableFields.Length; i++)
			{
				if (TableFields[i].fieldName.ToLower() == fieldName.ToLower())
				{
					TableFields[i].fieldValue = null;
					return;
				}
			}
			if (index == -1)
			{
				throw new ArgumentException("Invalid field name");
			}
		}

		/// <summary>
		///     Returns the primary key field
		/// </summary>
		/// <returns>DatabaseField which is the primary key</returns>
		public DatabaseField GetPrimaryKeyField()
		{
			for (int i = 0; i < TableFields.Length; i++)
			{
				if (TableFields[i].isPrimaryKey)
				{
					return TableFields[i];
				}
			}

			throw new ArgumentException("Missing primary key field");
		}

		/// <summary>
		///     Returns the specified database field.
		/// </summary>
		/// <param name="fieldName">The name of the DatabseField</param>
		/// <returns>DatabaseField</returns>
		// [Obsolete()]
		public DatabaseField GetField(string fieldName)
		{
			try
			{
				for (int i = 0; i < TableFields.Length; i++)
				{
					if (TableFields[i].fieldName == fieldName)
					{
						return (TableFields[i]);
					}
				}

				//not found
				throw new ArgumentException("Invalid database field name");
			}
			catch (Exception exception)
			{
				throw new DataBlockException("Failed to get field " + fieldName, exception);
			}
		}

		/// <summary>
		///     Returns the specified database field.
		/// </summary>
		/// <param name="index">The index of the database field</param>
		/// <returns>DatbaseField</returns>
		[Obsolete]
		public DatabaseField GetField(int index)
		{
			if (index < 0 || index > TableFields.Length)
			{
				throw new ArgumentException("Invalid index");
			}
			else
			{
				return TableFields[index];
			}
		}

		/// <summary>
		///     Sets a database field's fieldValue
		/// </summary>
		/// <param name="fieldName">The field name</param>
		/// <param name="fieldValue">The Value to be set.</param>
		public void SetFieldValue(string fieldName, object fieldValue)
		{
			try
			{
				for (int i = 0; i < TableFields.Length; i++)
				{
					if (TableFields[i].fieldName.ToLower() == fieldName.ToLower())
					{
						TableFields[i].fieldValue = fieldValue;
						return;
					}
				}

				throw new ArgumentException("Invalid field name");
			}
			catch (Exception ex)
			{
				throw new ArgumentException(ex.Message);
			}
		}

		/// <summary>
		///     Sets a database field's fieldValue.
		/// </summary>
		/// <param name="fieldIndex">The field name</param>
		/// <param name="fieldValue">The Value to be set.</param>
		public void SetFieldValue(int fieldIndex, object fieldValue)
		{
			try
			{
				if (fieldIndex > TableFields.Length || fieldIndex < 0)
				{
					throw new ArgumentException("Invalid field index");
				}

				TableFields[fieldIndex].fieldValue = fieldValue;
			}
			catch (Exception ex)
			{
				throw new ArgumentException(ex.Message, ex);
			}
		}

		/// <summary>
		///     Shows the contents as a string
		/// </summary>
		/// <returns></returns>
		public override string ToString()
		{
			StringBuilder sbuild = new StringBuilder();

			for (int i = 0; i < TableFields.Length; i++)
			{
				if (IsNull(i))
				{
					sbuild.Append(TableFields[i].fieldName + "= null;");
				}
				else
				{
					sbuild.Append(" " + TableFields[i].fieldName + " = " + TableFields[i].fieldValue + ";");
				}
			}

			return sbuild.ToString();
		}

		/// <summary>
		///     Returns data from a related table in Parent -> Child relationship. This is
		///     the underlying implementation of the generated GetXXX tables.
		/// </summary>
		/// <param name="childTableType">Type of the related entity</param>
		/// <returns>Array with the result</returns>
		protected TableMetadata[] GetRelatedTableData(Type childTableType)
		{
			PersistentObject persistent = null;

			TableRelation relation = null;

			try
			{
				//create a instance of the table so we can check the relations between out table and this
				TableMetadata childTable = (TableMetadata) Activator.CreateInstance(childTableType);

				//first check the fieldValue of the primary key
				if (GetPrimaryKeyField().fieldValue == null)
				{
					throw new InvalidOperationException("The primary key does not have a fieldValue");
				}

				//get the relation
				foreach (TableRelation var in Relations)
				{
					if (var.RelatedTableName == childTable.TableName)
					{
						relation = var;
						break;
					}
				}

				if (relation is ChildTableRelation)
				{
					return GetChildRelatedTableData(childTableType, (ChildTableRelation) relation);
				}

				//check if we got the relation
				if (relation == null)
				{
					throw new ArgumentException("A relation cannot be found between the tables");
				}

				//we have the relation between the 2 tables.
				persistent = new PersistentObject(this);

				TableMetadata[] data = null;

				if (relation.RelationCardinality == TableRelationCardinality.OneToOne)
				{
					ParentTableRelation pr = (ParentTableRelation) (relation);
					//take the fk value
					data = (TableMetadata[]) persistent.GetTableMetadata(relation.RelatedTableName, childTableType, GetField(pr.ForeignKeyName).fieldValue);
				}
				else
				{
					//take the pk value
					data = (TableMetadata[]) persistent.GetTableMetadata(relation.RelatedTableName, childTableType, GetPrimaryKeyField().fieldValue);
				}

				return data;
			}
			catch
			{
				throw;
			}
		}

		/// <summary>
		///     Gets the child related data.
		/// </summary>
		/// <param name="childTableType">Type of the child table.</param>
		/// <param name="relation">The relation.</param>
		/// <returns></returns>
		protected TableMetadata[] GetChildRelatedTableData(Type childTableType, ChildTableRelation relation)
		{
			PersistentObject persistent = null;

			try
			{
				//create a instance of the table so we can check the relations between out table and this
				TableMetadata childTable = (TableMetadata) Activator.CreateInstance(childTableType);

				object value = GetField(relation.ForeignKeyName).fieldValue;

				//we have the relation between the 2 tables.
				persistent = new PersistentObject(this);
				TableMetadata[] data = (TableMetadata[]) persistent.GetTableMetadata(relation.RelatedTableName, childTableType, value);

				return data;
			}
			catch
			{
				throw;
			}
			finally
			{
				if (persistent != null)
				{
					persistent.Dispose();
				}
			}
		}

		/// <summary>
		///     Gets the data from a related table in a ManyToMany relation.
		/// </summary>
		/// <param name="relatedTableType">Type of the related entity</param>
		/// <param name="intermediaryRelatedTableType">Type of the intermediary table</param>
		/// <returns>TableMetadata array which contains the results</returns>
		protected Array GetRelatedTableData(Type relatedTableType, Type intermediaryRelatedTableType)
		{
			PersistentObject persistent = null;
			ManyToManyTableRelation relation = null;
			DataSet ds = null;

			try
			{
				//create a instance of the table so we can check the relations between out table and this
				TableMetadata relatedTable = (TableMetadata) Activator.CreateInstance(relatedTableType);
				TableMetadata intermediateTable = (TableMetadata) Activator.CreateInstance(intermediaryRelatedTableType);

				object primaryKeyValue = GetPrimaryKeyField().fieldValue;

				//first check the fieldValue of the primary key
				if (GetPrimaryKeyField().fieldValue == null)
				{
					throw new InvalidOperationException("The primary key does not have a fieldValue");
				}

				//get the relation
				foreach (TableRelation var in Relations)
				{
					if (var is ManyToManyTableRelation)
					{
						if (var.RelatedTableName == relatedTable.TableName)
						{
							relation = (ManyToManyTableRelation) var;
							break;
						}
					}
				}

				//check if we got the relation
				if (relation == null)
				{
					throw new ArgumentException("A relation cannot be found between the tables");
				}

				//we have the relation between the 2 tables.
				persistent = new PersistentObject(this);

				QueryCriteria qcThis = new QueryCriteria(TableName, GetPrimaryKeyField());
				qcThis.Add(CriteriaOperator.Equality, GetPrimaryKeyField(), primaryKeyValue);

				//generate here the inner join
				QueryCriteria qcIntermediaryTable = new QueryCriteria(intermediateTable.TableName, intermediateTable.GetField(relation.IntermediaryKeyFieldFromParentTable));

				QueryCriteria qcChild = new QueryCriteria(relatedTable);

				qcThis.AddJoin(JoinType.Inner, TableName, GetPrimaryKeyField(), intermediateTable.TableName,
				               intermediateTable.GetField(relation.IntermediaryKeyFieldFromParentTable), qcIntermediaryTable);

				qcThis.AddJoin(JoinType.Inner, intermediateTable.TableName, intermediateTable.GetField(relation.IntermediaryKeyFieldFromChildTable), relatedTable.TableName,
				               relatedTable.GetField(relation.IntermediaryKeyFieldFromChildTable), qcChild);

				ds = persistent.GetDataSet(qcThis);

				Array data = Array.CreateInstance(relatedTableType, ds.Tables[0].Rows.Count);

				for (int i = 0; i < data.Length; i++)
				{
					object instance = Activator.CreateInstance(relatedTableType);
					data.SetValue(instance, i);

					//loop thru dataset
					for (int j = START_INDEX_FIELD; j < ds.Tables[0].Columns.Count; j++)
					{
						Type tp = data.GetValue(i).GetType();

						object[] args = new[] {ds.Tables[0].Columns[j].ColumnName, ds.Tables[0].Rows[i][j]};
						tp.InvokeMember("SetFieldValue", BindingFlags.InvokeMethod | BindingFlags.Public | BindingFlags.Instance, null, instance, args);
					}
				}

				return data;
			}
			catch
			{
				throw;
			}
			finally
			{
				if (ds != null)
				{
					ds.Dispose();
				}
			}
		}

		public void AttachTableMetadata(IEnumerable<TableMetadata> data)
		{
			//lazy initialization of the list 
			if (listAttachedData == null)
			{
				listAttachedData = new List<TableMetadata>();
			}

			if (Relations.Length == 0)
			{
				throw new ArgumentException("Cannot add data when there are no relations defined");
			}

			IEnumerator<TableMetadata> enumerator = data.GetEnumerator();

			while (enumerator.MoveNext())
			{
				//before adding the table metadata check if we have relations defined for this type

				bool foundRelation = false;
				//check if we have a relation defined for this data
				//do this check by matching the table names.
				foreach (TableRelation var in Relations)
				{
					if (var.RelatedTableName == enumerator.Current.TableName)
					{
						foundRelation = true;
						break;
					}
				}

				if (foundRelation)
				{
					listAttachedData.Add(enumerator.Current);
				}
			}
		}

		/// <summary>
		///     Attach related data (based on the table relations) to the table.
		/// </summary>
		/// <param name="table"></param>
		public void AttachTableMetadata(TableMetadata table)
		{
			//lazy initialization of the list 
			if (listAttachedData == null)
			{
				listAttachedData = new List<TableMetadata>();
			}

			if (Relations.Length == 0)
			{
				throw new ArgumentException("Cannot add data when there are no relations defined");
			}

			bool foundRelation = false;

			//check if we have a relation defined for this data
			//do this check by matching the table names.
			foreach (TableRelation var in Relations)
			{
				if (var.RelatedTableName == table.TableName)
				{
					foundRelation = true;
					break;
				}
			}

			if (foundRelation == false)
			{
				throw new ArgumentException("No relation defined for this type of data");
			}

			listAttachedData.Add(table);
		}

		/// <summary>
		///     Remove the associated data
		/// </summary>
		/// <param name="table"></param>
		public void RemoveTableMetadata(TableMetadata table)
		{
			if (listRemovedData == null)
			{
				listRemovedData = new List<TableMetadata>();
			}

			bool foundRelation = false;

			//check if we have a relation defined for this data
			//do this check by matching the table names.
			foreach (TableRelation var in Relations)
			{
				if (var.RelatedTableName == table.TableName)
				{
					foundRelation = true;
					break;
				}
			}

			if (foundRelation == false)
			{
				throw new ArgumentException("No relation defined for this type of data");
			}

			listRemovedData.Add(table);
		}
	}
}