/*

       file: DataConvertor.cs
description: Contains operation for data conversion from (and to) TableMetadata.
    
   (c) 2004 - 2008 Marius Gheorghe. All rights reserved.

*/

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace voidsoft.DataBlock
{
	/// <summary>
	///     Contains conversion methods
	/// </summary>
	public class DataConvertor
	{
		//const used to generate unique names for the IDataParameters.
		private const string PARAMETER_NAME_ENDING = "Next";

		/// <summary>
		///     Converts the fields of TableMetadata to IDataParameters
		/// </summary>
		/// <param name="database">Database server type</param>
		/// <param name="mainTable">TableMetadata from which the conversion is made</param>
		/// <returns>IDataParameter array </returns>
		public IDataParameter[] ConvertToDataParameter(DatabaseServer database, TableMetadata mainTable)
		{
			IDataParameter[] parameters = new IDataParameter[mainTable.TableFields.Length];

			for (int i = 0; i < parameters.Length; i++)
			{
				DataFactory.InitializeDataParameter(database, ref parameters[i]);
			}

			for (int i = 0; i < mainTable.TableFields.Length; i++)
			{
				parameters[i].ParameterName = mainTable.TableFields[i].fieldName;
				parameters[i].Value = mainTable.TableFields[i].fieldValue;
				parameters[i].DbType = mainTable.TableFields[i].fieldType;
			}
			return parameters;
		}

		/// <summary>
		///     Converts the fields to IDataParameter
		/// </summary>
		/// <param name="database">Database server type</param>
		/// <param name="fields">Fields to be converted</param>
		/// <returns>Array of IDataParameter</returns>
		public IDataParameter[] ConvertToDataParameter(DatabaseServer database, params DatabaseField[] fields)
		{
			if (fields == null || fields.Length == 0)
			{
				throw new ArgumentException("Invalid database fields");
			}

			IDataParameter[] parameters = new IDataParameter[fields.Length];

			for (int i = 0; i < fields.Length; i++)
			{
				DataFactory.InitializeDataParameter(database, ref parameters[i]);
			}

			for (int i = 0; i < fields.Length; i++)
			{
				parameters[i].ParameterName = DataFactory.GetParameterChar(database) + fields[i].fieldName;

				parameters[i].SourceColumn = fields[i].fieldName;

				if (fields[i].fieldValue == null)
				{
					parameters[i].Value = DBNull.Value;
				}
				else
				{
					parameters[i].Value = fields[i].fieldValue;
				}

				parameters[i].DbType = fields[i].fieldType;
			}

			return parameters;
		}

		/// <summary>
		///     Converts the fields to IDataParameter. The name of the parameter will also include
		///     the name of the table.
		/// </summary>
		/// <param name="database">Database server type</param>
		/// <param name="tableName">Name of the database table</param>
		/// <param name="field">Field to be converted</param>
		/// <returns>The IDataParameter</returns>
		public IDataParameter ConvertToDataParameter(DatabaseServer database, string tableName, DatabaseField field)
		{
			IDataParameter parameter = null;
			DataFactory.InitializeDataParameter(database, ref parameter);

			SqlGenerator generator = new SqlGenerator();

			parameter.ParameterName = DataFactory.GetParameterChar(database).ToString() + generator.GetTableName(tableName) + field.fieldName;
			parameter.SourceColumn = field.fieldName;

			if (field.fieldValue == null)
			{
				parameter.Value = DBNull.Value;
			}
			else
			{
				parameter.Value = field.fieldValue;
			}

			parameter.DbType = field.fieldType;

			return parameter;
		}

		/// <summary>
		///     Converts the specified DataField into a IDataParameter. This ensures that the
		///     name of the parameter is unique by comparing with a list of specified used names
		/// </summary>
		/// <param name="database">Database server</param>
		/// <param name="tableName">Name of the table to which the field belongs</param>
		/// <param name="field">DatabaseField which will be converted</param>
		/// <param name="listUsedParameterNames">List with used names of a parameter. When the name of the parameter is given it is checked against the values in this list. If the name if found then it is changed to be unique</param>
		/// <returns>The IDataParameter</returns>
		public IDataParameter ConvertToDataParameter(DatabaseServer database, string tableName, DatabaseField field, ref List<string> listUsedParameterNames)
		{
			IDataParameter parameter = null;

			SqlGenerator generator = new SqlGenerator();

			DataFactory.InitializeDataParameter(database, ref parameter);

			string parameterName = DataFactory.GetParameterChar(database) + generator.GetTableName(tableName) + field.fieldName;

			while (listUsedParameterNames.Contains(parameterName))
			{
				parameterName = parameterName + PARAMETER_NAME_ENDING;
			}

			parameter.ParameterName = parameterName;

			//add it to the external list
			listUsedParameterNames.Add(parameterName);

			if (field.fieldValue == null)
			{
				parameter.Value = DBNull.Value;
			}
			else
			{
				parameter.Value = field.fieldValue;
			}

			parameter.DbType = field.fieldType;
			parameter.SourceColumn = field.fieldName;

			return parameter;
		}

		/// <summary>
		///     Converts the type to table metadata.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="t">The t.</param>
		/// <returns></returns>
		public TableMetadata ConvertTypeToTableMetadata<T>(T t)
		{
			TableMetadata metadata = new TableMetadata();

			PropertyInfo[] properties = t.GetType().GetProperties();

			List<DatabaseField> list = new List<DatabaseField>();

			foreach (PropertyInfo info in properties)
			{
				if (info.Name == "TableFields" || info.Name == "TableName")
				{
					continue;
				}

				try
				{
					DatabaseField field = new DatabaseField();

					field.fieldName = info.Name;
					field.fieldType = GetMappedType(info.PropertyType);
					field.fieldValue = info.GetValue(t, null);

					list.Add(field);
				}
				catch
				{
					continue;
				}
			}

			metadata.TableFields = list.ToArray();

			return metadata;
		}

		/// <summary>
		///     Converts data from a DataTable to TableMetadata array. The scheme of the DataTable
		///     must be the same with that of the TableMetadata.
		/// </summary>
		/// <param name="table">DataTable which contains the data </param>
		/// <param name="metadata">Type of TableMetadata</param>
		/// <returns>TableMetadata array which holds the converted items</returns>
		public Array ConvertToTableMatadata(DataTable table, TableMetadata metadata)
		{
			Array arr = Array.CreateInstance(metadata.GetType(), table.Rows.Count);

			for (int i = 0; i < table.Rows.Count; i++)
			{
				TableMetadata meta = (TableMetadata) Activator.CreateInstance(metadata.GetType());

				for (int j = 0; j < table.Columns.Count; j++)
				{
					meta.SetFieldValue(table.Columns[j].ColumnName, table.Rows[i][j]);
				}

				arr.SetValue(meta, i);
			}

			return arr;
		}

		/// <summary>
		///     Converts data to TableMetadata
		/// </summary>
		/// <param name="rowIndex">Index of the row</param>
		/// <param name="table">DataTable</param>
		/// <param name="mainTable">TableMetadata on which we map the contents of the specified DataTable</param>
		public void ConvertToTableMetadata(int rowIndex, DataTable table, TableMetadata mainTable)
		{
			if (rowIndex < 0 || rowIndex >= table.Rows.Count)
			{
				throw new ArgumentException("Invalid row index");
			}

			for (int i = 0; i < mainTable.TableFields.Length; i++)
			{
				mainTable.SetFieldValue(mainTable.TableFields[i].fieldName, GetValue(mainTable.TableFields[i].fieldName, table, rowIndex));
			}
		}

		/// <summary>
		///     Return the value form the specified index in a DataTable.
		/// </summary>
		/// <param name="fieldName">Name of the field</param>
		/// <param name="table">Data source</param>
		/// <param name="rowIndex">Index of the row</param>
		/// <returns>The value</returns>
		internal object GetValue(string fieldName, DataTable table, int rowIndex)
		{
			return table.Rows[rowIndex][fieldName];
		}

		/// <summary>
		///     Converts to data table.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="t">The t.</param>
		/// <returns></returns>
		public DataTable ConvertTypeToDataTable<T>(T[] t, params string[] objectProperties)
		{
			if (t.Length == 0)
			{
				return new DataTable();
			}

			DataTable table = new DataTable();

			PropertyInfo[] properties = t[0].GetType().GetProperties();

			foreach (PropertyInfo info in properties)
			{
				if (objectProperties.Length > 0)
				{
					if (Array.IndexOf(objectProperties, info.Name) == -1)
					{
						continue;
					}
				}

				try
				{
					if (info.PropertyType.GetGenericTypeDefinition() == typeof (Nullable<>))
					{
						//check the underlying type for nullable
						Type[] tps = info.PropertyType.GetGenericArguments();

						table.Columns.Add(info.Name, Type.GetType(tps[0].FullName));
					}
				}
				catch
				{
					table.Columns.Add(info.Name, info.PropertyType);
				}
			}

			foreach (T current in t)
			{
				DataRow row = table.NewRow();

				foreach (DataColumn col in table.Columns)
				{
					try
					{
						row[col.ColumnName] = current.GetType().GetProperty(col.ColumnName).GetValue(current, null); // .GetRawConstantValue();
					}
					catch
					{
						if (current.GetType().GetGenericTypeDefinition() == typeof (Nullable<>))
						{
							Type[] tps = current.GetType().GetGenericArguments();

							row[col.ColumnName] = tps[0].GetProperty(col.ColumnName).GetRawConstantValue();
						}
					}
				}

				table.Rows.Add(row);
			}

			return table;
		}

		/// <summary>
		///     Converts the specified TableMetadata array into a DataTable
		/// </summary>
		/// <param name="data">TableMetadata array from which the conversion is made</param>
		/// <returns>Resulting DataTable</returns>
		public DataTable ConvertToDataTable(TableMetadata[] data)
		{
			DataTable table = new DataTable();

			TableMetadata tm = data[0];

			for (int i = 0; i < tm.TableFields.Length; i++)
			{
				DataColumn column = new DataColumn(tm.TableFields[i].fieldName, DataFactory.InitializeDataType(tm.TableFields[i].fieldType));

				column.AutoIncrement = tm.TableFields[i].isValueAutogenerated;
				column.AllowDBNull = true;
				table.Columns.Add(column);
			}

			for (int i = 0; i < data.Length; i++)
			{
				DataRow drow = table.NewRow();

				for (int j = 0; j < data[i].TableFields.Length; j++)
				{
					drow[j] = data[i].TableFields[j].fieldValue;
				}

				table.Rows.Add(drow);
			}

			return table;
		}

		/// <summary>
		///     Converts the specified TableMetadata's fields into a DataTable.
		/// </summary>
		/// <param name="data">TableMetadata array from which the conversion is made</param>
		/// <param name="fields">List of fields to be included</param>
		/// <returns>Resulting DataTable</returns>
		public DataTable ConvertToDataTable(TableMetadata[] data, params DatabaseField[] fields)
		{
			DataTable table = new DataTable();

			for (int i = 0; i < fields.Length; i++)
			{
				DataColumn column = new DataColumn(fields[i].fieldName, DataFactory.InitializeDataType(fields[i].fieldType));
				column.AutoIncrement = fields[i].isValueAutogenerated;
				column.AllowDBNull = true;
				table.Columns.Add(column);
			}

			for (int i = 0; i < data.Length; i++)
			{
				DataRow drow = table.NewRow();

				for (int j = 0; j < fields.Length; j++)
				{
					drow[j] = data[i].TableFields[j].fieldValue;
				}

				table.Rows.Add(drow);
			}

			return table;
		}

		/// <summary>
		///     Converts the specified 2 DatabaseFields to a hashtable. The first field cannot
		///     have duplicates.
		/// </summary>
		/// <param name="data">TableMetdata array from which we make the conversion</param>
		/// <param name="keyField">First field. This will be the key in the hashtable</param>
		/// <param name="valueField">Second field. This will be the value in the hashtable</param>
		/// <returns>Resulting hashtable</returns>
		public Hashtable ConvertToHashtable(TableMetadata[] data, DatabaseField keyField, DatabaseField valueField)
		{
			Hashtable htData = new Hashtable();

			for (int i = 0; i < data.Length; i++)
			{
				object key = data[i].GetField(keyField.fieldName).fieldValue;
				object value = data[i].GetField(valueField.fieldName).fieldValue;

				if (key != null && value != null)
				{
					htData.Add(key, value);
				}
				else
				{
					continue;
				}
			}

			return htData;
		}

		/// <summary>
		///     Converts the data from a DataTable into a Hashtable.
		/// </summary>
		/// <param name="table">DataTable from which we convert the data</param>
		/// <param name="keyColumnIndex">Index of the key column</param>
		/// <param name="valueColumnIndex">Index of the value column</param>
		/// <returns>Resulting hashtable</returns>
		public Hashtable ConvertToHashtable(DataTable table, int keyColumnIndex, int valueColumnIndex)
		{
			Hashtable htData = null;

			if (keyColumnIndex < 0 || keyColumnIndex > table.Columns.Count)
			{
				throw new ArgumentException("Invalid key column index");
			}

			if (valueColumnIndex < 0 || valueColumnIndex > table.Columns.Count)
			{
				throw new ArgumentException("Invalid value column index");
			}

			htData = new Hashtable();

			for (int i = 0; i < table.Rows.Count; i++)
			{
				htData.Add(table.Rows[i][keyColumnIndex], table.Rows[i][valueColumnIndex]);
			}

			return htData;
		}

		/// <summary>
		///     Converts the specified field from a TableMetadata array into a string collection.
		/// </summary>
		/// <param name="data">TableMetdata array from which we make the conversion</param>
		/// <param name="field">The database field which is added to the string collection</param>
		/// <returns>String Collection</returns>
		public StringCollection ConvertToStringCollection(TableMetadata[] data, DatabaseField field)
		{
			StringCollection scData = new StringCollection();

			for (int i = 0; i < data.Length; i++)
			{
				scData.Add(data[i].GetField(field.fieldName).fieldValue.ToString());
			}
			return scData;
		}

		/// <summary>
		///     Converts the specified data from DataTable to StringCollection
		/// </summary>
		/// <param name="table">DataTable from which we read data</param>
		/// <param name="columnIndex">DataTable column index</param>
		/// <returns>String Collection</returns>
		public StringCollection ConvertToStringCollection(DataTable table, int columnIndex)
		{
			StringCollection scData = null;

			if (columnIndex < 0 || columnIndex > table.Columns.Count)
			{
				throw new ArgumentException("invalid index");
			}

			scData = new StringCollection();

			for (int i = 0; i < table.Rows.Count; i++)
			{
				scData.Add(table.Rows[i][columnIndex].ToString());
			}

			return scData;
		}

		/// <summary>
		///     Converts the contents of a DataColumn to a ArrayList
		/// </summary>
		/// <param name="table">DataTable from which we read the values</param>
		/// <param name="columnIndex">Index of the DataColumn</param>
		/// <returns>ArrayList which contains the items read from data table</returns>
		public ArrayList ConvertToArrayList(DataTable table, int columnIndex)
		{
			ArrayList scData = null;

			if (columnIndex < 0 || columnIndex > table.Columns.Count)
			{
				throw new ArgumentException("invalid index");
			}

			scData = new ArrayList();

			for (int i = 0; i < table.Rows.Count; i++)
			{
				scData.Add(table.Rows[i][columnIndex]);
			}

			return scData;
		}

		/// <summary>
		///     Toes the list.
		/// </summary>
		/// <param name="table">The table.</param>
		/// <param name="columnIndex">Index of the column.</param>
		/// <returns></returns>
		public List<string> ConvertToList(DataTable table, int columnIndex)
		{
			if (columnIndex < 0 || columnIndex > table.Rows.Count)
			{
				throw new ArgumentException("Invalid column index");
			}

			List<string> list = new List<string>(table.Rows.Count);

			foreach (DataRow dataRow in table.Rows)
			{
				list.Add(dataRow[columnIndex].ToString());
			}

			return list;
		}

		private DbType GetMappedType(Type tp)
		{
			if (tp == typeof (Boolean))
			{
				return DbType.Boolean;
			}
			else if (tp == typeof (Int32))
			{
				return DbType.Int32;
			}
			else if (tp == typeof (Int16))
			{
				return DbType.Int16;
			}
			else if (tp == typeof (Int64))
			{
				return DbType.Int64;
			}
			else if (tp == typeof (Single))
			{
				return DbType.Single;
			}
			else if (tp == typeof (Double))
			{
				return DbType.Double;
			}
			else if (tp == typeof (DateTime))
			{
				return DbType.Date;
			}
			else if (tp == typeof (string))
			{
				return DbType.String;
			}

			return DbType.AnsiString;
		}
	}
}