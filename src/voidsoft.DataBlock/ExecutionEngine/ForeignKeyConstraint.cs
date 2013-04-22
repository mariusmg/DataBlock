/*

       file : ForeignKeyConstraint.cs
description : represents a FK constraint
   
   (c) 2004 - 2008 Marius Gheorghe. All rights reserved.
  
*/

namespace voidsoft.DataBlock
{
	/// <summary>
	///     Represents a foreign key constraint with the associated data.
	/// </summary>
	internal struct ForeignKeyConstraint
	{
		/// <summary>
		///     PK field name
		/// </summary>
		public string NameOfPrimaryKeyField;

		/// <summary>
		///     Holds the numebr of queries to be executed
		/// </summary>
		public int NumerOfRunningQueries;

		/// <summary>
		///     Table name
		/// </summary>
		public string TableName;
	}
}