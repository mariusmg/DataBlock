/*
  
	   file: TableRelation.cs
description: Represents a data relation between two tables.
  
     notes:  - if the relation mode is Parent then RelatedTableKey name is the
              Foreign Key.
             - if the relation mode is Child then the RelatedTableKey name is the
              Primary Key of the related Table.  

 (c) 2004 - 2006 Marius Gheorghe. All rights reserved.    
 
*/

using System;

namespace voidsoft.DataBlock
{
	/// <summary>
	///     Base class which represents a table relation
	/// </summary>
	[Serializable]
	public abstract class TableRelation
	{
		/// <summary>
		///     The cardinality with the related tabel
		/// </summary>
		public virtual TableRelationCardinality RelationCardinality
		{
			get
			{
				return TableRelationCardinality.OneToOne;
			}
		}

		/// <summary>
		///     The name of the related table
		/// </summary>
		public virtual string RelatedTableName
		{
			get
			{
				return string.Empty;
			}
		}
	}
}