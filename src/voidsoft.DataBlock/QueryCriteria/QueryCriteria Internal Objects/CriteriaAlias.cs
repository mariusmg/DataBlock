/*
 
       file : CriteriaAlias.cs
description : Allows the definition of column alias for the specified column name. 
 
  (c) 2004 - 2006 Marius Gheorghe. All rights reserved.

*/

namespace voidsoft.DataBlock
{
	/// <summary>
	///     Structure used to hold field aliases.
	/// </summary>
	internal struct CriteriaAlias
	{
		/// <summary>
		///     The field alias
		/// </summary>
		public string aliasName;

		/// <summary>
		///     Name of the field
		/// </summary>
		public string fieldName;

		/// <summary>
		///     Name of the table to which the field belongs
		/// </summary>
		public string tableName;

		/// <summary>
		///     Constructor
		/// </summary>
		/// <param name="fieldName">Name of the field</param>
		/// <param name="aliasName">The alias</param>
		/// <param name="tableName">Name of the table to which the field belongs</param>
		public CriteriaAlias(string fieldName, string aliasName, string tableName)
		{
			this.fieldName = fieldName;
			this.aliasName = aliasName;
			this.tableName = tableName;
		}
	}
}