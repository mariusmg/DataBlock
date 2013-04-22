/*

      file : TableRelationMode.cs
description: Relation mode. Sets the relation mode of our table.
   
    (c) 2004 - 2006 Marius Gheorghe. All rights reserved. 
*/

using System;

namespace voidsoft.DataBlock
{
	/// <summary>
	///     Enum which describes the relation mode of a table
	/// </summary>
	[Serializable]
	public enum TableRelationMode
	{
		/// <summary>
		///     Parent entity
		/// </summary>
		Parent,

		/// <summary>
		///     Child entity
		/// </summary>
		Child
	}
}