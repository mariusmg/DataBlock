/*

       file: RelationType.cs
description: Enum which describes a database table relation types.
  
   (c) 2004 - 2006 Marius Gheorghe. All rights reserved.  
  
 */

using System;

namespace voidsoft.DataBlock
{
	/// <summary>
	///     Enum which represents a database table relation type.
	/// </summary>
	[Serializable]
	public enum TableRelationCardinality
	{
		/// <summary>
		///     Represents a One to One relation
		/// </summary>
		OneToOne,

		/// <summary>
		///     Represents a One to Many table relation
		/// </summary>
		OneToMany,

		/// <summary>
		///     Many to Many relation. This can be used ONLY in ManyToManyTableRelations
		/// </summary>
		ManyToMany
	}
}