/*

       file : PrimaryKeyConstraint.cs
description : represents a PK constraint.
   
   (c) 2004 - 2008 Marius Gheorghe. All rights reserved.
  
*/

namespace voidsoft.DataBlock
{
	internal struct PrimaryKeyConstraint
	{
		/// <summary>
		///     Gets or sets the name of the table.
		/// </summary>
		/// <value>The name of the table.</value>
		public string TableName
		{
			get;
			set;
		}

		/// <summary>
		///     Gets or sets the name of the primary key field.
		/// </summary>
		/// <value>The name of the primary key field.</value>
		public string PrimaryKeyFieldName
		{
			get;
			set;
		}
	}
}