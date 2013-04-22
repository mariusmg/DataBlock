/*

        file : ChildTableRelation.cs 
 description : Describes a Child - Parent relationship
    
 (c) 2004 - 2006 Marius Gheorghe. All rights reserved.
  
*/

using System;

namespace voidsoft.DataBlock
{
	/// <summary>
	///     Represents a Child - Parent relation between two entities
	/// </summary>
	[Serializable]
	public class ChildTableRelation : TableRelation
	{
		private string foreignKeyName;
		private string relatedTableKeyName;
		private string relatedTableName;
		private TableRelationCardinality tableCardinality;

		/// <summary>
		///     Constructor.Creates a new instance of ChildTableRelation.
		/// </summary>
		/// <param name="relatedTableName"></param>
		/// <param name="tableCardinality"></param>
		/// <param name="relatedTableKeyName"></param>
		/// <param name="foreignKeyName"></param>
		public ChildTableRelation(string relatedTableName, TableRelationCardinality tableCardinality, string relatedTableKeyName, string foreignKeyName)
		{
			this.relatedTableName = relatedTableName;
			this.tableCardinality = tableCardinality;
			this.relatedTableKeyName = relatedTableKeyName;
			this.foreignKeyName = foreignKeyName;
		}

		/// <summary>
		///     Get the name of the related table
		/// </summary>
		public override string RelatedTableName
		{
			get
			{
				return relatedTableName;
			}
		}

		/// <summary>
		///     Gets the relation cardinality
		/// </summary>
		public override TableRelationCardinality RelationCardinality
		{
			get
			{
				return tableCardinality;
			}
		}

		//this is the primary key from the main table.

		/// <summary>
		///     Gets the name of the primary key from the parent table
		/// </summary>
		public string RelatedTableKeyName
		{
			get
			{
				return relatedTableKeyName;
			}
		}

		/// <summary>
		///     Gets the name of the foreign key from the child table
		/// </summary>
		public string ForeignKeyName
		{
			get
			{
				return foreignKeyName;
			}
		}
	}
}