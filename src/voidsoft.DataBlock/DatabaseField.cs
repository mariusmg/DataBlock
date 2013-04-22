/*
	   name: DatabaseField.cs
description: Structure which describes a database field.
    
  (c) 2004 - 2006 Marius Gheorghe. All rights reserved.
 
  
*/

using System;
using System.Data;

namespace voidsoft.DataBlock
{
	/// <summary>
	///     This struct represents a database field with associated name, data type and value.
	/// </summary>
	[Serializable]
	public struct DatabaseField
	{
		/// <summary>
		///     The field's  name
		/// </summary>
		public string fieldName;

		/// <summary>
		///     The field's  data type
		/// </summary>
		public DbType fieldType;

		/// <summary>
		///     The field's  value
		/// </summary>
		public object fieldValue;

		/// <summary>
		///     Flag to know if this is the table's primary key.
		/// </summary>
		public bool isPrimaryKey;

		/// <summary>
		///     Flag to know if the value is autogenerated/autoincremented for this field.
		/// </summary>
		public bool isValueAutogenerated;

		/// <summary>
		///     DatabaseField Constructor
		/// </summary>
		/// <param name="fieldType">The database field dataType.</param>
		/// <param name="fieldName">The field's name.</param>
		/// <param name="isPrimaryKey">Boolean value if this field is a PK</param>
		/// <param name="isValueAutogenerated">Boolean value if this field's value is autogenerated by the RDBMS. Is used only if the field is PK</param>
		/// <param name="fieldValue">The field's current value.</param>
		public DatabaseField(DbType fieldType, string fieldName, bool isPrimaryKey, bool isValueAutogenerated, object fieldValue)
		{
			this.fieldType = fieldType;
			this.fieldName = fieldName;
			this.fieldValue = fieldValue;
			this.isPrimaryKey = isPrimaryKey;
			this.isValueAutogenerated = isValueAutogenerated;
		}
	}
}